using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Windows.Forms;

namespace ChessGame
{
    public partial class History : Form
    {
        // Được gán từ Dashboard khi mở form
        public int UserId { get; set; }
        public string Username { get; set; }

        private System.Windows.Forms.Timer autoRefreshTimer;
        private bool isRefreshing = false;

        private class HistoryRow
        {
            public string ThoiGian { get; set; }
            public string DoiThu { get; set; }
            public string KetQua { get; set; }
            public string MauQuan { get; set; }
            public string KieuTran { get; set; }

            // Dùng nội bộ để lọc theo kết quả (win/draw/loss)
            public string ResultCode { get; set; }
        }

        public History()
        {
            InitializeComponent();

            Load += History_Load;
            FormClosed += History_FormClosed;
            btnSearch.Click += BtnSearch_Click;
        }

        private void History_Load(object sender, EventArgs e)
        {
            // Combo filter mặc định = "Tất cả"
            if (cmbFilterType.Items.Count > 0 && cmbFilterType.SelectedIndex < 0)
                cmbFilterType.SelectedIndex = 0;

            // Mặc định: 1 tháng gần nhất
            dtpTo.Value = DateTime.Now.Date;
            dtpFrom.Value = dtpTo.Value.AddMonths(-1);

            SetupGridColumns();

            // Tải lần đầu (coi như thao tác tay -> showError = true)
            LoadHistory(true);

            // Auto-refresh 5s, nhưng giữ nguyên scroll/selection
            autoRefreshTimer = new System.Windows.Forms.Timer();
            autoRefreshTimer.Interval = 5000;
            autoRefreshTimer.Tick += (s, ev) => LoadHistory(false);
            autoRefreshTimer.Start();
        }

        private void History_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (autoRefreshTimer != null)
            {
                try { autoRefreshTimer.Stop(); } catch { }
                autoRefreshTimer.Dispose();
                autoRefreshTimer = null;
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            // Người dùng bấm TÌM KIẾM -> reload và có thể show lỗi nếu có
            LoadHistory(true);
        }

        private void SetupGridColumns()
        {
            dgvHistory.AutoGenerateColumns = false;
            dgvHistory.Columns.Clear();

            // Các cột tự fill chiều ngang, chỉ cuộn dọc
            dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHistory.ScrollBars = ScrollBars.Vertical;

            var colTime = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ThoiGian",
                HeaderText = "Thời gian",
                FillWeight = 120
            };

            var colOpponent = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DoiThu",
                HeaderText = "Đối thủ",
                FillWeight = 165
            };

