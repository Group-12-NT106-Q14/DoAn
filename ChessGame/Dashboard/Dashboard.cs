using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessGame
{
    public partial class frmDashboard : Form
    {
        public string DisplayName;
        public int Elo;
        public int UserId;
        public string Email;
        public string Username;

        private System.Windows.Forms.Timer onlineUpdateTimer;
        private TCPClient chatClient;
        private Thread listenChatThread;
        private CancellationTokenSource chatCancellationTokenSource;
        private bool isClosing = false;

        // Bộ emoji unicode đa dạng nhất từ Messenger, Telegram, v.v.
        private string[] emoticons = new string[]
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

        public frmDashboard()
        {
            InitializeComponent();
            SetupEmojiPickerPanel();
        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {
            lblUsername.Text = DisplayName;
            lblUserRank.Text = $"Rating: {Elo}";
            LoadOnlineUsers();

            onlineUpdateTimer = new System.Windows.Forms.Timer();
            onlineUpdateTimer.Interval = 500;
            onlineUpdateTimer.Tick += OnlineUpdateTimer_Tick;
            onlineUpdateTimer.Start();

            StartChatReceiver();
        }

        private void OnlineUpdateTimer_Tick(object sender, EventArgs e)
        {
            LoadOnlineUsers();
        }

        private void btnBan_Click(object sender, EventArgs e)
        {
            this.Hide();
            var frm = new RoomList
            {
                Username = this.Username,
                DisplayName = this.DisplayName,
                Elo = this.Elo
            };
            frm.ShowDialog();
            this.Show();
        }

        private void btnBXH_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Ranking().ShowDialog();
            this.Show();
        }

        private void btnLichSu_Click(object sender, EventArgs e)
        {
            this.Hide();
            new History().ShowDialog();
            this.Show();
        }

        private void btnChoi_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Match().ShowDialog();
            this.Show();
        }

        private void btnĐX_Click(object sender, EventArgs e)
        {
            TCPClient client = new TCPClient();
            client.Connect();
            var request = new
            {
                action = "LOGOUT",
                username = Username
            };
            string response = client.SendRequest(request);
            client.Disconnect();
            MessageBox.Show("Đăng xuất thành công!");
            this.Hide();
            frmLogin login = new frmLogin();
            login.ShowDialog();
            this.Close();
        }

        private void btnCaiDat_Click(object sender, EventArgs e)
        {
            AccountSetting frm = new AccountSetting();
            frm.UserId = this.UserId;
            frm.CurrentDisplayName = this.DisplayName;
            frm.CurrentEmail = this.Email;
            this.Hide();
            frm.ShowDialog();
            this.Show();
            if (frm.IsUpdated)
            {
                this.DisplayName = frm.CurrentDisplayName;
                this.Email = frm.CurrentEmail;
                lblUsername.Text = this.DisplayName;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (isClosing) return;
            isClosing = true;

            if (onlineUpdateTimer != null)
                onlineUpdateTimer.Stop();

            if (chatCancellationTokenSource != null)
            {
                try
                {
                    if (!chatCancellationTokenSource.IsCancellationRequested)
                        chatCancellationTokenSource.Cancel();
                }
                catch (ObjectDisposedException) { }
            }

            if (listenChatThread != null && listenChatThread.IsAlive)
            {
                listenChatThread.Join(2000);
            }

            if (chatClient != null)
                chatClient.Disconnect();

            if (chatCancellationTokenSource != null)
            {
                try
                {
                    chatCancellationTokenSource.Dispose();
                }
                catch (ObjectDisposedException) { }
                chatCancellationTokenSource = null;
            }

            Application.Exit();
        }

        private void LoadOnlineUsers()
        {
            try
            {
                TCPClient client = new TCPClient();
                client.Connect();
                var request = new
                {
                    action = "GET_ONLINE_USERS"
                };
                string response = client.SendRequest(request);
                client.Disconnect();
                var obj = System.Text.Json.JsonDocument.Parse(response);
                if (obj.RootElement.GetProperty("success").GetBoolean())
                {
                    var users = obj.RootElement.GetProperty("users").EnumerateArray();
                    lstOnlinePlayers.Items.Clear();
                    int count = 0;
                    foreach (var u in users)
                    {
                        string displayName = u.GetProperty("displayName").GetString();
                        string username = u.GetProperty("username").GetString();
                        lstOnlinePlayers.Items.Add($"{displayName} ({username})");
                        count++;
                    }
                    lblOnlineCount.Text = "Có " + count.ToString() + " người chơi đang online";
                }
                else
                {
                    lblOnlineCount.Text = "Có 0 người chơi đang online";
                    lstOnlinePlayers.Items.Clear();
                }
            }
            catch
            {
                lblOnlineCount.Text = "Có 0 người chơi đang online";
                lstOnlinePlayers.Items.Clear();
            }
        }

        private void lblOnlineCount_Click(object sender, EventArgs e)
        {
            LoadOnlineUsers();
        }

        private void lstOnlinePlayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Optional: event chọn người chơi online
        }

        // ==== CHAT realtime, dùng thread safe cancellation ====
        public void StartChatReceiver()
        {
            chatClient = new TCPClient();
            chatClient.Connect();
            chatCancellationTokenSource = new CancellationTokenSource();
            listenChatThread = new Thread(() => ListenForChat(chatCancellationTokenSource.Token));
            listenChatThread.IsBackground = true;
            listenChatThread.Start();
        }

        private void ListenForChat(CancellationToken cancellationToken)
        {
            try
            {
                var stream = chatClient.GetStream();
                byte[] buffer = new byte[4096];

                while (!cancellationToken.IsCancellationRequested)
                {
                    if (stream.DataAvailable)
                    {
                        int bytesRead = stream.Read(buffer, 0, buffer.Length);
                        if (bytesRead > 0)
                        {
                            string msg = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                            var doc = System.Text.Json.JsonDocument.Parse(msg);
                            if (doc.RootElement.TryGetProperty("type", out var typeProp) && typeProp.GetString() == "CHAT")
                            {
                                string sender = doc.RootElement.GetProperty("sender").GetString();
                                string content = doc.RootElement.GetProperty("content").GetString();
                                string timestamp = doc.RootElement.GetProperty("timestamp").GetString();
                                string line = $"{sender}: {content} [{timestamp}]{Environment.NewLine}";
                                if (rtbChatMessages.InvokeRequired)
                                    rtbChatMessages.Invoke(new Action(() => rtbChatMessages.AppendText(line)));
                                else
                                    rtbChatMessages.AppendText(line);
                            }
                        }
                    }
                    else
                    {
                        Thread.Sleep(100);
                    }
                }
            }
            catch { }
        }

        // ==== EMOJI ==== 
        // ==== EMOJI ==== 
        private void SetupEmojiPickerPanel()
        {
            pnlEmojiPicker.Visible = false;
            pnlEmojiPicker.Controls.Clear();
            pnlEmojiPicker.AutoScroll = true;     
        }


        private void ShowEmojiPicker()
        {
            // Nếu đang mở rồi thì bấm lại nút emoji sẽ đóng panel
            if (pnlEmojiPicker.Visible && pnlEmojiPicker.Controls.Count > 0)
            {
                pnlEmojiPicker.Visible = false;
                return;
            }

            pnlEmojiPicker.Controls.Clear();

            int btnSize = 32;          // kích thước ô emoji
            int cols = 8;              // số cột
            int spacing = 4;           // khoảng cách giữa các ô

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
                    txtChatInput.Text += ((Button)s).Text;
                    txtChatInput.Focus();
                    // KHÔNG ẩn pnlEmojiPicker ở đây -> cho user chọn nhiều emoji liên tục
                };

                pnlEmojiPicker.Controls.Add(btn);
            }

            // Không thay đổi Size: giữ viewport 250x180, phần dư sẽ cuộn bằng AutoScroll
            pnlEmojiPicker.Visible = true;
            pnlEmojiPicker.BringToFront();
        }


        private void btnEmoji_Click(object sender, EventArgs e)
        {
            ShowEmojiPicker();
        }

        private void btnSendChat_Click(object sender, EventArgs e)
        {
            string text = txtChatInput.Text.Trim();
            if (string.IsNullOrEmpty(text)) return;
            var request = new
            {
                action = "CHAT",
                sender = Username,
                content = text
            };
            string msg = System.Text.Json.JsonSerializer.Serialize(request);
            byte[] msgData = Encoding.UTF8.GetBytes(msg);
            chatClient.GetStream().Write(msgData, 0, msgData.Length);
            txtChatInput.Clear();
            pnlEmojiPicker.Visible = false;
        }

        private void txtChatInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSendChat_Click(null, null);
                e.SuppressKeyPress = true;
                pnlEmojiPicker.Visible = false;
            }
        }
    }
}
