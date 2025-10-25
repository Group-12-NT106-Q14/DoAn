using MimeKit;
using MailKit.Net.Smtp;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace ChessServer
{
    public class TCPServer
    {
        private TcpListener tcpListener;
        private UserRepo userRepo;
        private static Dictionary<string, (string Otp, DateTime Expiry)> otpStore = new();
        public TCPServer()
        {
            userRepo = new UserRepo();
        }
        public void Start(int port)
        {
            Database.Initialize();
            Console.WriteLine($"Server khởi động trên port {port}\n");
            tcpListener = new TcpListener(IPAddress.Any, port);
            new Thread(ListenForClients) { IsBackground = true }.Start();
            Thread.Sleep(Timeout.Infinite);
        }
        private void ListenForClients()
        {
            tcpListener.Start();
            while (true)
            {
                TcpClient client = tcpListener.AcceptTcpClient();
                ClientHandler handler = new ClientHandler(client, this);
                new Thread(handler.HandleClient) { IsBackground = true }.Start();
            }
        }
        public UserRepo GetUserRepo() => userRepo;
        public class ClientHandler
        {
            private TcpClient tcpClient;
            private NetworkStream stream;
            private TCPServer server;
            private ClassUser loggedInUser;
            private string clientIP;
            public ClientHandler(TcpClient client, TCPServer server)
            {
                this.tcpClient = client;
                this.server = server;
                this.stream = client.GetStream();
                this.clientIP = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
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

                    return action switch
                    {
                        "REGISTER" => HandleRegister(root),
                        "LOGIN" => HandleLogin(root),
                        "GET_USER_INFO" => HandleGetUserInfo(),
                        "UPDATE_ACCOUNT" => HandleUpdateAccount(root),
                        "REQUEST_OTP" => HandleRequestOtp(root),
                        "RESET_PASSWORD" => HandleResetPassword(root),
                        "VERIFY_OTP" => HandleVerifyOtp(root),
                        "LOGOUT" => HandleLogout(root),
                        _ => ""
                    };
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
                    return CreateResponse(false, "Username đã tồn tại");
                if (repo.IsEmailExists(email))
                    return CreateResponse(false, "Email đã được sử dụng");
                repo.RegisterUser(email, displayName, username, password);
                Console.WriteLine($"[{clientIP}] Register: {username}");
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
                    Console.WriteLine($"[{clientIP}] Login: {username}");
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
                if (!repo.IsEmailExists(email)) return CreateResponse(false, "Email không tồn tại!");
                string otp = new Random().Next(100000, 999999).ToString();
                DateTime expiry = DateTime.Now.AddMinutes(10); 
                otpStore[email] = (otp, expiry);
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Nhóm 12 Lập trình mạng căn bản (NT106.Q14)", "group12.nt106.q14@gmail.com"));
                message.To.Add(new MailboxAddress("", email));
                message.Subject = "Mã xác nhận đổi mật khẩu đồ án Trò chơi Cờ vua chơi qua mạng";
                var builder = new BodyBuilder
                {
                    TextBody = $@"Xin chào,
Chúng tôi nhận được yêu cầu đặt lại mật khẩu cho tài khoản của bạn tại hệ thống Trò chơi Cờ vua chơi qua mạng.

MÃ XÁC NHẬN (OTP): {otp}

Mã này có hiệu lực trong vòng 10 phút kể từ thời điểm gửi. Vui lòng sử dụng mã trên để xác minh và đặt lại mật khẩu.
LƯU Ý:
• Tuyệt đối không chia sẻ mã xác nhận này với bất kỳ ai, kể cả người tự xưng là nhân viên hỗ trợ.
• Mỗi mã OTP chỉ được sử dụng một lần duy nhất.
• Nếu bạn không thực hiện yêu cầu này, vui lòng bỏ qua email này.

Trân trọng,
Nhóm 12 - Đồ án Lập trình mạng căn bản (NT106.Q14)
Trò chơi Cờ vua chơi qua mạng
---
Email này được gửi tự động, vui lòng không trả lời trực tiếp."
                };
                message.Body = builder.ToMessageBody();
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                    client.Authenticate("group12.nt106.q14@gmail.com", "tmjx bacw rvsg dybr");
                    client.Send(message);
                    client.Disconnect(true);
                }
                Console.WriteLine($"[{clientIP}] Gửi OTP đến {email}");
                return CreateResponse(true, "Đã gửi mã xác nhận qua email!");
            }
            private string HandleVerifyOtp(JsonElement req)
            {
                string email = req.GetProperty("email").GetString();
                string otp = req.GetProperty("otp").GetString();
                if (!otpStore.ContainsKey(email))
                    return CreateResponse(false, "Vui lòng yêu cầu mã mới!");
                var otpData = otpStore[email];
                if (DateTime.Now > otpData.Expiry)
                {
                    otpStore.Remove(email);
                    return CreateResponse(false, "Mã đã hết hạn, vui lòng yêu cầu mã mới!");
                }
                if (otpData.Otp != otp) return CreateResponse(false, "Mã xác nhận không đúng!");
                otpStore.Remove(email);
                Console.WriteLine($"[{clientIP}] Email {email} đã xác nhận otp thành công!");
                return CreateResponse(true, "Bạn đã xác nhận thành công. Bây giờ bạn có thẻ đổi mật khẩu!");
            }
            private string HandleResetPassword(JsonElement req)
            {
                string email = req.GetProperty("email").GetString();
                string newPassword = req.GetProperty("newPassword").GetString();
                UserRepo repo = server.GetUserRepo();
                ClassUser user = repo.GetAllUsers().FirstOrDefault(u => u.Email == email);
                string username = user.Username;
                repo.ResetPassword(email, newPassword);
                Console.WriteLine($"[{clientIP}] Reset Password: {username}");
                return CreateResponse(true, "Đổi mật khẩu thành công!");
            }
            private string HandleUpdateAccount(JsonElement req)
            {
                int userId = req.GetProperty("userId").GetInt32();
                string newDisplayName = req.GetProperty("displayName").GetString();
                string newEmail = req.GetProperty("email").GetString();
                string newPassword = null;
                if (req.TryGetProperty("password", out JsonElement pwdElement))
                {
                    newPassword = pwdElement.GetString();
                }
                UserRepo repo = server.GetUserRepo();
                ClassUser currentUser = repo.GetUserById(userId);
                string username = currentUser.Username;
                repo.UpdateUserAccount(userId, newDisplayName, newEmail, newPassword);
                Console.WriteLine($"[{clientIP}] Account Setting: {username}");
                return CreateResponse(true, "Cập nhật thành công");  
            }
            private string HandleGetUserInfo()
            {
                return JsonSerializer.Serialize(new
                {
                    success = true,
                    message = "OK",
                    user = loggedInUser != null ? new
                    {
                        userId = loggedInUser.UserID,
                        email = loggedInUser.Email,
                        displayName = loggedInUser.DisplayName,
                        username = loggedInUser.Username,
                        elo = loggedInUser.Elo
                    } : null
                });
            }
            private string HandleLogout(JsonElement req)
            {
                string? username = null;
                if (req.TryGetProperty("username", out JsonElement userProp)) username = userProp.GetString();
                Console.WriteLine($"[{clientIP}] Logout: {username}");
                return CreateResponse(true, "Đăng xuất thành công!");
            }
            private string CreateResponse(bool success, string message)
            {
                return JsonSerializer.Serialize(new { success, message });
            }
            public void SendMessage(string message)
            {
                byte[] buffer = Encoding.UTF8.GetBytes(message);
                stream.Write(buffer, 0, buffer.Length);
                stream.Flush();
            }
        }
    }
}
