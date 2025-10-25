namespace ChessGame
{
    partial class GameBoard
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
            pnlChessBoard = new Panel();
            pnlPlayerInfo = new Panel();
            pnlPlayer1 = new Panel();
            lblPlayer1Name = new Label();
            lblPlayer1Status = new Label();
            pnlPlayer2 = new Panel();
            lblPlayer2Name = new Label();
            lblPlayer2Status = new Label();
            pnlChat = new Panel();
            lblChatTitle = new Label();
            rtbChatMessages = new RichTextBox();
            txtChatInput = new TextBox();
            btnEmoji = new Button();
            btnSendChat = new Button();
            pnlEmojiPicker = new Panel();
            pnlGameInfo = new Panel();
            lblTimeRemaining = new Label();
            lblTime = new Label();
            lblCurrentTurn = new Label();
            lblTurnValue = new Label();
            btnSurrender = new Button();
            btnOfferDraw = new Button();
            pnlPlayerInfo.SuspendLayout();
            pnlPlayer1.SuspendLayout();
            pnlPlayer2.SuspendLayout();
            pnlChat.SuspendLayout();
            pnlGameInfo.SuspendLayout();
            SuspendLayout();
            // 
            // pnlChessBoard
            // 
            pnlChessBoard.BackColor = Color.FromArgb(240, 217, 181);
            pnlChessBoard.Location = new Point(20, 20);
            pnlChessBoard.Name = "pnlChessBoard";
            pnlChessBoard.Size = new Size(640, 640);
            pnlChessBoard.TabIndex = 0;
            // 
            // pnlPlayerInfo
            // 
            pnlPlayerInfo.BackColor = Color.FromArgb(240, 217, 181);
            pnlPlayerInfo.Controls.Add(pnlPlayer1);
            pnlPlayerInfo.Controls.Add(pnlPlayer2);
            pnlPlayerInfo.Location = new Point(680, 20);
            pnlPlayerInfo.Name = "pnlPlayerInfo";
            pnlPlayerInfo.Size = new Size(400, 80);
            pnlPlayerInfo.TabIndex = 1;
            // 
            // pnlPlayer1
            // 
            pnlPlayer1.BackColor = Color.FromArgb(118, 74, 61);
            pnlPlayer1.Controls.Add(lblPlayer1Name);
            pnlPlayer1.Controls.Add(lblPlayer1Status);
            pnlPlayer1.Location = new Point(0, 0);
            pnlPlayer1.Name = "pnlPlayer1";
            pnlPlayer1.Size = new Size(195, 80);
            pnlPlayer1.TabIndex = 0;
            // 
            // lblPlayer1Name
            // 
            lblPlayer1Name.AutoSize = true;
            lblPlayer1Name.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblPlayer1Name.ForeColor = Color.White;
            lblPlayer1Name.Location = new Point(15, 15);
            lblPlayer1Name.Name = "lblPlayer1Name";
            lblPlayer1Name.Size = new Size(91, 30);
            lblPlayer1Name.TabIndex = 0;
            lblPlayer1Name.Text = "player1";
            // 
            // lblPlayer1Status
            // 
            lblPlayer1Status.AutoSize = true;
            lblPlayer1Status.Font = new Font("Segoe UI", 9F);
            lblPlayer1Status.ForeColor = Color.FromArgb(255, 223, 186);
            lblPlayer1Status.Location = new Point(15, 48);
            lblPlayer1Status.Name = "lblPlayer1Status";
            lblPlayer1Status.Size = new Size(85, 20);
            lblPlayer1Status.TabIndex = 1;
            lblPlayer1Status.Text = "Quân Trắng";
            // 
            // pnlPlayer2
            // 
            pnlPlayer2.BackColor = Color.FromArgb(118, 74, 61);
            pnlPlayer2.Controls.Add(lblPlayer2Name);
            pnlPlayer2.Controls.Add(lblPlayer2Status);
            pnlPlayer2.Location = new Point(205, 0);
            pnlPlayer2.Name = "pnlPlayer2";
            pnlPlayer2.Size = new Size(195, 80);
            pnlPlayer2.TabIndex = 1;
            // 
            // lblPlayer2Name
            // 
            lblPlayer2Name.AutoSize = true;
            lblPlayer2Name.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblPlayer2Name.ForeColor = Color.White;
            lblPlayer2Name.Location = new Point(15, 15);
            lblPlayer2Name.Name = "lblPlayer2Name";
            lblPlayer2Name.Size = new Size(91, 30);
            lblPlayer2Name.TabIndex = 0;
            lblPlayer2Name.Text = "player2";
            // 
            // lblPlayer2Status
            // 
            lblPlayer2Status.AutoSize = true;
            lblPlayer2Status.Font = new Font("Segoe UI", 9F);
            lblPlayer2Status.ForeColor = Color.FromArgb(255, 223, 186);
            lblPlayer2Status.Location = new Point(15, 48);
            lblPlayer2Status.Name = "lblPlayer2Status";
            lblPlayer2Status.Size = new Size(75, 20);
            lblPlayer2Status.TabIndex = 1;
            lblPlayer2Status.Text = "Quân Đen";
            // 
            // pnlChat
            // 
            pnlChat.BackColor = Color.FromArgb(118, 74, 61);
            pnlChat.Controls.Add(lblChatTitle);
            pnlChat.Controls.Add(rtbChatMessages);
            pnlChat.Controls.Add(txtChatInput);
            pnlChat.Controls.Add(btnEmoji);
            pnlChat.Controls.Add(btnSendChat);
            pnlChat.Controls.Add(pnlEmojiPicker);
            pnlChat.Location = new Point(680, 115);
            pnlChat.Name = "pnlChat";
            pnlChat.Size = new Size(400, 380);
            pnlChat.TabIndex = 2;
            // 
            // lblChatTitle
            // 
            lblChatTitle.AutoSize = true;
            lblChatTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblChatTitle.ForeColor = Color.White;
            lblChatTitle.Location = new Point(15, 12);
            lblChatTitle.Name = "lblChatTitle";
            lblChatTitle.Size = new Size(61, 30);
            lblChatTitle.TabIndex = 0;
            lblChatTitle.Text = "Chat";
            // 
            // rtbChatMessages
            // 
            rtbChatMessages.BackColor = Color.FromArgb(247, 234, 214);
            rtbChatMessages.BorderStyle = BorderStyle.None;
            rtbChatMessages.Font = new Font("Segoe UI", 10F);
            rtbChatMessages.ForeColor = Color.FromArgb(78, 49, 41);
            rtbChatMessages.Location = new Point(15, 50);
            rtbChatMessages.Name = "rtbChatMessages";
            rtbChatMessages.ReadOnly = true;
            rtbChatMessages.Size = new Size(370, 270);
            rtbChatMessages.TabIndex = 1;
            rtbChatMessages.Text = "";
            // 
            // txtChatInput
            // 
            txtChatInput.BackColor = Color.FromArgb(247, 234, 214);
            txtChatInput.BorderStyle = BorderStyle.FixedSingle;
            txtChatInput.Font = new Font("Segoe UI", 10F);
            txtChatInput.ForeColor = Color.FromArgb(78, 49, 41);
            txtChatInput.Location = new Point(15, 330);
            txtChatInput.Multiline = true;
            txtChatInput.Name = "txtChatInput";
            txtChatInput.PlaceholderText = "Nhập tin nhắn...";
            txtChatInput.Size = new Size(240, 33);
            txtChatInput.TabIndex = 2;
            // 
            // btnEmoji
            // 
            btnEmoji.BackColor = Color.FromArgb(160, 106, 88);
            btnEmoji.FlatAppearance.BorderSize = 0;
            btnEmoji.FlatStyle = FlatStyle.Flat;
            btnEmoji.Font = new Font("Segoe UI", 14F);
            btnEmoji.ForeColor = Color.White;
            btnEmoji.Location = new Point(265, 330);
            btnEmoji.Name = "btnEmoji";
            btnEmoji.Size = new Size(45, 47);
            btnEmoji.TabIndex = 3;
            btnEmoji.Text = "😊";
            btnEmoji.UseVisualStyleBackColor = false;
            // 
            // btnSendChat
            // 
            btnSendChat.BackColor = Color.FromArgb(133, 181, 100);
            btnSendChat.FlatAppearance.BorderSize = 0;
            btnSendChat.FlatStyle = FlatStyle.Flat;
            btnSendChat.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSendChat.ForeColor = Color.White;
            btnSendChat.Location = new Point(320, 330);
            btnSendChat.Name = "btnSendChat";
            btnSendChat.Size = new Size(65, 47);
            btnSendChat.TabIndex = 4;
            btnSendChat.Text = "Gửi";
            btnSendChat.UseVisualStyleBackColor = false;
            // 
            // pnlEmojiPicker
            // 
            pnlEmojiPicker.BackColor = Color.FromArgb(247, 234, 214);
            pnlEmojiPicker.Location = new Point(15, 210);
            pnlEmojiPicker.Name = "pnlEmojiPicker";
            pnlEmojiPicker.Size = new Size(370, 110);
            pnlEmojiPicker.TabIndex = 5;
            pnlEmojiPicker.Visible = false;
            // 
            // pnlGameInfo
            // 
            pnlGameInfo.BackColor = Color.FromArgb(118, 74, 61);
            pnlGameInfo.Controls.Add(lblTimeRemaining);
            pnlGameInfo.Controls.Add(lblTime);
            pnlGameInfo.Controls.Add(lblCurrentTurn);
            pnlGameInfo.Controls.Add(lblTurnValue);
            pnlGameInfo.Location = new Point(680, 510);
            pnlGameInfo.Name = "pnlGameInfo";
            pnlGameInfo.Size = new Size(400, 100);
            pnlGameInfo.TabIndex = 3;
            // 
            // lblTimeRemaining
            // 
            lblTimeRemaining.AutoSize = true;
            lblTimeRemaining.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTimeRemaining.ForeColor = Color.White;
            lblTimeRemaining.Location = new Point(15, 15);
            lblTimeRemaining.Name = "lblTimeRemaining";
            lblTimeRemaining.Size = new Size(163, 25);
            lblTimeRemaining.TabIndex = 0;
            lblTimeRemaining.Text = "Thời gian còn lại";
            // 
            // lblTime
            // 
            lblTime.AutoSize = true;
            lblTime.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTime.ForeColor = Color.FromArgb(255, 223, 186);
            lblTime.Location = new Point(15, 45);
            lblTime.Name = "lblTime";
            lblTime.Size = new Size(77, 32);
            lblTime.TabIndex = 1;
            lblTime.Text = "06:48";
            // 
            // lblCurrentTurn
            // 
            lblCurrentTurn.AutoSize = true;
            lblCurrentTurn.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblCurrentTurn.ForeColor = Color.White;
            lblCurrentTurn.Location = new Point(220, 15);
            lblCurrentTurn.Name = "lblCurrentTurn";
            lblCurrentTurn.Size = new Size(95, 25);
            lblCurrentTurn.TabIndex = 2;
            lblCurrentTurn.Text = "Lượt của";
            // 
            // lblTurnValue
            // 
            lblTurnValue.AutoSize = true;
            lblTurnValue.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTurnValue.ForeColor = Color.FromArgb(255, 223, 186);
            lblTurnValue.Location = new Point(220, 45);
            lblTurnValue.Name = "lblTurnValue";
            lblTurnValue.Size = new Size(99, 32);
            lblTurnValue.TabIndex = 3;
            lblTurnValue.Text = "player2";
            // 
            // btnSurrender
            // 
            btnSurrender.BackColor = Color.FromArgb(160, 106, 88);
            btnSurrender.FlatAppearance.BorderSize = 0;
            btnSurrender.FlatStyle = FlatStyle.Flat;
            btnSurrender.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnSurrender.ForeColor = Color.White;
            btnSurrender.Location = new Point(680, 625);
            btnSurrender.Name = "btnSurrender";
            btnSurrender.Size = new Size(190, 45);
            btnSurrender.TabIndex = 4;
            btnSurrender.Text = "Đầu Hàng";
            btnSurrender.UseVisualStyleBackColor = false;
            // 
            // btnOfferDraw
            // 
            btnOfferDraw.BackColor = Color.FromArgb(160, 106, 88);
            btnOfferDraw.FlatAppearance.BorderSize = 0;
            btnOfferDraw.FlatStyle = FlatStyle.Flat;
            btnOfferDraw.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnOfferDraw.ForeColor = Color.White;
            btnOfferDraw.Location = new Point(890, 625);
            btnOfferDraw.Name = "btnOfferDraw";
            btnOfferDraw.Size = new Size(190, 45);
            btnOfferDraw.TabIndex = 5;
            btnOfferDraw.Text = "Cầu Hoà";
            btnOfferDraw.UseVisualStyleBackColor = false;
            // 
            // GameBoard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 217, 181);
            ClientSize = new Size(1100, 680);
            Controls.Add(btnOfferDraw);
            Controls.Add(btnSurrender);
            Controls.Add(pnlGameInfo);
            Controls.Add(pnlChat);
            Controls.Add(pnlPlayerInfo);
            Controls.Add(pnlChessBoard);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "GameBoard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "GameBoard";
            pnlPlayerInfo.ResumeLayout(false);
            pnlPlayer1.ResumeLayout(false);
            pnlPlayer1.PerformLayout();
            pnlPlayer2.ResumeLayout(false);
            pnlPlayer2.PerformLayout();
            pnlChat.ResumeLayout(false);
            pnlChat.PerformLayout();
            pnlGameInfo.ResumeLayout(false);
            pnlGameInfo.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlChessBoard;
        private Panel pnlPlayerInfo;
        private Panel pnlPlayer1;
        private Label lblPlayer1Name;
        private Label lblPlayer1Status;
        private Panel pnlPlayer2;
        private Label lblPlayer2Name;
        private Label lblPlayer2Status;
        private Panel pnlChat;
        private Label lblChatTitle;
        private RichTextBox rtbChatMessages;
        private TextBox txtChatInput;
        private Button btnEmoji;
        private Button btnSendChat;
        private Panel pnlEmojiPicker;
        private Panel pnlGameInfo;
        private Label lblTimeRemaining;
        private Label lblTime;
        private Label lblCurrentTurn;
        private Label lblTurnValue;
        private Button btnSurrender;
        private Button btnOfferDraw;
    }
}
