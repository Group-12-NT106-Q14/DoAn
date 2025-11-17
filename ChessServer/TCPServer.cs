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

        // OTP & phiên đăng nhập
        private static Dictionary<string, (string Otp, DateTime Expiry)> otpStore = new Dictionary<string, (string Otp, DateTime Expiry)>();
        private static Dictionary<string, SessionInfo> activeSessions = new Dictionary<string, SessionInfo>();

        // BẢN B: đếm số user đang có phiên (khác socket). Tăng khi ref-count từ 0 -> 1, giảm khi 1 -> 0.
        private static int clientCount = 0;

        // BẢN B: ref-count số kênh (socket) đang gắn với user
        private static Dictionary<string, int> connectionRefs = new Dictionary<string, int>();

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
            listenThread.IsBackground = false; // giữ process sống
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

        public UserRepo GetUserRepo() => userRepo;

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

            // Kênh push hay req?
            private bool isPushChannel = false;

            // Guard để Cleanup chỉ chạy đúng 1 lần
            private bool cleanedUp = false;

            // Danh sách client đang kết nối (broadcast CHAT/ROOMS/ROOM_EVENT/ROOMCHAT)
            private static List<ClientHandler> connectedClients = new List<ClientHandler>();

            // =========================
            //   ROOMS STATE (static)
            // =========================
            private static readonly object roomsLock = new object();
            private static readonly Dictionary<int, Room> rooms = new Dictionary<int, Room>();
            private static int nextRoomId = 1;

            // Room hiện tại của client này (nếu có) — dùng để lọc push
            private int? CurrentRoomId = null;

            private class Room
            {
                public int Id { get; set; }
                public string Name { get; set; }

                public int OwnerUserId { get; set; }
                public string OwnerUsername { get; set; }
                public string OwnerDisplayName { get; set; }
                public int OwnerElo { get; set; }

                public int? GuestUserId { get; set; }
                public string GuestUsername { get; set; }
                public string GuestDisplayName { get; set; }
                public int GuestElo { get; set; }

                public bool GuestReady { get; set; }

                // Thiết lập ván
                public int Minutes { get; set; } = 3;
                public int Increment { get; set; } = 0;
                public string Side { get; set; } = "white"; // bên của chủ phòng

                public int PlayersCount => (OwnerUserId != 0 ? 1 : 0) + (GuestUserId.HasValue ? 1 : 0);
            }

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

                        // Client có thể gửi liên tiếp nhiều JSON — tách an toàn theo depth
                        foreach (var json in SplitJsonStream(request))
                        {
                            string response = ProcessRequest(json);
                            if (!string.IsNullOrEmpty(response))
                                SendMessage(response);
                        }
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
                if (cleanedUp) return;
                cleanedUp = true;

                try
                {
                    // Nếu đang trong phòng -> xử lý rời (để chuyển chủ/xóa phòng)
                    try
                    {
                        if (CurrentRoomId.HasValue && loggedInUser != null)
                        {
                            var dummy = JsonDocument.Parse(JsonSerializer.Serialize(new
                            {
                                action = "ROOM_LEAVE",
                                username = loggedInUser.Username
                            }));
                            HandleRoomLeave(dummy.RootElement);
                        }
                    }
                    catch { }

                    lock (connectedClients) { connectedClients.Remove(this); }

                    // Giảm ref cho user (nếu có); khi về 0 -> xoá session + log duy nhất
                    if (isLoggedIn && loggedInUser != null)
                    {
                        bool removedSession = false;
                        lock (sessionLock)
                        {
                            if (connectionRefs.TryGetValue(loggedInUser.Username, out var n) && n > 0)
                            {
                                n--;
                                if (n == 0)
                                {
                                    connectionRefs.Remove(loggedInUser.Username);
                                    if (activeSessions.ContainsKey(loggedInUser.Username))
                                        activeSessions.Remove(loggedInUser.Username);
                                    Interlocked.Decrement(ref TCPServer.clientCount);
                                    removedSession = true;
                                }
                                else
                                {
                                    connectionRefs[loggedInUser.Username] = n;
                                }
                            }
                        }
                        if (removedSession)
                        {
                            Console.WriteLine($"[{clientIP}] All channels closed -> session removed: {loggedInUser.Username}");
                        }
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

                    // Tài khoản / OTP / Chat tổng / Online users
                    if (action == "REGISTER") return HandleRegister(root);
                    else if (action == "LOGIN") return HandleLogin(root);
                    else if (action == "AUTH_ATTACH") return HandleAuthAttach(root); // <== có mode
                    else if (action == "GET_USER_INFO") return HandleGetUserInfo();
                    else if (action == "CHAT") { HandleChat(root); return ""; }
                    else if (action == "UPDATE_ACCOUNT") return HandleUpdateAccount(root);
                    else if (action == "REQUEST_OTP") return HandleRequestOtp(root);
                    else if (action == "RESET_PASSWORD") return HandleResetPassword(root);
                    else if (action == "VERIFY_OTP") return HandleVerifyOtp(root);
                    else if (action == "GET_ONLINE_USERS") return HandleGetOnlineUsers();
                    else if (action == "LOGOUT") return HandleLogout(root);

                    // Phòng
                    else if (action == "ROOM_CREATE") return HandleRoomCreate(root);
                    else if (action == "ROOM_LIST") return HandleRoomList();
                    else if (action == "ROOM_JOIN") return HandleRoomJoin(root);
                    else if (action == "ROOM_LEAVE") return HandleRoomLeave(root);
                    else if (action == "ROOM_RENAME") return HandleRoomRename(root);
                    else if (action == "ROOM_READY") return HandleRoomReady(root);
                    else if (action == "ROOM_START") return HandleRoomStart(root);
                    else if (action == "ROOM_UPDATE_CONFIG") return HandleRoomUpdateConfig(root);
                    else if (action == "ROOM_CHAT") { HandleRoomChat(root); return ""; }

                    // Push binding & snapshot
                    else if (action == "ROOM_BIND") return HandleRoomBind(root);
                    else if (action == "ROOM_GET") return HandleRoomGet(root);

                    else return "";
                }
            }

            // ========= HANDLERS: Tài khoản / OTP / Chat tổng =========

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
                    bool firstRef = false;
                    lock (sessionLock)
                    {
                        // Ghi/ghi đè phiên (multi-channel cùng user)
                        activeSessions[username] = new SessionInfo
                        {
                            UserId = user.UserID,
                            Username = username,
                            ClientIP = clientIP,
                            LoginTime = DateTime.Now
                        };

                        // Tăng ref-count
                        if (!connectionRefs.ContainsKey(username))
                        {
                            connectionRefs[username] = 1;
                            firstRef = true;
                        }
                        else
                        {
                            connectionRefs[username] = connectionRefs[username] + 1;
                        }

                        // Log CHỈ khi lần đầu (0->1)
                        if (firstRef)
                        {
                            int current = Interlocked.Increment(ref TCPServer.clientCount);
                            Console.WriteLine($"[{clientIP}] Login: {username}. Active sessions: {current}");
                        }
                    }

                    loggedInUser = user;
                    isLoggedIn = true;

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
                else
                {
                    return CreateResponse(false, "Sai tài khoản hoặc mật khẩu");
                }
            }

            // NEW: Gắn phiên cho kết nối TCP mới từ client đã đăng nhập ở nơi khác
            private string HandleAuthAttach(JsonElement req)
            {
                string username = req.GetProperty("username").GetString();
                string mode = req.TryGetProperty("mode", out var jm) ? jm.GetString() : "req";
                isPushChannel = string.Equals(mode, "push", StringComparison.OrdinalIgnoreCase);

                ClassUser user;
                lock (sessionLock)
                {
                    if (!activeSessions.ContainsKey(username))
                        return CreateResponse(false, "Phiên không tồn tại. Vui lòng đăng nhập lại.");

                    user = server.GetUserRepo().GetAllUsers()
                          .FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
                    if (user == null) return CreateResponse(false, "Không tìm thấy người dùng.");

                    // tăng ref-count do attach kênh mới
                    if (!connectionRefs.ContainsKey(username))
                    {
                        connectionRefs[username] = 1;
                        Interlocked.Increment(ref TCPServer.clientCount); // 0->1 (hiếm khi xảy ra tại đây)
                    }
                    else
                    {
                        connectionRefs[username] = connectionRefs[username] + 1;
                    }
                    // KHÔNG log attach theo yêu cầu
                }

                loggedInUser = user;
                isLoggedIn = true;

                return CreateResponse(true, "Đã gắn phiên cho kết nối.");
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
                Console.WriteLine("[" + clientIP + "] UpdateAccount: " + username);

                if (loggedInUser != null && loggedInUser.UserID == userId)
                {
                    loggedInUser = repo.GetUserById(userId);
                }

                return CreateResponse(true, "Cập nhật tài khoản thành công!");
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

                // Gửi mail OTP
                MimeMessage message = new MimeMessage();
                message.From.Add(new MailboxAddress("Nhóm 12 Lập trình mạng căn bản (NT106.Q14)", "group12.nt106.q14@gmail.com"));
                message.To.Add(new MailboxAddress("", email));
                message.Subject = "Mã xác nhận đổi mật khẩu đồ án Trò chơi Cờ vua chơi qua mạng";
                string body = $"Xin chào,\n\nMÃ XÁC NHẬN (OTP): {otp}\nOTP có hiệu lực trong 10 phút.\n\nTrân trọng,\nNhóm 12 - NT106.Q14\nTrò chơi Cờ vua chơi qua mạng\n";
                BodyBuilder builder = new BodyBuilder { TextBody = body };
                message.Body = builder.ToMessageBody();

                using (SmtpClient client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                    client.Authenticate("group12.nt106.q14@gmail.com", "tmjx bacw rvsg dybr"); // App Password
                    client.Send(message);
                    client.Disconnect(true);
                }

                Console.WriteLine("[" + clientIP + "] Gửi OTP đến " + email);
                return CreateResponse(true, "Đã gửi mã xác nhận đến email!");
            }

            private string HandleVerifyOtp(JsonElement req)
            {
                string email = req.GetProperty("email").GetString();
                string otp = req.GetProperty("otp").GetString();

                if (!otpStore.ContainsKey(email))
                    return CreateResponse(false, "Bạn chưa yêu cầu mã xác nhận!");

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
                    if (u.Email == email) { found = u; break; }
                }
                if (found == null) return CreateResponse(false, "Email không tồn tại!");

                string username = found.Username;
                repo.ResetPassword(email, newPassword);
                Console.WriteLine("[" + clientIP + "] Reset Password: " + username);
                return CreateResponse(true, "Đổi mật khẩu thành công!");
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

            private string HandleLogout(JsonElement req)
            {
                string username = req.GetProperty("username").GetString();

                int current;
                lock (sessionLock)
                {
                    // Xoá toàn bộ ref-count của user này (các kênh coi như bị “tước phiên”)
                    if (connectionRefs.ContainsKey(username))
                        connectionRefs.Remove(username);

                    if (activeSessions.ContainsKey(username))
                    {
                        activeSessions.Remove(username);
                        // Chỉ khi đang có session thì mới giảm tổng số phiên
                        current = Interlocked.Decrement(ref TCPServer.clientCount);
                    }
                    else
                    {
                        // Không có session để xoá -> không giảm, chỉ đọc giá trị hiện tại để log
                        current = System.Threading.Volatile.Read(ref TCPServer.clientCount);
                    }
                }

                // Đánh dấu kênh hiện tại đã logout để CleanupClient không trừ lần nữa
                isLoggedIn = false;

                // 👉 Log logout như bạn muốn
                Console.WriteLine($"[{clientIP}] Logout: {username}. Active sessions: {current}");

                return CreateResponse(true, "Đăng xuất thành công!");
            }


            // ========= HANDLERS: Phòng (Rooms) =========

            private string HandleRoomCreate(JsonElement req)
            {
                if (loggedInUser == null) return CreateResponse(false, "Bạn cần đăng nhập.");
                string roomName = req.GetProperty("roomName").GetString();
                if (string.IsNullOrWhiteSpace(roomName))
                    return CreateResponse(false, "Tên phòng không hợp lệ.");

                Room room = new Room();
                lock (roomsLock)
                {
                    room.Id = nextRoomId++;
                    room.Name = roomName;
                    room.OwnerUserId = loggedInUser.UserID;
                    room.OwnerUsername = loggedInUser.Username;
                    room.OwnerDisplayName = loggedInUser.DisplayName;
                    room.OwnerElo = loggedInUser.Elo;
                    rooms[room.Id] = room;
                }
                CurrentRoomId = room.Id;

                BroadcastRooms();

                return JsonSerializer.Serialize(new
                {
                    success = true,
                    message = "Tạo phòng thành công.",
                    room = RoomDto(room)
                });
            }

            private string HandleRoomList()
            {
                var arr = new List<object>();
                lock (roomsLock)
                {
                    foreach (var r in rooms.Values)
                    {
                        arr.Add(new
                        {
                            id = r.Id,
                            name = r.Name,
                            ownerUsername = r.OwnerUsername,
                            ownerDisplayName = r.OwnerDisplayName,
                            ownerElo = r.OwnerElo,
                            players = r.PlayersCount, // gồm cả 2/2
                            minutes = r.Minutes,
                            increment = r.Increment
                        });
                    }
                }
                return JsonSerializer.Serialize(new { success = true, rooms = arr });
            }

            private string HandleRoomJoin(JsonElement req)
            {
                if (loggedInUser == null) return CreateResponse(false, "Bạn cần đăng nhập.");
                int roomId = req.GetProperty("roomId").GetInt32();

                lock (roomsLock)
                {
                    if (!rooms.ContainsKey(roomId)) return CreateResponse(false, "Phòng không tồn tại.");
                    var r = rooms[roomId];
                    if (r.GuestUserId.HasValue) return CreateResponse(false, "Phòng đã đầy.");

                    r.GuestUserId = loggedInUser.UserID;
                    r.GuestUsername = loggedInUser.Username;
                    r.GuestDisplayName = loggedInUser.DisplayName;
                    r.GuestElo = loggedInUser.Elo;
                    r.GuestReady = false;
                    CurrentRoomId = roomId;

                    // ROOM_EVENT JOINED
                    string ev = JsonSerializer.Serialize(new
                    {
                        type = "ROOM_EVENT",
                        roomId = r.Id,
                        @event = "JOINED",
                        eventAlt = "GUEST_JOINED",
                        room = RoomDto(r)
                    });
                    BroadcastToRoom(r.Id, ev);

                    BroadcastRooms();
                }
                return JsonSerializer.Serialize(new { success = true, message = "Đã vào phòng." });
            }

            private string HandleRoomLeave(JsonElement req)
            {
                string username = req.GetProperty("username").GetString();
                Room r = null;
                bool removed = false;
                bool ownerChanged = false;

                lock (roomsLock)
                {
                    if (!CurrentRoomId.HasValue)
                        return JsonSerializer.Serialize(new { success = true });

                    if (!rooms.TryGetValue(CurrentRoomId.Value, out r))
                    {
                        CurrentRoomId = null;
                        return JsonSerializer.Serialize(new { success = true });
                    }

                    bool isOwner = string.Equals(r.OwnerUsername, username, StringComparison.OrdinalIgnoreCase);
                    bool isGuest = r.GuestUserId.HasValue && string.Equals(r.GuestUsername, username, StringComparison.OrdinalIgnoreCase);

                    if (isOwner)
                    {
                        if (r.GuestUserId.HasValue)
                        {
                            // chuyển chủ cho khách
                            r.OwnerUserId = r.GuestUserId.Value;
                            r.OwnerUsername = r.GuestUsername;
                            r.OwnerDisplayName = r.GuestDisplayName;
                            r.OwnerElo = r.GuestElo;

                            r.GuestUserId = null;
                            r.GuestUsername = null;
                            r.GuestDisplayName = null;
                            r.GuestElo = 0;
                            r.GuestReady = false;

                            ownerChanged = true;

                            string ev = JsonSerializer.Serialize(new
                            {
                                type = "ROOM_EVENT",
                                roomId = r.Id,
                                @event = "OWNER_CHANGED",
                                eventAlt = "OWNER_GUEST",
                                room = RoomDto(r)
                            });
                            BroadcastToRoom(r.Id, ev);
                        }
                        else
                        {
                            // không có khách -> xóa phòng
                            rooms.Remove(r.Id);
                            removed = true;
                        }
                    }
                    else if (isGuest)
                    {
                        r.GuestUserId = null;
                        r.GuestUsername = null;
                        r.GuestDisplayName = null;
                        r.GuestElo = 0;
                        r.GuestReady = false;

                        string ev = JsonSerializer.Serialize(new
                        {
                            type = "ROOM_EVENT",
                            roomId = r.Id,
                            @event = "LEFT",
                            eventAlt = "GUEST_LEFT",
                            room = RoomDto(r)
                        });
                        BroadcastToRoom(r.Id, ev);
                    }

                    CurrentRoomId = null;
                }

                // Cập nhật danh sách (2/2 -> 1/2 hoặc bị xóa)
                BroadcastRooms();
                return JsonSerializer.Serialize(new { success = true, removed, ownerChanged });
            }

            private string HandleRoomRename(JsonElement req)
            {
                int roomId = req.GetProperty("roomId").GetInt32();
                string newName = req.GetProperty("newName").GetString();
                string username = req.GetProperty("username").GetString();

                lock (roomsLock)
                {
                    if (!rooms.TryGetValue(roomId, out var r))
                        return CreateResponse(false, "Phòng không tồn tại.");
                    if (!string.Equals(r.OwnerUsername, username, StringComparison.OrdinalIgnoreCase))
                        return CreateResponse(false, "Chỉ chủ phòng mới có quyền.");

                    r.Name = newName;

                    string ev = JsonSerializer.Serialize(new
                    {
                        type = "ROOM_EVENT",
                        roomId = r.Id,
                        @event = "RENAMED",
                        room = RoomDto(r)
                    });
                    BroadcastToRoom(r.Id, ev);
                }

                BroadcastRooms();
                return JsonSerializer.Serialize(new { success = true });
            }

            private string HandleRoomReady(JsonElement req)
            {
                int roomId = req.GetProperty("roomId").GetInt32();
                string username = req.GetProperty("username").GetString();
                bool ready = req.GetProperty("ready").GetBoolean();

                lock (roomsLock)
                {
                    if (!rooms.TryGetValue(roomId, out var r))
                        return CreateResponse(false, "Phòng không tồn tại.");
                    if (!r.GuestUserId.HasValue || !string.Equals(r.GuestUsername, username, StringComparison.OrdinalIgnoreCase))
                        return CreateResponse(false, "Chỉ khách trong phòng mới được sẵn sàng.");

                    r.GuestReady = ready;

                    string ev = JsonSerializer.Serialize(new
                    {
                        type = "ROOM_EVENT",
                        roomId = r.Id,
                        @event = "READY_CHANGED",
                        room = RoomDto(r)
                    });
                    BroadcastToRoom(r.Id, ev);
                }
                return JsonSerializer.Serialize(new { success = true });
            }

            private string HandleRoomStart(JsonElement req)
            {
                int roomId = req.GetProperty("roomId").GetInt32();
                string username = req.GetProperty("username").GetString();

                lock (roomsLock)
                {
                    if (!rooms.TryGetValue(roomId, out var r))
                        return CreateResponse(false, "Phòng không tồn tại.");
                    if (!string.Equals(r.OwnerUsername, username, StringComparison.OrdinalIgnoreCase))
                        return CreateResponse(false, "Chỉ chủ phòng mới được bắt đầu.");
                    if (!r.GuestUserId.HasValue)
                        return CreateResponse(false, "Chưa có khách.");
                    if (!r.GuestReady)
                        return CreateResponse(false, "Khách chưa sẵn sàng.");

                    string ev = JsonSerializer.Serialize(new
                    {
                        type = "ROOM_EVENT",
                        roomId = r.Id,
                        @event = "STARTED",
                        room = RoomDto(r)
                    });
                    BroadcastToRoom(r.Id, ev);
                }
                return JsonSerializer.Serialize(new { success = true, message = "Bắt đầu trò chơi (giả lập)." });
            }

            private string HandleRoomUpdateConfig(JsonElement req)
            {
                int roomId = req.GetProperty("roomId").GetInt32();
                int minutes = req.GetProperty("minutes").GetInt32();
                int increment = req.GetProperty("increment").GetInt32();
                string side = req.GetProperty("side").GetString();
                string username = req.GetProperty("username").GetString();

                lock (roomsLock)
                {
                    if (!rooms.TryGetValue(roomId, out var r))
                        return CreateResponse(false, "Phòng không tồn tại.");
                    if (!string.Equals(r.OwnerUsername, username, StringComparison.OrdinalIgnoreCase))
                        return CreateResponse(false, "Chỉ chủ phòng mới được đổi thiết lập.");

                    r.Minutes = minutes;
                    r.Increment = increment;
                    r.Side = side;

                    string ev = JsonSerializer.Serialize(new
                    {
                        type = "ROOM_EVENT",
                        roomId = r.Id,
                        @event = "CONFIG_UPDATED",
                        room = RoomDto(r)
                    });
                    BroadcastToRoom(r.Id, ev);
                }

                BroadcastRooms();
                return JsonSerializer.Serialize(new { success = true });
            }

            private void HandleRoomChat(JsonElement req)
            {
                int roomId = req.GetProperty("roomId").GetInt32();
                string sender = req.GetProperty("sender").GetString();
                string role = req.GetProperty("role").GetString();
                string content = req.GetProperty("content").GetString();

                string msgJson = JsonSerializer.Serialize(new
                {
                    type = "ROOMCHAT",
                    roomId = roomId,
                    sender = sender,
                    role = role,
                    content = content,
                    timestamp = DateTime.Now.ToString("HH:mm:ss")
                });
                BroadcastToRoom(roomId, msgJson);
            }

            // ========= Push binding & snapshot =========

            private string HandleRoomBind(JsonElement req)
            {
                int roomId = req.GetProperty("roomId").GetInt32();
                string username = req.GetProperty("username").GetString();

                lock (roomsLock)
                {
                    if (!rooms.TryGetValue(roomId, out var r))
                        return CreateResponse(false, "Phòng không tồn tại.");

                    bool isMember =
                        string.Equals(r.OwnerUsername, username, StringComparison.OrdinalIgnoreCase) ||
                        (r.GuestUserId.HasValue && string.Equals(r.GuestUsername, username, StringComparison.OrdinalIgnoreCase));

                    if (!isMember)
                        return CreateResponse(false, "Bạn không thuộc phòng này.");

                    CurrentRoomId = roomId;
                }
                return JsonSerializer.Serialize(new { success = true });
            }

            private string HandleRoomGet(JsonElement req)
            {
                int roomId = req.GetProperty("roomId").GetInt32();
                lock (roomsLock)
                {
                    if (!rooms.TryGetValue(roomId, out var r))
                        return CreateResponse(false, "Phòng không tồn tại.");
                    return JsonSerializer.Serialize(new { success = true, room = RoomDto(r) });
                }
            }

            // ========= Helpers chung =========

            private static string BuildRoomsSlimJson()
            {
                var arr = new List<object>();
                lock (roomsLock)
                {
                    foreach (var r in rooms.Values)
                    {
                        arr.Add(new
                        {
                            id = r.Id,
                            name = r.Name,
                            ownerUsername = r.OwnerUsername,
                            ownerDisplayName = r.OwnerDisplayName,
                            ownerElo = r.OwnerElo,
                            players = r.PlayersCount,
                            minutes = r.Minutes,
                            increment = r.Increment
                        });
                    }
                }
                return JsonSerializer.Serialize(new { type = "ROOMS", rooms = arr });
            }

            private static object RoomDto(Room r)
            {
                return new
                {
                    id = r.Id,
                    name = r.Name,
                    ownerUsername = r.OwnerUsername,
                    ownerDisplayName = r.OwnerDisplayName,
                    ownerElo = r.OwnerElo,
                    guestUsername = r.GuestUsername,
                    guestDisplayName = r.GuestDisplayName,
                    guestElo = r.GuestElo,
                    guestReady = r.GuestReady,
                    minutes = r.Minutes,
                    increment = r.Increment,
                    side = r.Side
                };
            }

            private static void BroadcastRooms()
            {
                string json = BuildRoomsSlimJson();
                lock (connectedClients)
                {
                    foreach (var c in connectedClients)
                    {
                        if (!c.isPushChannel) continue; // chỉ kênh push
                        try { c.SendMessage(json); }
                        catch { }
                    }
                }
            }

            private static void BroadcastToRoom(int roomId, string json)
            {
                lock (connectedClients)
                {
                    foreach (var c in connectedClients)
                    {
                        if (!c.isPushChannel) continue; // chỉ kênh push
                        if (c.CurrentRoomId == roomId)
                        {
                            try { c.SendMessage(json); }
                            catch { }
                        }
                    }
                }
            }

            private static IEnumerable<string> SplitJsonStream(string raw)
            {
                var list = new List<string>();
                int depth = 0, start = -1;
                for (int i = 0; i < raw.Length; i++)
                {
                    if (raw[i] == '{') { if (depth == 0) start = i; depth++; }
                    else if (raw[i] == '}')
                    {
                        depth--;
                        if (depth == 0 && start != -1)
                        {
                            list.Add(raw.Substring(start, i - start + 1));
                            start = -1;
                        }
                    }
                }
                if (list.Count == 0) list.Add(raw);
                return list;
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
