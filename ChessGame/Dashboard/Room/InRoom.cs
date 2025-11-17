using System;
using System.Drawing;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;
using System.Collections.Generic;
using WinFormsTimer = System.Windows.Forms.Timer;

namespace ChessGame
{
    public partial class InRoom : Form
    {
        // ======= Inject từ RoomList =======
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public int Elo { get; set; }

        // ======= Room context =======
        private readonly int roomId;
        private string roomName;
        private bool isHost;

        // ======= Networking =======
        private TCPClient requestClient;
        private TCPClient pushClient;
        private WinFormsTimer uiTimer;
        private readonly StringBuilder pushBuffer = new StringBuilder();

        // ======= In-room state =======
        private int minutes = 3;
        private int increment = 0;
        private string hostSide = "white"; // white/black (bên của chủ phòng)
        private bool guestReady = false;

        // Badge "ĐÃ SẴN SÀNG"
        private Label lblGuestReadyBadge;

        // Label hiển thị quân chơi
        private Label lblOwnerSide;
        private Label lblGuestSide;

        // Lưu nội dung chat tự gửi gần đây để chống trùng nếu server cũng echo
        private readonly Queue<string> selfChatRecent = new Queue<string>();
        private const int SelfChatKeep = 20;

        // Emoji list dùng trong phòng
        private readonly string[] emoticons = new[]
        {
// Smileys/Emotion
            "😀","😃","😄","😁","😆","😅","😂","🤣","🥲","😊","😇",
            "🙂","🙃","😉","😌","😍","🥰","😘","😗","😙","😚","😋",
            "😜","😝","😛","🤑","🤗","🤭","🤫","🤔","🤐","😐","😑",
            "😶","😶‍🌫️","🙄","😏","😒","😞","😔","😟","😕","🙁",
            "☹️","😣","😖","😫","😩","🥺","😢","😭","😤","😠","😡",
            "🤬","🤯","😳","🥵","🥶","😱","😨","😰","😥","😓","🤤",
            "😪","😴","😬","😮‍💨","🫠","😵","😵‍💫","🤐","🥴","😷",
            "🤒","🤕","🤢","🤮","🤧","😇","🥳","🥸","😎","🤓","🧐",
            "😕","😟","🙁","☹️","😮","😯","😲","😳","🥺","🥹","😦",
            "😧","😨","😩","😰","😱","😪","😵","🤐","🥴","😷","🤒",
            "🤕","🤢","🤮","🤧","😇","🥳","🥸","😎","🤓","🧐",
            // Gestures/People
            "👋","🤚","🖐️","✋","🖖","👌","🤌","🤏","✌️","🤞",
            "🤟","🤘","🤙","👈","👉","👆","🖕","👇","☝️","👍","👎",
            "✊","👊","🤛","🤜","👏","🙌","🫶","👐","🤲","🙏",
            "💪","🦾","🦵","🦿","🦶","👂","🦻","👃","👣","👀","👁️",
            "🫦","👄","🦷","🦴","👅",
            // Relations/Love
            "💋","👄","💘","💝","💖","💗","💓","💞","💕","💌","💟",
            "❣️","💔","❤️","🧡","💛","💚","💙","💜","🤎","🖤","🤍",
            // Animals/Nature
            "🐶","🐱","🐭","🐹","🐰","🦊","🐻","🐼","🐨","🐯","🦁",
            "🐮","🐷","🐸","🐵","🐔","🐧","🐦","🐤","🐣","🐥","🦆",
            "🦅","🦉","🦇","🐺","🐗","🐴","🦄","🐝","🐛","🦋","🐌",
            "🐞","🐜","🦟","🦗","🕷️","🦂","🐢","🐍","🦎","🦖","🦕",
            "🐙","🦑","🦐","🦞","🦀","🐠","🐟","🐡","🐬","🐳","🐋",
            "🦈","🐊","🐅","🐆","🦓","🦍","🦧","🐘","🦣","🦛","🦏",
            "🐪","🐫","🦒","🦘","🦥","🦦","🦨","🦡","🐁","🐀","🐇",
            "🦔",
            // Food/Drinks
            "🍏","🍎","🍐","🍊","🍋","🍌","🍉","🍇","🍓","🫐",
            "🍈","🍒","🍑","🥭","🍍","🥥","🥝","🍅","🍆","🥑","🥦",
            "🥬","🥒","🌶️","🫑","🌽","🥕","🧄","🧅","🥔","🍠",
            "🥐","🥯","🍞","🥖","🥨","🧀","🥚","🍳","🥞","🧇",
            "🥓","🥩","🍗","🍖","🦴","🌭","🍔","🍟","🍕","🫓",
            "🥪","🥙","🧆","🌮","🌯","🫔","🥗","🥘","🫕","🥫",
            "🍝","🍜","🍲","🍛","🍣","🍱","🥟","🦪","🍤","🍙",
            "🍚","🍘","🍥","🥠","🥮","🍢","🍡","🍧","🍨","🍦",
            "🥧","🧁","🍰","🎂","🍮","🍭","🍬","🍫","🍿","🧃",
            "🥤","🧋","🫖","☕","🍵","🧉","🍶","🍺","🍻","🥂",
            "🍷","🥃","🍸","🍹","🍾",
            // Activities/Objects
            "⚽","🏀","🏈","⚾","🥎","🎾","🏐","🏉","🥏","🎱",
            "🏓","🏸","🥅","🏒","🏑","🥍","🏏","🪃","🏹","🎣",
            "🤿","🥊","🥋","🎽","🛹","🛷","⛸️","🥌","🥇","🥈",
            "🥉","🏆","🏅","🎖️","🥫","🏵️","🎗️","🎫","🎟️",
            "🎪","🤹‍♂️","🤹‍♀️","🎭","🩰","🎨","🎬","🎤","🎧","🎼",
            "🎹","🥁","🎷","🎺","🎸","🪕",
            // Travel/Places
            "🚗","🚕","🚙","🚌","🚎","🏎️","🚓","🚑","🚒","🚐",
            "🚚","🚛","🚜","🛵","🏍️","🚲","🛴","🚏","🛣️","🛤️",
            "🗺️","🗿","🗽","🗼","🏰","🏯","🏟️","🎡","🎢","🎠",
            "⛲","⛱️","🏖️","🏝️","🛶","⛵","🚤","🛥️","🛳️","⛴️",
            "🚀","🛸","✈️","🛫","🛬",
            // Symbols/Flags
            "🏁","🚩","🎌","🏴","🏳️","🏳️‍🌈","🏳️‍⚧️","🏴‍☠️","🇦🇺",
            "🇨🇦","🇫🇷","🇩🇪","🇨🇳","🇯🇵","🇰🇷","🇷🇺","🇬🇧","🇺🇸"
        };

