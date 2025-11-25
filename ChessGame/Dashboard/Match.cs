using System;
using System.Collections.Generic;
using System.Media;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;
using WinFormsTimer = System.Windows.Forms.Timer;

namespace ChessGame
{
    public partial class Match : Form
    {
        // Inject từ Dashboard
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public int Elo { get; set; }

        private TCPClient requestClient;
        private TCPClient pushClient;
        private WinFormsTimer uiTimer;
        private readonly StringBuilder pushBuffer = new StringBuilder();

        private int currentGameId = 0;
        private GameBoard currentGameBoard;

        private bool isSearching = false;
        private bool gameStarted = false;
        private string opponentUsername;
        private string opponentDisplayName;
        private int opponentElo;
        private WinFormsTimer matchStartTimer;
        private int matchStartCountdown;
        private bool pendingLocalIsWhite;
        private int pendingMinutes;
        private int pendingIncrement;
        private SoundPlayer _matchFoundSound;


        public Match()
        {
            InitializeComponent();
            LoadMatchFoundSound();

            Load += Match_Load;
            FormClosed += Match_FormClosed;

            btnStartMatch.Click += BtnStartMatch_Click;
            btnCancelSearch.Click += BtnCancelSearch_Click;
        }

        private void Match_Load(object sender, EventArgs e)
        {
            lblActiveUsers.Text = "";
            lblOnlineCount.Text = "Nhấn \"Chơi Ngay\" để tìm đối thủ";
            pnlSearching.Visible = false;
            pnlMatchFound.Visible = false;
            btnCancelSearch.Visible = false;    // <- thêm
            btnStartMatch.Visible = true;       // <- cho chắc

            // Kết nối server
            requestClient = new TCPClient();
            pushClient = new TCPClient();
            TryConnectSilently(requestClient);
            TryConnectSilently(pushClient);

            try
            {
                requestClient.SendRequest(new { action = "AUTH_ATTACH", username = this.Username, mode = "req" });
                pushClient.SendRequest(new { action = "AUTH_ATTACH", username = this.Username, mode = "push" });
            }
            catch
            {
                MessageBox.Show("Không kết nối được server.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            uiTimer = new WinFormsTimer();
            uiTimer.Interval = 50;
            uiTimer.Tick += UiTimer_Tick;
            uiTimer.Start();
        }

        private void Match_FormClosed(object sender, FormClosedEventArgs e)
        {
            try { uiTimer?.Stop(); } catch { }
            try { matchStartTimer?.Stop(); } catch { }
            try { pushClient?.Disconnect(); } catch { }
            try { requestClient?.Disconnect(); } catch { }
        }

        private void TryConnectSilently(TCPClient c)
        {
            try { c.Connect(); } catch { }
        }

        // ============ UI EVENTS ============

        private void BtnStartMatch_Click(object sender, EventArgs e)
        {
            if (isSearching || gameStarted) return;
            isSearching = true;

            pnlSearching.Visible = true;
            pnlMatchFound.Visible = false;
            btnStartMatch.Enabled = false;
            btnStartMatch.Visible = false;      // <- thêm
            btnCancelSearch.Visible = true;     // <- để người chơi thấy nút hủy
            lblSearching.Text = "Đang tìm đối thủ phù hợp...";
            progressBar.Style = ProgressBarStyle.Marquee;

            try
            {
                string resJson = requestClient.SendRequest(new { action = "MATCH_FIND" });
                if (!string.IsNullOrWhiteSpace(resJson))
                {
                    using var doc = JsonDocument.Parse(resJson);
                    var root = doc.RootElement;
                    bool success = root.TryGetProperty("success", out var js) && js.GetBoolean();
                    if (!success)
                    {
                        string msg = root.TryGetProperty("message", out var jm) ? jm.GetString() : "Không thể tìm trận.";
                        MessageBox.Show(msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ResetSearchUI();
                    }
                    else
                    {
                        bool waiting = root.TryGetProperty("waiting", out var jw) && jw.ValueKind == JsonValueKind.True;
                        if (waiting)
                        {
                            lblSearching.Text = "Đã vào hàng chờ, đợi người chơi khác...";
                        }
                        else
                        {
                            // Có thể đã ghép ngay lập tức, nhưng push MATCH_FOUND sẽ tới sau -> cứ để timer xử lý
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Lỗi gửi yêu cầu MATCH_FIND.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ResetSearchUI();
            }
        }

        private void BtnCancelSearch_Click(object sender, EventArgs e)
        {
            if (!isSearching || gameStarted) return;

            try
            {
                requestClient.SendRequest(new { action = "MATCH_CANCEL" });
            }
            catch { }

            ResetSearchUI();
        }

        private void ResetSearchUI()
        {
            isSearching = false;
            pnlSearching.Visible = false;
            pnlMatchFound.Visible = false;
            btnStartMatch.Enabled = true;
            btnStartMatch.Visible = true;       // <- hiện lại
            btnCancelSearch.Visible = false;    // <- ẩn nút hủy
        }

        // ============ PUSH HANDLING ============

        private void UiTimer_Tick(object sender, EventArgs e)
        {
            var stream = pushClient.GetStream();
            if (stream == null) return;

            try
            {
                while (stream.DataAvailable)
                {
                    byte[] buf = new byte[4096];
                    int n = stream.Read(buf, 0, buf.Length);
                    if (n <= 0) break;
                    pushBuffer.Append(Encoding.UTF8.GetString(buf, 0, n));
                }

                foreach (var json in ExtractJsonObjects(pushBuffer))
                {
                    try
                    {
                        using var doc = JsonDocument.Parse(json);
                        var root = doc.RootElement;
                        if (!root.TryGetProperty("type", out var jt)) continue;
                        var type = jt.GetString();

                        if (type == "MATCH_FOUND")
                        {
                            HandleMatchFound(root);
                        }
                        else if (type == "GAME_EVENT")
                        {
                            HandleGameEvent(root);
                        }
                        else if (type == "GAMECHAT")
                        {
                            HandleGameChat(root);
                        }
                    }
                    catch
                    {
                        // ignore 1 payload lỗi
                    }
                }
            }
            catch
            {
                // ignore lỗi read
            }
        }

        private static IEnumerable<string> ExtractJsonObjects(StringBuilder sb)
        {
            var list = new List<string>();
            int depth = 0, start = -1;
            for (int i = 0; i < sb.Length; i++)
            {
                char c = sb[i];
                if (c == '{')
                {
                    if (depth == 0) start = i;
                    depth++;
                }
                else if (c == '}')
                {
                    depth--;
                    if (depth == 0 && start != -1)
                    {
                        list.Add(sb.ToString(start, i - start + 1));
                        start = -1;
                    }
                }
            }
            if (list.Count > 0)
            {
                var last = list[list.Count - 1];
                int idx = sb.ToString().LastIndexOf(last, StringComparison.Ordinal);
                if (idx >= 0) sb.Remove(0, idx + last.Length);
            }
            return list;
        }

        private void MatchStartTimer_Tick(object sender, EventArgs e)
        {
            matchStartCountdown--;

            if (matchStartCountdown > 0)
            {
                // Update text: "Trận đấu sẽ bắt đầu sau N giây"
                lblCountdown.Text = $"Trận đấu sẽ bắt đầu sau {matchStartCountdown} giây";
            }
            else
            {
                matchStartTimer.Stop();
                lblCountdown.Text = "Trận đấu đang bắt đầu...";

                // Hết đếm ngược thì vào game
                OpenGameBoard(pendingLocalIsWhite, pendingMinutes, pendingIncrement);
            }
        }


        // ============ XỬ LÝ MATCH_FOUND ============

        private void HandleMatchFound(JsonElement root)
        {
            isSearching = false;
            gameStarted = true;

            PlayMatchFoundSound();

            currentGameId = root.GetProperty("gameId").GetInt32();
            string whiteUsername = root.GetProperty("whiteUsername").GetString();
            string whiteDisplay = root.GetProperty("whiteDisplayName").GetString();
            string blackUsername = root.GetProperty("blackUsername").GetString();
            string blackDisplay = root.GetProperty("blackDisplayName").GetString();

            int minutes = root.GetProperty("minutes").GetInt32();
            int increment = root.GetProperty("increment").GetInt32();

            bool localIsWhite = string.Equals(whiteUsername, this.Username, StringComparison.OrdinalIgnoreCase);
            opponentUsername = localIsWhite ? blackUsername : whiteUsername;
            opponentDisplayName = localIsWhite ? blackDisplay : whiteDisplay;
            opponentElo = 0;

            // Lưu cấu hình ván để dùng sau khi đếm ngược xong
            pendingLocalIsWhite = localIsWhite;
            pendingMinutes = minutes;
            pendingIncrement = increment;

            // UI thông báo ghép trận thành công
            pnlSearching.Visible = false;
            pnlMatchFound.Visible = true;
            lblMatchFoundTitle.Text = "Đã tìm thấy đối thủ!";
            lblOpponentName.Text = opponentDisplayName;
            lblOpponentRating.Text = $"@{opponentUsername}";

            // Bắt đầu đếm ngược 5 giây
            matchStartCountdown = 5;
            lblCountdown.Text = $"Trận đấu sẽ bắt đầu sau {matchStartCountdown} giây";

            if (matchStartTimer == null)
            {
                matchStartTimer = new WinFormsTimer();
                matchStartTimer.Interval = 1000; // 1 giây
                matchStartTimer.Tick += MatchStartTimer_Tick;
            }

            matchStartTimer.Start();
        }



        private void OpenGameBoard(bool localIsWhite, int minutes, int increment)
        {
            if (currentGameBoard != null)
            {
                try { currentGameBoard.Close(); } catch { }
                currentGameBoard = null;
            }

            currentGameBoard = new GameBoard();
            currentGameBoard.Text = "Chơi Ngay";

            currentGameBoard.InitGameSession(
                localUsername: this.Username,
                localDisplayName: this.DisplayName,
                opponentUsername: opponentUsername,
                opponentDisplayName: opponentDisplayName,
                localIsWhite: localIsWhite,
                minutes: minutes,
                incrementSeconds: increment,
                roomId: null // quick match không dùng roomId
            );

            // Gửi nước đi local -> server
            currentGameBoard.LocalMovePlayed += (from, to, promo) =>
            {
                try
                {
                    requestClient.Send(new
                    {
                        action = "GAME_MOVE",
                        gameId = currentGameId,
                        username = this.Username,
                        from = from,
                        to = to,
                        promotion = promo
                    });
                }
                catch { }
            };

            // Chat trong ván
            currentGameBoard.LocalChatSent += (text) =>
            {
                try
                {
                    requestClient.Send(new
                    {
                        action = "GAME_CHAT",
                        gameId = currentGameId,
                        username = this.Username,
                        message = text
                    });
                }
                catch { }
            };

            // Đầu hàng / thoát ván
            currentGameBoard.LocalResignRequested += () =>
            {
                try
                {
                    string reason = currentGameBoard.LastResignWasDisconnect ? "disconnect" : "resign";

                    requestClient.Send(new
                    {
                        action = "GAME_RESIGN",
                        gameId = currentGameId,
                        username = this.Username,
                        reason = reason
                    });
                }
                catch { }
            };

            // Cầu hòa
            currentGameBoard.LocalOfferDrawRequested += () =>
            {
                try
                {
                    requestClient.Send(new
                    {
                        action = "GAME_DRAW_OFFER",
                        gameId = currentGameId,
                        username = this.Username
                    });
                }
                catch { }
            };

            // Kết thúc ván -> gửi GAME_RESULT
            currentGameBoard.LocalGameEnded += (result, reason) =>
            {
                bool isDisconnect = string.Equals(reason, "disconnect", StringComparison.OrdinalIgnoreCase);

                // Với các lý do bình thường (chiếu hết, hết giờ, hòa, đầu hàng...)
                // chỉ player có username nhỏ hơn mới gửi GAME_RESULT
                // để tránh cộng Elo 2 lần.
                if (!isDisconnect)
                {
                    if (string.Compare(this.Username, opponentUsername, StringComparison.OrdinalIgnoreCase) > 0)
                        return;
                }

                try
                {
                    requestClient.SendRequest(new
                    {
                        action = "GAME_RESULT",
                        gameId = currentGameId,
                        result = result,   // "white" / "black" / "draw"
                        reason = reason    // "checkmate" / "time" / "disconnect" ...
                    });
                }
                catch { }
            };


            this.Hide();
            currentGameBoard.ShowDialog(this);

            // Khi GameBoard đóng (sau khi người dùng bấm OK ở thông báo kết thúc trận):
            // reset trạng thái để có thể bấm "Chơi ngay" tiếp
            gameStarted = false;
            currentGameId = 0;
            currentGameBoard = null;
            ResetSearchUI();   // bật lại nút Start, ẩn panel tìm trận / match found

            this.Show();
        }

        // ============ XỬ LÝ GAME_EVENT / GAMECHAT (nhận từ server) ============

        private void HandleGameEvent(JsonElement root)
        {
            if (!root.TryGetProperty("gameId", out var jg) || jg.GetInt32() != currentGameId)
                return;

            if (currentGameBoard == null) return;

            string ev = root.GetProperty("event").GetString();
            string username = root.GetProperty("username").GetString();

            // true nếu event đến từ đối thủ
            bool fromOpponent = !string.Equals(username, this.Username, StringComparison.OrdinalIgnoreCase);

            if (ev == "MOVE" && fromOpponent)
            {
                string from = root.GetProperty("from").GetString();
                string to = root.GetProperty("to").GetString();
                string promo = root.TryGetProperty("promotion", out var jp) && jp.ValueKind == JsonValueKind.String
                    ? jp.GetString()
                    : null;

                currentGameBoard.ApplyNetworkMove(from, to, promo);
            }

            else if (ev == "RESIGN")
            {
                // Nếu fromOpponent == false => chính mình, true => đối thủ
                bool localResigned = !fromOpponent;

                string reason = "resign";
                if (root.TryGetProperty("reason", out var jr) && jr.ValueKind == JsonValueKind.String)
                    reason = jr.GetString();

                if (!localResigned && string.Equals(reason, "disconnect", StringComparison.OrdinalIgnoreCase))
                {
                    // Đối thủ thoát / disconnect -> mình thắng do đối thủ thoát
                    currentGameBoard.NotifyOpponentDisconnectedWin();
                }
                else
                {
                    currentGameBoard.FinishGameByResign(localResigned);
                }
            }

            else if (ev == "DRAW_OFFER")
            {
                // Nếu mình là người gửi lời đề nghị hòa thì không cần xử lý push nữa
                if (!fromOpponent) return;

                var r = MessageBox.Show(
                    currentGameBoard,
                    "Đối thủ đề nghị hòa ván đấu này.\nBạn có đồng ý hòa không?",
                    "Lời đề nghị hòa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                bool accepted = (r == DialogResult.Yes);

                try
                {
                    requestClient.Send(new
                    {
                        action = "GAME_DRAW_RESPONSE",
                        gameId = currentGameId,
                        username = this.Username,
                        accepted = accepted
                    });
                }
                catch
                {
                }

                if (accepted)
                {
                    currentGameBoard.FinishGameByAgreedDraw();
                }
                else
                {
                    MessageBox.Show(
                        currentGameBoard,
                        "Bạn đã từ chối lời đề nghị hòa.",
                        "Cầu hòa",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            else if (ev == "DRAW_RESPONSE")
            {
                bool accepted = root.TryGetProperty("accepted", out var ja) &&
                               ja.ValueKind == JsonValueKind.True;

                // Nếu username == mình thì đây là người trả lời (Yes/No) -> đã xử lý ở nhánh DRAW_OFFER rồi
                if (string.Equals(username, this.Username, StringComparison.OrdinalIgnoreCase))
                    return;

                if (accepted)
                {
                    // Đối thủ đã chấp nhận lời đề nghị hòa của mình
                    currentGameBoard.FinishGameByAgreedDraw();
                }
                else
                {
                    MessageBox.Show(
                        currentGameBoard,
                        "Đối thủ đã từ chối lời đề nghị hòa của bạn.",
                        "Cầu hòa",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
        }

        private void HandleGameChat(JsonElement root)
        {
            if (!root.TryGetProperty("gameId", out var jg) || jg.GetInt32() != currentGameId)
                return;

            if (currentGameBoard == null) return;

            // Lấy username (để xác định Trắng/Đen)
            string username = null;
            if (root.TryGetProperty("username", out var ju) && ju.ValueKind == JsonValueKind.String)
                username = ju.GetString();

            // Nếu không có, fallback sang sender/from
            if (string.IsNullOrEmpty(username))
            {
                if (root.TryGetProperty("sender", out var js) && js.ValueKind == JsonValueKind.String)
                    username = js.GetString();
                else if (root.TryGetProperty("from", out var jf) && jf.ValueKind == JsonValueKind.String)
                    username = jf.GetString();
                else
                    username = "Người chơi";
            }

            // Nội dung tin nhắn
            string content;
            if (root.TryGetProperty("content", out var jc) && jc.ValueKind == JsonValueKind.String)
                content = jc.GetString();
            else if (root.TryGetProperty("message", out var jm) && jm.ValueKind == JsonValueKind.String)
                content = jm.GetString();
            else
                content = "";

            string time = DateTime.Now.ToString("HH:mm:ss");
            if (root.TryGetProperty("timestamp", out var jt) && jt.ValueKind == JsonValueKind.String)
                time = jt.GetString();

            bool isLocal = string.Equals(username, this.Username, StringComparison.OrdinalIgnoreCase);

            currentGameBoard.AppendNetworkChat(username, content, time, isLocal);
        }

        private void LoadMatchFoundSound()
        {
            try
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string soundsDir = Path.Combine(baseDir, "sounds");
                string path = Path.Combine(soundsDir, "MatchFound.wav");

                if (!File.Exists(path))
                {
                    return;
                }

                _matchFoundSound = new SoundPlayer(path);
                _matchFoundSound.Load();
            }
            catch
            {
                _matchFoundSound = null;
            }
        }

        private void PlayMatchFoundSound()
        {
            try
            {
                _matchFoundSound?.Play();
            }
            catch
            {
                // nuốt lỗi, không cho crash form chỉ vì âm thanh
            }
        }
    }
}
