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
        public TCPServer()
        {
            userRepo = new UserRepo();
        }
        public void Start(int port)
        {
            Database.Initialize();
            Console.WriteLine($"✓ Server khởi động trên port {port}\n");
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
                        "LOGOUT" => HandleLogout(),
                        "GET_USER_INFO" => HandleGetUserInfo(),
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
            private string HandleLogout()
            {
                if (loggedInUser != null)
                {
                    Console.WriteLine($"[{clientIP}] Logout: {loggedInUser.Username}");
                    loggedInUser = null;
                }
                return CreateResponse(true, "Đăng xuất thành công");
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