        // (tuỳ) alias cho nút chính nếu code cũ từng gọi btnPrimary
        private Button btnPrimary => btnStartGame;

        public InRoom(int roomId, string roomName, bool isHost)
        {
            InitializeComponent();

            this.roomId = roomId;
            this.roomName = roomName;
            this.isHost = isHost;

            // Tạo badge sớm để mọi ShowGuestReady() không bị null
            lblGuestReadyBadge = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.Goldenrod,
                Text = "",
                Location = new Point(90, 60)
            };
            pnlPlayerB.Controls.Add(lblGuestReadyBadge);

            // Tạo label "Quân: ..." cho chủ phòng & khách
            InitSideLabels();
            UpdateSideLabels(hostSide);

            // Cấu hình các label trong 2 panel căn giữa + wrap
            ConfigureAllCenteredLabels();

            // Emoji panel
            SetupEmojiPickerPanel();

            // Wire events
            Load += InRoom_Load;
            FormClosed += InRoom_FormClosed;

            btnRenameRoom.Click += BtnRenameRoom_Click;
            btnStartGame.Click += BtnStartGame_Click;
            btnLeaveRoom.Click += BtnLeaveRoom_Click;
            btnSendChat.Click += BtnSendChat_Click;
            txtChat.KeyDown += TxtChat_KeyDown;
            btnEmoji.Click += BtnEmoji_Click;

