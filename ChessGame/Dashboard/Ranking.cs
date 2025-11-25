using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessGame
{
    public partial class Ranking : Form
    {
        // Username hiện tại (được truyền từ Dashboard)
        public string Username { get; set; }

        private System.Windows.Forms.Timer refreshTimer;
        private bool isRefreshing = false;

        private class RankingUser
        {
            public int Rank { get; set; }          // Hạng sau khi sắp xếp theo tiêu chí của bạn
            public int ServerRank { get; set; }    // Rank server gửi (dùng làm tie-break cuối)
            public string DisplayName { get; set; }
            public string Username { get; set; }
            public int Elo { get; set; }
            public int GamesPlayed { get; set; }
            public int Wins { get; set; }
            public int Draws { get; set; }
            public int Losses { get; set; }
            public double WinRate { get; set; }

            public string WinRateString
            {
                get
                {
                    if (GamesPlayed <= 0) return "0%";
                    double pct = WinRate * 100.0;
                    return pct.ToString("0.0") + "%";
                }
            }
        }

        public Ranking()
        {
            InitializeComponent();
            this.Load += Ranking_Load;
            btnRefresh.Click += BtnRefresh_Click;
        }

        private void Ranking_Load(object sender, EventArgs e)
        {
            SetupGrid();
            LoadRanking();
            SetupTimer();
        }

        private void SetupTimer()
        {
            refreshTimer = new System.Windows.Forms.Timer();
            refreshTimer.Interval = 300; // 0.3 giây auto-refresh (có thể tăng lên 2000-5000ms cho nhẹ)
            refreshTimer.Tick += (s, ev) => LoadRanking();
            refreshTimer.Start();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadRanking();
        }

        private void SetupGrid()
        {
            dgvRanking.AutoGenerateColumns = false;
            dgvRanking.Columns.Clear();

            dgvRanking.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Rank",
                HeaderText = "Hạng",
                Width = 60
            });
            dgvRanking.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DisplayName",
                HeaderText = "Người chơi",
                Width = 180
            });
            dgvRanking.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Elo",
                HeaderText = "Rating",
                Width = 80
            });
            dgvRanking.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Wins",
                HeaderText = "Thắng",
                Width = 80
            });
            dgvRanking.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Draws",
                HeaderText = "Hòa",
                Width = 80
            });
            dgvRanking.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Losses",
                HeaderText = "Thua",
                Width = 80
            });
            dgvRanking.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "GamesPlayed",
                HeaderText = "Tổng trận",
                Width = 90
            });
            dgvRanking.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "WinRateString",
                HeaderText = "Tỷ lệ thắng",
                Width = 100
            });
        }

        private void LoadRanking()
        {
            if (isRefreshing) return;
            isRefreshing = true;

            // ====================== NEW: Lưu trạng thái scroll + selection ======================
            int firstDisplayedRow = -1;
            string selectedUsername = null;
            int selectedColumnIndex = -1;

            try
            {
                if (dgvRanking.Rows.Count > 0)
                {
                    try
                    {
                        firstDisplayedRow = dgvRanking.FirstDisplayedScrollingRowIndex;
                    }
                    catch
                    {
                        firstDisplayedRow = -1;
                    }

                    if (dgvRanking.CurrentRow != null)
                    {
                        if (dgvRanking.CurrentRow.DataBoundItem is RankingUser currentUser)
                        {
                            selectedUsername = currentUser.Username;
                        }

                        if (dgvRanking.CurrentCell != null)
                        {
                            selectedColumnIndex = dgvRanking.CurrentCell.ColumnIndex;
                        }
                    }
                }

                // ====================== Lấy dữ liệu mới từ server ======================
                List<RankingUser> users = FetchRankingFromServer();

                if (users == null || users.Count == 0)
                {
                    dgvRanking.DataSource = null;
                    ClearPodium();
                    ClearUserCard();
                    return;
                }

                // Sắp xếp theo tiêu chí:
                // 1. Elo giảm dần
                // 2. Wins giảm dần
                // 3. Losses tăng dần
                // 4. GamesPlayed giảm dần
                // 5. ServerRank tăng dần
                var sorted = users
                    .OrderByDescending(u => u.Elo)
                    .ThenByDescending(u => u.Wins)
                    .ThenBy(u => u.Losses)
                    .ThenByDescending(u => u.GamesPlayed)
                    .ThenBy(u => u.ServerRank)
                    .ToList();

                for (int i = 0; i < sorted.Count; i++)
                {
                    sorted[i].Rank = i + 1;
                }

                dgvRanking.DataSource = sorted;

                // ====================== NEW: Khôi phục scroll ======================
                if (firstDisplayedRow >= 0 && firstDisplayedRow < dgvRanking.Rows.Count)
                {
                    try
                    {
                        dgvRanking.FirstDisplayedScrollingRowIndex = firstDisplayedRow;
                    }
                    catch
                    {
                        // ignore
                    }
                }

                // ====================== NEW: Khôi phục selection theo Username ======================
                if (!string.IsNullOrEmpty(selectedUsername))
                {
                    int rowIndex = sorted.FindIndex(u =>
                        string.Equals(u.Username, selectedUsername, StringComparison.OrdinalIgnoreCase));

                    if (rowIndex >= 0 && rowIndex < dgvRanking.Rows.Count)
                    {
                        dgvRanking.ClearSelection();

                        int colIndex = selectedColumnIndex >= 0 && selectedColumnIndex < dgvRanking.Columns.Count
                            ? selectedColumnIndex
                            : 0;

                        dgvRanking.CurrentCell = dgvRanking.Rows[rowIndex].Cells[colIndex];
                        dgvRanking.Rows[rowIndex].Selected = true;
                    }
                }

                // ====================== Cập nhật podium + card user ======================
                UpdatePodium(sorted);
                UpdateUserCard(sorted);
            }
            catch
            {
                // Nếu lỗi (mạng / parse) thì bỏ qua, giữ dữ liệu cũ
            }
            finally
            {
                isRefreshing = false;
            }
        }

        private List<RankingUser> FetchRankingFromServer()
        {
            TCPClient client = null;
            try
            {
                client = new TCPClient();
                client.Connect();
                string response = client.SendRequest(new { action = "GET_RANKING" });

                using var doc = JsonDocument.Parse(response);
                var root = doc.RootElement;

                if (!root.TryGetProperty("success", out var js) || !js.GetBoolean())
                    return null;

                if (!root.TryGetProperty("users", out var ju) || ju.ValueKind != JsonValueKind.Array)
                    return null;

                var list = new List<RankingUser>();

                foreach (var u in ju.EnumerateArray())
                {
                    var user = new RankingUser();

                    if (u.TryGetProperty("rank", out var jr) && jr.ValueKind == JsonValueKind.Number)
                        user.ServerRank = jr.GetInt32();

                    user.DisplayName = u.GetProperty("displayName").GetString();
                    user.Username = u.GetProperty("username").GetString();
                    user.Elo = u.GetProperty("elo").GetInt32();
                    user.GamesPlayed = u.GetProperty("gamesPlayed").GetInt32();
                    user.Wins = u.GetProperty("wins").GetInt32();
                    user.Draws = u.GetProperty("draws").GetInt32();
                    user.Losses = u.GetProperty("losses").GetInt32();

                    // Nếu server có gửi winRate thì dùng, không thì tự tính
                    if (u.TryGetProperty("winRate", out var jw) && jw.ValueKind == JsonValueKind.Number)
                    {
                        user.WinRate = jw.GetDouble();
                    }
                    else
                    {
                        if (user.GamesPlayed > 0)
                            user.WinRate = (double)user.Wins / user.GamesPlayed;
                        else
                            user.WinRate = 0;
                    }

                    list.Add(user);
                }

                return list;
            }
            catch
            {
                return null;
            }
            finally
            {
                try
                {
                    client?.Disconnect();
                }
                catch
                {
                }
            }
        }

        private void UpdatePodium(List<RankingUser> sorted)
        {
            // Reset trước
            lblFirst.Text = "Player #1";
            lblFirstRating.Text = "";
            lblSecond.Text = "Player #2";
            lblSecondRating.Text = "";
            lblThird.Text = "Player #3";
            lblThirdRating.Text = "";

            if (sorted.Count >= 1)
            {
                lblFirst.Text = sorted[0].DisplayName;
                lblFirstRating.Text = $"Rating: {sorted[0].Elo}";
            }

            if (sorted.Count >= 2)
            {
                lblSecond.Text = sorted[1].DisplayName;
                lblSecondRating.Text = $"Rating: {sorted[1].Elo}";
            }

            if (sorted.Count >= 3)
            {
                lblThird.Text = sorted[2].DisplayName;
                lblThirdRating.Text = $"Rating: {sorted[2].Elo}";
            }
        }

        private void ClearPodium()
        {
            lblFirst.Text = "Player #1";
            lblFirstRating.Text = "";
            lblSecond.Text = "Player #2";
            lblSecondRating.Text = "";
            lblThird.Text = "Player #3";
            lblThirdRating.Text = "";
        }

        private void UpdateUserCard(List<RankingUser> sorted)
        {
            if (string.IsNullOrEmpty(this.Username))
            {
                lblUserName.Text = "Chưa xác định";
                lblUserRank.Text = "";
                lblUserRating.Text = "";
                lblUserStats.Text = "Không xác định được người chơi hiện tại.";
                return;
            }

            var me = sorted.FirstOrDefault(u =>
                string.Equals(u.Username, this.Username, StringComparison.OrdinalIgnoreCase));

            if (me == null)
            {
                lblUserName.Text = this.Username;
                lblUserRank.Text = "Không có trong bảng xếp hạng";
                lblUserRating.Text = "";
                lblUserStats.Text = "";
                return;
            }

            lblUserName.Text = me.DisplayName;
            lblUserRank.Text = $"Hạng #{me.Rank}";
            lblUserRating.Text = $"Rating: {me.Elo}";

            string statLine =
                $"Thắng: {me.Wins} | Hòa: {me.Draws} | Thua: {me.Losses}{Environment.NewLine}" +
                $"Tỷ lệ thắng: {me.WinRateString}";
            lblUserStats.Text = statLine;
        }

        private void ClearUserCard()
        {
            lblUserName.Text = "Your Name";
            lblUserRank.Text = "Hạng #?";
            lblUserRating.Text = "Rating: -";
            lblUserStats.Text = "Chưa có dữ liệu.";
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (refreshTimer != null)
            {
                refreshTimer.Stop();
                refreshTimer.Dispose();
                refreshTimer = null;
            }
            base.OnFormClosed(e);
        }
    }
}