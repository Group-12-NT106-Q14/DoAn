namespace ChessGame
{
    partial class InRoom
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InRoom));
            lblTitle = new Label();
            pnlConfig = new Panel();
            lblConfigRoomName = new Label();
            txtConfigRoomName = new TextBox();
            btnRenameRoom = new Button();
            lblConfigTitle = new Label();
            lblChooseSide = new Label();
            radioWhite = new RadioButton();
            radioBlack = new RadioButton();
            lblTime = new Label();
            btn1min = new Button();
            btn3min = new Button();
            btn6min = new Button();
            btn10min = new Button();
            lblCustomMin = new Label();
            txtCustomMin = new TextBox();
            lblFischer = new Label();
            txtFischer = new TextBox();
            pnlPlayerA = new Panel();
            lblPlayerATitle = new Label();
            lblPlayerAName = new Label();
            lblPlayerARating = new Label();
            pnlPlayerB = new Panel();
            lblPlayerBTitle = new Label();
            lblPlayerBName = new Label();
            lblPlayerBRating = new Label();
            pnlChat = new Panel();
            pnlEmojiPicker = new Panel();
            lblChatTitle = new Label();
            rtbChat = new RichTextBox();
            txtChat = new TextBox();
            btnSendChat = new Button();
            btnEmoji = new Button();
            btnStartGame = new Button();
            btnLeaveRoom = new Button();
            pnlTimeControl = new Panel();
            lblTimeControlTitle = new Label();
            pnlConfig.SuspendLayout();
            pnlPlayerA.SuspendLayout();
            pnlPlayerB.SuspendLayout();
            pnlChat.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(78, 49, 41);
            lblTitle.Location = new Point(503, 37);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(0, 50);
            lblTitle.TabIndex = 0;
            // 
            // pnlConfig
            // 
            pnlConfig.BackColor = Color.FromArgb(118, 74, 61);
            pnlConfig.Controls.Add(lblConfigRoomName);
            pnlConfig.Controls.Add(txtConfigRoomName);
            pnlConfig.Controls.Add(btnRenameRoom);
            pnlConfig.Controls.Add(lblConfigTitle);
            pnlConfig.Controls.Add(lblChooseSide);
            pnlConfig.Controls.Add(radioWhite);
            pnlConfig.Controls.Add(radioBlack);
            pnlConfig.Controls.Add(lblTime);
            pnlConfig.Controls.Add(btn1min);
            pnlConfig.Controls.Add(btn3min);
            pnlConfig.Controls.Add(btn6min);
            pnlConfig.Controls.Add(btn10min);
            pnlConfig.Controls.Add(lblCustomMin);
            pnlConfig.Controls.Add(txtCustomMin);
            pnlConfig.Controls.Add(lblFischer);
            pnlConfig.Controls.Add(txtFischer);
            pnlConfig.Location = new Point(30, 110);
            pnlConfig.Name = "pnlConfig";
            pnlConfig.Size = new Size(320, 620);
            pnlConfig.TabIndex = 0;
            // 
            // lblConfigRoomName
            // 
            lblConfigRoomName.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblConfigRoomName.ForeColor = Color.White;
            lblConfigRoomName.Location = new Point(11, 10);
            lblConfigRoomName.Name = "lblConfigRoomName";
            lblConfigRoomName.Size = new Size(131, 28);
            lblConfigRoomName.TabIndex = 0;
            lblConfigRoomName.Text = "Tên phòng:";
            // 
            // txtConfigRoomName
            // 
            txtConfigRoomName.Font = new Font("Segoe UI", 11F);
            txtConfigRoomName.Location = new Point(11, 41);
            txtConfigRoomName.Name = "txtConfigRoomName";
            txtConfigRoomName.Size = new Size(301, 32);
            txtConfigRoomName.TabIndex = 1;
            // 
            // btnRenameRoom
            // 
            btnRenameRoom.BackColor = Color.FromArgb(133, 181, 100);
            btnRenameRoom.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnRenameRoom.ForeColor = Color.White;
            btnRenameRoom.Location = new Point(11, 79);
            btnRenameRoom.Name = "btnRenameRoom";
            btnRenameRoom.Size = new Size(84, 43);
            btnRenameRoom.TabIndex = 2;
            btnRenameRoom.Text = "Đổi tên";
            btnRenameRoom.UseVisualStyleBackColor = false;
            // 
            // lblConfigTitle
            // 
            lblConfigTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblConfigTitle.ForeColor = Color.White;
            lblConfigTitle.Location = new Point(11, 135);
            lblConfigTitle.Name = "lblConfigTitle";
            lblConfigTitle.Size = new Size(260, 42);
            lblConfigTitle.TabIndex = 3;
            lblConfigTitle.Text = "Thiết lập ván đấu (Chủ phòng)";
            // 
            // lblChooseSide
            // 
            lblChooseSide.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblChooseSide.ForeColor = Color.White;
            lblChooseSide.Location = new Point(11, 185);
            lblChooseSide.Name = "lblChooseSide";
            lblChooseSide.Size = new Size(287, 37);
            lblChooseSide.TabIndex = 4;
            lblChooseSide.Text = "Chọn bên chủ phòng chơi:";
            // 
            // radioWhite
            // 
            radioWhite.BackColor = Color.Transparent;
            radioWhite.Font = new Font("Segoe UI", 12F);
            radioWhite.ForeColor = Color.White;
            radioWhite.Location = new Point(31, 225);
            radioWhite.Name = "radioWhite";
            radioWhite.Size = new Size(87, 39);
            radioWhite.TabIndex = 5;
            radioWhite.Text = "Trắng";
            radioWhite.UseVisualStyleBackColor = false;
            // 
            // radioBlack
            // 
            radioBlack.BackColor = Color.Transparent;
            radioBlack.Font = new Font("Segoe UI", 12F);
            radioBlack.ForeColor = Color.White;
            radioBlack.Location = new Point(141, 225);
            radioBlack.Name = "radioBlack";
            radioBlack.Size = new Size(94, 39);
            radioBlack.TabIndex = 6;
            radioBlack.Text = "Đen";
            radioBlack.UseVisualStyleBackColor = false;
            // 
            // lblTime
            // 
            lblTime.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTime.ForeColor = Color.White;
            lblTime.Location = new Point(11, 265);
            lblTime.Name = "lblTime";
            lblTime.Size = new Size(287, 66);
            lblTime.TabIndex = 4;
            lblTime.Text = "Chọn thời gian mỗi bên (Tính bằng phút):";
            // 
            // btn1min
            // 
            btn1min.BackColor = Color.White;
            btn1min.FlatStyle = FlatStyle.Flat;
            btn1min.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btn1min.ForeColor = Color.FromArgb(78, 49, 41);
            btn1min.Location = new Point(11, 343);
            btn1min.Name = "btn1min";
            btn1min.Size = new Size(70, 32);
            btn1min.TabIndex = 5;
            btn1min.Text = "1 phút";
            btn1min.UseVisualStyleBackColor = false;
            // 
            // btn3min
            // 
            btn3min.BackColor = Color.White;
            btn3min.FlatStyle = FlatStyle.Flat;
            btn3min.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btn3min.ForeColor = Color.FromArgb(78, 49, 41);
            btn3min.Location = new Point(96, 343);
            btn3min.Name = "btn3min";
            btn3min.Size = new Size(70, 32);
            btn3min.TabIndex = 6;
            btn3min.Text = "3 phút";
            btn3min.UseVisualStyleBackColor = false;
            // 
            // btn6min
            // 
            btn6min.BackColor = Color.White;
            btn6min.FlatStyle = FlatStyle.Flat;
            btn6min.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btn6min.ForeColor = Color.FromArgb(78, 49, 41);
            btn6min.Location = new Point(181, 343);
            btn6min.Name = "btn6min";
            btn6min.Size = new Size(70, 32);
            btn6min.TabIndex = 7;
            btn6min.Text = "6 phút";
            btn6min.UseVisualStyleBackColor = false;
            // 
            // btn10min
            // 
            btn10min.BackColor = Color.White;
            btn10min.FlatStyle = FlatStyle.Flat;
            btn10min.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btn10min.ForeColor = Color.FromArgb(78, 49, 41);
            btn10min.Location = new Point(11, 388);
            btn10min.Name = "btn10min";
            btn10min.Size = new Size(85, 32);
            btn10min.TabIndex = 8;
            btn10min.Text = "10 phút";
            btn10min.UseVisualStyleBackColor = false;
            // 
            // lblCustomMin
            // 
            lblCustomMin.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblCustomMin.ForeColor = Color.White;
            lblCustomMin.Location = new Point(111, 396);
            lblCustomMin.Name = "lblCustomMin";
            lblCustomMin.Size = new Size(110, 29);
            lblCustomMin.TabIndex = 9;
            lblCustomMin.Text = "Tùy chọn:";
            // 
            // txtCustomMin
            // 
            txtCustomMin.Font = new Font("Segoe UI", 11F);
            txtCustomMin.Location = new Point(227, 393);
            txtCustomMin.Name = "txtCustomMin";
            txtCustomMin.PlaceholderText = "phút";
            txtCustomMin.Size = new Size(60, 32);
            txtCustomMin.TabIndex = 10;
            // 
            // lblFischer
            // 
            lblFischer.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblFischer.ForeColor = Color.White;
            lblFischer.Location = new Point(11, 443);
            lblFischer.Name = "lblFischer";
            lblFischer.Size = new Size(174, 58);
            lblFischer.TabIndex = 11;
            lblFischer.Text = "Cộng giây mỗi nước:";
            // 
            // txtFischer
            // 
            txtFischer.Font = new Font("Segoe UI", 11F);
            txtFischer.Location = new Point(195, 454);
            txtFischer.Name = "txtFischer";
            txtFischer.PlaceholderText = "0";
            txtFischer.Size = new Size(40, 32);
            txtFischer.TabIndex = 12;
            // 
            // pnlPlayerA
            // 
            pnlPlayerA.BackColor = Color.FromArgb(118, 74, 61);
            pnlPlayerA.Controls.Add(lblPlayerATitle);
            pnlPlayerA.Controls.Add(lblPlayerAName);
            pnlPlayerA.Controls.Add(lblPlayerARating);
            pnlPlayerA.Location = new Point(370, 110);
            pnlPlayerA.Name = "pnlPlayerA";
            pnlPlayerA.Size = new Size(280, 350);
            pnlPlayerA.TabIndex = 3;
            // 
            // lblPlayerATitle
            // 
            lblPlayerATitle.AutoSize = true;
            lblPlayerATitle.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold);
            lblPlayerATitle.ForeColor = Color.White;
            lblPlayerATitle.Location = new Point(62, 20);
            lblPlayerATitle.Name = "lblPlayerATitle";
            lblPlayerATitle.Size = new Size(152, 31);
            lblPlayerATitle.TabIndex = 0;
            lblPlayerATitle.Text = "CHỦ PHÒNG";
            // 
            // lblPlayerAName
            // 
            lblPlayerAName.AutoSize = true;
            lblPlayerAName.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblPlayerAName.ForeColor = Color.White;
            lblPlayerAName.Location = new Point(73, 141);
            lblPlayerAName.Name = "lblPlayerAName";
            lblPlayerAName.Size = new Size(145, 30);
            lblPlayerAName.TabIndex = 2;
            lblPlayerAName.Text = "Player Name";
            // 
            // lblPlayerARating
            // 
            lblPlayerARating.AutoSize = true;
            lblPlayerARating.Font = new Font("Segoe UI", 11F);
            lblPlayerARating.ForeColor = Color.FromArgb(255, 223, 186);
            lblPlayerARating.Location = new Point(78, 176);
            lblPlayerARating.Name = "lblPlayerARating";
            lblPlayerARating.Size = new Size(136, 25);
            lblPlayerARating.TabIndex = 3;
            lblPlayerARating.Text = "⭐ Rating: 1200";
            // 
            // pnlPlayerB
            // 
            pnlPlayerB.BackColor = Color.FromArgb(118, 74, 61);
            pnlPlayerB.Controls.Add(lblPlayerBTitle);
            pnlPlayerB.Controls.Add(lblPlayerBName);
            pnlPlayerB.Controls.Add(lblPlayerBRating);
            pnlPlayerB.Location = new Point(670, 110);
            pnlPlayerB.Name = "pnlPlayerB";
            pnlPlayerB.Size = new Size(280, 350);
            pnlPlayerB.TabIndex = 4;
            // 
            // lblPlayerBTitle
            // 
            lblPlayerBTitle.AutoSize = true;
            lblPlayerBTitle.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold);
            lblPlayerBTitle.ForeColor = Color.White;
            lblPlayerBTitle.Location = new Point(91, 20);
            lblPlayerBTitle.Name = "lblPlayerBTitle";
            lblPlayerBTitle.Size = new Size(95, 31);
            lblPlayerBTitle.TabIndex = 0;
            lblPlayerBTitle.Text = "KHÁCH";
            // 
            // lblPlayerBName
            // 
            lblPlayerBName.AutoSize = true;
            lblPlayerBName.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblPlayerBName.ForeColor = Color.FromArgb(200, 200, 200);
            lblPlayerBName.Location = new Point(80, 141);
            lblPlayerBName.Name = "lblPlayerBName";
            lblPlayerBName.Size = new Size(130, 30);
            lblPlayerBName.TabIndex = 2;
            lblPlayerBName.Text = "Đang chờ...";
            // 
            // lblPlayerBRating
            // 
            lblPlayerBRating.AutoSize = true;
            lblPlayerBRating.Font = new Font("Segoe UI", 11F);
            lblPlayerBRating.ForeColor = Color.FromArgb(200, 200, 200);
            lblPlayerBRating.Location = new Point(105, 176);
            lblPlayerBRating.Name = "lblPlayerBRating";
            lblPlayerBRating.Size = new Size(57, 25);
            lblPlayerBRating.TabIndex = 3;
            lblPlayerBRating.Text = "⭐ ---";
            lblPlayerBRating.Visible = false;
            // 
            // pnlChat
            // 
            pnlChat.BackColor = Color.FromArgb(118, 74, 61);
            pnlChat.Controls.Add(pnlEmojiPicker);
            pnlChat.Controls.Add(lblChatTitle);
            pnlChat.Controls.Add(rtbChat);
            pnlChat.Controls.Add(txtChat);
            pnlChat.Controls.Add(btnSendChat);
            pnlChat.Controls.Add(btnEmoji);
            pnlChat.Location = new Point(970, 110);
            pnlChat.Name = "pnlChat";
            pnlChat.Size = new Size(373, 620);
            pnlChat.TabIndex = 5;
            // 
            // pnlEmojiPicker
            // 
            pnlEmojiPicker.AutoScroll = true;
            pnlEmojiPicker.BackColor = Color.FromArgb(247, 234, 214);
            pnlEmojiPicker.BorderStyle = BorderStyle.FixedSingle;
            pnlEmojiPicker.Location = new Point(9, 390);
            pnlEmojiPicker.Name = "pnlEmojiPicker";
            pnlEmojiPicker.Size = new Size(359, 170);
            pnlEmojiPicker.TabIndex = 5;
            pnlEmojiPicker.Visible = false;
            // 
            // lblChatTitle
            // 
            lblChatTitle.AutoSize = true;
            lblChatTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblChatTitle.ForeColor = Color.White;
            lblChatTitle.Location = new Point(20, 20);
            lblChatTitle.Name = "lblChatTitle";
            lblChatTitle.Size = new Size(146, 32);
            lblChatTitle.TabIndex = 0;
            lblChatTitle.Text = "Chat Phòng";
            // 
            // rtbChat
            // 
            rtbChat.BackColor = Color.FromArgb(247, 234, 214);
            rtbChat.BorderStyle = BorderStyle.None;
            rtbChat.Font = new Font("Segoe UI", 9F);
            rtbChat.ForeColor = Color.FromArgb(78, 49, 41);
            rtbChat.Location = new Point(9, 60);
            rtbChat.Name = "rtbChat";
            rtbChat.ReadOnly = true;
            rtbChat.Size = new Size(359, 500);
            rtbChat.TabIndex = 1;
            rtbChat.Text = "";
            // 
            // txtChat
            // 
            txtChat.BackColor = Color.FromArgb(247, 234, 214);
            txtChat.BorderStyle = BorderStyle.None;
            txtChat.Font = new Font("Segoe UI", 10F);
            txtChat.ForeColor = Color.FromArgb(78, 49, 41);
            txtChat.Location = new Point(9, 576);
            txtChat.Name = "txtChat";
            txtChat.PlaceholderText = "Nhập tin nhắn...";
            txtChat.Size = new Size(250, 23);
            txtChat.TabIndex = 2;
            // 
            // btnSendChat
            // 
            btnSendChat.BackColor = Color.FromArgb(133, 181, 100);
            btnSendChat.FlatAppearance.BorderSize = 0;
            btnSendChat.FlatStyle = FlatStyle.Flat;
            btnSendChat.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSendChat.ForeColor = Color.White;
            btnSendChat.Location = new Point(306, 563);
            btnSendChat.Name = "btnSendChat";
            btnSendChat.Size = new Size(64, 54);
            btnSendChat.TabIndex = 4;
            btnSendChat.Text = "Gửi";
            btnSendChat.UseVisualStyleBackColor = false;
            // 
            // btnEmoji
            // 
            btnEmoji.BackColor = Color.FromArgb(160, 106, 88);
            btnEmoji.FlatAppearance.BorderSize = 0;
            btnEmoji.FlatStyle = FlatStyle.Flat;
            btnEmoji.Font = new Font("Segoe UI", 11F);
            btnEmoji.ForeColor = Color.White;
            btnEmoji.Location = new Point(265, 566);
            btnEmoji.Name = "btnEmoji";
            btnEmoji.Size = new Size(35, 51);
            btnEmoji.TabIndex = 3;
            btnEmoji.Text = "😊";
            btnEmoji.UseVisualStyleBackColor = false;
            // 
            // btnStartGame
            // 
            btnStartGame.BackColor = Color.FromArgb(133, 181, 100);
            btnStartGame.FlatAppearance.BorderSize = 0;
            btnStartGame.FlatStyle = FlatStyle.Flat;
            btnStartGame.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnStartGame.ForeColor = Color.White;
            btnStartGame.Location = new Point(370, 480);
            btnStartGame.Name = "btnStartGame";
            btnStartGame.Size = new Size(280, 60);
            btnStartGame.TabIndex = 6;
            btnStartGame.Text = "BẮT ĐẦU CHƠI";
            btnStartGame.UseVisualStyleBackColor = false;
            // 
            // btnLeaveRoom
            // 
            btnLeaveRoom.BackColor = Color.FromArgb(160, 106, 88);
            btnLeaveRoom.FlatAppearance.BorderSize = 0;
            btnLeaveRoom.FlatStyle = FlatStyle.Flat;
            btnLeaveRoom.Font = new Font("Segoe UI", 14F);
            btnLeaveRoom.ForeColor = Color.White;
            btnLeaveRoom.Location = new Point(670, 480);
            btnLeaveRoom.Name = "btnLeaveRoom";
            btnLeaveRoom.Size = new Size(280, 60);
            btnLeaveRoom.TabIndex = 7;
            btnLeaveRoom.Text = "RỜI PHÒNG";
            btnLeaveRoom.UseVisualStyleBackColor = false;
            // 
            // pnlTimeControl
            // 
            pnlTimeControl.Location = new Point(0, 0);
            pnlTimeControl.Name = "pnlTimeControl";
            pnlTimeControl.Size = new Size(200, 100);
            pnlTimeControl.TabIndex = 0;
            // 
            // lblTimeControlTitle
            // 
            lblTimeControlTitle.Location = new Point(0, 0);
            lblTimeControlTitle.Name = "lblTimeControlTitle";
            lblTimeControlTitle.Size = new Size(100, 23);
            lblTimeControlTitle.TabIndex = 0;
            // 
            // InRoom
            // 
            BackColor = Color.FromArgb(240, 217, 181);
            ClientSize = new Size(1350, 750);
            Controls.Add(pnlConfig);
            Controls.Add(btnLeaveRoom);
            Controls.Add(btnStartGame);
            Controls.Add(pnlChat);
            Controls.Add(pnlPlayerB);
            Controls.Add(pnlPlayerA);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "InRoom";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Phòng chơi";
            pnlConfig.ResumeLayout(false);
            pnlConfig.PerformLayout();
            pnlPlayerA.ResumeLayout(false);
            pnlPlayerA.PerformLayout();
            pnlPlayerB.ResumeLayout(false);
            pnlPlayerB.PerformLayout();
            pnlChat.ResumeLayout(false);
            pnlChat.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTitle;
        private Panel pnlConfig, pnlPlayerA, pnlPlayerB, pnlChat;
        private Label lblConfigRoomName, lblConfigTitle, lblChooseSide, lblTime, lblCustomMin, lblFischer;
        private RadioButton radioWhite, radioBlack;
        private Button btn1min, btn3min, btn6min, btn10min, btnRenameRoom, btnSendChat, btnEmoji, btnStartGame, btnLeaveRoom;
        private TextBox txtConfigRoomName, txtCustomMin, txtFischer;
        private Label lblPlayerATitle, lblPlayerAName, lblPlayerARating;
        private Label lblPlayerBTitle, lblPlayerBName, lblPlayerBRating;
        private Label lblChatTitle;
        private RichTextBox rtbChat;
        private TextBox txtChat;
        private Panel pnlTimeControl;
        private Label lblTimeControlTitle;

        // NEW
        private Panel pnlEmojiPicker;
    }
}