            btn1min.Click += (_, __) => SetMinutes(1);
            btn3min.Click += (_, __) => SetMinutes(3);
            btn6min.Click += (_, __) => SetMinutes(6);
            btn10min.Click += (_, __) => SetMinutes(10);

            txtCustomMin.TextChanged += (_, __) =>
            {
                if (!isHost) return;
                if (int.TryParse((txtCustomMin.Text ?? "").Trim(), out var m) && m > 0 && m <= 180)
                {
                    minutes = m;
                    PushConfigUpdate();
                }
            };

            txtFischer.TextChanged += (_, __) =>
            {
                if (!isHost) return;
                if (int.TryParse((txtFischer.Text ?? "").Trim(), out var inc) && inc >= 0 && inc <= 60)
                {
                    increment = inc;
                    PushConfigUpdate();
                }
            };

            radioWhite.CheckedChanged += (_, __) =>
            {
                if (isHost && radioWhite.Checked)
                {
                    hostSide = "white";
                    PushConfigUpdate();
                }
            };

            radioBlack.CheckedChanged += (_, __) =>
            {
                if (isHost && radioBlack.Checked)
                {
                    hostSide = "black";
                    PushConfigUpdate();
                }
            };
        }

        private void InRoom_Load(object sender, EventArgs e)
        {
            // 1) Kết nối & attach phiên cho 2 kênh
            requestClient = new TCPClient();
            pushClient = new TCPClient();
            TryConnectSilently(requestClient);
            TryConnectSilently(pushClient);

            requestClient.SendRequest(new { action = "AUTH_ATTACH", username = this.Username, mode = "req" });
            pushClient.SendRequest(new { action = "AUTH_ATTACH", username = this.Username, mode = "push" });

            // 2) Bind kênh push vào phòng để nhận ROOM_EVENT/ROOMCHAT
            pushClient.SendRequest(new { action = "ROOM_BIND", roomId = this.roomId, username = this.Username });

            // 3) Lấy snapshot phòng -> đổ UI ngay (khách sẽ không còn "Đang chờ...")
            try
            {
                var snap = requestClient.SendRequest(new { action = "ROOM_GET", roomId = this.roomId });
                using var doc = JsonDocument.Parse(snap);
                if (doc.RootElement.GetProperty("success").GetBoolean())
                {
                    var ro = doc.RootElement.GetProperty("room");
                    UpdatePlayersFromRoom(ro);

                    // Đồng bộ config
                    if (ro.TryGetProperty("minutes", out var jmin)) minutes = jmin.GetInt32();
                    if (ro.TryGetProperty("increment", out var jin)) increment = jin.GetInt32();
                    if (ro.TryGetProperty("side", out var js)) hostSide = js.GetString();

                    txtCustomMin.Text = minutes.ToString();
                    txtFischer.Text = increment.ToString();
                    if (hostSide == "white") radioWhite.Checked = true; else radioBlack.Checked = true;

                    // Cập nhật hiển thị quân cho 2 bên
                    UpdateSideLabels(hostSide);
                }
            }
            catch { /* ignore, push sẽ lấp đầy */ }

            // 4) Start timer đọc push
            uiTimer = new WinFormsTimer();
            uiTimer.Interval = 50;
            uiTimer.Tick += UiTimer_Tick;
            uiTimer.Start();

            // 5) Tiêu đề + quyền theo vai
            lblTitle.Text = $"Phòng: {roomName}";
            txtConfigRoomName.Text = roomName;
            ApplyRoleToUI();
        }

