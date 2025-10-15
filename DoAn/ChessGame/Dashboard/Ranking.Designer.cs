namespace ChessGame
{
    partial class Ranking
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle19 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle20 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle21 = new DataGridViewCellStyle();
            pnlTop = new Panel();
            lblTitle = new Label();
            cmbFilter = new ComboBox();
            lblFilter = new Label();
            txtSearch = new TextBox();
            pnlUserCard = new Panel();
            picUserAvatar = new PictureBox();
            lblUserName = new Label();
            lblUserRank = new Label();
            lblUserRating = new Label();
            lblUserStats = new Label();
            pnlRankingList = new Panel();
            lblRankingTitle = new Label();
            dgvRanking = new DataGridView();
            btnRefresh = new Button();
            pnlPodium = new Panel();
            pnlSecond = new Panel();
            picSecond = new PictureBox();
            lblSecond = new Label();
            lblSecondRating = new Label();
            pnlFirst = new Panel();
            picFirst = new PictureBox();
            lblFirst = new Label();
            lblFirstRating = new Label();
            pnlThird = new Panel();
            picThird = new PictureBox();
            lblThird = new Label();
            lblThirdRating = new Label();
            pnlTop.SuspendLayout();
            pnlUserCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picUserAvatar).BeginInit();
            pnlRankingList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRanking).BeginInit();
            pnlPodium.SuspendLayout();
            pnlSecond.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picSecond).BeginInit();
            pnlFirst.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picFirst).BeginInit();
            pnlThird.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picThird).BeginInit();
            SuspendLayout();
            // 
            // pnlTop
            // 
            pnlTop.BackColor = Color.FromArgb(118, 74, 61);
            pnlTop.Controls.Add(lblTitle);
            pnlTop.Controls.Add(cmbFilter);
            pnlTop.Controls.Add(lblFilter);
            pnlTop.Controls.Add(txtSearch);
            pnlTop.Location = new Point(20, 20);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(1160, 80);
            pnlTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(20, 22);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(297, 46);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "BẢNG XẾP HẠNG";
            // 
            // cmbFilter
            // 
            cmbFilter.BackColor = Color.FromArgb(247, 234, 214);
            cmbFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFilter.FlatStyle = FlatStyle.Flat;
            cmbFilter.Font = new Font("Segoe UI", 10F);
            cmbFilter.ForeColor = Color.FromArgb(78, 49, 41);
            cmbFilter.FormattingEnabled = true;
            cmbFilter.Items.AddRange(new object[] { "Tất cả", "Top 10", "Top 50", "Top 100" });
            cmbFilter.Location = new Point(600, 35);
            cmbFilter.Name = "cmbFilter";
            cmbFilter.Size = new Size(150, 31);
            cmbFilter.TabIndex = 2;
            // 
            // lblFilter
            // 
            lblFilter.AutoSize = true;
            lblFilter.Font = new Font("Segoe UI", 11F);
            lblFilter.ForeColor = Color.White;
            lblFilter.Location = new Point(600, 10);
            lblFilter.Name = "lblFilter";
            lblFilter.Size = new Size(68, 25);
            lblFilter.TabIndex = 1;
            lblFilter.Text = "Bộ lọc:";
            // 
            // txtSearch
            // 
            txtSearch.BackColor = Color.FromArgb(247, 234, 214);
            txtSearch.BorderStyle = BorderStyle.None;
            txtSearch.Font = new Font("Segoe UI", 11F);
            txtSearch.ForeColor = Color.FromArgb(78, 49, 41);
            txtSearch.Location = new Point(780, 35);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Tìm kiếm người chơi...";
            txtSearch.Size = new Size(360, 25);
            txtSearch.TabIndex = 3;
            // 
            // pnlUserCard
            // 
            pnlUserCard.BackColor = Color.FromArgb(118, 74, 61);
            pnlUserCard.Controls.Add(picUserAvatar);
            pnlUserCard.Controls.Add(lblUserName);
            pnlUserCard.Controls.Add(lblUserRank);
            pnlUserCard.Controls.Add(lblUserRating);
            pnlUserCard.Controls.Add(lblUserStats);
            pnlUserCard.Location = new Point(20, 120);
            pnlUserCard.Name = "pnlUserCard";
            pnlUserCard.Size = new Size(350, 180);
            pnlUserCard.TabIndex = 1;
            // 
            // picUserAvatar
            // 
            picUserAvatar.BackColor = Color.White;
            picUserAvatar.Location = new Point(20, 20);
            picUserAvatar.Name = "picUserAvatar";
            picUserAvatar.Size = new Size(80, 80);
            picUserAvatar.SizeMode = PictureBoxSizeMode.Zoom;
            picUserAvatar.TabIndex = 0;
            picUserAvatar.TabStop = false;
            // 
            // lblUserName
            // 
            lblUserName.AutoSize = true;
            lblUserName.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblUserName.ForeColor = Color.White;
            lblUserName.Location = new Point(110, 25);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(141, 32);
            lblUserName.TabIndex = 1;
            lblUserName.Text = "Your Name";
            // 
            // lblUserRank
            // 
            lblUserRank.AutoSize = true;
            lblUserRank.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblUserRank.ForeColor = Color.FromArgb(255, 223, 186);
            lblUserRank.Location = new Point(110, 55);
            lblUserRank.Name = "lblUserRank";
            lblUserRank.Size = new Size(116, 28);
            lblUserRank.TabIndex = 2;
            lblUserRank.Text = "Hạng #123";
            // 
            // lblUserRating
            // 
            lblUserRating.AutoSize = true;
            lblUserRating.Font = new Font("Segoe UI", 11F);
            lblUserRating.ForeColor = Color.White;
            lblUserRating.Location = new Point(110, 80);
            lblUserRating.Name = "lblUserRating";
            lblUserRating.Size = new Size(115, 25);
            lblUserRating.TabIndex = 3;
            lblUserRating.Text = "Rating: 1200";
            // 
            // lblUserStats
            // 
            lblUserStats.Font = new Font("Segoe UI", 10F);
            lblUserStats.ForeColor = Color.FromArgb(255, 223, 186);
            lblUserStats.Location = new Point(20, 115);
            lblUserStats.Name = "lblUserStats";
            lblUserStats.Size = new Size(310, 60);
            lblUserStats.TabIndex = 4;
            lblUserStats.Text = "Thắng: 45 | Hòa: 12 | Thua: 23\nTỷ lệ thắng: 56.2%";
            // 
            // pnlRankingList
            // 
            pnlRankingList.BackColor = Color.FromArgb(118, 74, 61);
            pnlRankingList.Controls.Add(lblRankingTitle);
            pnlRankingList.Controls.Add(dgvRanking);
            pnlRankingList.Controls.Add(btnRefresh);
            pnlRankingList.Location = new Point(20, 320);
            pnlRankingList.Name = "pnlRankingList";
            pnlRankingList.Size = new Size(1160, 410);
            pnlRankingList.TabIndex = 3;
            // 
            // lblRankingTitle
            // 
            lblRankingTitle.AutoSize = true;
            lblRankingTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblRankingTitle.ForeColor = Color.White;
            lblRankingTitle.Location = new Point(20, 15);
            lblRankingTitle.Name = "lblRankingTitle";
            lblRankingTitle.Size = new Size(284, 32);
            lblRankingTitle.TabIndex = 0;
            lblRankingTitle.Text = "Bảng Xếp Hạng Chi Tiết";
            // 
            // dgvRanking
            // 
            dgvRanking.AllowUserToAddRows = false;
            dgvRanking.AllowUserToDeleteRows = false;
            dataGridViewCellStyle19.BackColor = Color.FromArgb(240, 217, 181);
            dgvRanking.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle19;
            dgvRanking.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRanking.BackgroundColor = Color.FromArgb(247, 234, 214);
            dgvRanking.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle20.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle20.BackColor = Color.FromArgb(78, 49, 41);
            dataGridViewCellStyle20.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            dataGridViewCellStyle20.ForeColor = Color.White;
            dataGridViewCellStyle20.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle20.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle20.WrapMode = DataGridViewTriState.True;
            dgvRanking.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle20;
            dgvRanking.ColumnHeadersHeight = 35;
            dgvRanking.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle21.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle21.BackColor = Color.FromArgb(247, 234, 214);
            dataGridViewCellStyle21.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle21.ForeColor = Color.FromArgb(78, 49, 41);
            dataGridViewCellStyle21.SelectionBackColor = Color.FromArgb(133, 181, 100);
            dataGridViewCellStyle21.SelectionForeColor = Color.White;
            dataGridViewCellStyle21.WrapMode = DataGridViewTriState.False;
            dgvRanking.DefaultCellStyle = dataGridViewCellStyle21;
            dgvRanking.EnableHeadersVisualStyles = false;
            dgvRanking.Location = new Point(20, 55);
            dgvRanking.Name = "dgvRanking";
            dgvRanking.ReadOnly = true;
            dgvRanking.RowHeadersVisible = false;
            dgvRanking.RowHeadersWidth = 51;
            dgvRanking.RowTemplate.Height = 30;
            dgvRanking.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRanking.Size = new Size(1120, 295);
            dgvRanking.TabIndex = 1;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.FromArgb(133, 181, 100);
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(20, 360);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(1120, 40);
            btnRefresh.TabIndex = 2;
            btnRefresh.Text = "CẬP NHẬT BẢNG XẾP HẠNG";
            btnRefresh.UseVisualStyleBackColor = false;
            // 
            // pnlPodium
            // 
            pnlPodium.BackColor = Color.FromArgb(240, 217, 181);
            pnlPodium.Controls.Add(pnlSecond);
            pnlPodium.Controls.Add(pnlFirst);
            pnlPodium.Controls.Add(pnlThird);
            pnlPodium.Location = new Point(390, 120);
            pnlPodium.Name = "pnlPodium";
            pnlPodium.Size = new Size(790, 180);
            pnlPodium.TabIndex = 2;
            // 
            // pnlSecond
            // 
            pnlSecond.BackColor = Color.FromArgb(192, 192, 192);
            pnlSecond.Controls.Add(picSecond);
            pnlSecond.Controls.Add(lblSecond);
            pnlSecond.Controls.Add(lblSecondRating);
            pnlSecond.Location = new Point(10, 25);
            pnlSecond.Name = "pnlSecond";
            pnlSecond.Size = new Size(240, 145);
            pnlSecond.TabIndex = 1;
            // 
            // picSecond
            // 
            picSecond.BackColor = Color.White;
            picSecond.Location = new Point(80, 10);
            picSecond.Name = "picSecond";
            picSecond.Size = new Size(70, 70);
            picSecond.SizeMode = PictureBoxSizeMode.Zoom;
            picSecond.TabIndex = 0;
            picSecond.TabStop = false;
            // 
            // lblSecond
            // 
            lblSecond.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblSecond.ForeColor = Color.FromArgb(78, 49, 41);
            lblSecond.Location = new Point(10, 85);
            lblSecond.Name = "lblSecond";
            lblSecond.Size = new Size(220, 20);
            lblSecond.TabIndex = 1;
            lblSecond.Text = "Player #2";
            lblSecond.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSecondRating
            // 
            lblSecondRating.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSecondRating.ForeColor = Color.FromArgb(78, 49, 41);
            lblSecondRating.Location = new Point(10, 108);
            lblSecondRating.Name = "lblSecondRating";
            lblSecondRating.Size = new Size(220, 18);
            lblSecondRating.TabIndex = 2;
            lblSecondRating.Text = "2350";
            lblSecondRating.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlFirst
            // 
            pnlFirst.BackColor = Color.FromArgb(255, 215, 0);
            pnlFirst.Controls.Add(picFirst);
            pnlFirst.Controls.Add(lblFirst);
            pnlFirst.Controls.Add(lblFirstRating);
            pnlFirst.Location = new Point(270, 3);
            pnlFirst.Name = "pnlFirst";
            pnlFirst.Size = new Size(250, 167);
            pnlFirst.TabIndex = 0;
            // 
            // picFirst
            // 
            picFirst.BackColor = Color.White;
            picFirst.Location = new Point(85, 15);
            picFirst.Name = "picFirst";
            picFirst.Size = new Size(80, 80);
            picFirst.SizeMode = PictureBoxSizeMode.Zoom;
            picFirst.TabIndex = 0;
            picFirst.TabStop = false;
            // 
            // lblFirst
            // 
            lblFirst.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblFirst.ForeColor = Color.FromArgb(78, 49, 41);
            lblFirst.Location = new Point(10, 100);
            lblFirst.Name = "lblFirst";
            lblFirst.Size = new Size(230, 30);
            lblFirst.TabIndex = 1;
            lblFirst.Text = "Player #1";
            lblFirst.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblFirstRating
            // 
            lblFirstRating.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblFirstRating.ForeColor = Color.FromArgb(78, 49, 41);
            lblFirstRating.Location = new Point(10, 128);
            lblFirstRating.Name = "lblFirstRating";
            lblFirstRating.Size = new Size(230, 20);
            lblFirstRating.TabIndex = 2;
            lblFirstRating.Text = "2500";
            lblFirstRating.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlThird
            // 
            pnlThird.BackColor = Color.FromArgb(205, 127, 50);
            pnlThird.Controls.Add(picThird);
            pnlThird.Controls.Add(lblThird);
            pnlThird.Controls.Add(lblThirdRating);
            pnlThird.Location = new Point(540, 35);
            pnlThird.Name = "pnlThird";
            pnlThird.Size = new Size(240, 135);
            pnlThird.TabIndex = 2;
            // 
            // picThird
            // 
            picThird.BackColor = Color.White;
            picThird.Location = new Point(85, 10);
            picThird.Name = "picThird";
            picThird.Size = new Size(70, 70);
            picThird.SizeMode = PictureBoxSizeMode.Zoom;
            picThird.TabIndex = 0;
            picThird.TabStop = false;
            // 
            // lblThird
            // 
            lblThird.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblThird.ForeColor = Color.FromArgb(78, 49, 41);
            lblThird.Location = new Point(10, 85);
            lblThird.Name = "lblThird";
            lblThird.Size = new Size(220, 20);
            lblThird.TabIndex = 1;
            lblThird.Text = "Player #3";
            lblThird.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblThirdRating
            // 
            lblThirdRating.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblThirdRating.ForeColor = Color.FromArgb(78, 49, 41);
            lblThirdRating.Location = new Point(10, 105);
            lblThirdRating.Name = "lblThirdRating";
            lblThirdRating.Size = new Size(220, 18);
            lblThirdRating.TabIndex = 2;
            lblThirdRating.Text = "2280";
            lblThirdRating.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Ranking
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 217, 181);
            ClientSize = new Size(1200, 750);
            Controls.Add(pnlRankingList);
            Controls.Add(pnlPodium);
            Controls.Add(pnlUserCard);
            Controls.Add(pnlTop);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Ranking";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Bảng Xếp Hạng";
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            pnlUserCard.ResumeLayout(false);
            pnlUserCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picUserAvatar).EndInit();
            pnlRankingList.ResumeLayout(false);
            pnlRankingList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRanking).EndInit();
            pnlPodium.ResumeLayout(false);
            pnlSecond.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picSecond).EndInit();
            pnlFirst.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picFirst).EndInit();
            pnlThird.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picThird).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlTop;
        private Label lblTitle;
        private ComboBox cmbFilter;
        private Label lblFilter;
        private TextBox txtSearch;
        private Panel pnlUserCard;
        private PictureBox picUserAvatar;
        private Label lblUserName;
        private Label lblUserRank;
        private Label lblUserRating;
        private Label lblUserStats;
        private Panel pnlPodium;
        private Panel pnlFirst;
        private PictureBox picFirst;
        private Label lblFirst;
        private Label lblFirstRating;
        private Panel pnlSecond;
        private PictureBox picSecond;
        private Label lblSecond;
        private Label lblSecondRating;
        private Panel pnlThird;
        private PictureBox picThird;
        private Label lblThird;
        private Label lblThirdRating;
        private Panel pnlRankingList;
        private Label lblRankingTitle;
        private DataGridView dgvRanking;
        private Button btnRefresh;
    }
}
