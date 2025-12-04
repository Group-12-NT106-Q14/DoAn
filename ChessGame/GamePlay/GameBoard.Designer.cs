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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameBoard));
            pnlChessBoard = new Panel();
            pnlPlayer1 = new Panel();
            lblPlayer1Time = new Label();
            lblPlayer1Name = new Label();
            lblPlayer1Status = new Label();
            pnlPlayer2 = new Panel();
            lblPlayer2Time = new Label();
            lblPlayer2Name = new Label();
            lblPlayer2Status = new Label();
            pnlMoveHistory = new Panel();
            lvMoveHistory = new ListView();
            colMoveNumber = new ColumnHeader();
            colWhiteMove = new ColumnHeader();
            colBlackMove = new ColumnHeader();
            lblMoveHistoryTitle = new Label();
            pnlChat = new Panel();
            rtbChatMessages = new RichTextBox();
            txtChatInput = new TextBox();
            btnSendChat = new Button();
            lblChatTitle = new Label();
            btnEmoji = new Button();
            pnlEmojiPicker = new Panel();
            btnSurrender = new Button();
            btnOfferDraw = new Button();
            pnlPlayer1.SuspendLayout();
            pnlPlayer2.SuspendLayout();
            pnlMoveHistory.SuspendLayout();
            pnlChat.SuspendLayout();
            SuspendLayout();
            // 
            // pnlChessBoard
            // 
            pnlChessBoard.BackColor = Color.FromArgb(240, 217, 181);
            pnlChessBoard.Location = new Point(20, 80);
            pnlChessBoard.Name = "pnlChessBoard";
            pnlChessBoard.Size = new Size(640, 600);
            pnlChessBoard.TabIndex = 0;
            // 
            // pnlPlayer1
            // 
            pnlPlayer1.BackColor = Color.FromArgb(118, 74, 61);
            pnlPlayer1.Controls.Add(lblPlayer1Time);
            pnlPlayer1.Controls.Add(lblPlayer1Name);
            pnlPlayer1.Controls.Add(lblPlayer1Status);
            pnlPlayer1.Location = new Point(20, 712);
            pnlPlayer1.Name = "pnlPlayer1";
            pnlPlayer1.Size = new Size(640, 68);
            pnlPlayer1.TabIndex = 2;
            // 
            // lblPlayer1Time
            // 
            lblPlayer1Time.BackColor = Color.White;
            lblPlayer1Time.BorderStyle = BorderStyle.FixedSingle;
            lblPlayer1Time.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblPlayer1Time.ForeColor = Color.Black;
            lblPlayer1Time.Location = new Point(480, 4);
            lblPlayer1Time.Name = "lblPlayer1Time";
            lblPlayer1Time.Size = new Size(140, 46);
            lblPlayer1Time.TabIndex = 2;
            lblPlayer1Time.Text = "10:00";
            lblPlayer1Time.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblPlayer1Name
            // 
            lblPlayer1Name.AutoSize = true;
            lblPlayer1Name.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblPlayer1Name.ForeColor = Color.White;
            lblPlayer1Name.Location = new Point(15, 4);
            lblPlayer1Name.Name = "lblPlayer1Name";
            lblPlayer1Name.Size = new Size(83, 28);
            lblPlayer1Name.TabIndex = 0;
            lblPlayer1Name.Text = "player1";
            // 
            // lblPlayer1Status
            // 
            lblPlayer1Status.AutoSize = true;
            lblPlayer1Status.Font = new Font("Segoe UI", 9F);
            lblPlayer1Status.ForeColor = Color.FromArgb(255, 223, 186);
            lblPlayer1Status.Location = new Point(15, 30);
            lblPlayer1Status.Name = "lblPlayer1Status";
            lblPlayer1Status.Size = new Size(85, 20);
            lblPlayer1Status.TabIndex = 1;
            lblPlayer1Status.Text = "Quân Trắng";
            // 
            // pnlPlayer2
            // 
            pnlPlayer2.BackColor = Color.FromArgb(118, 74, 61);
            pnlPlayer2.Controls.Add(lblPlayer2Time);
            pnlPlayer2.Controls.Add(lblPlayer2Name);
            pnlPlayer2.Controls.Add(lblPlayer2Status);
            pnlPlayer2.Location = new Point(20, 12);
            pnlPlayer2.Name = "pnlPlayer2";
            pnlPlayer2.Size = new Size(640, 62);
            pnlPlayer2.TabIndex = 1;
            // 
            // lblPlayer2Time
            // 
            lblPlayer2Time.BackColor = Color.White;
            lblPlayer2Time.BorderStyle = BorderStyle.FixedSingle;
            lblPlayer2Time.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblPlayer2Time.ForeColor = Color.Black;
            lblPlayer2Time.Location = new Point(480, 4);
            lblPlayer2Time.Name = "lblPlayer2Time";
            lblPlayer2Time.Size = new Size(140, 46);
            lblPlayer2Time.TabIndex = 2;
            lblPlayer2Time.Text = "10:00";
            lblPlayer2Time.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblPlayer2Name
            // 
            lblPlayer2Name.AutoSize = true;
            lblPlayer2Name.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblPlayer2Name.ForeColor = Color.White;
            lblPlayer2Name.Location = new Point(15, 4);
            lblPlayer2Name.Name = "lblPlayer2Name";
            lblPlayer2Name.Size = new Size(83, 28);
            lblPlayer2Name.TabIndex = 0;
            lblPlayer2Name.Text = "player2";
            // 
            // lblPlayer2Status
            // 
            lblPlayer2Status.AutoSize = true;
            lblPlayer2Status.Font = new Font("Segoe UI", 9F);
            lblPlayer2Status.ForeColor = Color.FromArgb(255, 223, 186);
            lblPlayer2Status.Location = new Point(15, 30);
            lblPlayer2Status.Name = "lblPlayer2Status";
            lblPlayer2Status.Size = new Size(75, 20);
            lblPlayer2Status.TabIndex = 1;
            lblPlayer2Status.Text = "Quân Đen";
            // 
            // pnlMoveHistory
            // 
            pnlMoveHistory.BackColor = Color.FromArgb(118, 74, 61);
            pnlMoveHistory.Controls.Add(lvMoveHistory);
            pnlMoveHistory.Controls.Add(lblMoveHistoryTitle);
            pnlMoveHistory.Location = new Point(1050, 20);
            pnlMoveHistory.Name = "pnlMoveHistory";
            pnlMoveHistory.Size = new Size(350, 760);
            pnlMoveHistory.TabIndex = 3;
            // 
            // lvMoveHistory
            // 
            lvMoveHistory.BackColor = Color.FromArgb(247, 234, 214);
            lvMoveHistory.BorderStyle = BorderStyle.None;
            lvMoveHistory.Columns.AddRange(new ColumnHeader[] { colMoveNumber, colWhiteMove, colBlackMove });
            lvMoveHistory.Font = new Font("Segoe UI", 10F);
            lvMoveHistory.ForeColor = Color.FromArgb(78, 49, 41);
            lvMoveHistory.FullRowSelect = true;
            lvMoveHistory.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            lvMoveHistory.Location = new Point(15, 50);
            lvMoveHistory.MultiSelect = false;
            lvMoveHistory.Name = "lvMoveHistory";
            lvMoveHistory.Size = new Size(320, 692);
            lvMoveHistory.TabIndex = 1;
            lvMoveHistory.UseCompatibleStateImageBehavior = false;
            lvMoveHistory.View = View.Details;
            // 
            // colMoveNumber
            // 
            colMoveNumber.Text = "#";
            colMoveNumber.Width = 40;
            // 
            // colWhiteMove
            // 
            colWhiteMove.Text = "Trắng";
            colWhiteMove.Width = 140;
            // 
            // colBlackMove
            // 
            colBlackMove.Text = "Đen";
            colBlackMove.Width = 140;
            // 
            // lblMoveHistoryTitle
            // 
            lblMoveHistoryTitle.AutoSize = true;
            lblMoveHistoryTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblMoveHistoryTitle.ForeColor = Color.White;
            lblMoveHistoryTitle.Location = new Point(15, 12);
            lblMoveHistoryTitle.Name = "lblMoveHistoryTitle";
            lblMoveHistoryTitle.Size = new Size(135, 30);
            lblMoveHistoryTitle.TabIndex = 0;
            lblMoveHistoryTitle.Text = "Các nước đi";
            // 
            // pnlChat
            // 
            pnlChat.BackColor = Color.FromArgb(118, 74, 61);
            pnlChat.Controls.Add(rtbChatMessages);
            pnlChat.Controls.Add(txtChatInput);
            pnlChat.Controls.Add(btnSendChat);
            pnlChat.Controls.Add(lblChatTitle);
            pnlChat.Controls.Add(btnEmoji);
            pnlChat.Controls.Add(pnlEmojiPicker);
            pnlChat.Location = new Point(680, 20);
            pnlChat.Name = "pnlChat";
            pnlChat.Size = new Size(360, 700);
            pnlChat.TabIndex = 4;
            // 
            // rtbChatMessages
            // 
            rtbChatMessages.BackColor = Color.FromArgb(247, 234, 214);
            rtbChatMessages.BorderStyle = BorderStyle.None;
            rtbChatMessages.Font = new Font("Segoe UI", 10F);
            rtbChatMessages.ForeColor = Color.FromArgb(78, 49, 41);
            rtbChatMessages.Location = new Point(15, 53);
            rtbChatMessages.Name = "rtbChatMessages";
            rtbChatMessages.ReadOnly = true;
            rtbChatMessages.Size = new Size(330, 586);
            rtbChatMessages.TabIndex = 1;
            rtbChatMessages.Text = "";
            // 
            // txtChatInput
            // 
            txtChatInput.BackColor = Color.FromArgb(247, 234, 214);
            txtChatInput.BorderStyle = BorderStyle.FixedSingle;
            txtChatInput.Font = new Font("Segoe UI", 10F);
            txtChatInput.ForeColor = Color.FromArgb(78, 49, 41);
            txtChatInput.Location = new Point(18, 650);
            txtChatInput.Multiline = true;
            txtChatInput.Name = "txtChatInput";
            txtChatInput.PlaceholderText = "Nhập tin nhắn...";
            txtChatInput.Size = new Size(220, 33);
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
            btnSendChat.Location = new Point(298, 645);
            btnSendChat.Name = "btnSendChat";
            btnSendChat.Size = new Size(50, 47);
            btnSendChat.TabIndex = 4;
            btnSendChat.Text = "Gửi";
            btnSendChat.UseVisualStyleBackColor = false;
            btnSendChat.Click += btnSendChat_Click;
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
            // btnEmoji
            // 
            btnEmoji.BackColor = Color.FromArgb(160, 106, 88);
            btnEmoji.FlatAppearance.BorderSize = 0;
            btnEmoji.FlatStyle = FlatStyle.Flat;
            btnEmoji.Font = new Font("Segoe UI", 14F);
            btnEmoji.ForeColor = Color.White;
            btnEmoji.Location = new Point(248, 645);
            btnEmoji.Name = "btnEmoji";
            btnEmoji.Size = new Size(45, 47);
            btnEmoji.TabIndex = 3;
            btnEmoji.Text = "😊";
            btnEmoji.UseVisualStyleBackColor = false;
            btnEmoji.Click += btnEmoji_Click;
            // 
            // pnlEmojiPicker
            // 
            pnlEmojiPicker.BackColor = Color.FromArgb(247, 234, 214);
            pnlEmojiPicker.Location = new Point(15, 444);
            pnlEmojiPicker.Name = "pnlEmojiPicker";
            pnlEmojiPicker.Size = new Size(330, 195);
            pnlEmojiPicker.TabIndex = 5;
            pnlEmojiPicker.Visible = false;
            // 
            // btnSurrender
            // 
            btnSurrender.BackColor = Color.FromArgb(160, 106, 88);
            btnSurrender.FlatAppearance.BorderSize = 0;
            btnSurrender.FlatStyle = FlatStyle.Flat;
            btnSurrender.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnSurrender.ForeColor = Color.White;
            btnSurrender.Location = new Point(680, 740);
            btnSurrender.Name = "btnSurrender";
            btnSurrender.Size = new Size(168, 40);
            btnSurrender.TabIndex = 5;
            btnSurrender.Text = "Đầu Hàng";
            btnSurrender.UseVisualStyleBackColor = false;
            btnSurrender.Click += btnSurrender_Click;
            // 
            // btnOfferDraw
            // 
            btnOfferDraw.BackColor = Color.FromArgb(160, 106, 88);
            btnOfferDraw.FlatAppearance.BorderSize = 0;
            btnOfferDraw.FlatStyle = FlatStyle.Flat;
            btnOfferDraw.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnOfferDraw.ForeColor = Color.White;
            btnOfferDraw.Location = new Point(870, 740);
            btnOfferDraw.Name = "btnOfferDraw";
            btnOfferDraw.Size = new Size(170, 40);
            btnOfferDraw.TabIndex = 6;
            btnOfferDraw.Text = "Cầu Hoà";
            btnOfferDraw.UseVisualStyleBackColor = false;
            btnOfferDraw.Click += btnOfferDraw_Click;
            // 
            // GameBoard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 217, 181);
            ClientSize = new Size(1450, 800);
            Controls.Add(btnOfferDraw);
            Controls.Add(btnSurrender);
            Controls.Add(pnlChat);
            Controls.Add(pnlMoveHistory);
            Controls.Add(pnlPlayer2);
            Controls.Add(pnlPlayer1);
            Controls.Add(pnlChessBoard);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "GameBoard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "GameBoard";
            pnlPlayer1.ResumeLayout(false);
            pnlPlayer1.PerformLayout();
            pnlPlayer2.ResumeLayout(false);
            pnlPlayer2.PerformLayout();
            pnlMoveHistory.ResumeLayout(false);
            pnlMoveHistory.PerformLayout();
            pnlChat.ResumeLayout(false);
            pnlChat.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlChessBoard;
        private System.Windows.Forms.Panel pnlPlayer1;
        private System.Windows.Forms.Label lblPlayer1Name;
        private System.Windows.Forms.Label lblPlayer1Status;
        private System.Windows.Forms.Label lblPlayer1Time;
        private System.Windows.Forms.Panel pnlPlayer2;
        private System.Windows.Forms.Label lblPlayer2Name;
        private System.Windows.Forms.Label lblPlayer2Status;
        private System.Windows.Forms.Label lblPlayer2Time;
        private System.Windows.Forms.Panel pnlChat;
        private System.Windows.Forms.Label lblChatTitle;
        private System.Windows.Forms.RichTextBox rtbChatMessages;
        private System.Windows.Forms.Panel pnlMoveHistory;
        private System.Windows.Forms.Label lblMoveHistoryTitle;
        private System.Windows.Forms.ListView lvMoveHistory;
        private System.Windows.Forms.ColumnHeader colMoveNumber;
        private System.Windows.Forms.ColumnHeader colWhiteMove;
        private System.Windows.Forms.ColumnHeader colBlackMove;
        private System.Windows.Forms.Button btnSurrender;
        private System.Windows.Forms.Button btnOfferDraw;

        // giữ để GameBoard.cs compile, logic sẽ sửa lại sau
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblTurnValue;
        private TextBox txtChatInput;
        private Button btnSendChat;
        private Button btnEmoji;
        private Panel pnlEmojiPicker;
    }
}