using System;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ChessGame
{
    public partial class AccountSetting : Form
    {
        // Thông tin user hiện tại (Dashboard gán trước khi mở form)
        public int UserId { get; set; }
        public string Username { get; set; } = "";
        public string DisplayName { get; set; } = "";
        public string Email { get; set; } = "";
        public int Elo { get; set; }

        // Thống kê
        public int GamesPlayed { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Losses { get; set; }

        private System.Windows.Forms.Timer autoRefreshTimer;
        private bool isRefreshing = false;
        private bool isPasswordVisible = false;
        private bool isConfirmPasswordVisible = false;

        public AccountSetting()
        {
            InitializeComponent();
            Load += AccountSetting_Load;
            FormClosed += AccountSetting_FormClosed;
        }

        private void AccountSetting_Load(object sender, EventArgs e)
        {
            // Mặc định che mật khẩu
            txtEditPassword.UseSystemPasswordChar = true;
            txtEditPasswordConfirm.UseSystemPasswordChar = true;

            if (string.IsNullOrWhiteSpace(Username))
            {
                MessageBox.Show(
                    "Không có thông tin tài khoản. Vui lòng mở Cài đặt tài khoản từ Dashboard sau khi đăng nhập.",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Close();
                return;
            }

            // Đổ thông tin ban đầu lên UI (lấy từ Dashboard)
            ApplyUserInfoToLabels();
            ApplyStatsToCards();
            FillEditFieldsFromCurrent();

            // Auto refresh mỗi 3s để cập nhật rating / thống kê / email
            autoRefreshTimer = new System.Windows.Forms.Timer();
            autoRefreshTimer.Interval = 3000;
            autoRefreshTimer.Tick += AutoRefreshTimer_Tick;
            autoRefreshTimer.Start();
        }

        private void AccountSetting_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (autoRefreshTimer != null)
            {
                autoRefreshTimer.Stop();
                autoRefreshTimer.Tick -= AutoRefreshTimer_Tick;
                autoRefreshTimer.Dispose();
                autoRefreshTimer = null;
            }
        }

        private void AutoRefreshTimer_Tick(object sender, EventArgs e)
        {
            RefreshInfoFromServer(false);
        }

        /// <summary>
        /// Lấy lại thông tin user từ GET_RANKING (không dùng GET_USER_INFO để tránh lỗi session)
        /// </summary>
        private void RefreshInfoFromServer(bool showError)
        {
            if (isRefreshing) return;
            isRefreshing = true;

            try
            {
                TCPClient client = null;
                string response = null;

                try
                {
                    client = new TCPClient();
                    client.Connect();
                    response = client.SendRequest(new { action = "GET_RANKING" });
                }
                finally
                {
                    if (client != null)
                    {
                        try { client.Disconnect(); } catch { }
                    }
                }

                if (string.IsNullOrWhiteSpace(response))
                {
                    if (showError)
                    {
                        MessageBox.Show("Server không trả về dữ liệu.", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return;
                }

                using var doc = JsonDocument.Parse(response);
                var root = doc.RootElement;

                if (!root.TryGetProperty("success", out var js) || !js.GetBoolean())
                {
                    if (showError)
                    {
                        string msg =
                            root.TryGetProperty("message", out var jm) && jm.ValueKind == JsonValueKind.String
                                ? jm.GetString()
                                : "Không lấy được dữ liệu từ server.";
                        MessageBox.Show(msg, "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return;
                }

                if (!root.TryGetProperty("users", out var ju) || ju.ValueKind != JsonValueKind.Array)
                    return;

                foreach (var u in ju.EnumerateArray())
                {
                    string username = u.GetProperty("username").GetString() ?? "";
                    if (!string.Equals(username, Username, StringComparison.OrdinalIgnoreCase))
                        continue;

                    string displayName = u.GetProperty("displayName").GetString() ?? "";
                    string email = Email;
                    if (u.TryGetProperty("email", out var je) && je.ValueKind == JsonValueKind.String)
                    {
                        email = je.GetString();
                    }

                    int elo = u.TryGetProperty("elo", out var jeo) && jeo.ValueKind == JsonValueKind.Number
                        ? jeo.GetInt32()
                        : Elo;

                    int games = u.TryGetProperty("gamesPlayed", out var jg) && jg.ValueKind == JsonValueKind.Number
                        ? jg.GetInt32()
                        : GamesPlayed;

                    int wins = u.TryGetProperty("wins", out var jw) && jw.ValueKind == JsonValueKind.Number
                        ? jw.GetInt32()
                        : Wins;

                    int draws = u.TryGetProperty("draws", out var jd) && jd.ValueKind == JsonValueKind.Number
                        ? jd.GetInt32()
                        : Draws;

                    int losses = u.TryGetProperty("losses", out var jl) && jl.ValueKind == JsonValueKind.Number
                        ? jl.GetInt32()
                        : Losses;

                    double winRate;
                    if (u.TryGetProperty("winRate", out var jwr) && jwr.ValueKind == JsonValueKind.Number)
                        winRate = jwr.GetDouble() * 100.0;
                    else if (games > 0)
                        winRate = (double)wins / games * 100.0;
                    else
                        winRate = 0.0;

                    DisplayName = displayName;
                    Email = email ?? "";
                    Elo = elo;
                    GamesPlayed = games;
                    Wins = wins;
                    Draws = draws;
                    Losses = losses;

                    ApplyUserInfoToLabels();
                    ApplyStatsToCardsInternal(winRate);

                    // Chỉ đè lên textbox khi panel chỉnh sửa đang đóng
                    if (!pnlEditAccount.Visible)
                    {
                        FillEditFieldsFromCurrent();
                    }

                    break;
                }
            }
            catch
            {
                if (showError)
                {
                    MessageBox.Show("Không tải được thông tin từ server.",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                isRefreshing = false;
            }
        }

        private void ApplyUserInfoToLabels()
        {
            lblUsername.Text = $"Tên tài khoản: {Username}";
            lblDisplayName.Text = $"Tên hiển thị: {DisplayName}";
            lblEmail.Text = $"Email: {Email}";
            lblRating.Text = $"Rating (Elo): {Elo}";

            UpdateInfoLayout();
        }

        // Sắp xếp lại các label để không bị đè nhau khi text dài
        private void UpdateInfoLayout()
        {
            int spacing = 20;      // khoảng cách giữa 2 cụm thông tin
            int innerSpacing = 6;  // khoảng cách icon - label

            // Hàng 1: Username | DisplayName
            lblDisplayNameIcon.Left = lblUsername.Right + spacing;
            lblDisplayName.Left = lblDisplayNameIcon.Right + innerSpacing;

            // Hàng 2: Email | Rating
            lblRatingIcon.Left = lblEmail.Right + spacing;
            lblRating.Left = lblRatingIcon.Right + innerSpacing;
        }

        private void ApplyStatsToCards()
        {
            double winRate = GamesPlayed > 0
                ? (double)Wins / GamesPlayed * 100.0
                : 0.0;

            ApplyStatsToCardsInternal(winRate);
        }

        private void ApplyStatsToCardsInternal(double winRate)
        {
            lblCardGamesVal.Text = GamesPlayed.ToString();
            lblCardWinsVal.Text = Wins.ToString();
            lblCardDrawsVal.Text = Draws.ToString();
            lblCardLossesVal.Text = Losses.ToString();
            lblCardWinrateVal.Text = $"{winRate:0.0}%";
        }

        private void FillEditFieldsFromCurrent()
        {
            txtEditDisplayName.Text = DisplayName ?? string.Empty;
            txtEditEmail.Text = Email ?? string.Empty;
            txtEditPassword.Text = string.Empty;
            txtEditPasswordConfirm.Text = string.Empty;
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        // ================== SỰ KIỆN NÚT ==================

        private void btnShowEdit_Click(object sender, EventArgs e)
        {
            FillEditFieldsFromCurrent();
            pnlEditAccount.Visible = true;
            txtEditDisplayName.Focus();
        }

        private void btnCloseEdit_Click(object sender, EventArgs e)
        {
            pnlEditAccount.Visible = false;
        }

        private void btnEditShowPassword_Click(object sender, EventArgs e)
        {
            isPasswordVisible = !isPasswordVisible;
            txtEditPassword.UseSystemPasswordChar = !isPasswordVisible;
        }

        private void btnEditShowPasswordConfirm_Click(object sender, EventArgs e)
        {
            isConfirmPasswordVisible = !isConfirmPasswordVisible;
            txtEditPasswordConfirm.UseSystemPasswordChar = !isConfirmPasswordVisible;
        }

        private void btnSaveEdit_Click(object sender, EventArgs e)
        {
            if (UserId <= 0)
            {
                MessageBox.Show("Không xác định được người dùng.", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string newDisplayName = txtEditDisplayName.Text.Trim();
            string newEmail = txtEditEmail.Text.Trim();
            string newPassword = txtEditPassword.Text;
            string confirmPassword = txtEditPasswordConfirm.Text;

            // ===== Validate phía client =====

            if (string.IsNullOrWhiteSpace(newDisplayName))
            {
                MessageBox.Show("Tên hiển thị không được để trống.", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEditDisplayName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(newEmail))
            {
                MessageBox.Show("Email không được để trống.", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEditEmail.Focus();
                return;
            }

            if (!IsValidEmail(newEmail))
            {
                MessageBox.Show("Email không hợp lệ.", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEditEmail.Focus();
                return;
            }

            bool isChangingPassword =
                !string.IsNullOrEmpty(newPassword) ||
                !string.IsNullOrEmpty(confirmPassword);

            if (isChangingPassword)
            {
                if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ mật khẩu mới và xác nhận.",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (newPassword.Length < 8)
                {
                    MessageBox.Show("Mật khẩu mới phải có ít nhất 8 ký tự.",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (newPassword != confirmPassword)
                {
                    MessageBox.Show("Mật khẩu xác nhận không khớp.",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Nếu không đổi gì
            if (!isChangingPassword &&
                string.Equals(newDisplayName, DisplayName, StringComparison.Ordinal) &&
                string.Equals(newEmail, Email, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Bạn chưa thay đổi thông tin nào.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // ===== Gửi request UPDATE_ACCOUNT lên server =====

            try
            {
                TCPClient client = null;
                string response = null;

                try
                {
                    client = new TCPClient();
                    client.Connect();

                    var request = new
                    {
                        action = "UPDATE_ACCOUNT",
                        userId = UserId,
                        displayName = newDisplayName,
                        email = newEmail,
                        password = isChangingPassword ? newPassword : null
                    };

                    response = client.SendRequest(request);
                }
                finally
                {
                    if (client != null)
                    {
                        try { client.Disconnect(); } catch { }
                    }
                }

                if (string.IsNullOrWhiteSpace(response))
                {
                    MessageBox.Show("Server không trả về dữ liệu.", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using var doc = JsonDocument.Parse(response);
                var root = doc.RootElement;

                bool success =
                    root.TryGetProperty("success", out var js) &&
                    js.ValueKind == JsonValueKind.True &&
                    js.GetBoolean();

                string message =
                    root.TryGetProperty("message", out var jm) && jm.ValueKind == JsonValueKind.String
                        ? jm.GetString()
                        : (success ? "Cập nhật tài khoản thành công!"
                                   : "Cập nhật tài khoản thất bại.");

                if (!success)
                {
                    MessageBox.Show(message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show(message, "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Sau khi update xong: refresh lại từ server (kể cả email)
                pnlEditAccount.Visible = false;
                RefreshInfoFromServer(false);
            }
            catch
            {
                MessageBox.Show("Có lỗi xảy ra khi gửi yêu cầu cập nhật tài khoản.",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}