using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
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
        private static Dictionary<string, (string Otp, DateTime Expiry)> otpStore = new Dictionary<string, (string Otp, DateTime Expiry)>();
        private static Dictionary<string, SessionInfo> activeSessions = new Dictionary<string, SessionInfo>();
        private static int clientCount = 0;
        private static Dictionary<string, int> connectionRefs = new Dictionary<string, int>();
        private static object sessionLock = new object();
        public static void Log(string message)
        {
            string time = DateTime.Now.ToString("HH:mm:ss");
            Console.WriteLine($"[{time}] {message}");
        }
        public TCPServer()
        {
            userRepo = new UserRepo();
        }
        public void Start(int port)
        {
            Database.Initialize();
            Log($"[SERVER] Khởi động trên port {port}");
            tcpListener = new TcpListener(IPAddress.Any, port);
            Thread listenThread = new Thread(ListenForClients);
            listenThread.IsBackground = false;
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
            private bool isPushChannel = false;
            private bool cleanedUp = false;
            private DateTime lastActivityUtc = DateTime.UtcNow;
            private static readonly TimeSpan HeartbeatTimeout = TimeSpan.FromSeconds(45);
            private static readonly System.Threading.Timer HeartbeatTimer;
            private static List<ClientHandler> connectedClients = new List<ClientHandler>();
            private static readonly object roomsLock = new object();
            private static readonly Dictionary<int, Room> rooms = new Dictionary<int, Room>();
            private static int nextRoomId = 1;
            private int? CurrentRoomId = null;
            private class QuickQueueEntry
            {
                public int UserId;
                public string Username;
                public string DisplayName;
                public int Elo;
            }
            private class QuickGame
            {
                public int GameId;
                public int WhiteUserId;
                public int BlackUserId;
                public string WhiteUsername;
                public string BlackUsername;
                public string WhiteDisplayName;
                public string BlackDisplayName;
                public int Minutes;
                public int Increment;
            }
            private static readonly object quickLock = new object();
            private static readonly Queue<QuickQueueEntry> quickQueue = new Queue<QuickQueueEntry>();
            private static readonly Dictionary<int, QuickGame> quickGames = new Dictionary<int, QuickGame>();
            private static int nextQuickGameId = 1;
            class Room
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
                public bool IsPlaying { get; set; } = false;
                public int Minutes { get; set; } = 3;
                public int Increment { get; set; } = 0;
                public string Side { get; set; } = "white";
                public int PlayersCount => (OwnerUserId != 0 ? 1 : 0) + (GuestUserId.HasValue ? 1 : 0);
            }
            static ClientHandler()
            {
                HeartbeatTimer = new System.Threading.Timer(CheckHeartbeats, null,
                    TimeSpan.FromSeconds(10),
                    TimeSpan.FromSeconds(10));
            }
            public ClientHandler(TcpClient client, TCPServer server)
            {
                tcpClient = client;
                this.server = server;
                stream = tcpClient.GetStream();
                clientIP = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
                lastActivityUtc = DateTime.UtcNow;
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
                    TCPServer.Log($"[{clientIP}] IOException: {ioEx.Message}");
                }
                catch (SocketException sockEx)
                {
                    TCPServer.Log($"[{clientIP}] SocketException: {sockEx.Message}");
                }
                catch (Exception ex)
                {
                    TCPServer.Log($"[{clientIP}] Exception: {ex.Message}");
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
                            TCPServer.Log($"[{clientIP}] All channels closed -> session removed: {loggedInUser.Username}");
                        }
                    }
                    isLoggedIn = false;
                    stream?.Close();
                    tcpClient?.Close();
                    if (loggedInUser != null)
                    {
                        TCPServer.Log($"[{clientIP}] TCP connection closed (user={loggedInUser.Username})");
                    }
                }
                catch (Exception ex)
                {
                    TCPServer.Log($"[{clientIP}] Cleanup error: {ex.Message}");
                }
            }
            private static void CheckHeartbeats(object state)
            {
                List<ClientHandler> snapshot;
                lock (connectedClients)
                {
                    snapshot = new List<ClientHandler>(connectedClients);
                }
                var now = DateTime.UtcNow;
                foreach (var c in snapshot)
                {
                    try
                    {
                        if (c.cleanedUp) continue;
                        if ((now - c.lastActivityUtc) > HeartbeatTimeout)
                        {
                            TCPServer.Log($"[{c.clientIP}] Heartbeat timeout -> auto logout");
                            c.CleanupClient();
                        }
                    }
                    catch
                    {
                    }
                }
            }
            private string ProcessRequest(string requestData)
            {
                lastActivityUtc = DateTime.UtcNow;
                using (JsonDocument doc = JsonDocument.Parse(requestData))
                {
                    JsonElement root = doc.RootElement;
                    string action = root.GetProperty("action").GetString();
                    if (action == "HEARTBEAT")
                    {
                        return "";
                    }
                    if (action == "REGISTER") return HandleRegister(root);
                    else if (action == "LOGIN") return HandleLogin(root);
                    else if (action == "AUTH_ATTACH") return HandleAuthAttach(root);
                    else if (action == "GET_USER_INFO") return HandleGetUserInfo();
                    else if (action == "CHAT") { HandleChat(root); return ""; }
                    else if (action == "UPDATE_ACCOUNT") return HandleUpdateAccount(root);
                    else if (action == "REQUEST_OTP") return HandleRequestOtp(root);
                    else if (action == "RESET_PASSWORD") return HandleResetPassword(root);
                    else if (action == "VERIFY_OTP") return HandleVerifyOtp(root);
                    else if (action == "GET_ONLINE_USERS") return HandleGetOnlineUsers();
                    else if (action == "GET_HISTORY") return HandleGetHistory(root);
                    else if (action == "GET_RANKING") return HandleGetRanking();
                    else if (action == "MATCH_FIND") return HandleMatchFind(root);
                    else if (action == "MATCH_CANCEL") return HandleMatchCancel();
                    else if (action == "LOGOUT") return HandleLogout(root);
                    else if (action == "ROOM_CREATE") return HandleRoomCreate(root);
                    else if (action == "ROOM_LIST") return HandleRoomList();
                    else if (action == "ROOM_JOIN") return HandleRoomJoin(root);
                    else if (action == "ROOM_LEAVE") return HandleRoomLeave(root);
                    else if (action == "ROOM_RENAME") return HandleRoomRename(root);
                    else if (action == "ROOM_READY") return HandleRoomReady(root);
                    else if (action == "ROOM_START") return HandleRoomStart(root);
                    else if (action == "ROOM_UPDATE_CONFIG") return HandleRoomUpdateConfig(root);
                    else if (action == "ROOM_CHAT") { HandleRoomChat(root); return ""; }
                    else if (action == "ROOM_BIND") return HandleRoomBind(root);
                    else if (action == "GAME_MOVE") { HandleGameMove(root); return ""; }
                    else if (action == "GAME_CHAT") { HandleGameChat(root); return ""; }
                    else if (action == "GAME_RESIGN") { HandleGameResign(root); return ""; }
                    else if (action == "GAME_DRAW_OFFER") { HandleGameDrawOffer(root); return ""; }
                    else if (action == "GAME_DRAW_RESPONSE") { HandleGameDrawResponse(root); return ""; }
                    else if (action == "GAME_RESULT") return HandleGameResult(root);
                    else if (action == "ROOM_GET") return HandleRoomGet(root);
                    else return "";
                }
            }
            private string HandleRegister(JsonElement request)
            {
                string email = request.GetProperty("email").GetString();
                string displayName = request.GetProperty("displayName").GetString();
                string username = request.GetProperty("username").GetString();
                string password = request.GetProperty("password").GetString();
                UserRepo repo = new UserRepo();
                if (repo.IsUsernameExists(username))
                    return CreateResponse(false, "Username đã tồn tại.");
                if (repo.IsEmailExists(email))
                    return CreateResponse(false, "Email đã được sử dụng.");
                bool ok = repo.RegisterUser(email, displayName, username, password);
                if (!ok)
                {
                    TCPServer.Log($"[{clientIP}] Register FAILED (DB error): {username}");
                    return CreateResponse(false, "Đăng ký thất bại do lỗi hệ thống. Vui lòng thử lại.");
                }
                TCPServer.Log($"[{clientIP}] Register OK: {username}");
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
                        if (activeSessions.ContainsKey(username))
                        {
                            TCPServer.Log($"[{clientIP}] Login BLOCKED for username={username} (already online)");
                            return CreateResponse(false, "Tài khoản này đang được đăng nhập ở một nơi khác.");
                        }
                        activeSessions[username] = new SessionInfo
                        {
                            UserId = user.UserID,
                            Username = username,
                            ClientIP = clientIP,
                            LoginTime = DateTime.Now
                        };
                        if (!connectionRefs.ContainsKey(username))
                        {
                            connectionRefs[username] = 1;
                            firstRef = true;
                        }
                        else
                        {
                            connectionRefs[username] = connectionRefs[username] + 1;
                        }
                        if (firstRef)
                        {
                            int current = Interlocked.Increment(ref TCPServer.clientCount);
                            TCPServer.Log($"[{clientIP}] Login: {username}. Active sessions: {current}");
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
                    TCPServer.Log($"[{clientIP}] Login FAILED for username={username}");
                    return CreateResponse(false, "Sai tài khoản hoặc mật khẩu");
                }
            }
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
                    if (!connectionRefs.ContainsKey(username))
                    {
                        connectionRefs[username] = 1;
                        Interlocked.Increment(ref TCPServer.clientCount);
                    }
                    else
                    {
                        connectionRefs[username] = connectionRefs[username] + 1;
                    }
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
                string? newPassword = null;
                if (req.TryGetProperty("password", out JsonElement pwd) &&
                    pwd.ValueKind == JsonValueKind.String)
                {
                    newPassword = pwd.GetString();
                }
                newDisplayName = newDisplayName?.Trim() ?? "";
                newEmail = newEmail?.Trim() ?? "";
                UserRepo repo = server.GetUserRepo();
                ClassUser currentUser = repo.GetUserById(userId);
                if (currentUser == null)
                {
                    return CreateResponse(false, "Người dùng không tồn tại.");
                }
                string username = currentUser.Username;
                if (string.IsNullOrWhiteSpace(newDisplayName))
                {
                    return CreateResponse(false, "Tên hiển thị không được để trống.");
                }
                if (string.IsNullOrWhiteSpace(newEmail))
                {
                    return CreateResponse(false, "Email không được để trống.");
                }
                if (!newEmail.Contains("@") || !newEmail.Contains("."))
                {
                    return CreateResponse(false, "Email không hợp lệ.");
                }
                if (!string.Equals(newEmail, currentUser.Email, StringComparison.OrdinalIgnoreCase))
                {
                    if (repo.IsEmailExists(newEmail))
                    {
                        return CreateResponse(false, "Email đã được sử dụng bởi tài khoản khác.");
                    }
                }
                bool ok;
                try
                {
                    ok = repo.UpdateUserAccount(userId, newDisplayName, newEmail, newPassword);
                }
                catch (Exception ex)
                {
                    TCPServer.Log($"[{clientIP}] UpdateAccount exception: {ex.Message}");
                    return CreateResponse(false, "Cập nhật tài khoản thất bại. Vui lòng thử lại.");
                }
                if (!ok)
                {
                    TCPServer.Log($"[{clientIP}] UpdateAccount FAILED (DB error): {username}");
                    return CreateResponse(false, "Cập nhật tài khoản thất bại. Vui lòng thử lại.");
                }
                TCPServer.Log($"[{clientIP}] UpdateAccount: {username}");
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
                    client.Authenticate("group12.nt106.q14@gmail.com", "tmjx bacw rvsg dybr");
                    client.Send(message);
                    client.Disconnect(true);
                }
                TCPServer.Log($"[{clientIP}] Gửi OTP đến {email}");
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
                TCPServer.Log($"[{clientIP}] Email {email} đã xác nhận otp thành công!");
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
                TCPServer.Log($"[{clientIP}] Reset Password: {username}");
                return CreateResponse(true, "Đổi mật khẩu thành công!");
            }
            private string HandleGetHistory(JsonElement req)
            {
                int userId;
                if (req.TryGetProperty("userId", out JsonElement jUserId) && jUserId.ValueKind == JsonValueKind.Number)
                {
                    userId = jUserId.GetInt32();
                }
                else
                {
                    if (loggedInUser == null) return CreateResponse(false, "Không xác định được người dùng.");
                    userId = loggedInUser.UserID;
                }
                string filter = "all";
                if (req.TryGetProperty("resultFilter", out JsonElement jFilter) && jFilter.ValueKind == JsonValueKind.String)
                {
                    filter = jFilter.GetString() ?? "all";
                }
                filter = filter.ToLowerInvariant();
                DateTime? fromDate = null;
                DateTime? toDate = null;
                if (req.TryGetProperty("from", out JsonElement jFrom) && jFrom.ValueKind == JsonValueKind.String)
                {
                    DateTime tmp;
                    if (DateTime.TryParse(jFrom.GetString(), out tmp))
                    {
                        fromDate = tmp.Date;
                    }
                }
                if (req.TryGetProperty("to", out JsonElement jTo) && jTo.ValueKind == JsonValueKind.String)
                {
                    DateTime tmp;
                    if (DateTime.TryParse(jTo.GetString(), out tmp))
                    {
                        toDate = tmp.Date;
                    }
                }
                var matches = new List<object>();
                int total = 0;
                int wins = 0;
                int draws = 0;
                int losses = 0;
                using (SQLiteConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string sql =
                        "SELECT m.MatchID, m.WhiteUserID, m.BlackUserID, m.Result, " +
                        "m.StartTime, m.EndTime, m.TimeControlMinutes, m.IncrementSeconds, " +
                        "m.WhiteEloAfter, m.BlackEloAfter, " +
                        "w.DisplayName AS WhiteName, w.Username AS WhiteUsername, " +
                        "b.DisplayName AS BlackName, b.Username AS BlackUsername " +
                        "FROM Matches m " +
                        "JOIN Users w ON m.WhiteUserID = w.UserID " +
                        "JOIN Users b ON m.BlackUserID = b.UserID " +
                        "WHERE m.WhiteUserID = @UserID OR m.BlackUserID = @UserID " +
                        "ORDER BY m.EndTime DESC";
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userId);
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int matchId = reader.GetInt32(0);
                                int whiteId = reader.GetInt32(1);
                                int blackId = reader.GetInt32(2);
                                int resultCode = reader.GetInt32(3);
                                string startTimeStr = reader.IsDBNull(4) ? null : reader.GetString(4);
                                string endTimeStr = reader.IsDBNull(5) ? null : reader.GetString(5);
                                int timeMinutes = reader.IsDBNull(6) ? 0 : reader.GetInt32(6);
                                int incSeconds = reader.IsDBNull(7) ? 0 : reader.GetInt32(7);
                                int whiteEloAfter = reader.IsDBNull(8) ? 0 : reader.GetInt32(8);
                                int blackEloAfter = reader.IsDBNull(9) ? 0 : reader.GetInt32(9);
                                string whiteName = reader.GetString(10);
                                string whiteUsername = reader.GetString(11);
                                string blackName = reader.GetString(12);
                                string blackUsername = reader.GetString(13);
                                bool isWhite = (whiteId == userId);
                                string opponentName = isWhite ? blackName : whiteName;
                                string opponentUsername = isWhite ? blackUsername : whiteUsername;
                                int opponentRating = isWhite ? blackEloAfter : whiteEloAfter;
                                bool isWin = false;
                                bool isDraw = false;
                                bool isLoss = false;
                                string resultStr;
                                if (resultCode == 2)
                                {
                                    resultStr = "draw";
                                    isDraw = true;
                                }
                                else if ((resultCode == 0 && isWhite) || (resultCode == 1 && !isWhite))
                                {
                                    resultStr = "win";
                                    isWin = true;
                                }
                                else
                                {
                                    resultStr = "loss";
                                    isLoss = true;
                                }
                                DateTime? endTime = null;
                                if (!string.IsNullOrEmpty(endTimeStr))
                                {
                                    DateTime t;
                                    if (DateTime.TryParse(endTimeStr, out t))
                                    {
                                        endTime = t;
                                    }
                                }
                                if (fromDate.HasValue && endTime.HasValue && endTime.Value.Date < fromDate.Value.Date)
                                    continue;
                                if (toDate.HasValue && endTime.HasValue && endTime.Value.Date > toDate.Value.Date)
                                    continue;
                                if (filter == "win" && !isWin) continue;
                                if (filter == "draw" && !isDraw) continue;
                                if (filter == "loss" && !isLoss) continue;
                                total++;
                                if (isWin) wins++;
                                if (isDraw) draws++;
                                if (isLoss) losses++;
                                string endTimeOut = endTime.HasValue
                                    ? endTime.Value.ToString("yyyy-MM-dd HH:mm:ss")
                                    : endTimeStr;
                                matches.Add(new
                                {
                                    matchId = matchId,
                                    opponentName = opponentName,
                                    opponentUsername = opponentUsername,
                                    opponentRating = opponentRating,
                                    isWhite = isWhite,
                                    result = resultStr,
                                    timeControlMinutes = timeMinutes,
                                    incrementSeconds = incSeconds,
                                    endTime = endTimeOut
                                });
                            }
                        }
                    }
                }
                double winRate = total > 0 ? (double)wins / total : 0.0;
                var stats = new
                {
                    total = total,
                    wins = wins,
                    draws = draws,
                    losses = losses,
                    winRate = winRate
                };
                return JsonSerializer.Serialize(new { success = true, stats = stats, matches = matches });
            }
            private string HandleGetRanking()
            {
                var users = new List<object>();
                using (SQLiteConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string sql =
                        "SELECT u.UserID, u.Email, u.DisplayName, u.Username, u.Elo, " +
                        "IFNULL(s.GamesPlayed, 0), IFNULL(s.Wins, 0), IFNULL(s.Draws, 0), IFNULL(s.Losses, 0) " +
                        "FROM Users u " +
                        "LEFT JOIN UserStats s ON u.UserID = s.UserID " +
                        "ORDER BY u.Elo DESC, u.UserID ASC";
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        int rank = 1;
                        while (reader.Read())
                        {
                            string email = reader.GetString(1);
                            string displayName = reader.GetString(2);
                            string username = reader.GetString(3);
                            int elo = reader.IsDBNull(4) ? 0 : reader.GetInt32(4);
                            int gamesPlayed = reader.IsDBNull(5) ? 0 : reader.GetInt32(5);
                            int wins = reader.IsDBNull(6) ? 0 : reader.GetInt32(6);
                            int draws = reader.IsDBNull(7) ? 0 : reader.GetInt32(7);
                            int losses = reader.IsDBNull(8) ? 0 : reader.GetInt32(8);
                            double winRate = gamesPlayed > 0 ? (double)wins / gamesPlayed : 0.0;
                            users.Add(new
                            {
                                rank = rank,
                                email = email,
                                displayName = displayName,
                                username = username,
                                elo = elo,
                                gamesPlayed = gamesPlayed,
                                wins = wins,
                                draws = draws,
                                losses = losses,
                                winRate = winRate
                            });
                            rank++;
                        }
                    }
                }
                return JsonSerializer.Serialize(new { success = true, users = users });
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
            private string HandleMatchFind(JsonElement req)
            {
                if (!isLoggedIn || loggedInUser == null)
                    return CreateResponse(false, "Chưa đăng nhập.");
                QuickGame createdGame = null;
                lock (quickLock)
                {
                    var tmp = new Queue<QuickQueueEntry>();
                    while (quickQueue.Count > 0)
                    {
                        var e = quickQueue.Dequeue();
                        if (e.UserId != loggedInUser.UserID)
                        {
                            tmp.Enqueue(e);
                        }
                    }
                    while (tmp.Count > 0) quickQueue.Enqueue(tmp.Dequeue());
                    QuickQueueEntry opponent = null;
                    while (quickQueue.Count > 0)
                    {
                        var cand = quickQueue.Dequeue();
                        if (cand.UserId == loggedInUser.UserID)
                        {
                            continue;
                        }
                        opponent = cand;
                        break;
                    }
                    if (opponent == null)
                    {
                        quickQueue.Enqueue(new QuickQueueEntry
                        {
                            UserId = loggedInUser.UserID,
                            Username = loggedInUser.Username,
                            DisplayName = loggedInUser.DisplayName,
                            Elo = loggedInUser.Elo
                        });
                        TCPServer.Log($"[{clientIP}] MATCH_FIND -> user={loggedInUser.Username} vào hàng chờ");
                        return JsonSerializer.Serialize(new
                        {
                            success = true,
                            waiting = true
                        });
                    }
                    int gameId = nextQuickGameId++;
                    var rnd = new Random();
                    bool thisIsWhite = rnd.Next(2) == 0;
                    createdGame = new QuickGame
                    {
                        GameId = gameId,
                        Minutes = 10,
                        Increment = 0
                    };
                    if (thisIsWhite)
                    {
                        createdGame.WhiteUserId = loggedInUser.UserID;
                        createdGame.WhiteUsername = loggedInUser.Username;
                        createdGame.WhiteDisplayName = loggedInUser.DisplayName;
                        createdGame.BlackUserId = opponent.UserId;
                        createdGame.BlackUsername = opponent.Username;
                        createdGame.BlackDisplayName = opponent.DisplayName;
                    }
                    else
                    {
                        createdGame.BlackUserId = loggedInUser.UserID;
                        createdGame.BlackUsername = loggedInUser.Username;
                        createdGame.BlackDisplayName = loggedInUser.DisplayName;
                        createdGame.WhiteUserId = opponent.UserId;
                        createdGame.WhiteUsername = opponent.Username;
                        createdGame.WhiteDisplayName = opponent.DisplayName;
                    }
                    quickGames[gameId] = createdGame;
                }
                if (createdGame != null)
                {
                    string json = JsonSerializer.Serialize(new
                    {
                        type = "MATCH_FOUND",
                        gameId = createdGame.GameId,
                        whiteUsername = createdGame.WhiteUsername,
                        whiteDisplayName = createdGame.WhiteDisplayName,
                        blackUsername = createdGame.BlackUsername,
                        blackDisplayName = createdGame.BlackDisplayName,
                        minutes = createdGame.Minutes,
                        increment = createdGame.Increment
                    });
                    BroadcastToQuickGame(createdGame.GameId, json);
                    TCPServer.Log($"[{clientIP}] MATCH_FOUND gameId={createdGame.GameId} white={createdGame.WhiteUsername} black={createdGame.BlackUsername}");
                    return JsonSerializer.Serialize(new
                    {
                        success = true,
                        waiting = false
                    });
                }
                return JsonSerializer.Serialize(new { success = true });
            }
            private static bool RemoveFromQuickQueue(int userId)
            {
                lock (quickLock)
                {
                    if (quickQueue.Count == 0) return false;
                    var tmp = new Queue<QuickQueueEntry>();
                    bool removed = false;
                    while (quickQueue.Count > 0)
                    {
                        var e = quickQueue.Dequeue();
                        if (e.UserId == userId)
                        {
                            removed = true;
                            continue;
                        }
                        tmp.Enqueue(e);
                    }
                    while (tmp.Count > 0) quickQueue.Enqueue(tmp.Dequeue());
                    return removed;
                }
            }
            private string HandleMatchCancel()
            {
                if (!isLoggedIn || loggedInUser == null)
                    return JsonSerializer.Serialize(new { success = true });
                bool removed = RemoveFromQuickQueue(loggedInUser.UserID);
                if (removed)
                {
                    TCPServer.Log($"[{clientIP}] MATCH_CANCEL -> user={loggedInUser.Username} rời khỏi hàng chờ");
                }
                return JsonSerializer.Serialize(new
                {
                    success = true,
                    removed = removed
                });
            }
            private string HandleLogout(JsonElement req)
            {
                string username = req.GetProperty("username").GetString();
                int current;
                lock (sessionLock)
                {
                    if (connectionRefs.ContainsKey(username))
                        connectionRefs.Remove(username);
                    if (activeSessions.ContainsKey(username))
                    {
                        activeSessions.Remove(username);
                        current = Interlocked.Decrement(ref TCPServer.clientCount);
                    }
                    else
                    {
                        current = System.Threading.Volatile.Read(ref TCPServer.clientCount);
                    }
                }
                isLoggedIn = false;
                TCPServer.Log($"[{clientIP}] Logout: {username}. Active sessions: {current}");
                return CreateResponse(true, "Đăng xuất thành công!");
            }
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
                TCPServer.Log($"[{clientIP}] ROOM_CREATE by {loggedInUser.Username}, roomId={room.Id}, name={room.Name}");
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
                            players = r.PlayersCount,
                            minutes = r.Minutes,
                            increment = r.Increment,
                            status = r.IsPlaying ? "playing" : "lobby"
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
                    TCPServer.Log($"[{clientIP}] ROOM_JOIN user={loggedInUser.Username}, roomId={r.Id}");
                }
                return JsonSerializer.Serialize(new { success = true, message = "Đã vào phòng." });
            }
            private string HandleRoomLeave(JsonElement req)
            {
                string username = req.GetProperty("username").GetString();
                Room r = null;
                bool removed = false;
                bool ownerChanged = false;
                int? leftRoomId = null;
                lock (roomsLock)
                {
                    if (!CurrentRoomId.HasValue)
                        return JsonSerializer.Serialize(new { success = true });
                    if (!rooms.TryGetValue(CurrentRoomId.Value, out r))
                    {
                        leftRoomId = CurrentRoomId.Value;
                        CurrentRoomId = null;
                        return JsonSerializer.Serialize(new { success = true });
                    }
                    leftRoomId = r.Id;
                    bool isOwner = string.Equals(r.OwnerUsername, username, StringComparison.OrdinalIgnoreCase);
                    bool isGuest = r.GuestUserId.HasValue && string.Equals(r.GuestUsername, username, StringComparison.OrdinalIgnoreCase);
                    if (isOwner)
                    {
                        if (r.GuestUserId.HasValue)
                        {
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
                BroadcastRooms();
                if (leftRoomId.HasValue)
                {
                    TCPServer.Log($"[{clientIP}] ROOM_LEAVE user={username}, roomId={leftRoomId.Value}, removed={removed}, ownerChanged={ownerChanged}");
                }
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
                    TCPServer.Log($"[{clientIP}] ROOM_RENAME by {username}, roomId={r.Id}, newName={newName}");
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
                    TCPServer.Log($"[{clientIP}] ROOM_READY user={username}, roomId={r.Id}, ready={ready}");
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
                    r.GuestReady = false;
                    r.IsPlaying = true;
                    string ev = JsonSerializer.Serialize(new
                    {
                        type = "ROOM_EVENT",
                        roomId = r.Id,
                        @event = "STARTED",
                        room = RoomDto(r)
                    });
                    BroadcastToRoom(r.Id, ev);
                    TCPServer.Log($"[{clientIP}] ROOM_START by {username}, roomId={r.Id}");
                }
                BroadcastRooms();
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
                    TCPServer.Log($"[{clientIP}] ROOM_UPDATE_CONFIG by {username}, roomId={r.Id}, minutes={minutes}, inc={increment}, side={side}");
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
            private void HandleGameMove(JsonElement req)
            {
                string username = req.GetProperty("username").GetString();
                string from = req.GetProperty("from").GetString();
                string to = req.GetProperty("to").GetString();
                string promo = null;
                if (req.TryGetProperty("promotion", out var jp) && jp.ValueKind == JsonValueKind.String)
                    promo = jp.GetString();
                string timestamp = DateTime.Now.ToString("HH:mm:ss");
                if (req.TryGetProperty("roomId", out var jr) && jr.ValueKind == JsonValueKind.Number)
                {
                    int roomId = jr.GetInt32();
                    string msgJson = JsonSerializer.Serialize(new
                    {
                        type = "GAME_EVENT",
                        roomId = roomId,
                        @event = "MOVE",
                        username = username,
                        from = from,
                        to = to,
                        promotion = promo,
                        timestamp = timestamp
                    });
                    BroadcastToRoom(roomId, msgJson);
                    return;
                }
                if (req.TryGetProperty("gameId", out var jg) && jg.ValueKind == JsonValueKind.Number)
                {
                    int gameId = jg.GetInt32();
                    string msgJson = JsonSerializer.Serialize(new
                    {
                        type = "GAME_EVENT",
                        gameId = gameId,
                        @event = "MOVE",
                        username = username,
                        from = from,
                        to = to,
                        promotion = promo,
                        timestamp = timestamp
                    });
                    BroadcastToQuickGame(gameId, msgJson);
                    return;
                }
            }
            private void HandleGameChat(JsonElement req)
            {
                string username = null;
                if (req.TryGetProperty("username", out var ju) && ju.ValueKind == JsonValueKind.String)
                    username = ju.GetString();
                string sender = null;
                if (req.TryGetProperty("sender", out var js) && js.ValueKind == JsonValueKind.String)
                    sender = js.GetString();
                else
                    sender = username ?? "Người chơi";
                string content = "";
                if (req.TryGetProperty("content", out var jc) && jc.ValueKind == JsonValueKind.String)
                    content = jc.GetString();
                else if (req.TryGetProperty("message", out var jm) && jm.ValueKind == JsonValueKind.String)
                    content = jm.GetString();
                string kind = "text";
                if (req.TryGetProperty("kind", out var jk) && jk.ValueKind == JsonValueKind.String)
                    kind = jk.GetString();
                string timestamp = DateTime.Now.ToString("HH:mm:ss");
                if (req.TryGetProperty("roomId", out var jr) && jr.ValueKind == JsonValueKind.Number)
                {
                    int roomId = jr.GetInt32();
                    string msgJson = JsonSerializer.Serialize(new
                    {
                        type = "GAMECHAT",
                        roomId = roomId,
                        sender = sender,
                        username = username,
                        content = content,
                        kind = kind,
                        timestamp = timestamp
                    });
                    BroadcastToRoom(roomId, msgJson);
                    return;
                }
                if (req.TryGetProperty("gameId", out var jg) && jg.ValueKind == JsonValueKind.Number)
                {
                    int gameId = jg.GetInt32();
                    string msgJson = JsonSerializer.Serialize(new
                    {
                        type = "GAMECHAT",
                        gameId = gameId,
                        sender = sender,
                        username = username,
                        content = content,
                        kind = kind,
                        timestamp = timestamp
                    });
                    BroadcastToQuickGame(gameId, msgJson);
                    return;
                }
            }
            private void HandleGameResign(JsonElement req)
            {
                string username = req.GetProperty("username").GetString();
                string timestamp = DateTime.Now.ToString("HH:mm:ss");
                string reason = "resign";
                if (req.TryGetProperty("reason", out var jrReason) && jrReason.ValueKind == JsonValueKind.String)
                {
                    reason = jrReason.GetString();
                }
                if (req.TryGetProperty("roomId", out var jr) && jr.ValueKind == JsonValueKind.Number)
                {
                    int roomId = jr.GetInt32();
                    string msgJson = JsonSerializer.Serialize(new
                    {
                        type = "GAME_EVENT",
                        roomId = roomId,
                        @event = "RESIGN",
                        username = username,
                        reason = reason,
                        timestamp = timestamp
                    });
                    BroadcastToRoom(roomId, msgJson);
                    TCPServer.Log($"[{clientIP}] GAME_RESIGN user={username}, roomId={roomId}, reason={reason}");
                    return;
                }
                if (req.TryGetProperty("gameId", out var jg) && jg.ValueKind == JsonValueKind.Number)
                {
                    int gameId = jg.GetInt32();
                    string msgJson = JsonSerializer.Serialize(new
                    {
                        type = "GAME_EVENT",
                        gameId = gameId,
                        @event = "RESIGN",
                        username = username,
                        reason = reason,
                        timestamp = timestamp
                    });
                    BroadcastToQuickGame(gameId, msgJson);
                    TCPServer.Log($"[{clientIP}] GAME_RESIGN user={username}, gameId={gameId}, reason={reason}");
                    return;
                }
            }
            private void HandleGameDrawOffer(JsonElement req)
            {
                string username = req.GetProperty("username").GetString();
                string timestamp = DateTime.Now.ToString("HH:mm:ss");
                if (req.TryGetProperty("roomId", out var jr) && jr.ValueKind == JsonValueKind.Number)
                {
                    int roomId = jr.GetInt32();
                    string msgJson = JsonSerializer.Serialize(new
                    {
                        type = "GAME_EVENT",
                        roomId = roomId,
                        @event = "DRAW_OFFER",
                        username = username,
                        timestamp = timestamp
                    });
                    BroadcastToRoom(roomId, msgJson);
                    TCPServer.Log($"[{clientIP}] GAME_DRAW_OFFER user={username}, roomId={roomId}");
                    return;
                }
                if (req.TryGetProperty("gameId", out var jg) && jg.ValueKind == JsonValueKind.Number)
                {
                    int gameId = jg.GetInt32();
                    string msgJson = JsonSerializer.Serialize(new
                    {
                        type = "GAME_EVENT",
                        gameId = gameId,
                        @event = "DRAW_OFFER",
                        username = username,
                        timestamp = timestamp
                    });
                    BroadcastToQuickGame(gameId, msgJson);
                    TCPServer.Log($"[{clientIP}] GAME_DRAW_OFFER user={username}, gameId={gameId}");
                    return;
                }
            }
            private void HandleGameDrawResponse(JsonElement req)
            {
                string username = req.GetProperty("username").GetString();
                bool accepted = req.GetProperty("accepted").GetBoolean();
                string timestamp = DateTime.Now.ToString("HH:mm:ss");
                if (req.TryGetProperty("roomId", out var jr) && jr.ValueKind == JsonValueKind.Number)
                {
                    int roomId = jr.GetInt32();
                    string msgJson = JsonSerializer.Serialize(new
                    {
                        type = "GAME_EVENT",
                        roomId = roomId,
                        @event = "DRAW_RESPONSE",
                        username = username,
                        accepted = accepted,
                        timestamp = timestamp
                    });
                    BroadcastToRoom(roomId, msgJson);
                    TCPServer.Log($"[{clientIP}] GAME_DRAW_RESPONSE user={username}, roomId={roomId}, accepted={accepted}");
                    return;
                }
                if (req.TryGetProperty("gameId", out var jg) && jg.ValueKind == JsonValueKind.Number)
                {
                    int gameId = jg.GetInt32();
                    string msgJson = JsonSerializer.Serialize(new
                    {
                        type = "GAME_EVENT",
                        gameId = gameId,
                        @event = "DRAW_RESPONSE",
                        username = username,
                        accepted = accepted,
                        timestamp = timestamp
                    });
                    BroadcastToQuickGame(gameId, msgJson);
                    TCPServer.Log($"[{clientIP}] GAME_DRAW_RESPONSE user={username}, gameId={gameId}, accepted={accepted}");
                    return;
                }
            }
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
            private string HandleGameResult(JsonElement req)
            {
                string result = req.GetProperty("result").GetString();
                string reason = null;
                if (req.TryGetProperty("reason", out var jr) && jr.ValueKind == JsonValueKind.String)
                {
                    reason = jr.GetString();
                }
                int whiteUserId = 0;
                int blackUserId = 0;
                int timeControlMinutes = 0;
                int incrementSeconds = 0;
                if (req.TryGetProperty("gameId", out var jg) && jg.ValueKind == JsonValueKind.Number)
                {
                    int gameId = jg.GetInt32();
                    lock (quickLock)
                    {
                        if (quickGames.TryGetValue(gameId, out var g))
                        {
                            whiteUserId = g.WhiteUserId;
                            blackUserId = g.BlackUserId;
                            timeControlMinutes = g.Minutes;
                            incrementSeconds = g.Increment;
                            quickGames.Remove(gameId);
                        }
                    }
                }
                else if (req.TryGetProperty("roomId", out var jrRoom) && jrRoom.ValueKind == JsonValueKind.Number)
                {
                    int roomId = jrRoom.GetInt32();
                    string side;
                    lock (roomsLock)
                    {
                        if (rooms.TryGetValue(roomId, out var r))
                        {
                            side = r.Side ?? "white";
                            bool hostIsWhite = string.Equals(side, "white", StringComparison.OrdinalIgnoreCase);
                            if (hostIsWhite)
                            {
                                whiteUserId = r.OwnerUserId;
                                blackUserId = r.GuestUserId ?? 0;
                            }
                            else
                            {
                                blackUserId = r.OwnerUserId;
                                whiteUserId = r.GuestUserId ?? 0;
                            }
                            timeControlMinutes = r.Minutes;
                            incrementSeconds = r.Increment;
                        }
                    }
                }
                if (whiteUserId == 0 || blackUserId == 0)
                {
                    return CreateResponse(false, "Không xác định được người chơi trắng/đen.");
                }
                try
                {
                    UserRepo repo = new UserRepo();
                    repo.SaveMatchAndUpdateStats(whiteUserId, blackUserId, result, reason, timeControlMinutes, incrementSeconds);
                    TCPServer.Log($"[{clientIP}] GAME_RESULT saved: whiteUserId={whiteUserId}, blackUserId={blackUserId}, result={result}, reason={reason}");
                    if (req.TryGetProperty("roomId", out var jrRoom2) && jrRoom2.ValueKind == JsonValueKind.Number)
                    {
                        int roomId2 = jrRoom2.GetInt32();
                        ClassUser wUser = repo.GetUserById(whiteUserId);
                        ClassUser bUser = repo.GetUserById(blackUserId);
                        Room roomSnapshot = null;
                        lock (roomsLock)
                        {
                            if (rooms.TryGetValue(roomId2, out var r2))
                            {
                                string sideLocal = r2.Side ?? "white";
                                bool hostIsWhite = string.Equals(sideLocal, "white", StringComparison.OrdinalIgnoreCase);
                                if (hostIsWhite)
                                {
                                    if (wUser != null) r2.OwnerElo = wUser.Elo;
                                    if (bUser != null) r2.GuestElo = bUser.Elo;
                                }
                                else
                                {
                                    if (bUser != null) r2.OwnerElo = bUser.Elo;
                                    if (wUser != null) r2.GuestElo = wUser.Elo;
                                }
                                r2.IsPlaying = false;
                                r2.GuestReady = false;
                                roomSnapshot = r2;
                            }
                        }
                        if (roomSnapshot != null)
                        {
                            string ev = JsonSerializer.Serialize(new
                            {
                                type = "ROOM_EVENT",
                                roomId = roomSnapshot.Id,
                                @event = "RESULT_APPLIED",
                                room = RoomDto(roomSnapshot)
                            });
                            BroadcastToRoom(roomSnapshot.Id, ev);
                            BroadcastRooms();
                        }
                    }
                    return JsonSerializer.Serialize(new { success = true });
                }
                catch (Exception ex)
                {
                    TCPServer.Log($"[{clientIP}] Lỗi GAME_RESULT: {ex}");
                    return CreateResponse(false, "Lỗi lưu kết quả trận đấu.");
                }
            }
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
                            increment = r.Increment,
                            status = r.IsPlaying ? "playing" : "lobby"
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
                        if (!c.isPushChannel) continue;
                        try { c.SendMessage(json); }
                        catch { }
                    }
                }
            }
            private static void BroadcastToQuickGame(int gameId, string json)
            {
                QuickGame game = null;
                lock (quickLock)
                {
                    quickGames.TryGetValue(gameId, out game);
                }
                if (game == null) return;
                lock (connectedClients)
                {
                    foreach (var c in connectedClients)
                    {
                        if (!c.isPushChannel) continue;
                        if (c.loggedInUser == null) continue;
                        string u = c.loggedInUser.Username;
                        if (string.Equals(u, game.WhiteUsername, StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(u, game.BlackUsername, StringComparison.OrdinalIgnoreCase))
                        {
                            try { c.SendMessage(json); }
                            catch { }
                        }
                    }
                }
            }
            private static void BroadcastToRoom(int roomId, string json)
            {
                lock (connectedClients)
                {
                    foreach (var c in connectedClients)
                    {
                        if (!c.isPushChannel) continue;
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