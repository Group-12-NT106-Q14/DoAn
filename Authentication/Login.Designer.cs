namespace ChessGame
{
    partial class frmLogin
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
            pnlLeft = new Panel();
            lblKingIcon = new Label();
            lblQuote = new Label();
            pnlRight = new Panel();
            lblTieuDe = new Label();
            lblTKLabel = new Label();
            txtTK = new TextBox();
            pnlTKUnderline = new Panel();
            lblMKLabel = new Label();
            txtMK = new TextBox();
            pnlMKUnderline = new Panel();
            btnĐăngNhập = new Button();
            lblMoiDen = new Label();
            btnĐK = new Button();
            btnQMK = new Button();
            btnShow = new Button();
            btnHide = new Button();
            pnlLeft.SuspendLayout();
            pnlRight.SuspendLayout();
            SuspendLayout();
            // 
            // pnlLeft
            // 
            pnlLeft.BackColor = Color.FromArgb(118, 74, 61);
            pnlLeft.Controls.Add(lblKingIcon);
            pnlLeft.Controls.Add(lblQuote);
            pnlLeft.Dock = DockStyle.Left;
            pnlLeft.Location = new Point(0, 0);
            pnlLeft.Name = "pnlLeft";
            pnlLeft.Size = new Size(360, 600);
            pnlLeft.TabIndex = 0;
            // 
            // lblKingIcon
            // 
            lblKingIcon.AutoSize = true;
            lblKingIcon.Font = new Font("Segoe UI", 72F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblKingIcon.ForeColor = Color.White;
            lblKingIcon.Location = new Point(86, 201);
            lblKingIcon.Name = "lblKingIcon";
            lblKingIcon.Size = new Size(187, 159);
            lblKingIcon.TabIndex = 2;
            lblKingIcon.Text = "♔";
            // 
            // lblQuote
            // 
            lblQuote.Font = new Font("Segoe UI", 12F, FontStyle.Italic);
            lblQuote.ForeColor = Color.White;
            lblQuote.Location = new Point(30, 360);
            lblQuote.Name = "lblQuote";
            lblQuote.Size = new Size(300, 80);
            lblQuote.TabIndex = 1;
            lblQuote.Text = "\"Người thành công là người có lối đi riêng\"";
            lblQuote.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlRight
            // 
            pnlRight.BackColor = Color.FromArgb(240, 217, 181);
            pnlRight.Controls.Add(lblTieuDe);
            pnlRight.Controls.Add(lblTKLabel);
            pnlRight.Controls.Add(txtTK);
            pnlRight.Controls.Add(pnlTKUnderline);
            pnlRight.Controls.Add(lblMKLabel);
            pnlRight.Controls.Add(txtMK);
            pnlRight.Controls.Add(pnlMKUnderline);
            pnlRight.Controls.Add(btnĐăngNhập);
            pnlRight.Controls.Add(lblMoiDen);
            pnlRight.Controls.Add(btnĐK);
            pnlRight.Controls.Add(btnQMK);
            pnlRight.Controls.Add(btnShow);
            pnlRight.Controls.Add(btnHide);
            pnlRight.Location = new Point(360, 0);
            pnlRight.Name = "pnlRight";
            pnlRight.Size = new Size(540, 600);
            pnlRight.TabIndex = 1;
            pnlRight.Paint += pnlRight_Paint;
            // 
            // lblTieuDe
            // 
            lblTieuDe.AutoSize = true;
            lblTieuDe.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTieuDe.ForeColor = Color.FromArgb(78, 49, 41);
            lblTieuDe.Location = new Point(150, 60);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(268, 54);
            lblTieuDe.TabIndex = 0;
            lblTieuDe.Text = "ĐĂNG NHẬP";
            // 
            // lblTKLabel
            // 
            lblTKLabel.AutoSize = true;
            lblTKLabel.Font = new Font("Segoe UI", 10F);
            lblTKLabel.ForeColor = Color.FromArgb(78, 49, 41);
            lblTKLabel.Location = new Point(80, 150);
            lblTKLabel.Name = "lblTKLabel";
            lblTKLabel.Size = new Size(82, 23);
            lblTKLabel.TabIndex = 1;
            lblTKLabel.Text = "Tài khoản";
            // 
            // txtTK
            // 
            txtTK.BackColor = Color.FromArgb(247, 234, 214);
            txtTK.BorderStyle = BorderStyle.None;
            txtTK.Font = new Font("Segoe UI", 12F);
            txtTK.ForeColor = Color.FromArgb(78, 49, 41);
            txtTK.Location = new Point(80, 175);
            txtTK.Name = "txtTK";
            txtTK.Size = new Size(380, 27);
            txtTK.TabIndex = 2;
            // 
            // pnlTKUnderline
            // 
            pnlTKUnderline.BackColor = Color.FromArgb(118, 74, 61);
            pnlTKUnderline.Location = new Point(80, 200);
            pnlTKUnderline.Name = "pnlTKUnderline";
            pnlTKUnderline.Size = new Size(380, 1);
            pnlTKUnderline.TabIndex = 3;
            // 
            // lblMKLabel
            // 
            lblMKLabel.AutoSize = true;
            lblMKLabel.Font = new Font("Segoe UI", 10F);
            lblMKLabel.ForeColor = Color.FromArgb(78, 49, 41);
            lblMKLabel.Location = new Point(80, 230);
            lblMKLabel.Name = "lblMKLabel";
            lblMKLabel.Size = new Size(82, 23);
            lblMKLabel.TabIndex = 4;
            lblMKLabel.Text = "Mật khẩu";
            // 
            // txtMK
            // 
            txtMK.BackColor = Color.FromArgb(247, 234, 214);
            txtMK.BorderStyle = BorderStyle.None;
            txtMK.Font = new Font("Segoe UI", 12F);
            txtMK.ForeColor = Color.FromArgb(78, 49, 41);
            txtMK.Location = new Point(80, 255);
            txtMK.Name = "txtMK";
            txtMK.Size = new Size(340, 27);
            txtMK.TabIndex = 5;
            txtMK.UseSystemPasswordChar = true;
            txtMK.TextChanged += txtMK_TextChanged;
            // 
            // pnlMKUnderline
            // 
            pnlMKUnderline.BackColor = Color.FromArgb(118, 74, 61);
            pnlMKUnderline.Location = new Point(80, 280);
            pnlMKUnderline.Name = "pnlMKUnderline";
            pnlMKUnderline.Size = new Size(380, 1);
            pnlMKUnderline.TabIndex = 6;
            // 
            // btnĐăngNhập
            // 
            btnĐăngNhập.BackColor = Color.FromArgb(133, 181, 100);
            btnĐăngNhập.FlatAppearance.BorderSize = 0;
            btnĐăngNhập.FlatStyle = FlatStyle.Flat;
            btnĐăngNhập.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnĐăngNhập.ForeColor = Color.White;
            btnĐăngNhập.Location = new Point(80, 343);
            btnĐăngNhập.Name = "btnĐăngNhập";
            btnĐăngNhập.Size = new Size(380, 50);
            btnĐăngNhập.TabIndex = 12;
            btnĐăngNhập.Text = "ĐĂNG NHẬP";
            btnĐăngNhập.UseVisualStyleBackColor = false;
            btnĐăngNhập.Click += btnĐăngNhập_Click;
            // 
            // lblMoiDen
            // 
            lblMoiDen.AutoSize = true;
            lblMoiDen.Font = new Font("Segoe UI", 10F);
            lblMoiDen.ForeColor = Color.FromArgb(78, 49, 41);
            lblMoiDen.Location = new Point(150, 433);
            lblMoiDen.Name = "lblMoiDen";
            lblMoiDen.Size = new Size(157, 23);
            lblMoiDen.TabIndex = 13;
            lblMoiDen.Text = "Chưa có tài khoản?";
            lblMoiDen.Click += lblMoiDen_Click;
            // 
            // btnĐK
            // 
            btnĐK.BackColor = Color.Transparent;
            btnĐK.FlatAppearance.BorderSize = 0;
            btnĐK.FlatStyle = FlatStyle.Flat;
            btnĐK.Font = new Font("Segoe UI", 10F, FontStyle.Underline);
            btnĐK.ForeColor = Color.FromArgb(78, 49, 41);
            btnĐK.Location = new Point(310, 427);
            btnĐK.Name = "btnĐK";
            btnĐK.Size = new Size(127, 35);
            btnĐK.TabIndex = 14;
            btnĐK.Text = "Đăng ký ngay";
            btnĐK.UseVisualStyleBackColor = false;
            btnĐK.Click += btnĐK_Click;
            // 
            // btnQMK
            // 
            btnQMK.BackColor = Color.Transparent;
            btnQMK.FlatAppearance.BorderSize = 0;
            btnQMK.FlatStyle = FlatStyle.Flat;
            btnQMK.Font = new Font("Segoe UI", 9F, FontStyle.Underline);
            btnQMK.ForeColor = Color.FromArgb(78, 49, 41);
            btnQMK.Location = new Point(80, 297);
            btnQMK.Name = "btnQMK";
            btnQMK.Size = new Size(130, 30);
            btnQMK.TabIndex = 11;
            btnQMK.Text = "Quên mật khẩu?";
            btnQMK.UseVisualStyleBackColor = false;
            btnQMK.Click += btnQMK_Click;
            // 
            // btnShow
            // 
            btnShow.BackColor = Color.Transparent;
            btnShow.FlatAppearance.BorderSize = 0;
            btnShow.FlatStyle = FlatStyle.Flat;
            btnShow.Location = new Point(425, 252);
            btnShow.Name = "btnShow";
            btnShow.Size = new Size(30, 30);
            btnShow.TabIndex = 7;
            btnShow.Text = "👁";
            btnShow.UseVisualStyleBackColor = false;
            btnShow.Click += btnShow_Click;
            // 
            // btnHide
            // 
            btnHide.BackColor = Color.Transparent;
            btnHide.FlatAppearance.BorderSize = 0;
            btnHide.FlatStyle = FlatStyle.Flat;
            btnHide.Location = new Point(425, 252);
            btnHide.Name = "btnHide";
            btnHide.Size = new Size(30, 25);
            btnHide.TabIndex = 8;
            btnHide.Text = "👁‍🗨";
            btnHide.UseVisualStyleBackColor = false;
            btnHide.Visible = false;
            btnHide.Click += btnHide_Click;
            // 
            // frmLogin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 217, 181);
            ClientSize = new Size(900, 600);
            Controls.Add(pnlLeft);
            Controls.Add(pnlRight);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "frmLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Chess - Đăng Nhập";
            Load += frmDN_Load;
            pnlLeft.ResumeLayout(false);
            pnlLeft.PerformLayout();
            pnlRight.ResumeLayout(false);
            pnlRight.PerformLayout();
            ResumeLayout(false);
        }

        private Panel pnlLeft;
        private Label lblKingIcon;
        private Panel pnlRight;
        private Label lblQuote;
        private Label lblTieuDe;
        private Label lblTKLabel;
        private TextBox txtTK;
        private Panel pnlTKUnderline;
        private Label lblMKLabel;
        private TextBox txtMK;
        private Panel pnlMKUnderline;
        private Button btnShow;
        private Button btnHide;
        private Button btnQMK;
        private Button btnĐăngNhập;
        private Label lblMoiDen;
        private Button btnĐK;
    }
}
