using MimeKit;
using MailKit.Net.Smtp;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace ChessServer
{
    public class TCPServer
    {
        private TcpListener tcpListener;
        private UserRepo userRepo;
        private static Dictionary<string, (string Otp, DateTime Expiry)> otpStore = new Dictionary<string, (string, DateTime)>();
        private static int clientCount = 0;
        public TCPServer()
        {
            userRepo = new UserRepo();
        }
        public void Start(int port)
        {
            Database.Initialize();
            Console.WriteLine("Server khởi động trên port " + port + "\n");
            tcpListener = new TcpListener(IPAddress.Any, port);
            Thread listenThread = new Thread(ListenForClients);
            listenThread.IsBackground = true;
            listenThread.Start();
            Thread.Sleep(Timeout.Infinite);
        }
        private void ListenForClients()
        {
            tcpListener.Start();
            while (true)
            {
                TcpClient client = tcpListener.AcceptTcpClient();
                ClientHandler handler = new ClientHandler(client, this);
                Thread t = new Thread(handler.HandleClient);
                t.IsBackground = true;
                t.Start();
            }
        }
        public UserRepo GetUserRepo()
        {
            return userRepo;
        }
        public class ClientHandler
        {
            private TcpClient tcpClient;
            private NetworkStream stream;
            private TCPServer server;
            private ClassUser loggedInUser;
            private string clientIP;
            public ClientHandler(TcpClient client, TCPServer server)
            {
                tcpClient = client;
                this.server = server;
                stream = tcpClient.GetStream();
                clientIP = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
            }
            public void HandleClient()
            {
                byte[] buffer = new byte[4096];
                int bytesRead;
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    string request = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    string response = ProcessRequest(request);
                    SendMessage(response);
                }
            }
            private string ProcessRequest(string requestData)
            {
                using (JsonDocument doc = JsonDocument.Parse(requestData))
                {
                    JsonElement root = doc.RootElement;
                    string action = root.GetProperty("action").GetString();
                    if (action == "REGISTER") return HandleRegister(root);
                    else if (action == "LOGIN") return HandleLogin(root);
                    else if (action == "GET_USER_INFO") return HandleGetUserInfo();
                    else if (action == "UPDATE_ACCOUNT") return HandleUpdateAccount(root);
                    else if (action == "REQUEST_OTP") return HandleRequestOtp(root);
                    else if (action == "RESET_PASSWORD") return HandleResetPassword(root);
                    else if (action == "VERIFY_OTP") return HandleVerifyOtp(root);
                    else if (action == "LOGOUT") return HandleLogout(root);
                    else return "";
                }
            }
            private string HandleRegister(JsonElement req)
            {
                string email = req.GetProperty("email").GetString();
                string displayName = req.GetProperty("displayName").GetString();
                string username = req.GetProperty("username").GetString();
                string password = req.GetProperty("password").GetString();
                UserRepo repo = server.GetUserRepo();
                if (repo.IsUsernameExists(username))
                {
                    return CreateResponse(false, "Username đã tồn tại");
                }
                if (repo.IsEmailExists(email))
                {
                    return CreateResponse(false, "Email đã được sử dụng");
                }
                repo.RegisterUser(email, displayName, username, password);
                Console.WriteLine("[" + clientIP + "] Register: " + username);
                return CreateResponse(true, "Đăng ký thành công");
            }
            private string HandleLogin(JsonElement req)
            {
                string username = req.GetProperty("username").GetString();
                string password = req.GetProperty("password").GetString();
                ClassUser user = server.GetUserRepo().LoginUser(username, password);
                if (user != null)
                {
                    loggedInUser = user;
                    Console.WriteLine($"[{clientIP}] Login: {username}. Tổng số client hiện tại: {Interlocked.Increment(ref TCPServer.clientCount)}");
                    return JsonSerializer.Serialize(new
                    {
                        success = true,
                        message = "Đăng nhập thành công",
                        user = new
                        {
                            userId = user.UserID,
                            email = user.Email,
                            displayName = user.DisplayName,
                            username = user.Username,
                            elo = user.Elo
                        }
                    });
                }
                return CreateResponse(false, "Sai tài khoản hoặc mật khẩu");
            }
            private string HandleRequestOtp(JsonElement req)
            {
                string email = req.GetProperty("email").GetString();
                UserRepo repo = server.GetUserRepo();
                if (repo.IsEmailExists(email) == false)
                {
                    return CreateResponse(false, "Email không tồn tại!");
                }
                Random random = new Random();
                string otp = random.Next(100000, 999999).ToString();
                DateTime expiry = DateTime.Now.AddMinutes(10);
                otpStore[email] = (otp, expiry);
                MimeMessage message = new MimeMessage();
                message.From.Add(new MailboxAddress("Nhóm 12 Lập trình mạng căn bản (NT106.Q14)", "group12.nt106.q14@gmail.com"));
                message.To.Add(new MailboxAddress("", email));
                message.Subject = "Mã xác nhận đổi mật khẩu đồ án Trò chơi Cờ vua chơi qua mạng";
                string body = "Xin chào,\n\nMÃ XÁC NHẬN (OTP): " + otp + "\n\nMã này có hiệu lực trong 10 phút.\n\nNhóm 12 - Đồ án Lập trình mạng căn bản (NT106.Q14)\nTrò chơi Cờ vua chơi qua mạng\n";
                BodyBuilder builder = new BodyBuilder();
                builder.TextBody = body;
                message.Body = builder.ToMessageBody();
                SmtpClient client = new SmtpClient();
                client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                client.Authenticate("group12.nt106.q14@gmail.com", "tmjx bacw rvsg dybr");
                client.Send(message);
                client.Disconnect(true);
                Console.WriteLine("[" + clientIP + "] Gửi OTP đến " + email);
                return CreateResponse(true, "Đã gửi mã xác nhận qua email!");
            }
            private string HandleVerifyOtp(JsonElement req)
            {
                string email = req.GetProperty("email").GetString();
                string otp = req.GetProperty("otp").GetString();
                if (otpStore.ContainsKey(email) == false)
                {
                    return CreateResponse(false, "Vui lòng yêu cầu mã mới!");
                }
                var otpData = otpStore[email];
                if (DateTime.Now > otpData.Expiry)
                {
                    otpStore.Remove(email);
                    return CreateResponse(false, "Mã đã hết hạn, vui lòng yêu cầu mã mới!");
                }
                if (otpData.Otp != otp)
                {
                    return CreateResponse(false, "Mã xác nhận không đúng!");
                }
                otpStore.Remove(email);
                Console.WriteLine("[" + clientIP + "] Email " + email + " đã xác nhận otp thành công!");
                return CreateResponse(true, "Bạn đã xác nhận thành công. Bây giờ bạn có thể đổi mật khẩu!");
            }
            private string HandleResetPassword(JsonElement req)
            {
                string email = req.GetProperty("email").GetString();
                string newPassword = req.GetProperty("newPassword").GetString();
                UserRepo repo = server.GetUserRepo();
                // tìm user từ email
                List<ClassUser> all = repo.GetAllUsers();
                ClassUser found = null;
                foreach (ClassUser u in all)
                {
                    if (u.Email == email) found = u;
                }
                string username = found.Username;
                repo.ResetPassword(email, newPassword);
                Console.WriteLine("[" + clientIP + "] Reset Password: " + username);
                return CreateResponse(true, "Đổi mật khẩu thành công!");
            }
            private string HandleUpdateAccount(JsonElement req)
            {
                int userId = req.GetProperty("userId").GetInt32();
                string newDisplayName = req.GetProperty("displayName").GetString();
                string newEmail = req.GetProperty("email").GetString();
                string newPassword = null;
                if (req.TryGetProperty("password", out JsonElement pwd))
                {
                    newPassword = pwd.GetString();
                }
                UserRepo repo = server.GetUserRepo();
                ClassUser currentUser = repo.GetUserById(userId);
                string username = currentUser.Username;
                repo.UpdateUserAccount(userId, newDisplayName, newEmail, newPassword);
                Console.WriteLine("[" + clientIP + "] Account Setting: " + username);
                return CreateResponse(true, "Cập nhật thành công");
            }
            private string HandleGetUserInfo()
            {
                if (loggedInUser == null)
                {
                    return JsonSerializer.Serialize(new { success = true, message = "OK", user = (object)null });
                }
                return JsonSerializer.Serialize(new
                {
                    success = true,
                    message = "OK",
                    user = new
                    {
                        userId = loggedInUser.UserID,
                        email = loggedInUser.Email,
                        displayName = loggedInUser.DisplayName,
                        username = loggedInUser.Username,
                        elo = loggedInUser.Elo
                    }
                });
            }
            private string HandleLogout(JsonElement req)
            {
                string username = req.GetProperty("username").GetString();
                Console.WriteLine($"[{clientIP}] Logout: {username}. Tổng số client hiện tại: {Interlocked.Decrement(ref TCPServer.clientCount)}");
                return CreateResponse(true, "Đăng xuất thành công!");
            }
            private string CreateResponse(bool success, string message)
            {
                return JsonSerializer.Serialize(new { success = success, message = message });
            }
            public void SendMessage(string message)
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                stream.Write(data, 0, data.Length);
                stream.Flush();
            }
        }
    }
}
