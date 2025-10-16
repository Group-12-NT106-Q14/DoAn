namespace ChessGame
{
    partial class History
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            pnlTop = new Panel();
            lblTitle = new Label();
            lblFilter = new Label();
            cmbFilterType = new ComboBox();
            lblDateRange = new Label();
            dtpFrom = new DateTimePicker();
            dtpTo = new DateTimePicker();
            btnSearch = new Button();
            pnlStats = new Panel();
            lblStatsTitle = new Label();
            lblTotalGames = new Label();
            lblWins = new Label();
            lblDraws = new Label();
            lblLosses = new Label();
            lblWinRate = new Label();
            pnlHistory = new Panel();
            lblHistoryTitle = new Label();
            dgvHistory = new DataGridView();
            pnlTop.SuspendLayout();
            pnlStats.SuspendLayout();
            pnlHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHistory).BeginInit();
            SuspendLayout();

            pnlTop.BackColor = Color.FromArgb(118, 74, 61);
            pnlTop.Controls.Add(lblTitle);
            pnlTop.Controls.Add(lblFilter);
            pnlTop.Controls.Add(cmbFilterType);
            pnlTop.Controls.Add(lblDateRange);
            pnlTop.Controls.Add(dtpFrom);
            pnlTop.Controls.Add(dtpTo);
            pnlTop.Controls.Add(btnSearch);
            pnlTop.Location = new Point(20, 20);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(1160, 100);
            pnlTop.TabIndex = 0;

            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(20, 30);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(335, 46);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "LỊCH SỬ TRẬN ĐẤU";

            lblFilter.AutoSize = true;
            lblFilter.Font = new Font("Segoe UI", 10F);
            lblFilter.ForeColor = Color.White;
            lblFilter.Location = new Point(418, 14);
            lblFilter.Name = "lblFilter";
            lblFilter.Size = new Size(80, 23);
            lblFilter.TabIndex = 1;
            lblFilter.Text = "Lọc theo:";

            cmbFilterType.BackColor = Color.FromArgb(247, 234, 214);
            cmbFilterType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFilterType.FlatStyle = FlatStyle.Flat;
            cmbFilterType.Font = new Font("Segoe UI", 10F);
            cmbFilterType.FormattingEnabled = true;
            cmbFilterType.Items.AddRange(new object[] { "Tất cả", "Thắng", "Hòa", "Thua", "Với AI", "Với người chơi" });
            cmbFilterType.Location = new Point(400, 40);
            cmbFilterType.Name = "cmbFilterType";
            cmbFilterType.Size = new Size(180, 31);
            cmbFilterType.TabIndex = 2;

            lblDateRange.AutoSize = true;
            lblDateRange.Font = new Font("Segoe UI", 10F);
            lblDateRange.ForeColor = Color.White;
            lblDateRange.Location = new Point(610, 15);
            lblDateRange.Name = "lblDateRange";
            lblDateRange.Size = new Size(146, 23);
            lblDateRange.TabIndex = 3;
            lblDateRange.Text = "Khoảng thời gian:";

            dtpFrom.CalendarMonthBackground = Color.FromArgb(247, 234, 214);
            dtpFrom.Font = new Font("Segoe UI", 9F);
            dtpFrom.Format = DateTimePickerFormat.Short;
            dtpFrom.Location = new Point(610, 40);
            dtpFrom.Name = "dtpFrom";
            dtpFrom.Size = new Size(130, 27);
            dtpFrom.TabIndex = 4;

            dtpTo.CalendarMonthBackground = Color.FromArgb(247, 234, 214);
            dtpTo.Font = new Font("Segoe UI", 9F);
            dtpTo.Format = DateTimePickerFormat.Short;
            dtpTo.Location = new Point(760, 40);
            dtpTo.Name = "dtpTo";
            dtpTo.Size = new Size(130, 27);
            dtpTo.TabIndex = 5;

            btnSearch.BackColor = Color.FromArgb(133, 181, 100);
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSearch.ForeColor = Color.White;
            btnSearch.Location = new Point(920, 30);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(220, 45);
            btnSearch.TabIndex = 6;
            btnSearch.Text = "TÌM KIẾM";
            btnSearch.UseVisualStyleBackColor = false;

            pnlStats.BackColor = Color.FromArgb(118, 74, 61);
            pnlStats.Controls.Add(lblStatsTitle);
            pnlStats.Controls.Add(lblTotalGames);
            pnlStats.Controls.Add(lblWins);
            pnlStats.Controls.Add(lblDraws);
            pnlStats.Controls.Add(lblLosses);
            pnlStats.Controls.Add(lblWinRate);
            pnlStats.Location = new Point(20, 140);
            pnlStats.Name = "pnlStats";
            pnlStats.Size = new Size(380, 261);
            pnlStats.TabIndex = 1;

            lblStatsTitle.AutoSize = true;
            lblStatsTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblStatsTitle.ForeColor = Color.White;
            lblStatsTitle.Location = new Point(20, 20);
            lblStatsTitle.Name = "lblStatsTitle";
            lblStatsTitle.Size = new Size(139, 37);
            lblStatsTitle.TabIndex = 0;
            lblStatsTitle.Text = "Thống Kê";

            lblTotalGames.AutoSize = true;
            lblTotalGames.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblTotalGames.ForeColor = Color.White;
            lblTotalGames.Location = new Point(20, 70);
            lblTotalGames.Name = "lblTotalGames";
            lblTotalGames.Size = new Size(153, 30);
            lblTotalGames.TabIndex = 1;
            lblTotalGames.Text = "Tổng trận: 80";

            lblWins.AutoSize = true;
            lblWins.Font = new Font("Segoe UI", 12F);
            lblWins.ForeColor = Color.FromArgb(133, 181, 100);
            lblWins.Location = new Point(20, 110);
            lblWins.Name = "lblWins";
            lblWins.Size = new Size(97, 28);
            lblWins.TabIndex = 2;
            lblWins.Text = "Thắng: 45";

            lblDraws.AutoSize = true;
            lblDraws.Font = new Font("Segoe UI", 12F);
            lblDraws.ForeColor = Color.FromArgb(255, 223, 186);
            lblDraws.Location = new Point(20, 145);
            lblDraws.Name = "lblDraws";
            lblDraws.Size = new Size(79, 28);
            lblDraws.TabIndex = 3;
            lblDraws.Text = "Hòa: 12";

            lblLosses.AutoSize = true;
            lblLosses.Font = new Font("Segoe UI", 12F);
            lblLosses.ForeColor = Color.FromArgb(200, 100, 100);
            lblLosses.Location = new Point(20, 180);
            lblLosses.Name = "lblLosses";
            lblLosses.Size = new Size(85, 28);
            lblLosses.TabIndex = 4;
            lblLosses.Text = "Thua: 23";

            lblWinRate.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblWinRate.ForeColor = Color.FromArgb(255, 223, 186);
            lblWinRate.Location = new Point(20, 220);
            lblWinRate.Name = "lblWinRate";
            lblWinRate.Size = new Size(340, 41);
            lblWinRate.TabIndex = 5;
            lblWinRate.Text = "Tỷ lệ thắng: 56.2%";

            pnlHistory.BackColor = Color.FromArgb(118, 74, 61);
            pnlHistory.Controls.Add(lblHistoryTitle);
            pnlHistory.Controls.Add(dgvHistory);
            pnlHistory.Location = new Point(420, 140);
            pnlHistory.Name = "pnlHistory";
            pnlHistory.Size = new Size(760, 559);
            pnlHistory.TabIndex = 2;

            lblHistoryTitle.AutoSize = true;
            lblHistoryTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblHistoryTitle.ForeColor = Color.White;
            lblHistoryTitle.Location = new Point(20, 20);
            lblHistoryTitle.Name = "lblHistoryTitle";
            lblHistoryTitle.Size = new Size(257, 32);
            lblHistoryTitle.TabIndex = 0;
            lblHistoryTitle.Text = "Chi Tiết Các Trận Đấu";

            dgvHistory.AllowUserToAddRows = false;
            dgvHistory.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(240, 217, 181);
            dgvHistory.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHistory.BackgroundColor = Color.FromArgb(247, 234, 214);
            dgvHistory.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = Color.FromArgb(78, 49, 41);
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = Color.White;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            dgvHistory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            dgvHistory.ColumnHeadersHeight = 35;
            dgvHistory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = Color.FromArgb(247, 234, 214);
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = Color.FromArgb(78, 49, 41);
            dataGridViewCellStyle6.SelectionBackColor = Color.FromArgb(133, 181, 100);
            dataGridViewCellStyle6.SelectionForeColor = Color.White;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
            dgvHistory.DefaultCellStyle = dataGridViewCellStyle6;
            dgvHistory.EnableHeadersVisualStyles = false;
            dgvHistory.Location = new Point(20, 60);
            dgvHistory.Name = "dgvHistory";
            dgvHistory.ReadOnly = true;
            dgvHistory.RowHeadersVisible = false;
            dgvHistory.RowHeadersWidth = 51;
            dgvHistory.RowTemplate.Height = 30;
            dgvHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHistory.Size = new Size(720, 460);
            dgvHistory.TabIndex = 1;

            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 217, 181);
            ClientSize = new Size(1200, 750);
            Controls.Add(pnlHistory);
            Controls.Add(pnlStats);
            Controls.Add(pnlTop);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "History";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Lịch Sử Trận Đấu";
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            pnlStats.ResumeLayout(false);
            pnlStats.PerformLayout();
            pnlHistory.ResumeLayout(false);
            pnlHistory.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHistory).EndInit();
            ResumeLayout(false);
        }

        private Panel pnlTop;
        private Label lblTitle;
        private Label lblFilter;
        private ComboBox cmbFilterType;
        private Label lblDateRange;
        private DateTimePicker dtpFrom;
        private DateTimePicker dtpTo;
        private Button btnSearch;
        private Panel pnlStats;
        private Label lblStatsTitle;
        private Label lblTotalGames;
        private Label lblWins;
        private Label lblDraws;
        private Label lblLosses;
        private Label lblWinRate;
        private Panel pnlHistory;
        private Label lblHistoryTitle;
        private DataGridView dgvHistory;
    }
}
