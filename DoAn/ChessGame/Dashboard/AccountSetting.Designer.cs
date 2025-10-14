namespace ChessGame
{
    partial class AccountSetting
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
            lblTieuDe = new Label();
            pnlAvatar = new Panel();
            picAvatar = new PictureBox();
            btnChangeAvatar = new Button();
            lblUsername = new Label();
            txtUsername = new TextBox();
            pnlUsernameUnderline = new Panel();
            lblEmail = new Label();
            txtEmail = new TextBox();
            pnlEmailUnderline = new Panel();
            lblMatKhauMoi = new Label();
            txtMatKhauMoi = new TextBox();
            pnlMKMoiUnderline = new Panel();
            btnShowMKMoi = new Button();
            lblNhapLaiMK = new Label();
            txtNhapLaiMK = new TextBox();
            pnlNLMKUnderline = new Panel();
            btnShowNLMK = new Button();
            btnSave = new Button();
            btnCancel = new Button();
            lblHuongDan = new Label();
            pnlAvatar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picAvatar).BeginInit();
            SuspendLayout();
            // 
            // lblTieuDe
            // 
            lblTieuDe.AutoSize = true;
            lblTieuDe.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTieuDe.ForeColor = Color.FromArgb(78, 49, 41);
            lblTieuDe.Location = new Point(121, 19);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(479, 54);
            lblTieuDe.TabIndex = 0;
            lblTieuDe.Text = "⚙ CÀI ĐẶT TÀI KHOẢN";
            // 
            // pnlAvatar
            // 
            pnlAvatar.BackColor = Color.FromArgb(118, 74, 61);
            pnlAvatar.Controls.Add(picAvatar);
            pnlAvatar.Controls.Add(btnChangeAvatar);
            pnlAvatar.Location = new Point(250, 85);
            pnlAvatar.Name = "pnlAvatar";
            pnlAvatar.Size = new Size(200, 170);
            pnlAvatar.TabIndex = 1;
            // 
            // picAvatar
            // 
            picAvatar.BackColor = Color.White;
            picAvatar.Location = new Point(50, 20);
            picAvatar.Name = "picAvatar";
            picAvatar.Size = new Size(100, 100);
            picAvatar.SizeMode = PictureBoxSizeMode.Zoom;
            picAvatar.TabIndex = 0;
            picAvatar.TabStop = false;
            // 
            // btnChangeAvatar
            // 
            btnChangeAvatar.BackColor = Color.FromArgb(133, 181, 100);
            btnChangeAvatar.FlatAppearance.BorderSize = 0;
            btnChangeAvatar.FlatStyle = FlatStyle.Flat;
            btnChangeAvatar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnChangeAvatar.ForeColor = Color.White;
            btnChangeAvatar.Location = new Point(20, 130);
            btnChangeAvatar.Name = "btnChangeAvatar";
            btnChangeAvatar.Size = new Size(160, 35);
            btnChangeAvatar.TabIndex = 1;
            btnChangeAvatar.Text = "📷 Đổi Avatar";
            btnChangeAvatar.UseVisualStyleBackColor = false;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblUsername.ForeColor = Color.FromArgb(78, 49, 41);
            lblUsername.Location = new Point(100, 280);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(131, 28);
            lblUsername.TabIndex = 2;
            lblUsername.Text = "Tên Hiển Thị";
            // 
            // txtUsername
            // 
            txtUsername.BackColor = Color.FromArgb(247, 234, 214);
            txtUsername.BorderStyle = BorderStyle.None;
            txtUsername.Font = new Font("Segoe UI", 12F);
            txtUsername.ForeColor = Color.FromArgb(78, 49, 41);
            txtUsername.Location = new Point(100, 310);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(500, 27);
            txtUsername.TabIndex = 3;
            txtUsername.Text = "Player123";
            // 
            // pnlUsernameUnderline
            // 
            pnlUsernameUnderline.BackColor = Color.FromArgb(118, 74, 61);
            pnlUsernameUnderline.Location = new Point(100, 335);
            pnlUsernameUnderline.Name = "pnlUsernameUnderline";
            pnlUsernameUnderline.Size = new Size(500, 1);
            pnlUsernameUnderline.TabIndex = 4;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblEmail.ForeColor = Color.FromArgb(78, 49, 41);
            lblEmail.Location = new Point(100, 360);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(64, 28);
            lblEmail.TabIndex = 5;
            lblEmail.Text = "Email";
            // 
            // txtEmail
            // 
            txtEmail.BackColor = Color.FromArgb(247, 234, 214);
            txtEmail.BorderStyle = BorderStyle.None;
            txtEmail.Font = new Font("Segoe UI", 12F);
            txtEmail.ForeColor = Color.FromArgb(78, 49, 41);
            txtEmail.Location = new Point(100, 390);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(500, 27);
            txtEmail.TabIndex = 6;
            txtEmail.Text = "player@example.com";
            // 
            // pnlEmailUnderline
            // 
            pnlEmailUnderline.BackColor = Color.FromArgb(118, 74, 61);
            pnlEmailUnderline.Location = new Point(100, 415);
            pnlEmailUnderline.Name = "pnlEmailUnderline";
            pnlEmailUnderline.Size = new Size(500, 1);
            pnlEmailUnderline.TabIndex = 7;
            // 
            // lblMatKhauMoi
            // 
            lblMatKhauMoi.AutoSize = true;
            lblMatKhauMoi.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblMatKhauMoi.ForeColor = Color.FromArgb(78, 49, 41);
            lblMatKhauMoi.Location = new Point(100, 440);
            lblMatKhauMoi.Name = "lblMatKhauMoi";
            lblMatKhauMoi.Size = new Size(240, 28);
            lblMatKhauMoi.TabIndex = 8;
            lblMatKhauMoi.Text = "Mật Khẩu Mới (nếu đổi)";
            // 
            // txtMatKhauMoi
            // 
            txtMatKhauMoi.BackColor = Color.FromArgb(247, 234, 214);
            txtMatKhauMoi.BorderStyle = BorderStyle.None;
            txtMatKhauMoi.Font = new Font("Segoe UI", 12F);
            txtMatKhauMoi.ForeColor = Color.FromArgb(78, 49, 41);
            txtMatKhauMoi.Location = new Point(100, 470);
            txtMatKhauMoi.Name = "txtMatKhauMoi";
            txtMatKhauMoi.Size = new Size(460, 27);
            txtMatKhauMoi.TabIndex = 9;
            txtMatKhauMoi.UseSystemPasswordChar = true;
            // 
            // pnlMKMoiUnderline
            // 
            pnlMKMoiUnderline.BackColor = Color.FromArgb(118, 74, 61);
            pnlMKMoiUnderline.Location = new Point(100, 495);
            pnlMKMoiUnderline.Name = "pnlMKMoiUnderline";
            pnlMKMoiUnderline.Size = new Size(500, 1);
            pnlMKMoiUnderline.TabIndex = 10;
            // 
            // btnShowMKMoi
            // 
            btnShowMKMoi.BackColor = Color.Transparent;
            btnShowMKMoi.FlatAppearance.BorderSize = 0;
            btnShowMKMoi.FlatStyle = FlatStyle.Flat;
            btnShowMKMoi.Font = new Font("Segoe UI", 10F);
            btnShowMKMoi.Location = new Point(565, 467);
            btnShowMKMoi.Name = "btnShowMKMoi";
            btnShowMKMoi.Size = new Size(30, 25);
            btnShowMKMoi.TabIndex = 11;
            btnShowMKMoi.Text = "👁";
            btnShowMKMoi.UseVisualStyleBackColor = false;
            // 
            // lblNhapLaiMK
            // 
            lblNhapLaiMK.AutoSize = true;
            lblNhapLaiMK.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblNhapLaiMK.ForeColor = Color.FromArgb(78, 49, 41);
            lblNhapLaiMK.Location = new Point(100, 510);
            lblNhapLaiMK.Name = "lblNhapLaiMK";
            lblNhapLaiMK.Size = new Size(194, 28);
            lblNhapLaiMK.TabIndex = 12;
            lblNhapLaiMK.Text = "Nhập Lại Mật Khẩu";
            // 
            // txtNhapLaiMK
            // 
            txtNhapLaiMK.BackColor = Color.FromArgb(247, 234, 214);
            txtNhapLaiMK.BorderStyle = BorderStyle.None;
            txtNhapLaiMK.Font = new Font("Segoe UI", 12F);
            txtNhapLaiMK.ForeColor = Color.FromArgb(78, 49, 41);
            txtNhapLaiMK.Location = new Point(100, 540);
            txtNhapLaiMK.Name = "txtNhapLaiMK";
            txtNhapLaiMK.Size = new Size(460, 27);
            txtNhapLaiMK.TabIndex = 13;
            txtNhapLaiMK.UseSystemPasswordChar = true;
            // 
            // pnlNLMKUnderline
            // 
            pnlNLMKUnderline.BackColor = Color.FromArgb(118, 74, 61);
            pnlNLMKUnderline.Location = new Point(100, 565);
            pnlNLMKUnderline.Name = "pnlNLMKUnderline";
            pnlNLMKUnderline.Size = new Size(500, 1);
            pnlNLMKUnderline.TabIndex = 14;
            // 
            // btnShowNLMK
            // 
            btnShowNLMK.BackColor = Color.Transparent;
            btnShowNLMK.FlatAppearance.BorderSize = 0;
            btnShowNLMK.FlatStyle = FlatStyle.Flat;
            btnShowNLMK.Font = new Font("Segoe UI", 10F);
            btnShowNLMK.Location = new Point(565, 537);
            btnShowNLMK.Name = "btnShowNLMK";
            btnShowNLMK.Size = new Size(30, 25);
            btnShowNLMK.TabIndex = 15;
            btnShowNLMK.Text = "👁";
            btnShowNLMK.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(133, 181, 100);
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(202, 598);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(150, 45);
            btnSave.TabIndex = 17;
            btnSave.Text = "✅ LƯU";
            btnSave.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(160, 106, 88);
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 14F);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(372, 598);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(130, 45);
            btnCancel.TabIndex = 18;
            btnCancel.Text = "❌ Hủy";
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // lblHuongDan
            // 
            lblHuongDan.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblHuongDan.ForeColor = Color.FromArgb(118, 74, 61);
            lblHuongDan.Location = new Point(100, 575);
            lblHuongDan.Name = "lblHuongDan";
            lblHuongDan.Size = new Size(500, 20);
            lblHuongDan.TabIndex = 16;
            lblHuongDan.Text = "* Để trống mật khẩu nếu không muốn đổi";
            // 
            // AccountSetting
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 217, 181);
            ClientSize = new Size(700, 650);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(lblHuongDan);
            Controls.Add(btnShowNLMK);
            Controls.Add(pnlNLMKUnderline);
            Controls.Add(txtNhapLaiMK);
            Controls.Add(lblNhapLaiMK);
            Controls.Add(btnShowMKMoi);
            Controls.Add(pnlMKMoiUnderline);
            Controls.Add(txtMatKhauMoi);
            Controls.Add(lblMatKhauMoi);
            Controls.Add(pnlEmailUnderline);
            Controls.Add(txtEmail);
            Controls.Add(lblEmail);
            Controls.Add(pnlUsernameUnderline);
            Controls.Add(txtUsername);
            Controls.Add(lblUsername);
            Controls.Add(pnlAvatar);
            Controls.Add(lblTieuDe);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "AccountSetting";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Chess - Cài Đặt Tài Khoản";
            pnlAvatar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picAvatar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTieuDe;
        private Panel pnlAvatar;
        private PictureBox picAvatar;
        private Button btnChangeAvatar;
        private Label lblUsername;
        private TextBox txtUsername;
        private Panel pnlUsernameUnderline;
        private Label lblEmail;
        private TextBox txtEmail;
        private Panel pnlEmailUnderline;
        private Label lblMatKhauMoi;
        private TextBox txtMatKhauMoi;
        private Panel pnlMKMoiUnderline;
        private Button btnShowMKMoi;
        private Label lblNhapLaiMK;
        private TextBox txtNhapLaiMK;
        private Panel pnlNLMKUnderline;
        private Button btnShowNLMK;
        private Button btnSave;
        private Button btnCancel;
        private Label lblHuongDan;
    }
}
