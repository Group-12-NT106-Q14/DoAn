namespace ChessGame
{
    partial class Match
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Match));
            lblTitle = new Label();
            pnlQuickMatch = new Panel();
            lblOnlineCount = new Label();
            btnStartMatch = new Button();
            pnlSearching = new Panel();
            lblSearching = new Label();
            progressBar = new ProgressBar();
            btnCancelSearch = new Button();
            pnlMatchFound = new Panel();
            lblMatchFoundTitle = new Label();
            picOpponent = new PictureBox();
            lblOpponentName = new Label();
            lblOpponentRating = new Label();
            lblCountdown = new Label();
            lblActiveUsers = new Label();
            btnBackToLobby = new Button();
            pnlQuickMatch.SuspendLayout();
            pnlSearching.SuspendLayout();
            pnlMatchFound.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picOpponent).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(78, 49, 41);
            lblTitle.Location = new Point(315, 28);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(285, 62);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "CHƠI NGAY";
            // 
            // pnlQuickMatch
            // 
            pnlQuickMatch.BackColor = Color.FromArgb(118, 74, 61);
            pnlQuickMatch.Controls.Add(lblOnlineCount);
            pnlQuickMatch.Controls.Add(btnStartMatch);
            pnlQuickMatch.Controls.Add(pnlSearching);
            pnlQuickMatch.Location = new Point(211, 118);
            pnlQuickMatch.Name = "pnlQuickMatch";
            pnlQuickMatch.Size = new Size(500, 180);
            pnlQuickMatch.TabIndex = 3;
            // 
            // lblOnlineCount
            // 
            lblOnlineCount.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblOnlineCount.ForeColor = Color.FromArgb(133, 181, 100);
            lblOnlineCount.Location = new Point(0, 30);
            lblOnlineCount.Name = "lblOnlineCount";
            lblOnlineCount.Size = new Size(500, 42);
            lblOnlineCount.TabIndex = 0;
            lblOnlineCount.Text = "342 người đang tìm trận";
            lblOnlineCount.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnStartMatch
            // 
            btnStartMatch.BackColor = Color.FromArgb(133, 181, 100);
            btnStartMatch.FlatAppearance.BorderSize = 0;
            btnStartMatch.FlatStyle = FlatStyle.Flat;
            btnStartMatch.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            btnStartMatch.ForeColor = Color.White;
            btnStartMatch.Location = new Point(100, 75);
            btnStartMatch.Name = "btnStartMatch";
            btnStartMatch.Size = new Size(300, 70);
            btnStartMatch.TabIndex = 1;
            btnStartMatch.Text = "TÌM ĐỐI THỦ";
            btnStartMatch.UseVisualStyleBackColor = false;
            // 
            // pnlSearching
            // 
            pnlSearching.BackColor = Color.FromArgb(118, 74, 61);
            pnlSearching.Controls.Add(lblSearching);
            pnlSearching.Controls.Add(progressBar);
            pnlSearching.Controls.Add(btnCancelSearch);
            pnlSearching.Location = new Point(3, 0);
            pnlSearching.Name = "pnlSearching";
            pnlSearching.Size = new Size(500, 180);
            pnlSearching.TabIndex = 4;
            pnlSearching.Visible = false;
            // 
            // lblSearching
            // 
            lblSearching.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblSearching.ForeColor = Color.FromArgb(255, 223, 186);
            lblSearching.Location = new Point(0, 30);
            lblSearching.Name = "lblSearching";
            lblSearching.Size = new Size(500, 28);
            lblSearching.TabIndex = 0;
            lblSearching.Text = "Đang tìm đối thủ...";
            lblSearching.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(50, 75);
            progressBar.MarqueeAnimationSpeed = 30;
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(400, 25);
            progressBar.Style = ProgressBarStyle.Marquee;
            progressBar.TabIndex = 1;
            // 
            // btnCancelSearch
            // 
            btnCancelSearch.BackColor = Color.FromArgb(160, 106, 88);
            btnCancelSearch.FlatAppearance.BorderSize = 0;
            btnCancelSearch.FlatStyle = FlatStyle.Flat;
            btnCancelSearch.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnCancelSearch.ForeColor = Color.White;
            btnCancelSearch.Location = new Point(150, 120);
            btnCancelSearch.Name = "btnCancelSearch";
            btnCancelSearch.Size = new Size(200, 45);
            btnCancelSearch.TabIndex = 2;
            btnCancelSearch.Text = "HỦY TÌM KIẾM";
            btnCancelSearch.UseVisualStyleBackColor = false;
            // 
            // pnlMatchFound
            // 
            pnlMatchFound.BackColor = Color.FromArgb(118, 74, 61);
            pnlMatchFound.Controls.Add(lblMatchFoundTitle);
            pnlMatchFound.Controls.Add(picOpponent);
            pnlMatchFound.Controls.Add(lblOpponentName);
            pnlMatchFound.Controls.Add(lblOpponentRating);
            pnlMatchFound.Controls.Add(lblCountdown);
            pnlMatchFound.Location = new Point(211, 103);
            pnlMatchFound.Name = "pnlMatchFound";
            pnlMatchFound.Size = new Size(500, 237);
            pnlMatchFound.TabIndex = 5;
            pnlMatchFound.Visible = false;
            // 
            // lblMatchFoundTitle
            // 
            lblMatchFoundTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblMatchFoundTitle.ForeColor = Color.FromArgb(133, 181, 100);
            lblMatchFoundTitle.Location = new Point(0, 0);
            lblMatchFoundTitle.Name = "lblMatchFoundTitle";
            lblMatchFoundTitle.Size = new Size(500, 55);
            lblMatchFoundTitle.TabIndex = 0;
            lblMatchFoundTitle.Text = "ĐÃ TÌM THẤY ĐỐI THỦ!";
            lblMatchFoundTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // picOpponent
            // 
            picOpponent.BackColor = Color.White;
            picOpponent.Image = (Image)resources.GetObject("picOpponent.Image");
            picOpponent.Location = new Point(202, 80);
            picOpponent.Name = "picOpponent";
            picOpponent.Size = new Size(100, 100);
            picOpponent.SizeMode = PictureBoxSizeMode.Zoom;
            picOpponent.TabIndex = 1;
            picOpponent.TabStop = false;
            // 
            // lblOpponentName
            // 
            lblOpponentName.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblOpponentName.ForeColor = Color.White;
            lblOpponentName.Location = new Point(50, 175);
            lblOpponentName.Name = "lblOpponentName";
            lblOpponentName.Size = new Size(400, 34);
            lblOpponentName.TabIndex = 2;
            lblOpponentName.Text = "ChessMaster99";
            lblOpponentName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblOpponentRating
            // 
            lblOpponentRating.Font = new Font("Segoe UI", 11F);
            lblOpponentRating.ForeColor = Color.FromArgb(255, 223, 186);
            lblOpponentRating.Location = new Point(50, 200);
            lblOpponentRating.Name = "lblOpponentRating";
            lblOpponentRating.Size = new Size(400, 37);
            lblOpponentRating.TabIndex = 3;
            lblOpponentRating.Text = "⭐ Rating: 1450";
            lblOpponentRating.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblCountdown
            // 
            lblCountdown.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
            lblCountdown.ForeColor = Color.FromArgb(255, 223, 186);
            lblCountdown.Location = new Point(0, 53);
            lblCountdown.Name = "lblCountdown";
            lblCountdown.Size = new Size(500, 24);
            lblCountdown.TabIndex = 4;
            lblCountdown.Text = "Trận đấu bắt đầu sau 05 giây";
            lblCountdown.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblActiveUsers
            // 
            lblActiveUsers.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblActiveUsers.ForeColor = Color.FromArgb(118, 74, 61);
            lblActiveUsers.Location = new Point(9, 353);
            lblActiveUsers.Name = "lblActiveUsers";
            lblActiveUsers.Size = new Size(900, 25);
            lblActiveUsers.TabIndex = 6;
            lblActiveUsers.Text = "342 người đang online";
            lblActiveUsers.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnBackToLobby
            // 
            btnBackToLobby.BackColor = Color.FromArgb(160, 106, 88);
            btnBackToLobby.FlatAppearance.BorderSize = 0;
            btnBackToLobby.FlatStyle = FlatStyle.Flat;
            btnBackToLobby.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnBackToLobby.ForeColor = Color.White;
            btnBackToLobby.Location = new Point(350, 520);
            btnBackToLobby.Name = "btnBackToLobby";
            btnBackToLobby.Size = new Size(200, 50);
            btnBackToLobby.TabIndex = 7;
            btnBackToLobby.Text = "QUAY VỀ SẢNH";
            btnBackToLobby.UseVisualStyleBackColor = false;
            btnBackToLobby.Click += btnBackToLobby_Click;
            // 
            // Match
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 217, 181);
            ClientSize = new Size(900, 600);
            Controls.Add(btnBackToLobby);
            Controls.Add(lblActiveUsers);
            Controls.Add(pnlMatchFound);
            Controls.Add(pnlQuickMatch);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Match";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Chơi Ngay";
            pnlQuickMatch.ResumeLayout(false);
            pnlSearching.ResumeLayout(false);
            pnlMatchFound.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picOpponent).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Panel pnlQuickMatch;
        private Label lblOnlineCount;
        private Button btnStartMatch;
        private Panel pnlSearching;
        private Label lblSearching;
        private ProgressBar progressBar;
        private Button btnCancelSearch;
        private Panel pnlMatchFound;
        private Label lblMatchFoundTitle;
        private PictureBox picOpponent;
        private Label lblOpponentName;
        private Label lblOpponentRating;
        private Label lblCountdown;
        private Label lblActiveUsers;
        private Button btnBackToLobby;
    }
}
