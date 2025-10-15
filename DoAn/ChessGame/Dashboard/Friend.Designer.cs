namespace ChessGame
{
    partial class Friend
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblTitle = new Label();
            pnlRoomList = new Panel();
            lblRoomListTitle = new Label();
            txtSearchRoom = new TextBox();
            lstRooms = new ListBox();
            btnRefresh = new Button();
            pnlPlayerA = new Panel();
            lblPlayerATitle = new Label();
            picPlayerA = new PictureBox();
            lblPlayerAName = new Label();
            lblPlayerARating = new Label();
            pnlPlayerB = new Panel();
            lblPlayerBTitle = new Label();
            picPlayerB = new PictureBox();
            lblPlayerBName = new Label();
            lblPlayerBRating = new Label();
            pnlChat = new Panel();
            lblChatTitle = new Label();
            rtbChat = new RichTextBox();
            txtChat = new TextBox();
            btnSendChat = new Button();
            btnEmoji = new Button();
            btnStartGame = new Button();
            btnLeaveRoom = new Button();
            pnlTimeControl = new Panel();
            lblTimeControlTitle = new Label();
            btn1min = new Button();
            btn3min = new Button();
            btn6min = new Button();
            btn10min = new Button();
            lblCustomMin = new Label();
            txtCustomMin = new TextBox();
            pnlRoomList.SuspendLayout();
            pnlPlayerA.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picPlayerA).BeginInit();
            pnlPlayerB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picPlayerB).BeginInit();
            pnlChat.SuspendLayout();
            pnlTimeControl.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(78, 49, 41);
            lblTitle.Location = new Point(503, 37);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(279, 50);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "CHƠI VỚI BẠN";
            // 
            // pnlRoomList
            // 
            pnlRoomList.BackColor = Color.FromArgb(118, 74, 61);
            pnlRoomList.Controls.Add(lblRoomListTitle);
            pnlRoomList.Controls.Add(txtSearchRoom);
            pnlRoomList.Controls.Add(lstRooms);
            pnlRoomList.Controls.Add(btnRefresh);
            pnlRoomList.Location = new Point(30, 110);
            pnlRoomList.Name = "pnlRoomList";
            pnlRoomList.Size = new Size(320, 620);
            pnlRoomList.TabIndex = 2;
            // 
            // lblRoomListTitle
            // 
            lblRoomListTitle.AutoSize = true;
            lblRoomListTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblRoomListTitle.ForeColor = Color.White;
            lblRoomListTitle.Location = new Point(20, 20);
            lblRoomListTitle.Name = "lblRoomListTitle";
            lblRoomListTitle.Size = new Size(214, 32);
            lblRoomListTitle.TabIndex = 0;
            lblRoomListTitle.Text = "Danh Sách Phòng";
            // 
            // txtSearchRoom
            // 
            txtSearchRoom.BackColor = Color.FromArgb(247, 234, 214);
            txtSearchRoom.BorderStyle = BorderStyle.None;
            txtSearchRoom.Font = new Font("Segoe UI", 10F);
            txtSearchRoom.ForeColor = Color.FromArgb(78, 49, 41);
            txtSearchRoom.Location = new Point(20, 60);
            txtSearchRoom.Name = "txtSearchRoom";
            txtSearchRoom.PlaceholderText = "Tìm kiếm phòng...";
            txtSearchRoom.Size = new Size(280, 23);
            txtSearchRoom.TabIndex = 1;
            // 
            // lstRooms
            // 
            lstRooms.BackColor = Color.FromArgb(247, 234, 214);
            lstRooms.BorderStyle = BorderStyle.None;
            lstRooms.Font = new Font("Segoe UI", 10F);
            lstRooms.ForeColor = Color.FromArgb(78, 49, 41);
            lstRooms.ItemHeight = 23;
            lstRooms.Location = new Point(20, 95);
            lstRooms.Name = "lstRooms";
            lstRooms.Size = new Size(280, 391);
            lstRooms.TabIndex = 2;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.FromArgb(160, 106, 88);
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(85, 512);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(152, 58);
            btnRefresh.TabIndex = 3;
            btnRefresh.Text = "Làm Mới";
            btnRefresh.UseVisualStyleBackColor = false;
            // 
            // pnlPlayerA
            // 
            pnlPlayerA.BackColor = Color.FromArgb(118, 74, 61);
            pnlPlayerA.Controls.Add(lblPlayerATitle);
            pnlPlayerA.Controls.Add(picPlayerA);
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
            lblPlayerATitle.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPlayerATitle.ForeColor = Color.White;
            lblPlayerATitle.Location = new Point(19, 20);
            lblPlayerATitle.Name = "lblPlayerATitle";
            lblPlayerATitle.Size = new Size(258, 31);
            lblPlayerATitle.TabIndex = 0;
            lblPlayerATitle.Text = "NGƯỜI CHƠI A (Trắng)";
            // 
            // picPlayerA
            // 
            picPlayerA.BackColor = Color.White;
            picPlayerA.Location = new Point(70, 70);
            picPlayerA.Name = "picPlayerA";
            picPlayerA.Size = new Size(140, 140);
            picPlayerA.SizeMode = PictureBoxSizeMode.Zoom;
            picPlayerA.TabIndex = 1;
            picPlayerA.TabStop = false;
            // 
            // lblPlayerAName
            // 
            lblPlayerAName.AutoSize = true;
            lblPlayerAName.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblPlayerAName.ForeColor = Color.White;
            lblPlayerAName.Location = new Point(80, 230);
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
            lblPlayerARating.Location = new Point(85, 265);
            lblPlayerARating.Name = "lblPlayerARating";
            lblPlayerARating.Size = new Size(136, 25);
            lblPlayerARating.TabIndex = 3;
            lblPlayerARating.Text = "⭐ Rating: 1200";
            // 
            // pnlPlayerB
            // 
            pnlPlayerB.BackColor = Color.FromArgb(118, 74, 61);
            pnlPlayerB.Controls.Add(lblPlayerBTitle);
            pnlPlayerB.Controls.Add(picPlayerB);
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
            lblPlayerBTitle.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPlayerBTitle.ForeColor = Color.White;
            lblPlayerBTitle.Location = new Point(23, 21);
            lblPlayerBTitle.Name = "lblPlayerBTitle";
            lblPlayerBTitle.Size = new Size(240, 31);
            lblPlayerBTitle.TabIndex = 0;
            lblPlayerBTitle.Text = "NGƯỜI CHƠI B (Đen)";
            // 
            // picPlayerB
            // 
            picPlayerB.BackColor = Color.FromArgb(200, 200, 200);
            picPlayerB.Location = new Point(70, 70);
            picPlayerB.Name = "picPlayerB";
            picPlayerB.Size = new Size(140, 140);
            picPlayerB.SizeMode = PictureBoxSizeMode.Zoom;
            picPlayerB.TabIndex = 1;
            picPlayerB.TabStop = false;
            // 
            // lblPlayerBName
            // 
            lblPlayerBName.AutoSize = true;
            lblPlayerBName.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblPlayerBName.ForeColor = Color.FromArgb(200, 200, 200);
            lblPlayerBName.Location = new Point(85, 230);
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
            lblPlayerBRating.Location = new Point(110, 265);
            lblPlayerBRating.Name = "lblPlayerBRating";
            lblPlayerBRating.Size = new Size(57, 25);
            lblPlayerBRating.TabIndex = 3;
            lblPlayerBRating.Text = "⭐ ---";
            lblPlayerBRating.Visible = false;
            // 
            // pnlChat
            // 
            pnlChat.BackColor = Color.FromArgb(118, 74, 61);
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
            btnStartGame.Enabled = false;
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
            pnlTimeControl.BackColor = Color.FromArgb(247, 234, 214);
            pnlTimeControl.Controls.Add(lblTimeControlTitle);
            pnlTimeControl.Controls.Add(btn1min);
            pnlTimeControl.Controls.Add(btn3min);
            pnlTimeControl.Controls.Add(btn6min);
            pnlTimeControl.Controls.Add(btn10min);
            pnlTimeControl.Controls.Add(lblCustomMin);
            pnlTimeControl.Controls.Add(txtCustomMin);
            pnlTimeControl.Location = new Point(370, 560);
            pnlTimeControl.Name = "pnlTimeControl";
            pnlTimeControl.Size = new Size(580, 80);
            pnlTimeControl.TabIndex = 8;
            // 
            // lblTimeControlTitle
            // 
            lblTimeControlTitle.AutoSize = true;
            lblTimeControlTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTimeControlTitle.ForeColor = Color.FromArgb(78, 49, 41);
            lblTimeControlTitle.Location = new Point(15, 12);
            lblTimeControlTitle.Name = "lblTimeControlTitle";
            lblTimeControlTitle.Size = new Size(437, 28);
            lblTimeControlTitle.TabIndex = 0;
            lblTimeControlTitle.Text = "Thời gian chơi của mỗi bên (tính bằng phút):";
            // 
            // btn1min
            // 
            btn1min.BackColor = Color.White;
            btn1min.FlatStyle = FlatStyle.Flat;
            btn1min.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btn1min.ForeColor = Color.FromArgb(78, 49, 41);
            btn1min.Location = new Point(15, 45);
            btn1min.Name = "btn1min";
            btn1min.Size = new Size(70, 30);
            btn1min.TabIndex = 1;
            btn1min.Text = "1 phút";
            btn1min.UseVisualStyleBackColor = false;
            // 
            // btn3min
            // 
            btn3min.BackColor = Color.White;
            btn3min.FlatStyle = FlatStyle.Flat;
            btn3min.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btn3min.ForeColor = Color.FromArgb(78, 49, 41);
            btn3min.Location = new Point(95, 45);
            btn3min.Name = "btn3min";
            btn3min.Size = new Size(70, 30);
            btn3min.TabIndex = 2;
            btn3min.Text = "3 phút";
            btn3min.UseVisualStyleBackColor = false;
            // 
            // btn6min
            // 
            btn6min.BackColor = Color.White;
            btn6min.FlatStyle = FlatStyle.Flat;
            btn6min.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btn6min.ForeColor = Color.FromArgb(78, 49, 41);
            btn6min.Location = new Point(175, 45);
            btn6min.Name = "btn6min";
            btn6min.Size = new Size(70, 30);
            btn6min.TabIndex = 3;
            btn6min.Text = "6 phút";
            btn6min.UseVisualStyleBackColor = false;
            // 
            // btn10min
            // 
            btn10min.BackColor = Color.White;
            btn10min.FlatStyle = FlatStyle.Flat;
            btn10min.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btn10min.ForeColor = Color.FromArgb(78, 49, 41);
            btn10min.Location = new Point(255, 45);
            btn10min.Name = "btn10min";
            btn10min.Size = new Size(75, 30);
            btn10min.TabIndex = 4;
            btn10min.Text = "10 phút";
            btn10min.UseVisualStyleBackColor = false;
            // 
            // lblCustomMin
            // 
            lblCustomMin.AutoSize = true;
            lblCustomMin.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCustomMin.ForeColor = Color.FromArgb(78, 49, 41);
            lblCustomMin.Location = new Point(350, 50);
            lblCustomMin.Name = "lblCustomMin";
            lblCustomMin.Size = new Size(86, 23);
            lblCustomMin.TabIndex = 5;
            lblCustomMin.Text = "Tùy chọn:";
            // 
            // txtCustomMin
            // 
            txtCustomMin.Font = new Font("Segoe UI", 10F);
            txtCustomMin.Location = new Point(442, 47);
            txtCustomMin.Name = "txtCustomMin";
            txtCustomMin.PlaceholderText = "phút";
            txtCustomMin.Size = new Size(81, 30);
            txtCustomMin.TabIndex = 6;
            txtCustomMin.TextAlign = HorizontalAlignment.Center;
            // 
            // Room
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 217, 181);
            ClientSize = new Size(1350, 750);
            Controls.Add(pnlTimeControl);
            Controls.Add(btnLeaveRoom);
            Controls.Add(btnStartGame);
            Controls.Add(pnlChat);
            Controls.Add(pnlPlayerB);
            Controls.Add(pnlPlayerA);
            Controls.Add(pnlRoomList);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Room";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Chơi Với Bạn";
            pnlRoomList.ResumeLayout(false);
            pnlRoomList.PerformLayout();
            pnlPlayerA.ResumeLayout(false);
            pnlPlayerA.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picPlayerA).EndInit();
            pnlPlayerB.ResumeLayout(false);
            pnlPlayerB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picPlayerB).EndInit();
            pnlChat.ResumeLayout(false);
            pnlChat.PerformLayout();
            pnlTimeControl.ResumeLayout(false);
            pnlTimeControl.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        // Room List
        private Panel pnlRoomList;
        private Label lblRoomListTitle;
        private TextBox txtSearchRoom;
        private ListBox lstRooms;
        private Button btnRefresh;
        // Player A
        private Panel pnlPlayerA;
        private Label lblPlayerATitle;
        private PictureBox picPlayerA;
        private Label lblPlayerAName;
        private Label lblPlayerARating;
        // Player B
        private Panel pnlPlayerB;
        private Label lblPlayerBTitle;
        private PictureBox picPlayerB;
        private Label lblPlayerBName;
        private Label lblPlayerBRating;
        // Chat
        private Panel pnlChat;
        private Label lblChatTitle;
        private RichTextBox rtbChat;
        private TextBox txtChat;
        private Button btnSendChat;
        private Button btnEmoji;
        // Actions
        private Button btnStartGame;
        private Button btnLeaveRoom;
        // Time Control (MỚI)
        private Panel pnlTimeControl;
        private Label lblTimeControlTitle;
        private Button btn1min;
        private Button btn3min;
        private Button btn6min;
        private Button btn10min;
        private Label lblCustomMin;
        private TextBox txtCustomMin;
    }
}