        private void InRoom_FormClosed(object sender, FormClosedEventArgs e)
        {
            try { uiTimer?.Stop(); } catch { }
            try { pushClient?.Disconnect(); } catch { }
            try { requestClient?.Disconnect(); } catch { }
        }

        private void TryConnectSilently(TCPClient c)
        {
            try { c.Connect(); } catch { }
        }

        // ======= Label quân chơi =======
        private void InitSideLabels()
        {
            // Label CHỦ PHÒNG
            lblOwnerSide = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 11F, FontStyle.Regular),
                ForeColor = Color.White,
                Text = "Quân: -",
                Location = new Point(lblPlayerARating.Left, lblPlayerARating.Bottom + 10)
            };
            pnlPlayerA.Controls.Add(lblOwnerSide);

            // Label KHÁCH
            lblGuestSide = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 11F, FontStyle.Regular),
                ForeColor = Color.White,
                Text = "Quân: -",
                Location = new Point(lblPlayerBRating.Left, lblPlayerBRating.Bottom + 10)
            };
            pnlPlayerB.Controls.Add(lblGuestSide);
        }

        // Cấu hình label để căn giữa & wrap trong panel
        private void ConfigureCenterLabel(Label lbl, Panel parent)
        {
            if (lbl == null) return;

            lbl.AutoSize = true;
            lbl.MaximumSize = new Size(parent.ClientSize.Width - 40, 0); // chừa margin 20px mỗi bên
            lbl.TextAlign = ContentAlignment.MiddleCenter;

            int x = (parent.ClientSize.Width - lbl.Width) / 2;
            if (x < 0) x = 0;
            lbl.Left = x;
        }

        private void ConfigureAllCenteredLabels()
        {
            ConfigureCenterLabel(lblPlayerAName, pnlPlayerA);
            ConfigureCenterLabel(lblPlayerARating, pnlPlayerA);
            ConfigureCenterLabel(lblOwnerSide, pnlPlayerA);

            ConfigureCenterLabel(lblPlayerBName, pnlPlayerB);
            ConfigureCenterLabel(lblPlayerBRating, pnlPlayerB);
            ConfigureCenterLabel(lblGuestSide, pnlPlayerB);
            ConfigureCenterLabel(lblGuestReadyBadge, pnlPlayerB);
        }

        private void UpdateSideLabels(string ownerSide)
        {
            if (lblOwnerSide == null || lblOwnerSide.IsDisposed ||
                lblGuestSide == null || lblGuestSide.IsDisposed)
                return;

            bool ownerIsBlack = string.Equals(ownerSide, "black", StringComparison.OrdinalIgnoreCase);
            string ownerColorVi = ownerIsBlack ? "Đen" : "Trắng";
            string guestColorVi = ownerIsBlack ? "Trắng" : "Đen";

            lblOwnerSide.Text = $"Quân: {ownerColorVi}";
            lblGuestSide.Text = $"Quân: {guestColorVi}";

            lblOwnerSide.ForeColor = ownerIsBlack ? Color.Black : Color.White;
            lblGuestSide.ForeColor = ownerIsBlack ? Color.White : Color.Black;

            ConfigureCenterLabel(lblOwnerSide, pnlPlayerA);
            ConfigureCenterLabel(lblGuestSide, pnlPlayerB);
        }

        // ======= Emoji helpers =======
        private void SetupEmojiPickerPanel()
        {
            if (pnlEmojiPicker == null) return;
            pnlEmojiPicker.Visible = false;
            pnlEmojiPicker.Controls.Clear();
            pnlEmojiPicker.AutoScroll = true;
        }

        private void BtnEmoji_Click(object sender, EventArgs e)
        {
            ShowEmojiPicker();
        }

        private void ShowEmojiPicker()
        {
            if (pnlEmojiPicker == null) return;

            // Toggle ẩn/hiện
            if (pnlEmojiPicker.Visible && pnlEmojiPicker.Controls.Count > 0)
            {
                pnlEmojiPicker.Visible = false;
                return;
            }

            pnlEmojiPicker.Controls.Clear();
            pnlEmojiPicker.AutoScroll = true;

            int btnSize = 32;
            int cols = 8;
            int spacing = 4;

            for (int i = 0; i < emoticons.Length; i++)
            {
                var btn = new Button();
                btn.Font = new Font("Segoe UI Emoji", 16F, FontStyle.Regular);
                btn.Text = emoticons[i];
                btn.Width = btn.Height = btnSize;

                int col = i % cols;
                int row = i / cols;

                btn.Left = col * (btnSize + spacing);
                btn.Top = row * (btnSize + spacing);

                btn.BackColor = Color.White;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;

                btn.Click += (s, e) =>
                {
                    txtChat.Text += ((Button)s).Text;
                    txtChat.Focus();
                };

                pnlEmojiPicker.Controls.Add(btn);
            }

            pnlEmojiPicker.Visible = true;
            pnlEmojiPicker.BringToFront();
        }

        // ======= Role/UI =======
        private void ApplyRoleToUI()
        {
            if (isHost)
            {
                btnStartGame.Text = "BẮT ĐẦU CHƠI";
                txtConfigRoomName.Enabled = true;
                btnRenameRoom.Enabled = true;
                radioWhite.Enabled = true;
                radioBlack.Enabled = true;
                txtFischer.Enabled = true;
                txtCustomMin.Enabled = true;
                btn1min.Enabled = btn3min.Enabled = btn6min.Enabled = btn10min.Enabled = true;
            }
            else
            {
                btnStartGame.Text = guestReady ? "HUỶ SẴN SÀNG" : "SẴN SÀNG";
                txtConfigRoomName.Enabled = false;
                btnRenameRoom.Enabled = false;
                radioWhite.Enabled = false;
                radioBlack.Enabled = false;
                txtFischer.Enabled = false;
                txtCustomMin.Enabled = false;
                btn1min.Enabled = btn3min.Enabled = btn6min.Enabled = btn10min.Enabled = false;
            }

            UpdateReadyUI();
        }

        private void UpdateReadyUI()
        {
            ShowGuestReady(guestReady);

            if (isHost)
            {
                btnStartGame.Text = "BẮT ĐẦU CHƠI";
                btnStartGame.Enabled = guestReady;
            }
            else
            {
                btnStartGame.Text = guestReady ? "HUỶ SẴN SÀNG" : "SẴN SÀNG";
                btnStartGame.Enabled = true;
            }
        }

        // ======= Push reader =======
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

                        if (type == "ROOM_EVENT")
                        {
                            HandleRoomEvent(root);
                        }
                        else if (type == "ROOMCHAT")
                        {
                            HandleRoomChatPush(root);
                        }
                    }
                    catch { }
                }
            }
            catch { }
        }

        private void HandleRoomEvent(JsonElement root)
        {
            var ev = root.GetProperty("event").GetString();
            var ro = root.GetProperty("room");

            UpdatePlayersFromRoom(ro);

            if (ev == "RENAMED")
            {
                roomName = ro.GetProperty("name").GetString();
                lblTitle.Text = $"Phòng: {roomName}";
                txtConfigRoomName.Text = roomName;
            }
            else if (ev == "OWNER_CHANGED")
            {
                string newOwner = ro.GetProperty("ownerUsername").GetString();
                bool becameHost = string.Equals(newOwner, this.Username, StringComparison.OrdinalIgnoreCase);
                if (becameHost && !isHost)
                {
                    isHost = true;
                    ApplyRoleToUI();
                }
            }
            else if (ev == "CONFIG_UPDATED")
            {
                if (ro.TryGetProperty("minutes", out var jmin)) minutes = jmin.GetInt32();
                if (ro.TryGetProperty("increment", out var jin)) increment = jin.GetInt32();
                if (ro.TryGetProperty("side", out var js)) hostSide = js.GetString();

                txtCustomMin.Text = minutes.ToString();
                txtFischer.Text = increment.ToString();
                if (hostSide == "white") radioWhite.Checked = true; else radioBlack.Checked = true;

                UpdateSideLabels(hostSide);
            }
            else if (ev == "STARTED")
            {
                MessageBox.Show("Bắt đầu trò chơi (giả lập).");
            }
        }

        // Chat push từ server (hiển thị trừ khi trùng chính tin mình đã echo)
        private void HandleRoomChatPush(JsonElement root)
        {
            int rid = root.GetProperty("roomId").GetInt32();
            if (rid != this.roomId) return;

            string sender = root.GetProperty("sender").GetString();
            string role = root.GetProperty("role").GetString();
            string content = root.GetProperty("content").GetString();
            string ts = root.GetProperty("timestamp").GetString();

            if (string.Equals(sender, this.Username, StringComparison.OrdinalIgnoreCase))
            {
                foreach (var s in selfChatRecent)
                {
                    if (s == content) return;
                }
            }

            string line = $"[{ts}] {sender} ({role}): {content}\n";
            rtbChat.AppendText(line);
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

        // ======= UI update từ room snapshot =======
        private void UpdatePlayersFromRoom(JsonElement ro)
        {
            string ownerDisplay = ro.GetProperty("ownerDisplayName").GetString();
            int ownerElo = ro.GetProperty("ownerElo").GetInt32();

            string guestDisplay = null;
            int guestEloLocal = 0;
            bool readyLocal = false;

            if (ro.TryGetProperty("guestDisplayName", out var gdn) && gdn.ValueKind != JsonValueKind.Null)
                guestDisplay = gdn.GetString();
            if (ro.TryGetProperty("guestElo", out var gelo) && gelo.ValueKind == JsonValueKind.Number)
                guestEloLocal = gelo.GetInt32();
            if (ro.TryGetProperty("guestReady", out var gr) && (gr.ValueKind == JsonValueKind.True || gr.ValueKind == JsonValueKind.False))
                readyLocal = gr.GetBoolean();

            // Panel A = CHỦ
            lblPlayerATitle.Text = "CHỦ PHÒNG";
            lblPlayerAName.Text = ownerDisplay;
            lblPlayerARating.Text = $"⭐ Rating: {ownerElo}";
            lblPlayerAName.ForeColor = Color.White;
            lblPlayerARating.ForeColor = Color.FromArgb(255, 223, 186);

            // Panel B = KHÁCH
            if (!string.IsNullOrWhiteSpace(guestDisplay))
            {
                lblPlayerBTitle.Text = "KHÁCH";
                lblPlayerBName.Text = guestDisplay;
                lblPlayerBRating.Text = $"⭐ Rating: {guestEloLocal}";
                lblPlayerBRating.Visible = true;
                lblPlayerBName.ForeColor = Color.White;
                lblPlayerBRating.ForeColor = Color.FromArgb(255, 223, 186);
            }
            else
            {
                lblPlayerBTitle.Text = "KHÁCH";
                lblPlayerBName.Text = "Đang chờ...";
                lblPlayerBRating.Visible = false;
                lblPlayerBName.ForeColor = Color.FromArgb(200, 200, 200);
                lblPlayerBRating.ForeColor = Color.FromArgb(200, 200, 200);
            }

            guestReady = readyLocal;

            ConfigureAllCenteredLabels();
            UpdateReadyUI();
        }

        private void ShowGuestReady(bool ready)
        {
            if (lblGuestReadyBadge == null || lblGuestReadyBadge.IsDisposed) return;
            lblGuestReadyBadge.Text = ready ? "ĐÃ SẴN SÀNG" : "";
            lblGuestReadyBadge.ForeColor = ready ? Color.Gold : Color.Goldenrod;
            ConfigureCenterLabel(lblGuestReadyBadge, pnlPlayerB);
        }

        // ======= Actions =======
        private void BtnRenameRoom_Click(object sender, EventArgs e)
        {
            if (!isHost) return;

            var newName = (txtConfigRoomName.Text ?? "").Trim();
            if (string.IsNullOrEmpty(newName))
            {
                MessageBox.Show("Vui lòng nhập tên phòng.");
                return;
            }

            try
            {
                var res = requestClient.SendRequest(new
                {
                    action = "ROOM_RENAME",
                    roomId = this.roomId,
                    newName = newName,
                    username = this.Username
                });
                using var doc = JsonDocument.Parse(res);
                if (!doc.RootElement.GetProperty("success").GetBoolean())
                {
                    MessageBox.Show(doc.RootElement.GetProperty("message").GetString());
                    return;
                }
            }
            catch { }
        }

        private void BtnStartGame_Click(object sender, EventArgs e)
        {
            if (isHost)
            {
                try
                {
                    var res = requestClient.SendRequest(new
                    {
                        action = "ROOM_START",
                        roomId = this.roomId,
                        username = this.Username
                    });
                    using var doc = JsonDocument.Parse(res);
                    bool ok = doc.RootElement.GetProperty("success").GetBoolean();
                    string message = doc.RootElement.TryGetProperty("message", out var jm) ? jm.GetString() : "";
                    if (!ok) MessageBox.Show(message);
                }
                catch { }
            }
            else
            {
                bool newReady = !guestReady;
                try
                {
                    var res = requestClient.SendRequest(new
                    {
                        action = "ROOM_READY",
                        roomId = this.roomId,
                        username = this.Username,
                        ready = newReady
                    });

                    using var doc = JsonDocument.Parse(res);
                    bool ok = doc.RootElement.GetProperty("success").GetBoolean();
                    if (ok)
                    {
                        guestReady = newReady;
                        UpdateReadyUI();
                    }
                    else
                    {
                        string msg = doc.RootElement.TryGetProperty("message", out var jm) ? jm.GetString() : "Lỗi";
                        MessageBox.Show(msg);
                    }
                }
                catch
                {
                }
            }
        }

        private void BtnLeaveRoom_Click(object sender, EventArgs e)
        {
            try
            {
                requestClient.SendRequest(new
                {
                    action = "ROOM_LEAVE",
                    username = this.Username
                });
            }
            catch { }
            finally
            {
                Close();
            }
        }

        private void BtnSendChat_Click(object sender, EventArgs e)
        {
            var msg = (txtChat.Text ?? "").Trim();
            if (msg.Length == 0) return;

            var ts = DateTime.Now.ToString("HH:mm:ss");
            var roleText = isHost ? "Chủ phòng" : "Khách";
            rtbChat.AppendText($"[{ts}] {this.Username} ({roleText}): {msg}\n");

            selfChatRecent.Enqueue(msg);
            if (selfChatRecent.Count > SelfChatKeep) selfChatRecent.Dequeue();

            try
            {
                pushClient.SendRequest(new
                {
                    action = "ROOM_CHAT",
                    roomId = this.roomId,
                    sender = this.Username,
                    role = roleText,
                    content = msg
                });
                txtChat.Clear();
                if (pnlEmojiPicker != null) pnlEmojiPicker.Visible = false;
            }
            catch
            {
            }
        }

        private void TxtChat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                BtnSendChat_Click(sender, EventArgs.Empty);
            }
        }

        // ======= Config helpers =======
        private void SetMinutes(int m)
        {
            if (!isHost) return;
            minutes = m;
            txtCustomMin.Text = m.ToString();
            PushConfigUpdate();
        }

        private void PushConfigUpdate()
        {
            if (!isHost) return;
            if (requestClient == null) return;
            var stream = requestClient.GetStream();
            if (stream == null) return;

            try
            {
                UpdateSideLabels(this.hostSide);

                requestClient.SendRequest(new
                {
                    action = "ROOM_UPDATE_CONFIG",
                    roomId = this.roomId,
                    minutes = this.minutes,
                    increment = this.increment,
                    side = this.hostSide,
                    username = this.Username
                });
            }
            catch
            {
            }
        }
    }
}
