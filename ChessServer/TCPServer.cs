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
using System.IO;

namespace ChessServer
{
    public class TCPServer
    {
        private TcpListener tcpListener;
        private UserRepo userRepo;
        private static Dictionary<string, (string Otp, DateTime Expiry)> otpStore = new Dictionary<string, (string Otp, DateTime Expiry)>();
        private static Dictionary<string, SessionInfo> activeSessions = new Dictionary<string, SessionInfo>();
        private static int clientCount = 0;
        private static object sessionLock = new object();
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
            listenThread.Start();
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
        public class SessionInfo
        {
            public int UserId { get; set; }
            public string Username { get; set; }
            public string ClientIP { get; set; }
            public DateTime LoginTime { get; set; }
        }
        public class ClientHandler
        {
            private TcpClient tcpClient;
            private NetworkStream stream;
            private TCPServer server;
            private ClassUser loggedInUser;
            private string clientIP;
            private bool isLoggedIn = false;
            private static List<ClientHandler> connectedClients = new List<ClientHandler>();
            public ClientHandler(TcpClient client, TCPServer server)
            {
                tcpClient = client;
                this.server = server;
                stream = tcpClient.GetStream();
                clientIP = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
                lock (connectedClients)
                {
                    connectedClients.Add(this);
                }
            }
            public void HandleClient()
            {
                byte[] buffer = new byte[4096];
                int bytesRead;
                try
                {
                    while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        string request = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        string response = ProcessRequest(request);
                        if (!string.IsNullOrEmpty(response))
                            SendMessage(response);
                    }
                }
                catch (IOException ioEx)
                {
                    Console.WriteLine($"[{clientIP}] IOException: {ioEx.Message}");
                }
                catch (SocketException sockEx)
                {
                    Console.WriteLine($"[{clientIP}] SocketException: {sockEx.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[{clientIP}] Exception: {ex.Message}");
                }
                finally
                {
                    CleanupClient();
                }
            }
            private void CleanupClient()
            {
                try
                {
                    lock (connectedClients) { connectedClients.Remove(this); }
                    if (isLoggedIn && loggedInUser != null)
                    {
                        lock (sessionLock)
                        {
                            if (activeSessions.ContainsKey(loggedInUser.Username))
                                activeSessions.Remove(loggedInUser.Username);
                        }
                        int currentCount = Interlocked.Decrement(ref TCPServer.clientCount);
                        Console.WriteLine($"[{clientIP}] User {loggedInUser.Username} disconnected unexpectedly. Tổng số client: {currentCount}");
                    }
                    isLoggedIn = false;
                    stream?.Close();
                    tcpClient?.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[{clientIP}] Cleanup error: {ex.Message}");
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
                    else if (action == "CHAT") { HandleChat(root); return ""; }
                    else if (action == "UPDATE_ACCOUNT") return HandleUpdateAccount(root);
                    else if (action == "REQUEST_OTP") return HandleRequestOtp(root);
                    else if (action == "RESET_PASSWORD") return HandleResetPassword(root);
                    else if (action == "VERIFY_OTP") return HandleVerifyOtp(root);
                    else if (action == "GET_ONLINE_USERS") return HandleGetOnlineUsers();
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
                    return CreateResponse(false, "Username đã tồn tại");
                if (repo.IsEmailExists(email))
                    return CreateResponse(false, "Email đã được sử dụng");
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
                    lock (sessionLock)
                    {
                        if (!activeSessions.ContainsKey(username))
                        {
                            loggedInUser = user;
                            isLoggedIn = true;
                            activeSessions[username] = new SessionInfo
                            {
                                UserId = user.UserID, // Lưu UserId vào session
                                Username = username,
                                ClientIP = clientIP,
                                LoginTime = DateTime.Now
                            };
                            int currentCount = Interlocked.Increment(ref TCPServer.clientCount);
                            Console.WriteLine($"[{clientIP}] Login: {username}. Tổng số client hiện tại: {currentCount}");
                        }
                        else
                        {
                            Console.WriteLine($"[{clientIP}] User {username} already logged in");
                        }
                    }
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
            private string HandleGetOnlineUsers()
            {
                var result = new List<object>();
                lock (sessionLock)
                {
                    foreach (var session in activeSessions.Values)
                    {
                        ClassUser user = server.GetUserRepo().GetUserById(session.UserId);
                        if (user != null)
                        {
                            result.Add(new
                            {
                                displayName = user.DisplayName,
                                username = user.Username
                            });
                        }
                    }
                }
                return JsonSerializer.Serialize(new { success = true, users = result });
            }
            private void HandleChat(JsonElement req)
            {
                string sender = req.GetProperty("sender").GetString();
                string content = req.GetProperty("content").GetString();
                string msgJson = JsonSerializer.Serialize(new
                {
                    type = "CHAT",
                    sender = sender,
                    content = content,
                    timestamp = DateTime.Now.ToString("HH:mm:ss")
                });

                lock (connectedClients)
                {
                    foreach (var client in connectedClients)
                    {
                        try { client.SendMessage(msgJson); }
                        catch { }
                    }
                }
            }
            private string HandleRequestOtp(JsonElement req)
            {
                string email = req.GetProperty("email").GetString();
                UserRepo repo = server.GetUserRepo();
                if (repo.IsEmailExists(email) == false) return CreateResponse(false, "Email không tồn tại!");
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
                    return CreateResponse(false, "Vui lòng yêu cầu mã mới!");
                var otpData = otpStore[email];
                if (DateTime.Now > otpData.Expiry)
                {
                    otpStore.Remove(email);
                    return CreateResponse(false, "Mã đã hết hạn, vui lòng yêu cầu mã mới!");
                }
                if (otpData.Otp != otp)
                    return CreateResponse(false, "Mã xác nhận không đúng!");
                otpStore.Remove(email);
                Console.WriteLine("[" + clientIP + "] Email " + email + " đã xác nhận otp thành công!");
                return CreateResponse(true, "Bạn đã xác nhận thành công. Bây giờ bạn có thể đổi mật khẩu!");
            }
            private string HandleResetPassword(JsonElement req)
            {
                string email = req.GetProperty("email").GetString();
                string newPassword = req.GetProperty("newPassword").GetString();
                UserRepo repo = server.GetUserRepo();
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
                    return JsonSerializer.Serialize(new { success = true, message = "OK", user = (object)null });
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
                lock (sessionLock)
                {
                    if (activeSessions.ContainsKey(username))
                        activeSessions.Remove(username);
                    int currentCount = Interlocked.Decrement(ref TCPServer.clientCount);
                    Console.WriteLine($"[{clientIP}] Logout: {username}. Tổng số client hiện tại: {currentCount}");
                }
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
