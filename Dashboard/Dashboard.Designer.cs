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

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDashboard));
            btnChoi = new Button();
            btnBXH = new Button();
            btnBan = new Button();
            btnThuGon = new Button();
            btnCaiDat = new Button();
            btnĐX = new Button();
            btnLichSu = new Button();
            pictureBox2 = new PictureBox();
            pictureBox4 = new PictureBox();
            pictureBox5 = new PictureBox();
            pictureBox6 = new PictureBox();
            pnlUserProfile = new Panel();
            lblUserRank = new Label();
            lblUsername = new Label();
            pnlOnlinePlayers = new Panel();
            lblOnlineTitle = new Label();
            lblOnlineCount = new Label();
            lstOnlinePlayers = new ListBox();
            pnlChat = new Panel();
            lblChatTitle = new Label();
            rtbChatMessages = new RichTextBox();
            txtChatInput = new TextBox();
            btnSendChat = new Button();
            btnEmoji = new Button();
            pnlEmojiPicker = new Panel();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            pnlUserProfile.SuspendLayout();
            pnlOnlinePlayers.SuspendLayout();
            pnlChat.SuspendLayout();
            SuspendLayout();
            // 
            // btnChoi
            // 
            btnChoi.BackColor = Color.FromArgb(133, 181, 100);
            btnChoi.FlatAppearance.BorderSize = 0;
            btnChoi.FlatStyle = FlatStyle.Flat;
            btnChoi.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
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
            // btnBXH
            // 
            btnBXH.BackColor = Color.FromArgb(160, 106, 88);
            btnBXH.FlatAppearance.BorderSize = 0;
            btnBXH.FlatStyle = FlatStyle.Flat;
            btnBXH.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnBXH.ForeColor = Color.White;
            btnBXH.Location = new Point(400, 252);
            btnBXH.Margin = new Padding(3, 4, 3, 4);
            btnBXH.Name = "btnBXH";
            btnBXH.Size = new Size(284, 60);
            btnBXH.TabIndex = 3;
            btnBXH.Text = "Bảng Xếp Hạng";
            btnBXH.UseVisualStyleBackColor = false;
            btnBXH.Click += btnBXH_Click;
            // 
            // btnBan
            // 
            btnBan.BackColor = Color.FromArgb(160, 106, 88);
            btnBan.FlatAppearance.BorderSize = 0;
            btnBan.FlatStyle = FlatStyle.Flat;
            btnBan.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnBan.ForeColor = Color.White;
            btnBan.Location = new Point(400, 162);
            btnBan.Margin = new Padding(3, 4, 3, 4);
            btnBan.Name = "btnBan";
            btnBan.Size = new Size(284, 60);
            btnBan.TabIndex = 2;
            btnBan.Text = "Phòng Chơi";
            btnBan.UseVisualStyleBackColor = false;
            btnBan.Click += btnBan_Click;
            // 
            // btnThuGon
            // 
            btnThuGon.Location = new Point(1393, 932);
            btnThuGon.Margin = new Padding(3, 4, 3, 4);
            btnThuGon.Name = "btnThuGon";
            btnThuGon.Size = new Size(48, 29);
            btnThuGon.TabIndex = 12;
            btnThuGon.Text = "button5";
            btnThuGon.UseVisualStyleBackColor = true;
            btnThuGon.Visible = false;
            // 
            // btnCaiDat
            // 
            btnCaiDat.BackColor = Color.FromArgb(133, 181, 100);
            btnCaiDat.FlatAppearance.BorderSize = 0;
            btnCaiDat.FlatStyle = FlatStyle.Flat;
            btnCaiDat.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnCaiDat.ForeColor = Color.White;
            btnCaiDat.Location = new Point(30, 145);
            btnCaiDat.Margin = new Padding(3, 4, 3, 4);
            btnCaiDat.Name = "btnCaiDat";
            btnCaiDat.Size = new Size(280, 45);
            btnCaiDat.TabIndex = 15;
            btnCaiDat.Text = "Cài Đặt Tài Khoản";
            btnCaiDat.UseVisualStyleBackColor = false;
            btnCaiDat.Click += btnCaiDat_Click;
            // 
            // btnĐX
            // 
            btnĐX.BackColor = Color.FromArgb(160, 106, 88);
            btnĐX.FlatAppearance.BorderSize = 0;
            btnĐX.FlatStyle = FlatStyle.Flat;
            btnĐX.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnĐX.ForeColor = Color.White;
            btnĐX.Location = new Point(1160, 710);
            btnĐX.Margin = new Padding(3, 4, 3, 4);
            btnĐX.Name = "btnĐX";
            btnĐX.Size = new Size(160, 45);
            btnĐX.TabIndex = 16;
            btnĐX.Text = " Đăng Xuất";
            btnĐX.UseVisualStyleBackColor = false;
            btnĐX.Click += btnĐX_Click;
            // 
            // btnLichSu
            // 
            btnLichSu.BackColor = Color.FromArgb(160, 106, 88);
            btnLichSu.FlatAppearance.BorderSize = 0;
            btnLichSu.FlatStyle = FlatStyle.Flat;
            btnLichSu.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnLichSu.ForeColor = Color.White;
            btnLichSu.Location = new Point(400, 342);
            btnLichSu.Margin = new Padding(3, 4, 3, 4);
            btnLichSu.Name = "btnLichSu";
            btnLichSu.Size = new Size(284, 60);
            btnLichSu.TabIndex = 4;
            btnLichSu.Text = "Lịch Sử Trận Đấu";
            btnLichSu.UseVisualStyleBackColor = false;
            btnLichSu.Click += btnLichSu_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.FromArgb(160, 106, 88);
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(345, 252);
            pictureBox2.Margin = new Padding(3, 4, 3, 4);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(52, 60);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 18;
            pictureBox2.TabStop = false;
            // 
            // pictureBox4
            // 
            pictureBox4.BackColor = Color.FromArgb(133, 181, 100);
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(345, 70);
            pictureBox4.Margin = new Padding(3, 4, 3, 4);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(52, 60);
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.TabIndex = 20;
            pictureBox4.TabStop = false;
            // 
            // pictureBox5
            // 
            pictureBox5.BackColor = Color.FromArgb(160, 106, 88);
            pictureBox5.Image = (Image)resources.GetObject("pictureBox5.Image");
            pictureBox5.Location = new Point(345, 162);
            pictureBox5.Margin = new Padding(3, 4, 3, 4);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(52, 60);
            pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox5.TabIndex = 21;
            pictureBox5.TabStop = false;
            // 
            // pictureBox6
            // 
            pictureBox6.BackColor = Color.FromArgb(160, 106, 88);
            pictureBox6.Image = (Image)resources.GetObject("pictureBox6.Image");
            pictureBox6.Location = new Point(345, 342);
            pictureBox6.Margin = new Padding(3, 4, 3, 4);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(52, 60);
            pictureBox6.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox6.TabIndex = 24;
            pictureBox6.TabStop = false;
            // 
            // pnlUserProfile
            // 
            pnlUserProfile.BackColor = Color.FromArgb(118, 74, 61);
            pnlUserProfile.Controls.Add(lblUserRank);
            pnlUserProfile.Controls.Add(lblUsername);
            pnlUserProfile.Location = new Point(30, 20);
            pnlUserProfile.Name = "pnlUserProfile";
            pnlUserProfile.Size = new Size(280, 110);
            pnlUserProfile.TabIndex = 100;
            // 
            // lblUserRank
            // 
            lblUserRank.Dock = DockStyle.Top;
            lblUserRank.Font = new Font("Segoe UI", 9F);
            lblUserRank.ForeColor = Color.FromArgb(255, 223, 186);
            lblUserRank.Location = new Point(0, 70);
            lblUserRank.Name = "lblUserRank";
            lblUserRank.Size = new Size(280, 40);
            lblUserRank.TabIndex = 2;
            lblUserRank.Text = "Rating: 1200";
            lblUserRank.TextAlign = ContentAlignment.TopCenter;
            // 
            // lblUsername
            // 
            lblUsername.Dock = DockStyle.Top;
            lblUsername.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblUsername.ForeColor = Color.White;
            lblUsername.Location = new Point(0, 0);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(280, 70);
            lblUsername.TabIndex = 1;
            lblUsername.Text = "Player Name";
            lblUsername.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlOnlinePlayers
            // 
            pnlOnlinePlayers.BackColor = Color.FromArgb(118, 74, 61);
            pnlOnlinePlayers.Controls.Add(lblOnlineTitle);
            pnlOnlinePlayers.Controls.Add(lblOnlineCount);
            pnlOnlinePlayers.Controls.Add(lstOnlinePlayers);
            pnlOnlinePlayers.Location = new Point(730, 20);
            pnlOnlinePlayers.Name = "pnlOnlinePlayers";
            pnlOnlinePlayers.Size = new Size(590, 590);
            pnlOnlinePlayers.TabIndex = 101;
            // 
            // lblOnlineTitle
            // 
            lblOnlineTitle.AutoSize = true;
            lblOnlineTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblOnlineTitle.ForeColor = Color.White;
            lblOnlineTitle.Location = new Point(20, 18);
            lblOnlineTitle.Name = "lblOnlineTitle";
            lblOnlineTitle.Size = new Size(235, 35);
            lblOnlineTitle.TabIndex = 0;
            lblOnlineTitle.Text = "Người Chơi Online";
            // 
            // lblOnlineCount
            // 
            lblOnlineCount.AutoSize = true;
            lblOnlineCount.Font = new Font("Segoe UI", 10F);
            lblOnlineCount.ForeColor = Color.FromArgb(255, 223, 186);
            lblOnlineCount.Location = new Point(20, 52);
            lblOnlineCount.Name = "lblOnlineCount";
            lblOnlineCount.Size = new Size(164, 23);
            lblOnlineCount.TabIndex = 1;
            lblOnlineCount.Text = "5 người đang online";
            lblOnlineCount.Click += lblOnlineCount_Click;
            // 
            // lstOnlinePlayers
            // 
            lstOnlinePlayers.BackColor = Color.FromArgb(247, 234, 214);
            lstOnlinePlayers.BorderStyle = BorderStyle.None;
            lstOnlinePlayers.Font = new Font("Segoe UI", 11F);
            lstOnlinePlayers.ForeColor = Color.FromArgb(78, 49, 41);
            lstOnlinePlayers.ItemHeight = 25;
            lstOnlinePlayers.Location = new Point(20, 85);
            lstOnlinePlayers.Name = "lstOnlinePlayers";
            lstOnlinePlayers.Size = new Size(550, 475);
            lstOnlinePlayers.TabIndex = 2;
            lstOnlinePlayers.SelectedIndexChanged += lstOnlinePlayers_SelectedIndexChanged;
            // 
            // pnlChat
            // 
            pnlChat.BackColor = Color.FromArgb(118, 74, 61);
            pnlChat.Controls.Add(lblChatTitle);
            pnlChat.Controls.Add(rtbChatMessages);
            pnlChat.Controls.Add(txtChatInput);
            pnlChat.Controls.Add(btnSendChat);
            pnlChat.Controls.Add(btnEmoji);
            pnlChat.Controls.Add(pnlEmojiPicker);
            pnlChat.Location = new Point(30, 210);
            pnlChat.Name = "pnlChat";
            pnlChat.Size = new Size(280, 560);
            pnlChat.TabIndex = 102;
            // 
            // lblChatTitle
            // 
            lblChatTitle.AutoSize = true;
            lblChatTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblChatTitle.ForeColor = Color.White;
            lblChatTitle.Location = new Point(15, 15);
            lblChatTitle.Name = "lblChatTitle";
            lblChatTitle.Size = new Size(106, 30);
            lblChatTitle.TabIndex = 0;
            lblChatTitle.Text = "Chat Box";
            // 
            // rtbChatMessages
            // 
            rtbChatMessages.BackColor = Color.FromArgb(247, 234, 214);
            rtbChatMessages.BorderStyle = BorderStyle.None;
            rtbChatMessages.Font = new Font("Segoe UI", 9F);
            rtbChatMessages.ForeColor = Color.FromArgb(78, 49, 41);
            rtbChatMessages.Location = new Point(15, 50);
            rtbChatMessages.Name = "rtbChatMessages";
            rtbChatMessages.ReadOnly = true;
            rtbChatMessages.Size = new Size(250, 406);
            rtbChatMessages.TabIndex = 1;
            rtbChatMessages.Text = "";
            // 
            // txtChatInput
            // 
            txtChatInput.BackColor = Color.FromArgb(247, 234, 214);
            txtChatInput.BorderStyle = BorderStyle.None;
            txtChatInput.Font = new Font("Segoe UI", 10F);
            txtChatInput.ForeColor = Color.FromArgb(78, 49, 41);
            txtChatInput.Location = new Point(17, 462);
            txtChatInput.Multiline = true;
            txtChatInput.Name = "txtChatInput";
            txtChatInput.PlaceholderText = "Nhập tin nhắn...";
            txtChatInput.Size = new Size(200, 30);
            txtChatInput.TabIndex = 2;
            txtChatInput.KeyDown += txtChatInput_KeyDown;
            // 
            // btnSendChat
            // 
            btnSendChat.BackColor = Color.FromArgb(133, 181, 100);
            btnSendChat.FlatAppearance.BorderSize = 0;
            btnSendChat.FlatStyle = FlatStyle.Flat;
            btnSendChat.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSendChat.ForeColor = Color.White;
            btnSendChat.Location = new Point(223, 465);
            btnSendChat.Name = "btnSendChat";
            btnSendChat.Size = new Size(54, 80);
            btnSendChat.TabIndex = 4;
            btnSendChat.Text = "Gửi";
            btnSendChat.UseVisualStyleBackColor = false;
            btnSendChat.Click += btnSendChat_Click;
            // 
            // btnEmoji
            // 
            btnEmoji.BackColor = Color.FromArgb(160, 106, 88);
            btnEmoji.FlatAppearance.BorderSize = 0;
            btnEmoji.FlatStyle = FlatStyle.Flat;
            btnEmoji.Font = new Font("Segoe UI", 12F);
            btnEmoji.ForeColor = Color.White;
            btnEmoji.Location = new Point(15, 505);
            btnEmoji.Name = "btnEmoji";
            btnEmoji.Size = new Size(40, 40);
            btnEmoji.TabIndex = 3;
            btnEmoji.Text = "😊";
            btnEmoji.UseVisualStyleBackColor = false;
            btnEmoji.Click += btnEmoji_Click;
            // 
            // pnlEmojiPicker
            // 
            pnlEmojiPicker.AutoScroll = true;
            pnlEmojiPicker.BackColor = Color.FromArgb(247, 234, 214);
            pnlEmojiPicker.BorderStyle = BorderStyle.FixedSingle;
            pnlEmojiPicker.Location = new Point(15, 300);
            pnlEmojiPicker.Name = "pnlEmojiPicker";
            pnlEmojiPicker.Size = new Size(250, 180);
            pnlEmojiPicker.TabIndex = 5;
            pnlEmojiPicker.Visible = false;
            // 
            // frmDashboard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 217, 181);
            ClientSize = new Size(1380, 792);
            Controls.Add(pnlChat);
            Controls.Add(btnĐX);
            Controls.Add(pnlOnlinePlayers);
            Controls.Add(pictureBox6);
            Controls.Add(btnLichSu);
            Controls.Add(pictureBox5);
            Controls.Add(pictureBox4);
            Controls.Add(pictureBox2);
            Controls.Add(btnCaiDat);
            Controls.Add(btnThuGon);
            Controls.Add(btnBan);
            Controls.Add(btnBXH);
            Controls.Add(btnChoi);
            Controls.Add(pnlUserProfile);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 4, 3, 4);
            Name = "frmDashboard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Dashboard";
            Load += frmDashboard_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            pnlUserProfile.ResumeLayout(false);
            pnlOnlinePlayers.ResumeLayout(false);
            pnlOnlinePlayers.PerformLayout();
            pnlChat.ResumeLayout(false);
            pnlChat.PerformLayout();
            ResumeLayout(false);
        }

        private void AddEmojiToPanel()
        {
            string[] emojis = { "😊", "😂", "😍", "😎", "😢", "😡", "👍", "❤️", "🎉", "🏆", "♟️", "🔥" };
            int x = 5;
            int y = 5;
            int btnSize = 35;
            int spacing = 5;
            int columns = 6;

            for (int i = 0; i < emojis.Length; i++)
            {
                Button btnEmojiItem = new Button();
                btnEmojiItem.BackColor = System.Drawing.Color.White;
                btnEmojiItem.FlatStyle = FlatStyle.Flat;
                btnEmojiItem.FlatAppearance.BorderSize = 0;
                btnEmojiItem.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
                btnEmojiItem.Size = new System.Drawing.Size(btnSize, btnSize);
                btnEmojiItem.Location = new System.Drawing.Point(x + (i % columns) * (btnSize + spacing),
                                                   y + (i / columns) * (btnSize + spacing));
                btnEmojiItem.Text = emojis[i];
                btnEmojiItem.UseVisualStyleBackColor = false;

                pnlEmojiPicker.Controls.Add(btnEmojiItem);
            }
        }

        private Button btnChoi;
        private Button btnBXH;
        private Button btnBan;
        private Button btnThuGon;
        private PictureBox pictureBox2;
        private PictureBox pictureBox4;
        private PictureBox pictureBox5;
        private Button btnCaiDat;
        private Button btnĐX;
        private Button btnLichSu;
        private PictureBox pictureBox6;
        private Panel pnlUserProfile;
        private Label lblUsername;
        private Label lblUserRank;
        private Panel pnlOnlinePlayers;
        private Label lblOnlineTitle;
        private Label lblOnlineCount;
        private ListBox lstOnlinePlayers;
        private Panel pnlChat;
        private Label lblChatTitle;
        private RichTextBox rtbChatMessages;
        private TextBox txtChatInput;
        private Button btnSendChat;
        private Button btnEmoji;
        private Panel pnlEmojiPicker;
    }
}
