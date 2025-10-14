namespace ChessGame
{
    partial class frmDashboard
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
            btnChoi = new Button();
            btnBXH = new Button();
            btnMay = new Button();
            btnBan = new Button();
            btnThuGon = new Button();
            btnCaiDat = new Button();
            btnĐX = new Button();
            btnLichSu = new Button();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            pictureBox4 = new PictureBox();
            pictureBox5 = new PictureBox();
            pictureBox6 = new PictureBox();
            pnlUserProfile = new Panel();
            picAvatar = new PictureBox();
            lblUsername = new Label();
            lblUserRank = new Label();
            pnlOnlinePlayers = new Panel();
            lblOnlineTitle = new Label();
            lblOnlineCount = new Label();
            lstOnlinePlayers = new ListBox();

            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            pnlUserProfile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picAvatar).BeginInit();
            pnlOnlinePlayers.SuspendLayout();
            SuspendLayout();

            // 
            // frmDashboard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 217, 181); // #F0D9B5
            ClientSize = new Size(1345, 772);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 4, 3, 4);
            Name = "frmDashboard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Chess - Dashboard";
            Load += frmDashboard_Load;

            // 
            // pnlUserProfile (Top Left - User Profile)
            // 
            pnlUserProfile.BackColor = Color.FromArgb(118, 74, 61); // #764A3D
            pnlUserProfile.Location = new Point(30, 20);
            pnlUserProfile.Name = "pnlUserProfile";
            pnlUserProfile.Size = new Size(280, 110);
            pnlUserProfile.TabIndex = 100;
            pnlUserProfile.Controls.Add(picAvatar);
            pnlUserProfile.Controls.Add(lblUsername);
            pnlUserProfile.Controls.Add(lblUserRank);

            // 
            // picAvatar
            // 
            picAvatar.BackColor = Color.White;
            picAvatar.Location = new Point(12, 15);
            picAvatar.Name = "picAvatar";
            picAvatar.Size = new Size(75, 75);
            picAvatar.SizeMode = PictureBoxSizeMode.Zoom;
            picAvatar.TabIndex = 0;
            picAvatar.TabStop = false;

            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 13F, FontStyle.Bold, GraphicsUnit.Point);
            lblUsername.ForeColor = Color.White;
            lblUsername.Location = new Point(95, 25);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(120, 25);
            lblUsername.TabIndex = 1;
            lblUsername.Text = "Player Name";

            // 
            // lblUserRank
            // 
            lblUserRank.AutoSize = true;
            lblUserRank.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblUserRank.ForeColor = Color.FromArgb(255, 223, 186);
            lblUserRank.Location = new Point(95, 55);
            lblUserRank.Name = "lblUserRank";
            lblUserRank.Size = new Size(95, 15);
            lblUserRank.TabIndex = 2;
            lblUserRank.Text = "⭐ Rating: 1200";

            // 
            // btnCaiDat (Account Settings - Directly Below User Profile)
            // 
            btnCaiDat.BackColor = Color.FromArgb(133, 181, 100); // #85B564
            btnCaiDat.FlatAppearance.BorderSize = 0;
            btnCaiDat.FlatStyle = FlatStyle.Flat;
            btnCaiDat.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            btnCaiDat.ForeColor = Color.White;
            btnCaiDat.Location = new Point(30, 145);
            btnCaiDat.Margin = new Padding(3, 4, 3, 4);
            btnCaiDat.Name = "btnCaiDat";
            btnCaiDat.Size = new Size(280, 45);
            btnCaiDat.TabIndex = 15;
            btnCaiDat.Text = "⚙️ Cài Đặt Tài Khoản";
            btnCaiDat.UseVisualStyleBackColor = false;
            btnCaiDat.Click += btnCaiDat_Click;

            // 
            // btnChoi
            // 
            btnChoi.BackColor = Color.FromArgb(133, 181, 100);
            btnChoi.FlatAppearance.BorderSize = 0;
            btnChoi.FlatStyle = FlatStyle.Flat;
            btnChoi.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            btnChoi.ForeColor = Color.White;
            btnChoi.Location = new Point(400, 70);
            btnChoi.Margin = new Padding(3, 4, 3, 4);
            btnChoi.Name = "btnChoi";
            btnChoi.Size = new Size(284, 60);
            btnChoi.TabIndex = 0;
            btnChoi.Text = "Chơi Ngay";
            btnChoi.UseVisualStyleBackColor = false;
            btnChoi.Click += btnChoi_Click;

            // 
            // btnMay
            // 
            btnMay.BackColor = Color.FromArgb(160, 106, 88);
            btnMay.FlatAppearance.BorderSize = 0;
            btnMay.FlatStyle = FlatStyle.Flat;
            btnMay.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            btnMay.ForeColor = Color.White;
            btnMay.Location = new Point(400, 160);
            btnMay.Margin = new Padding(3, 4, 3, 4);
            btnMay.Name = "btnMay";
            btnMay.Size = new Size(284, 60);
            btnMay.TabIndex = 1;
            btnMay.Text = "Chơi Với Máy";
            btnMay.UseVisualStyleBackColor = false;
            btnMay.Click += btnMay_Click;

            // 
            // btnBan
            // 
            btnBan.BackColor = Color.FromArgb(160, 106, 88);
            btnBan.FlatAppearance.BorderSize = 0;
            btnBan.FlatStyle = FlatStyle.Flat;
            btnBan.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            btnBan.ForeColor = Color.White;
            btnBan.Location = new Point(400, 250);
            btnBan.Margin = new Padding(3, 4, 3, 4);
            btnBan.Name = "btnBan";
            btnBan.Size = new Size(284, 60);
            btnBan.TabIndex = 2;
            btnBan.Text = "Tạo Phòng";
            btnBan.UseVisualStyleBackColor = false;
            btnBan.Click += btnBan_Click;

            // 
            // btnBXH
            // 
            btnBXH.BackColor = Color.FromArgb(160, 106, 88);
            btnBXH.FlatAppearance.BorderSize = 0;
            btnBXH.FlatStyle = FlatStyle.Flat;
            btnBXH.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            btnBXH.ForeColor = Color.White;
            btnBXH.Location = new Point(400, 340);
            btnBXH.Margin = new Padding(3, 4, 3, 4);
            btnBXH.Name = "btnBXH";
            btnBXH.Size = new Size(284, 60);
            btnBXH.TabIndex = 3;
            btnBXH.Text = "Bảng Xếp Hạng";
            btnBXH.UseVisualStyleBackColor = false;
            btnBXH.Click += btnBXH_Click;

            // 
            // btnLichSu
            // 
            btnLichSu.BackColor = Color.FromArgb(160, 106, 88);
            btnLichSu.FlatAppearance.BorderSize = 0;
            btnLichSu.FlatStyle = FlatStyle.Flat;
            btnLichSu.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            btnLichSu.ForeColor = Color.White;
            btnLichSu.Location = new Point(400, 430);
            btnLichSu.Margin = new Padding(3, 4, 3, 4);
            btnLichSu.Name = "btnLichSu";
            btnLichSu.Size = new Size(284, 60);
            btnLichSu.TabIndex = 4;
            btnLichSu.Text = "Lịch Sử Trận Đấu";
            btnLichSu.UseVisualStyleBackColor = false;
            btnLichSu.Click += btnLichSu_Click;

            // 
            // pictureBox4 (icon for btnChoi)
            // 
            pictureBox4.BackColor = Color.FromArgb(133, 181, 100);
            pictureBox4.Location = new Point(345, 70);
            pictureBox4.Margin = new Padding(3, 4, 3, 4);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(52, 60);
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.TabIndex = 20;
            pictureBox4.TabStop = false;

            // 
            // pictureBox3 (icon for btnMay)
            // 
            pictureBox3.BackColor = Color.FromArgb(160, 106, 88);
            pictureBox3.Location = new Point(345, 160);
            pictureBox3.Margin = new Padding(3, 4, 3, 4);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(52, 60);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 19;
            pictureBox3.TabStop = false;

            // 
            // pictureBox5 (icon for btnBan)
            // 
            pictureBox5.BackColor = Color.FromArgb(160, 106, 88);
            pictureBox5.Location = new Point(345, 250);
            pictureBox5.Margin = new Padding(3, 4, 3, 4);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(52, 60);
            pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox5.TabIndex = 21;
            pictureBox5.TabStop = false;

            // 
            // pictureBox2 (icon for btnBXH)
            // 
            pictureBox2.BackColor = Color.FromArgb(160, 106, 88);
            pictureBox2.Location = new Point(345, 340);
            pictureBox2.Margin = new Padding(3, 4, 3, 4);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(52, 60);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 18;
            pictureBox2.TabStop = false;

            // 
            // pictureBox6 (icon for btnLichSu)
            // 
            pictureBox6.BackColor = Color.FromArgb(160, 106, 88);
            pictureBox6.Location = new Point(345, 430);
            pictureBox6.Margin = new Padding(3, 4, 3, 4);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(52, 60);
            pictureBox6.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox6.TabIndex = 24;
            pictureBox6.TabStop = false;

            // 
            // pnlOnlinePlayers (Right Panel - Online Players List)
            // 
            pnlOnlinePlayers.BackColor = Color.FromArgb(118, 74, 61);
            pnlOnlinePlayers.Location = new Point(730, 20);
            pnlOnlinePlayers.Name = "pnlOnlinePlayers";
            pnlOnlinePlayers.Size = new Size(590, 590);
            pnlOnlinePlayers.TabIndex = 101;
            pnlOnlinePlayers.Controls.Add(lblOnlineTitle);
            pnlOnlinePlayers.Controls.Add(lblOnlineCount);
            pnlOnlinePlayers.Controls.Add(lstOnlinePlayers);

            // 
            // lblOnlineTitle
            // 
            lblOnlineTitle.AutoSize = true;
            lblOnlineTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point);
            lblOnlineTitle.ForeColor = Color.White;
            lblOnlineTitle.Location = new Point(20, 18);
            lblOnlineTitle.Name = "lblOnlineTitle";
            lblOnlineTitle.Size = new Size(230, 28);
            lblOnlineTitle.TabIndex = 0;
            lblOnlineTitle.Text = "🟢 Người Chơi Online";

            // 
            // lblOnlineCount
            // 
            lblOnlineCount.AutoSize = true;
            lblOnlineCount.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblOnlineCount.ForeColor = Color.FromArgb(255, 223, 186);
            lblOnlineCount.Location = new Point(20, 52);
            lblOnlineCount.Name = "lblOnlineCount";
            lblOnlineCount.Size = new Size(130, 19);
            lblOnlineCount.TabIndex = 1;
            lblOnlineCount.Text = "5 người đang online";

            // 
            // lstOnlinePlayers
            // 
            lstOnlinePlayers.BackColor = Color.FromArgb(247, 234, 214);
            lstOnlinePlayers.BorderStyle = BorderStyle.None;
            lstOnlinePlayers.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            lstOnlinePlayers.ForeColor = Color.FromArgb(78, 49, 41);
            lstOnlinePlayers.ItemHeight = 20;
            lstOnlinePlayers.Location = new Point(20, 85);
            lstOnlinePlayers.Name = "lstOnlinePlayers";
            lstOnlinePlayers.Size = new Size(550, 480);
            lstOnlinePlayers.TabIndex = 2;

            // 
            // btnĐX (Logout Button - Bottom Right Corner)
            // 
            btnĐX.BackColor = Color.FromArgb(160, 106, 88); // #A06A58
            btnĐX.FlatAppearance.BorderSize = 0;
            btnĐX.FlatStyle = FlatStyle.Flat;
            btnĐX.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            btnĐX.ForeColor = Color.White;
            btnĐX.Location = new Point(1160, 710);
            btnĐX.Margin = new Padding(3, 4, 3, 4);
            btnĐX.Name = "btnĐX";
            btnĐX.Size = new Size(160, 45);
            btnĐX.TabIndex = 16;
            btnĐX.Text = "🚪 Đăng Xuất";
            btnĐX.UseVisualStyleBackColor = false;
            btnĐX.Click += btnĐX_Click;

            // 
            // btnThuGon (Hidden button)
            // 
            btnThuGon.Location = new Point(1393, 932);
            btnThuGon.Margin = new Padding(3, 4, 3, 4);
            btnThuGon.Name = "btnThuGon";
            btnThuGon.Size = new Size(48, 29);
            btnThuGon.TabIndex = 12;
            btnThuGon.Text = "button5";
            btnThuGon.UseVisualStyleBackColor = true;
            btnThuGon.Visible = false;

            Controls.Add(btnĐX);
            Controls.Add(pnlOnlinePlayers);
            Controls.Add(pictureBox6);
            Controls.Add(btnLichSu);
            Controls.Add(pictureBox5);
            Controls.Add(pictureBox4);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(btnCaiDat);
            Controls.Add(btnThuGon);
            Controls.Add(btnBan);
            Controls.Add(btnMay);
            Controls.Add(btnBXH);
            Controls.Add(btnChoi);
            Controls.Add(pnlUserProfile);

            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            pnlUserProfile.ResumeLayout(false);
            pnlUserProfile.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picAvatar).EndInit();
            pnlOnlinePlayers.ResumeLayout(false);
            pnlOnlinePlayers.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button btnChoi;
        private Button btnBXH;
        private Button btnMay;
        private Button btnBan;
        private Button btnThuGon;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private PictureBox pictureBox4;
        private PictureBox pictureBox5;
        private Button btnCaiDat;
        private Button btnĐX;
        private Button btnLichSu;
        private PictureBox pictureBox6;
        private Panel pnlUserProfile;
        private PictureBox picAvatar;
        private Label lblUsername;
        private Label lblUserRank;
        private Panel pnlOnlinePlayers;
        private Label lblOnlineTitle;
        private Label lblOnlineCount;
        private ListBox lstOnlinePlayers;
    }
}
