namespace ChessGame
{
    partial class AccountSetting
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlInfoTop;
        private System.Windows.Forms.Label lblUsernameIcon, lblUsername, lblDisplayNameIcon, lblDisplayName, lblEmailIcon, lblEmail, lblRatingIcon, lblRating;
        private System.Windows.Forms.Panel pnlStats, cardGames, cardWins, cardDraws, cardLosses, cardWinrate;
        private System.Windows.Forms.Label lblCardGames, lblCardGamesVal, lblCardWins, lblCardWinsVal, lblCardDraws, lblCardDrawsVal, lblCardLosses, lblCardLossesVal, lblCardWinrate, lblCardWinrateVal;
        private System.Windows.Forms.Button btnShowEdit;
        private System.Windows.Forms.Button btnBackToLobby;
        private System.Windows.Forms.Panel pnlEditAccount;
        private System.Windows.Forms.Label lblEditTitle, lblEditDisplayName, lblEditEmail, lblEditPassword, lblEditPasswordConfirm, lblEditHint;
        private System.Windows.Forms.TextBox txtEditDisplayName, txtEditEmail, txtEditPassword, txtEditPasswordConfirm;
        private System.Windows.Forms.Button btnEditShowPassword, btnEditShowPasswordConfirm, btnSaveEdit, btnCloseEdit;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountSetting));
            lblTitle = new Label();
            pnlInfoTop = new Panel();
            lblUsernameIcon = new Label();
            lblUsername = new Label();
            lblDisplayNameIcon = new Label();
            lblDisplayName = new Label();
            lblEmailIcon = new Label();
            lblEmail = new Label();
            lblRatingIcon = new Label();
            lblRating = new Label();
            pnlStats = new Panel();
            cardGames = new Panel();
            lblCardGames = new Label();
            lblCardGamesVal = new Label();
            cardWins = new Panel();
            lblCardWins = new Label();
            lblCardWinsVal = new Label();
            cardDraws = new Panel();
            lblCardDraws = new Label();
            lblCardDrawsVal = new Label();
            cardLosses = new Panel();
            lblCardLosses = new Label();
            lblCardLossesVal = new Label();
            cardWinrate = new Panel();
            lblCardWinrate = new Label();
            lblCardWinrateVal = new Label();
            btnShowEdit = new Button();
            btnBackToLobby = new Button();
            pnlEditAccount = new Panel();
            lblEditTitle = new Label();
            lblEditDisplayName = new Label();
            txtEditDisplayName = new TextBox();
            lblEditEmail = new Label();
            txtEditEmail = new TextBox();
            lblEditPassword = new Label();
            txtEditPassword = new TextBox();
            btnEditShowPassword = new Button();
            lblEditPasswordConfirm = new Label();
            txtEditPasswordConfirm = new TextBox();
            btnEditShowPasswordConfirm = new Button();
            btnSaveEdit = new Button();
            btnCloseEdit = new Button();
            lblEditHint = new Label();
            pnlInfoTop.SuspendLayout();
            pnlStats.SuspendLayout();
            cardGames.SuspendLayout();
            cardWins.SuspendLayout();
            cardDraws.SuspendLayout();
            cardLosses.SuspendLayout();
            cardWinrate.SuspendLayout();
            pnlEditAccount.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(121, 85, 72);
            lblTitle.Location = new Point(176, 29);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(562, 62);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "THÔNG TIN TÀI KHOẢN";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlInfoTop
            // 
            pnlInfoTop.BackColor = Color.White;
            pnlInfoTop.BorderStyle = BorderStyle.Fixed3D;
            pnlInfoTop.Controls.Add(lblUsernameIcon);
            pnlInfoTop.Controls.Add(lblUsername);
            pnlInfoTop.Controls.Add(lblDisplayNameIcon);
            pnlInfoTop.Controls.Add(lblDisplayName);
            pnlInfoTop.Controls.Add(lblEmailIcon);
            pnlInfoTop.Controls.Add(lblEmail);
            pnlInfoTop.Controls.Add(lblRatingIcon);
            pnlInfoTop.Controls.Add(lblRating);
            pnlInfoTop.Location = new Point(6, 110);
            pnlInfoTop.Name = "pnlInfoTop";
            pnlInfoTop.Size = new Size(932, 127);
            pnlInfoTop.TabIndex = 1;
            // 
            // lblUsernameIcon
            // 
            lblUsernameIcon.AutoSize = true;
            lblUsernameIcon.Font = new Font("Segoe UI Emoji", 20F);
            lblUsernameIcon.ForeColor = Color.DarkSlateBlue;
            lblUsernameIcon.Location = new Point(7, 18);
            lblUsernameIcon.Name = "lblUsernameIcon";
            lblUsernameIcon.Size = new Size(67, 46);
            lblUsernameIcon.TabIndex = 0;
            lblUsernameIcon.Text = "👤";
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 15.5F, FontStyle.Bold);
            lblUsername.ForeColor = Color.FromArgb(63, 81, 181);
            lblUsername.Location = new Point(80, 26);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(215, 36);
            lblUsername.TabIndex = 1;
            lblUsername.Text = "Tên tài khoản: ...";
            // 
            // lblDisplayNameIcon
            // 
            lblDisplayNameIcon.AutoSize = true;
            lblDisplayNameIcon.Font = new Font("Segoe UI Emoji", 20F);
            lblDisplayNameIcon.ForeColor = Color.DarkMagenta;
            lblDisplayNameIcon.Location = new Point(330, 18);
            lblDisplayNameIcon.Name = "lblDisplayNameIcon";
            lblDisplayNameIcon.Size = new Size(67, 46);
            lblDisplayNameIcon.TabIndex = 2;
            lblDisplayNameIcon.Text = "🏷";
            // 
            // lblDisplayName
            // 
            lblDisplayName.AutoSize = true;
            lblDisplayName.Font = new Font("Segoe UI", 13.5F, FontStyle.Bold);
            lblDisplayName.ForeColor = Color.FromArgb(205, 96, 144);
            lblDisplayName.Location = new Point(399, 26);
            lblDisplayName.Name = "lblDisplayName";
            lblDisplayName.Size = new Size(170, 31);
            lblDisplayName.TabIndex = 3;
            lblDisplayName.Text = "Tên hiển thị: ...";
            // 
            // lblEmailIcon
            // 
            lblEmailIcon.AutoSize = true;
            lblEmailIcon.Font = new Font("Segoe UI Emoji", 20F);
            lblEmailIcon.ForeColor = Color.Teal;
            lblEmailIcon.Location = new Point(7, 72);
            lblEmailIcon.Name = "lblEmailIcon";
            lblEmailIcon.Size = new Size(67, 46);
            lblEmailIcon.TabIndex = 4;
            lblEmailIcon.Text = "✉";
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 13.2000008F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblEmail.ForeColor = Color.Teal;
            lblEmail.Location = new Point(80, 80);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(103, 31);
            lblEmail.TabIndex = 5;
            lblEmail.Text = "Email: ...";
            // 
            // lblRatingIcon
            // 
            lblRatingIcon.AutoSize = true;
            lblRatingIcon.Font = new Font("Segoe UI Emoji", 20F);
            lblRatingIcon.ForeColor = Color.Goldenrod;
            lblRatingIcon.Location = new Point(330, 70);
            lblRatingIcon.Name = "lblRatingIcon";
            lblRatingIcon.Size = new Size(67, 46);
            lblRatingIcon.TabIndex = 6;
            lblRatingIcon.Text = "⭐";
            // 
            // lblRating
            // 
            lblRating.AutoSize = true;
            lblRating.Font = new Font("Segoe UI", 16.8F, FontStyle.Bold);
            lblRating.ForeColor = Color.DarkOrange;
            lblRating.Location = new Point(389, 80);
            lblRating.Name = "lblRating";
            lblRating.Size = new Size(211, 38);
            lblRating.TabIndex = 7;
            lblRating.Text = "Rating (Elo): ...";
            // 
            // pnlStats
            // 
            pnlStats.BackColor = Color.Transparent;
            pnlStats.Controls.Add(cardGames);
            pnlStats.Controls.Add(cardWins);
            pnlStats.Controls.Add(cardDraws);
            pnlStats.Controls.Add(cardLosses);
            pnlStats.Controls.Add(cardWinrate);
            pnlStats.Location = new Point(52, 255);
            pnlStats.Name = "pnlStats";
            pnlStats.Size = new Size(840, 140);
            pnlStats.TabIndex = 2;
            // 
            // cardGames
            // 
            cardGames.BackColor = Color.FromArgb(254, 248, 225);
            cardGames.BorderStyle = BorderStyle.Fixed3D;
            cardGames.Controls.Add(lblCardGames);
            cardGames.Controls.Add(lblCardGamesVal);
            cardGames.Location = new Point(0, 9);
            cardGames.Name = "cardGames";
            cardGames.Size = new Size(166, 120);
            cardGames.TabIndex = 0;
            // 
            // lblCardGames
            // 
            lblCardGames.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblCardGames.ForeColor = Color.FromArgb(202, 170, 53);
            lblCardGames.Location = new Point(23, 16);
            lblCardGames.Name = "lblCardGames";
            lblCardGames.Size = new Size(112, 36);
            lblCardGames.TabIndex = 0;
            lblCardGames.Text = "Tổng trận";
            // 
            // lblCardGamesVal
            // 
            lblCardGamesVal.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblCardGamesVal.ForeColor = Color.FromArgb(202, 170, 53);
            lblCardGamesVal.Location = new Point(3, 52);
            lblCardGamesVal.Name = "lblCardGamesVal";
            lblCardGamesVal.Size = new Size(156, 50);
            lblCardGamesVal.TabIndex = 1;
            lblCardGamesVal.Text = "80";
            lblCardGamesVal.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cardWins
            // 
            cardWins.BackColor = Color.FromArgb(232, 245, 233);
            cardWins.BorderStyle = BorderStyle.Fixed3D;
            cardWins.Controls.Add(lblCardWins);
            cardWins.Controls.Add(lblCardWinsVal);
            cardWins.Location = new Point(168, 9);
            cardWins.Name = "cardWins";
            cardWins.Size = new Size(166, 120);
            cardWins.TabIndex = 1;
            // 
            // lblCardWins
            // 
            lblCardWins.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblCardWins.ForeColor = Color.FromArgb(76, 175, 80);
            lblCardWins.Location = new Point(15, 16);
            lblCardWins.Name = "lblCardWins";
            lblCardWins.Size = new Size(112, 36);
            lblCardWins.TabIndex = 0;
            lblCardWins.Text = "Thắng";
            // 
            // lblCardWinsVal
            // 
            lblCardWinsVal.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblCardWinsVal.ForeColor = Color.FromArgb(76, 175, 80);
            lblCardWinsVal.Location = new Point(3, 52);
            lblCardWinsVal.Name = "lblCardWinsVal";
            lblCardWinsVal.Size = new Size(156, 50);
            lblCardWinsVal.TabIndex = 1;
            lblCardWinsVal.Text = "45";
            lblCardWinsVal.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cardDraws
            // 
            cardDraws.BackColor = Color.FromArgb(255, 249, 196);
            cardDraws.BorderStyle = BorderStyle.Fixed3D;
            cardDraws.Controls.Add(lblCardDraws);
            cardDraws.Controls.Add(lblCardDrawsVal);
            cardDraws.Location = new Point(336, 9);
            cardDraws.Name = "cardDraws";
            cardDraws.Size = new Size(166, 120);
            cardDraws.TabIndex = 2;
            // 
            // lblCardDraws
            // 
            lblCardDraws.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblCardDraws.ForeColor = Color.FromArgb(255, 152, 0);
            lblCardDraws.Location = new Point(15, 16);
            lblCardDraws.Name = "lblCardDraws";
            lblCardDraws.Size = new Size(112, 36);
            lblCardDraws.TabIndex = 0;
            lblCardDraws.Text = "Hòa";
            // 
            // lblCardDrawsVal
            // 
            lblCardDrawsVal.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblCardDrawsVal.ForeColor = Color.FromArgb(255, 152, 0);
            lblCardDrawsVal.Location = new Point(3, 52);
            lblCardDrawsVal.Name = "lblCardDrawsVal";
            lblCardDrawsVal.Size = new Size(156, 50);
            lblCardDrawsVal.TabIndex = 1;
            lblCardDrawsVal.Text = "12";
            lblCardDrawsVal.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cardLosses
            // 
            cardLosses.BackColor = Color.FromArgb(255, 235, 238);
            cardLosses.BorderStyle = BorderStyle.Fixed3D;
            cardLosses.Controls.Add(lblCardLosses);
            cardLosses.Controls.Add(lblCardLossesVal);
            cardLosses.Location = new Point(504, 9);
            cardLosses.Name = "cardLosses";
            cardLosses.Size = new Size(166, 120);
            cardLosses.TabIndex = 3;
            // 
            // lblCardLosses
            // 
            lblCardLosses.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblCardLosses.ForeColor = Color.FromArgb(244, 67, 54);
            lblCardLosses.Location = new Point(15, 16);
            lblCardLosses.Name = "lblCardLosses";
            lblCardLosses.Size = new Size(112, 36);
            lblCardLosses.TabIndex = 0;
            lblCardLosses.Text = "Thua";
            // 
            // lblCardLossesVal
            // 
            lblCardLossesVal.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblCardLossesVal.ForeColor = Color.FromArgb(244, 67, 54);
            lblCardLossesVal.Location = new Point(3, 52);
            lblCardLossesVal.Name = "lblCardLossesVal";
            lblCardLossesVal.Size = new Size(156, 50);
            lblCardLossesVal.TabIndex = 1;
            lblCardLossesVal.Text = "23";
            lblCardLossesVal.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cardWinrate
            // 
            cardWinrate.BackColor = Color.FromArgb(227, 242, 253);
            cardWinrate.BorderStyle = BorderStyle.Fixed3D;
            cardWinrate.Controls.Add(lblCardWinrate);
            cardWinrate.Controls.Add(lblCardWinrateVal);
            cardWinrate.Location = new Point(672, 9);
            cardWinrate.Name = "cardWinrate";
            cardWinrate.Size = new Size(166, 120);
            cardWinrate.TabIndex = 4;
            // 
            // lblCardWinrate
            // 
            lblCardWinrate.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblCardWinrate.ForeColor = Color.FromArgb(33, 150, 243);
            lblCardWinrate.Location = new Point(15, 16);
            lblCardWinrate.Name = "lblCardWinrate";
            lblCardWinrate.Size = new Size(112, 36);
            lblCardWinrate.TabIndex = 0;
            lblCardWinrate.Text = "Tỉ lệ thắng";
            // 
            // lblCardWinrateVal
            // 
            lblCardWinrateVal.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblCardWinrateVal.ForeColor = Color.FromArgb(33, 150, 243);
            lblCardWinrateVal.Location = new Point(3, 52);
            lblCardWinrateVal.Name = "lblCardWinrateVal";
            lblCardWinrateVal.Size = new Size(156, 50);
            lblCardWinrateVal.TabIndex = 1;
            lblCardWinrateVal.Text = "56.2%";
            lblCardWinrateVal.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnShowEdit
            // 
            btnShowEdit.BackColor = Color.FromArgb(255, 213, 79);
            btnShowEdit.FlatAppearance.BorderSize = 0;
            btnShowEdit.FlatStyle = FlatStyle.Flat;
            btnShowEdit.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            btnShowEdit.ForeColor = Color.FromArgb(120, 100, 20);
            btnShowEdit.Location = new Point(250, 420);
            btnShowEdit.Name = "btnShowEdit";
            btnShowEdit.Size = new Size(200, 50);
            btnShowEdit.TabIndex = 3;
            btnShowEdit.Text = "CHỈNH SỬA";
            btnShowEdit.UseVisualStyleBackColor = false;
            btnShowEdit.Click += btnShowEdit_Click;
            // 
            // btnBackToLobby
            // 
            btnBackToLobby.BackColor = Color.FromArgb(160, 106, 88);
            btnBackToLobby.FlatAppearance.BorderSize = 0;
            btnBackToLobby.FlatStyle = FlatStyle.Flat;
            btnBackToLobby.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            btnBackToLobby.ForeColor = Color.White;
            btnBackToLobby.Location = new Point(490, 420);
            btnBackToLobby.Name = "btnBackToLobby";
            btnBackToLobby.Size = new Size(210, 50);
            btnBackToLobby.TabIndex = 4;
            btnBackToLobby.Text = "QUAY VỀ SẢNH";
            btnBackToLobby.UseVisualStyleBackColor = false;
            btnBackToLobby.Click += btnBackToLobby_Click;
            // 
            // pnlEditAccount
            // 
            pnlEditAccount.BackColor = Color.FromArgb(255, 253, 231);
            pnlEditAccount.BorderStyle = BorderStyle.FixedSingle;
            pnlEditAccount.Controls.Add(lblEditTitle);
            pnlEditAccount.Controls.Add(lblEditDisplayName);
            pnlEditAccount.Controls.Add(txtEditDisplayName);
            pnlEditAccount.Controls.Add(lblEditEmail);
            pnlEditAccount.Controls.Add(txtEditEmail);
            pnlEditAccount.Controls.Add(lblEditPassword);
            pnlEditAccount.Controls.Add(txtEditPassword);
            pnlEditAccount.Controls.Add(btnEditShowPassword);
            pnlEditAccount.Controls.Add(lblEditPasswordConfirm);
            pnlEditAccount.Controls.Add(txtEditPasswordConfirm);
            pnlEditAccount.Controls.Add(btnEditShowPasswordConfirm);
            pnlEditAccount.Controls.Add(btnSaveEdit);
            pnlEditAccount.Controls.Add(btnCloseEdit);
            pnlEditAccount.Controls.Add(lblEditHint);
            pnlEditAccount.Location = new Point(6, 490);
            pnlEditAccount.Name = "pnlEditAccount";
            pnlEditAccount.Size = new Size(932, 230);
            pnlEditAccount.TabIndex = 5;
            pnlEditAccount.Visible = false;
            // 
            // lblEditTitle
            // 
            lblEditTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblEditTitle.ForeColor = Color.FromArgb(121, 85, 72);
            lblEditTitle.Location = new Point(3, 10);
            lblEditTitle.Name = "lblEditTitle";
            lblEditTitle.Size = new Size(852, 35);
            lblEditTitle.TabIndex = 0;
            lblEditTitle.Text = "CHỈNH SỬA THÔNG TIN";
            lblEditTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblEditDisplayName
            // 
            lblEditDisplayName.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblEditDisplayName.Location = new Point(403, 55);
            lblEditDisplayName.Name = "lblEditDisplayName";
            lblEditDisplayName.Size = new Size(133, 26);
            lblEditDisplayName.TabIndex = 1;
            lblEditDisplayName.Text = "Tên hiển thị:";
            // 
            // txtEditDisplayName
            // 
            txtEditDisplayName.BackColor = Color.FromArgb(232, 245, 233);
            txtEditDisplayName.Font = new Font("Segoe UI", 11F);
            txtEditDisplayName.Location = new Point(542, 55);
            txtEditDisplayName.Name = "txtEditDisplayName";
            txtEditDisplayName.Size = new Size(281, 32);
            txtEditDisplayName.TabIndex = 2;
            // 
            // lblEditEmail
            // 
            lblEditEmail.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblEditEmail.Location = new Point(4, 55);
            lblEditEmail.Name = "lblEditEmail";
            lblEditEmail.Size = new Size(70, 26);
            lblEditEmail.TabIndex = 3;
            lblEditEmail.Text = "Email:";
            // 
            // txtEditEmail
            // 
            txtEditEmail.BackColor = Color.FromArgb(232, 245, 233);
            txtEditEmail.Font = new Font("Segoe UI", 11F);
            txtEditEmail.Location = new Point(78, 55);
            txtEditEmail.Name = "txtEditEmail";
            txtEditEmail.Size = new Size(309, 32);
            txtEditEmail.TabIndex = 4;
            // 
            // lblEditPassword
            // 
            lblEditPassword.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblEditPassword.Location = new Point(4, 100);
            lblEditPassword.Name = "lblEditPassword";
            lblEditPassword.Size = new Size(140, 22);
            lblEditPassword.TabIndex = 5;
            lblEditPassword.Text = "Mật khẩu mới:";
            // 
            // txtEditPassword
            // 
            txtEditPassword.BackColor = Color.FromArgb(255, 249, 224);
            txtEditPassword.Font = new Font("Segoe UI", 11F);
            txtEditPassword.Location = new Point(150, 97);
            txtEditPassword.Name = "txtEditPassword";
            txtEditPassword.Size = new Size(201, 32);
            txtEditPassword.TabIndex = 6;
            txtEditPassword.UseSystemPasswordChar = true;
            // 
            // btnEditShowPassword
            // 
            btnEditShowPassword.BackColor = Color.Transparent;
            btnEditShowPassword.FlatAppearance.BorderSize = 0;
            btnEditShowPassword.FlatStyle = FlatStyle.Flat;
            btnEditShowPassword.Location = new Point(357, 97);
            btnEditShowPassword.Name = "btnEditShowPassword";
            btnEditShowPassword.Size = new Size(30, 25);
            btnEditShowPassword.TabIndex = 7;
            btnEditShowPassword.Text = "👁";
            btnEditShowPassword.UseVisualStyleBackColor = false;
            btnEditShowPassword.Click += btnEditShowPassword_Click;
            // 
            // lblEditPasswordConfirm
            // 
            lblEditPasswordConfirm.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblEditPasswordConfirm.Location = new Point(403, 95);
            lblEditPasswordConfirm.Name = "lblEditPasswordConfirm";
            lblEditPasswordConfirm.Size = new Size(188, 29);
            lblEditPasswordConfirm.TabIndex = 8;
            lblEditPasswordConfirm.Text = "Nhập lại mật khẩu:";
            // 
            // txtEditPasswordConfirm
            // 
            txtEditPasswordConfirm.BackColor = Color.FromArgb(255, 249, 224);
            txtEditPasswordConfirm.Font = new Font("Segoe UI", 11F);
            txtEditPasswordConfirm.Location = new Point(587, 92);
            txtEditPasswordConfirm.Name = "txtEditPasswordConfirm";
            txtEditPasswordConfirm.Size = new Size(200, 32);
            txtEditPasswordConfirm.TabIndex = 9;
            txtEditPasswordConfirm.UseSystemPasswordChar = true;
            // 
            // btnEditShowPasswordConfirm
            // 
            btnEditShowPasswordConfirm.BackColor = Color.Transparent;
            btnEditShowPasswordConfirm.FlatAppearance.BorderSize = 0;
            btnEditShowPasswordConfirm.FlatStyle = FlatStyle.Flat;
            btnEditShowPasswordConfirm.Location = new Point(793, 90);
            btnEditShowPasswordConfirm.Name = "btnEditShowPasswordConfirm";
            btnEditShowPasswordConfirm.Size = new Size(30, 39);
            btnEditShowPasswordConfirm.TabIndex = 10;
            btnEditShowPasswordConfirm.Text = "👁";
            btnEditShowPasswordConfirm.UseVisualStyleBackColor = false;
            btnEditShowPasswordConfirm.Click += btnEditShowPasswordConfirm_Click;
            // 
            // btnSaveEdit
            // 
            btnSaveEdit.BackColor = Color.FromArgb(255, 213, 79);
            btnSaveEdit.FlatAppearance.BorderSize = 0;
            btnSaveEdit.FlatStyle = FlatStyle.Flat;
            btnSaveEdit.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            btnSaveEdit.ForeColor = Color.FromArgb(120, 100, 20);
            btnSaveEdit.Location = new Point(360, 173);
            btnSaveEdit.Name = "btnSaveEdit";
            btnSaveEdit.Size = new Size(110, 38);
            btnSaveEdit.TabIndex = 11;
            btnSaveEdit.Text = "Lưu";
            btnSaveEdit.UseVisualStyleBackColor = false;
            btnSaveEdit.Click += btnSaveEdit_Click;
            // 
            // btnCloseEdit
            // 
            btnCloseEdit.BackColor = Color.Transparent;
            btnCloseEdit.FlatAppearance.BorderSize = 0;
            btnCloseEdit.FlatStyle = FlatStyle.Flat;
            btnCloseEdit.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
            btnCloseEdit.ForeColor = Color.Gray;
            btnCloseEdit.Location = new Point(861, 3);
            btnCloseEdit.Name = "btnCloseEdit";
            btnCloseEdit.Size = new Size(57, 48);
            btnCloseEdit.TabIndex = 12;
            btnCloseEdit.Text = "✖";
            btnCloseEdit.UseVisualStyleBackColor = false;
            btnCloseEdit.Click += btnCloseEdit_Click;
            // 
            // lblEditHint
            // 
            lblEditHint.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
            lblEditHint.ForeColor = Color.Gray;
            lblEditHint.Location = new Point(125, 134);
            lblEditHint.Name = "lblEditHint";
            lblEditHint.Size = new Size(628, 36);
            lblEditHint.TabIndex = 13;
            lblEditHint.Text = "* Đổi tên/email hoặc mật khẩu mới (ít nhất 8 ký tự, để trống nếu không đổi)";
            // 
            // AccountSetting
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(251, 238, 212);
            ClientSize = new Size(950, 740);
            Controls.Add(lblTitle);
            Controls.Add(pnlInfoTop);
            Controls.Add(pnlStats);
            Controls.Add(btnShowEdit);
            Controls.Add(btnBackToLobby);
            Controls.Add(pnlEditAccount);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AccountSetting";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Thông Tin Tài Khoản";
            pnlInfoTop.ResumeLayout(false);
            pnlInfoTop.PerformLayout();
            pnlStats.ResumeLayout(false);
            cardGames.ResumeLayout(false);
            cardWins.ResumeLayout(false);
            cardDraws.ResumeLayout(false);
            cardLosses.ResumeLayout(false);
            cardWinrate.ResumeLayout(false);
            pnlEditAccount.ResumeLayout(false);
            pnlEditAccount.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
