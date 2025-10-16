namespace ChessGame
{
    partial class Recovery
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
            lblTieuDe = new Label();
            lblMoTa = new Label();
            lblMatKhauMoi = new Label();
            txtMatKhauMoi = new TextBox();
            pnlMKMoiUnderline = new Panel();
            btnShowMKMoi = new Button();
            btnHideMKMoi = new Button();
            lblNhapLaiMK = new Label();
            txtNhapLaiMK = new TextBox();
            pnlNLMKUnderline = new Panel();
            btnShowNLMK = new Button();
            btnHideNLMK = new Button();
            btnXacNhan = new Button();
            SuspendLayout();

            lblTieuDe.AutoSize = true;
            lblTieuDe.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTieuDe.ForeColor = Color.FromArgb(78, 49, 41);
            lblTieuDe.Location = new Point(150, 30);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(401, 54);
            lblTieuDe.TabIndex = 0;
            lblTieuDe.Text = "ĐẶT LẠI MẬT KHẨU";

            lblMoTa.Font = new Font("Segoe UI", 11F);
            lblMoTa.ForeColor = Color.FromArgb(78, 49, 41);
            lblMoTa.Location = new Point(150, 95);
            lblMoTa.Name = "lblMoTa";
            lblMoTa.Size = new Size(400, 25);
            lblMoTa.TabIndex = 1;
            lblMoTa.Text = "Nhập mật khẩu mới cho tài khoản của bạn";
            lblMoTa.TextAlign = ContentAlignment.MiddleCenter;

            lblMatKhauMoi.AutoSize = true;
            lblMatKhauMoi.Font = new Font("Segoe UI", 11F);
            lblMatKhauMoi.ForeColor = Color.FromArgb(78, 49, 41);
            lblMatKhauMoi.Location = new Point(99, 136);
            lblMatKhauMoi.Name = "lblMatKhauMoi";
            lblMatKhauMoi.Size = new Size(131, 25);
            lblMatKhauMoi.TabIndex = 3;
            lblMatKhauMoi.Text = "Mật Khẩu Mới";

            txtMatKhauMoi.BackColor = Color.FromArgb(247, 234, 214);
            txtMatKhauMoi.BorderStyle = BorderStyle.None;
            txtMatKhauMoi.Font = new Font("Segoe UI", 12F);
            txtMatKhauMoi.ForeColor = Color.FromArgb(78, 49, 41);
            txtMatKhauMoi.Location = new Point(99, 161);
            txtMatKhauMoi.Name = "txtMatKhauMoi";
            txtMatKhauMoi.Size = new Size(460, 27);
            txtMatKhauMoi.TabIndex = 4;
            txtMatKhauMoi.UseSystemPasswordChar = true;

            pnlMKMoiUnderline.BackColor = Color.FromArgb(118, 74, 61);
            pnlMKMoiUnderline.Location = new Point(99, 186);
            pnlMKMoiUnderline.Name = "pnlMKMoiUnderline";
            pnlMKMoiUnderline.Size = new Size(500, 1);
            pnlMKMoiUnderline.TabIndex = 5;

            btnShowMKMoi.BackColor = Color.Transparent;
            btnShowMKMoi.FlatAppearance.BorderSize = 0;
            btnShowMKMoi.FlatStyle = FlatStyle.Flat;
            btnShowMKMoi.Location = new Point(564, 158);
            btnShowMKMoi.Name = "btnShowMKMoi";
            btnShowMKMoi.Size = new Size(30, 25);
            btnShowMKMoi.TabIndex = 6;
            btnShowMKMoi.Text = "👁";
            btnShowMKMoi.UseVisualStyleBackColor = false;

            btnHideMKMoi.BackColor = Color.Transparent;
            btnHideMKMoi.FlatAppearance.BorderSize = 0;
            btnHideMKMoi.FlatStyle = FlatStyle.Flat;
            btnHideMKMoi.Location = new Point(564, 158);
            btnHideMKMoi.Name = "btnHideMKMoi";
            btnHideMKMoi.Size = new Size(30, 25);
            btnHideMKMoi.TabIndex = 7;
            btnHideMKMoi.Text = "👁‍🗨";
            btnHideMKMoi.UseVisualStyleBackColor = false;
            btnHideMKMoi.Visible = false;

            lblNhapLaiMK.AutoSize = true;
            lblNhapLaiMK.Font = new Font("Segoe UI", 11F);
            lblNhapLaiMK.ForeColor = Color.FromArgb(78, 49, 41);
            lblNhapLaiMK.Location = new Point(99, 206);
            lblNhapLaiMK.Name = "lblNhapLaiMK";
            lblNhapLaiMK.Size = new Size(173, 25);
            lblNhapLaiMK.TabIndex = 8;
            lblNhapLaiMK.Text = "Nhập Lại Mật Khẩu";

            txtNhapLaiMK.BackColor = Color.FromArgb(247, 234, 214);
            txtNhapLaiMK.BorderStyle = BorderStyle.None;
            txtNhapLaiMK.Font = new Font("Segoe UI", 12F);
            txtNhapLaiMK.ForeColor = Color.FromArgb(78, 49, 41);
            txtNhapLaiMK.Location = new Point(99, 231);
            txtNhapLaiMK.Name = "txtNhapLaiMK";
            txtNhapLaiMK.Size = new Size(460, 27);
            txtNhapLaiMK.TabIndex = 9;
            txtNhapLaiMK.UseSystemPasswordChar = true;

            pnlNLMKUnderline.BackColor = Color.FromArgb(118, 74, 61);
            pnlNLMKUnderline.Location = new Point(99, 256);
            pnlNLMKUnderline.Name = "pnlNLMKUnderline";
            pnlNLMKUnderline.Size = new Size(500, 1);
            pnlNLMKUnderline.TabIndex = 10;

            btnShowNLMK.BackColor = Color.Transparent;
            btnShowNLMK.FlatAppearance.BorderSize = 0;
            btnShowNLMK.FlatStyle = FlatStyle.Flat;
            btnShowNLMK.Location = new Point(564, 228);
            btnShowNLMK.Name = "btnShowNLMK";
            btnShowNLMK.Size = new Size(30, 25);
            btnShowNLMK.TabIndex = 11;
            btnShowNLMK.Text = "👁";
            btnShowNLMK.UseVisualStyleBackColor = false;

            btnHideNLMK.BackColor = Color.Transparent;
            btnHideNLMK.FlatAppearance.BorderSize = 0;
            btnHideNLMK.FlatStyle = FlatStyle.Flat;
            btnHideNLMK.Location = new Point(564, 228);
            btnHideNLMK.Name = "btnHideNLMK";
            btnHideNLMK.Size = new Size(30, 25);
            btnHideNLMK.TabIndex = 12;
            btnHideNLMK.Text = "👁‍🗨";
            btnHideNLMK.UseVisualStyleBackColor = false;
            btnHideNLMK.Visible = false;

            btnXacNhan.BackColor = Color.FromArgb(133, 181, 100);
            btnXacNhan.FlatAppearance.BorderSize = 0;
            btnXacNhan.FlatStyle = FlatStyle.Flat;
            btnXacNhan.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnXacNhan.ForeColor = Color.White;
            btnXacNhan.Location = new Point(150, 283);
            btnXacNhan.Name = "btnXacNhan";
            btnXacNhan.Size = new Size(364, 50);
            btnXacNhan.TabIndex = 14;
            btnXacNhan.Text = "XÁC NHẬN";
            btnXacNhan.UseVisualStyleBackColor = false;

            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 217, 181);
            ClientSize = new Size(700, 550);
            Controls.Add(btnXacNhan);
            Controls.Add(btnHideNLMK);
            Controls.Add(btnShowNLMK);
            Controls.Add(pnlNLMKUnderline);
            Controls.Add(txtNhapLaiMK);
            Controls.Add(lblNhapLaiMK);
            Controls.Add(btnHideMKMoi);
            Controls.Add(btnShowMKMoi);
            Controls.Add(pnlMKMoiUnderline);
            Controls.Add(txtMatKhauMoi);
            Controls.Add(lblMatKhauMoi);
            Controls.Add(lblMoTa);
            Controls.Add(lblTieuDe);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Recovery";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đặt Lại Mật Khẩu";
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTieuDe;
        private Label lblMoTa;
        private Label lblMatKhauMoi;
        private TextBox txtMatKhauMoi;
        private Panel pnlMKMoiUnderline;
        private Button btnShowMKMoi;
        private Button btnHideMKMoi;
        private Label lblNhapLaiMK;
        private TextBox txtNhapLaiMK;
        private Panel pnlNLMKUnderline;
        private Button btnShowNLMK;
        private Button btnHideNLMK;
        private Button btnXacNhan;
    }
}