            var colResult = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "KetQua",
                HeaderText = "Kết quả",
                FillWeight = 55
            };

            var colColor = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MauQuan",
                HeaderText = "Quân",
                FillWeight = 40
            };

            var colTimeControl = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "KieuTran",
                HeaderText = "Thời lượng",
                FillWeight = 70
            };

            dgvHistory.Columns.Add(colTime);
            dgvHistory.Columns.Add(colOpponent);
            dgvHistory.Columns.Add(colResult);
            dgvHistory.Columns.Add(colColor);
            dgvHistory.Columns.Add(colTimeControl);
        }

        private string GetFilterValue()
        {
            switch (cmbFilterType.SelectedIndex)
            {
                case 1: return "win";
                case 2: return "draw";
                case 3: return "loss";
                default: return "all";
            }
        }

        private void LoadHistory(bool showError)
        {
            // showError = true  -> gọi từ người dùng (lần đầu / bấm TÌM KIẾM)
            // showError = false -> auto-refresh, cần giữ nguyên scroll & selection

            if (isRefreshing) return;
            isRefreshing = true;

            bool isAutoRefresh = !showError;

            int selectedIndex = -1;
            int firstRowIndex = -1;

            if (isAutoRefresh && dgvHistory.Rows.Count > 0)
            {
                try
                {
                    if (dgvHistory.CurrentCell != null)
                        selectedIndex = dgvHistory.CurrentCell.RowIndex;
                }
                catch { }

                try
                {
                    firstRowIndex = dgvHistory.FirstDisplayedScrollingRowIndex;
                }
                catch
                {
                    firstRowIndex = -1;
                }
            }

            try
            {
                if (UserId <= 0)
                {
                    if (showError)
                    {
                        MessageBox.Show(
                            "Không xác định được người dùng. Vui lòng mở Lịch Sử sau khi đăng nhập.",
                            "Lỗi",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                    return;
                }

                DateTime fromDate = dtpFrom.Value.Date;
                DateTime toDate = dtpTo.Value.Date;

                if (fromDate > toDate)
                {
                    var tmp = fromDate;
                    fromDate = toDate;
                    toDate = tmp;
                }

                string filter = GetFilterValue(); // dùng để lọc client-side
                string fromStr = fromDate.ToString("yyyy-MM-dd");
                string toStr = toDate.ToString("yyyy-MM-dd");

                string response = null;
                TCPClient client = null;

                try
                {
                    client = new TCPClient();
                    client.Connect();

                    // KHÔNG gửi resultFilter cho server => server luôn trả stats & matches cho ALL kết quả
                    var request = new
                    {
                        action = "GET_HISTORY",
                        userId = this.UserId,
                        from = fromStr,
                        to = toStr
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
                    if (showError)
                    {
                        MessageBox.Show(
                            "Server không trả về dữ liệu (response rỗng).",
                            "Lỗi",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                    return;
                }

                using JsonDocument doc = JsonDocument.Parse(response);
                JsonElement root = doc.RootElement;

                bool success = root.TryGetProperty("success", out JsonElement jSuccess) &&
                               jSuccess.ValueKind == JsonValueKind.True;

                if (!success)
                {
                    if (showError)
                    {
                        string msg = root.TryGetProperty("message", out JsonElement jMsg) &&
                                     jMsg.ValueKind == JsonValueKind.String
                            ? jMsg.GetString()
                            : "Không tải được lịch sử trận đấu.";

                        MessageBox.Show(
                            msg,
                            "Lỗi",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                    return;
                }

                // ===== Thống kê bên trái: luôn là tổng trong khoảng thời gian (KHÔNG phụ thuộc lọc win/draw/loss) =====
                if (root.TryGetProperty("stats", out JsonElement jStats) &&
                    jStats.ValueKind == JsonValueKind.Object)
                {
                    int total = jStats.TryGetProperty("total", out JsonElement jTotal) && jTotal.ValueKind == JsonValueKind.Number
                        ? jTotal.GetInt32()
                        : 0;
                    int wins = jStats.TryGetProperty("wins", out JsonElement jWins) && jWins.ValueKind == JsonValueKind.Number
                        ? jWins.GetInt32()
                        : 0;
                    int draws = jStats.TryGetProperty("draws", out JsonElement jDraws) && jDraws.ValueKind == JsonValueKind.Number
                        ? jDraws.GetInt32()
                        : 0;
                    int losses = jStats.TryGetProperty("losses", out JsonElement jLosses) && jLosses.ValueKind == JsonValueKind.Number
                        ? jLosses.GetInt32()
                        : 0;
                    double winRate = jStats.TryGetProperty("winRate", out JsonElement jWr) && jWr.ValueKind == JsonValueKind.Number
                        ? jWr.GetDouble() * 100.0
                        : 0.0;

                    lblTotalGames.Text = $"Tổng trận: {total}";
                    lblWins.Text = $"Thắng: {wins}";
                    lblDraws.Text = $"Hòa: {draws}";
                    lblLosses.Text = $"Thua: {losses}";
                    lblWinRate.Text = $"Tỷ lệ thắng: {winRate:F1}%";
                }
                else
                {
                    lblTotalGames.Text = "Tổng trận: 0";
                    lblWins.Text = "Thắng: 0";
                    lblDraws.Text = "Hòa: 0";
                    lblLosses.Text = "Thua: 0";
                    lblWinRate.Text = "Tỷ lệ thắng: 0%";
                }

                // ===== Danh sách trận đấu: lấy ALL từ server, rồi lọc client-side theo filter =====
                var allRows = new List<HistoryRow>();

                if (root.TryGetProperty("matches", out JsonElement jMatches) &&
                    jMatches.ValueKind == JsonValueKind.Array)
                {
                    foreach (JsonElement m in jMatches.EnumerateArray())
                    {
                        string opponentName = m.TryGetProperty("opponentName", out JsonElement jName) &&
                                              jName.ValueKind == JsonValueKind.String
                            ? jName.GetString()
                            : "";

                        string opponentUsername = m.TryGetProperty("opponentUsername", out JsonElement jUser) &&
                                                  jUser.ValueKind == JsonValueKind.String
                            ? jUser.GetString()
                            : "";

                        bool isWhite = m.TryGetProperty("isWhite", out JsonElement jWhite) &&
                                       (jWhite.ValueKind == JsonValueKind.True || jWhite.ValueKind == JsonValueKind.False) &&
                                       jWhite.GetBoolean();

                        string resultCode = m.TryGetProperty("result", out JsonElement jRes) &&
                                            jRes.ValueKind == JsonValueKind.String
                            ? jRes.GetString()
                            : "";

                        int minutes = m.TryGetProperty("timeControlMinutes", out JsonElement jMin) &&
                                      jMin.ValueKind == JsonValueKind.Number
                            ? jMin.GetInt32()
                            : 0;

                        int increment = m.TryGetProperty("incrementSeconds", out JsonElement jInc) &&
                                        jInc.ValueKind == JsonValueKind.Number
                            ? jInc.GetInt32()
                            : 0;

                        string endTimeRaw = m.TryGetProperty("endTime", out JsonElement jEnd) &&
                                            jEnd.ValueKind == JsonValueKind.String
                            ? jEnd.GetString()
                            : null;

                        string doiThu = string.IsNullOrWhiteSpace(opponentUsername)
                            ? opponentName
                            : $"{opponentName} ({opponentUsername})";

                        string ketQua;
                        switch (resultCode)
                        {
                            case "win": ketQua = "Thắng"; break;
                            case "draw": ketQua = "Hòa"; break;
                            case "loss": ketQua = "Thua"; break;
                            default: ketQua = resultCode; break;
                        }

                        string mauQuan = isWhite ? "Trắng" : "Đen";
                        string kieuTran = $"{minutes} phút";

                        string thoiGian = "";
                        if (!string.IsNullOrWhiteSpace(endTimeRaw) &&
                            DateTime.TryParse(endTimeRaw, out DateTime endTime))
                        {
                            thoiGian = endTime.ToString("dd/MM/yyyy HH:mm");
                        }

                        allRows.Add(new HistoryRow
                        {
                            ThoiGian = thoiGian,
                            DoiThu = doiThu,
                            KetQua = ketQua,
                            MauQuan = mauQuan,
                            KieuTran = kieuTran,
                            ResultCode = resultCode
                        });
                    }
                }

                // Lọc client-side theo combobox (Tất cả / Thắng / Hòa / Thua)
                IEnumerable<HistoryRow> filteredRows = allRows;

                switch (filter)
                {
                    case "win":
                        filteredRows = allRows.FindAll(r => r.ResultCode == "win");
                        break;
                    case "draw":
                        filteredRows = allRows.FindAll(r => r.ResultCode == "draw");
                        break;
                    case "loss":
                        filteredRows = allRows.FindAll(r => r.ResultCode == "loss");
                        break;
                }

                var rowsToShow = new List<HistoryRow>(filteredRows);
                dgvHistory.DataSource = rowsToShow;

                // Khôi phục scroll & selection sau auto-refresh
                if (isAutoRefresh && dgvHistory.Rows.Count > 0)
                {
                    try
                    {
                        if (selectedIndex >= 0 && selectedIndex < dgvHistory.Rows.Count)
                        {
                            dgvHistory.CurrentCell = dgvHistory.Rows[selectedIndex].Cells[0];
                        }
                    }
                    catch { }

                    try
                    {
                        if (firstRowIndex >= 0 && firstRowIndex < dgvHistory.Rows.Count)
                        {
                            dgvHistory.FirstDisplayedScrollingRowIndex = firstRowIndex;
                        }
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                if (showError)
                {
                    MessageBox.Show(
                        "Không tải được lịch sử trận đấu. Vui lòng thử lại.\n\nChi tiết lỗi: " + ex.Message,
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            finally
            {
                isRefreshing = false;
            }
        }

        private void btnBackToLobby_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}