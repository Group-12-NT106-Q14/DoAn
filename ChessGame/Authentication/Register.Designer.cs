namespace ChessGame
{
    partial class frmRegister
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRegister));
            pnlLeft = new Panel();
            lblKingIcon = new Label();
            lblQuote = new Label();
            pnlRight = new Panel();
            btnĐN = new Button();
            lblTieuDe = new Label();
            lblEmail = new Label();
            txtEmail = new TextBox();
            pnlEmailUnderline = new Panel();
            lblTenHienThi = new Label();
            txtTenHienThi = new TextBox();
            pnlTenHienThiUnderline = new Panel();
            lblTK = new Label();
            txtTK = new TextBox();
            pnlTKUnderline = new Panel();
            lblMK = new Label();
            txtMK = new TextBox();
            pnlMKUnderline = new Panel();
            lblNLMK = new Label();
            txtNLMK = new TextBox();
            pnlNLMKUnderline = new Panel();
            btnĐK = new Button();
            lblDaCo = new Label();
            btnShow1 = new Button();
            btnHide1 = new Button();
            btnShow2 = new Button();
            btnHide2 = new Button();
            errorProvider1 = new ErrorProvider(components);
            pnlLeft.SuspendLayout();
            pnlRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
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
            lblKingIcon.Location = new Point(89, 213);
            lblKingIcon.Name = "lblKingIcon";
            lblKingIcon.Size = new Size(187, 159);
            lblKingIcon.TabIndex = 3;
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
            pnlRight.Controls.Add(btnĐN);
            pnlRight.Controls.Add(lblTieuDe);
            pnlRight.Controls.Add(lblEmail);
            pnlRight.Controls.Add(txtEmail);
            pnlRight.Controls.Add(pnlEmailUnderline);
            pnlRight.Controls.Add(lblTenHienThi);
            pnlRight.Controls.Add(txtTenHienThi);
            pnlRight.Controls.Add(pnlTenHienThiUnderline);
            pnlRight.Controls.Add(lblTK);
            pnlRight.Controls.Add(txtTK);
            pnlRight.Controls.Add(pnlTKUnderline);
            pnlRight.Controls.Add(lblMK);
            pnlRight.Controls.Add(txtMK);
            pnlRight.Controls.Add(pnlMKUnderline);
            pnlRight.Controls.Add(lblNLMK);
            pnlRight.Controls.Add(txtNLMK);
            pnlRight.Controls.Add(pnlNLMKUnderline);
            pnlRight.Controls.Add(btnĐK);
            pnlRight.Controls.Add(lblDaCo);
            pnlRight.Controls.Add(btnShow1);
            pnlRight.Controls.Add(btnHide1);
            pnlRight.Controls.Add(btnShow2);
            pnlRight.Controls.Add(btnHide2);
            pnlRight.Location = new Point(360, 0);
            pnlRight.Name = "pnlRight";
            pnlRight.Size = new Size(540, 600);
            pnlRight.TabIndex = 1;
            // 
            // btnĐN
            // 
            btnĐN.BackColor = Color.Transparent;
            btnĐN.FlatAppearance.BorderSize = 0;
            btnĐN.FlatStyle = FlatStyle.Flat;
            btnĐN.Font = new Font("Segoe UI", 10F, FontStyle.Underline);
            btnĐN.ForeColor = Color.FromArgb(78, 49, 41);
            btnĐN.Location = new Point(267, 538);
            btnĐN.Name = "btnĐN";
            btnĐN.Size = new Size(146, 35);
            btnĐN.TabIndex = 22;
            btnĐN.Text = "Đăng nhập ngay";
            btnĐN.UseVisualStyleBackColor = false;
            btnĐN.Click += btnĐN_Click;
            // 
            // lblTieuDe
            // 
            lblTieuDe.AutoSize = true;
            lblTieuDe.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTieuDe.ForeColor = Color.FromArgb(78, 49, 41);
            lblTieuDe.Location = new Point(180, 25);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(202, 54);
            lblTieuDe.TabIndex = 0;
            lblTieuDe.Text = "ĐĂNG KÝ";
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 10F);
            lblEmail.ForeColor = Color.FromArgb(78, 49, 41);
            lblEmail.Location = new Point(70, 90);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(51, 23);
            lblEmail.TabIndex = 1;
            lblEmail.Text = "Email";
            // 
            // txtEmail
            // 
            txtEmail.BackColor = Color.FromArgb(247, 234, 214);
            txtEmail.BorderStyle = BorderStyle.None;
            txtEmail.Font = new Font("Segoe UI", 11F);
            txtEmail.ForeColor = Color.FromArgb(78, 49, 41);
            txtEmail.Location = new Point(70, 112);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(400, 25);
            txtEmail.TabIndex = 2;
            // 
            // pnlEmailUnderline
            // 
            pnlEmailUnderline.BackColor = Color.FromArgb(118, 74, 61);
            pnlEmailUnderline.Location = new Point(70, 135);
            pnlEmailUnderline.Name = "pnlEmailUnderline";
            pnlEmailUnderline.Size = new Size(400, 1);
            pnlEmailUnderline.TabIndex = 3;
            // 
            // lblTenHienThi
            // 
            lblTenHienThi.AutoSize = true;
            lblTenHienThi.Font = new Font("Segoe UI", 10F);
            lblTenHienThi.ForeColor = Color.FromArgb(78, 49, 41);
            lblTenHienThi.Location = new Point(70, 150);
            lblTenHienThi.Name = "lblTenHienThi";
            lblTenHienThi.Size = new Size(99, 23);
            lblTenHienThi.TabIndex = 4;
            lblTenHienThi.Text = "Tên hiển thị";
            // 
            // txtTenHienThi
            // 
            txtTenHienThi.BackColor = Color.FromArgb(247, 234, 214);
            txtTenHienThi.BorderStyle = BorderStyle.None;
            txtTenHienThi.Font = new Font("Segoe UI", 11F);
            txtTenHienThi.ForeColor = Color.FromArgb(78, 49, 41);
            txtTenHienThi.Location = new Point(70, 172);
            txtTenHienThi.Name = "txtTenHienThi";
            txtTenHienThi.Size = new Size(400, 25);
            txtTenHienThi.TabIndex = 5;
            // 
            // pnlTenHienThiUnderline
            // 
            pnlTenHienThiUnderline.BackColor = Color.FromArgb(118, 74, 61);
            pnlTenHienThiUnderline.Location = new Point(70, 195);
            pnlTenHienThiUnderline.Name = "pnlTenHienThiUnderline";
            pnlTenHienThiUnderline.Size = new Size(400, 1);
            pnlTenHienThiUnderline.TabIndex = 6;
            // 
            // lblTK
            // 
            lblTK.AutoSize = true;
            lblTK.Font = new Font("Segoe UI", 10F);
            lblTK.ForeColor = Color.FromArgb(78, 49, 41);
            lblTK.Location = new Point(70, 210);
            lblTK.Name = "lblTK";
            lblTK.Size = new Size(82, 23);
            lblTK.TabIndex = 7;
            lblTK.Text = "Tài khoản";
            // 
            // txtTK
            // 
            txtTK.BackColor = Color.FromArgb(247, 234, 214);
            txtTK.BorderStyle = BorderStyle.None;
            txtTK.Font = new Font("Segoe UI", 11F);
            txtTK.ForeColor = Color.FromArgb(78, 49, 41);
            txtTK.Location = new Point(70, 232);
            txtTK.Name = "txtTK";
            txtTK.Size = new Size(400, 25);
            txtTK.TabIndex = 8;
            // 
            // pnlTKUnderline
            // 
            pnlTKUnderline.BackColor = Color.FromArgb(118, 74, 61);
            pnlTKUnderline.Location = new Point(70, 255);
            pnlTKUnderline.Name = "pnlTKUnderline";
            pnlTKUnderline.Size = new Size(400, 1);
            pnlTKUnderline.TabIndex = 9;
            // 
            // lblMK
            // 
            lblMK.AutoSize = true;
            lblMK.Font = new Font("Segoe UI", 10F);
            lblMK.ForeColor = Color.FromArgb(78, 49, 41);
            lblMK.Location = new Point(70, 270);
            lblMK.Name = "lblMK";
            lblMK.Size = new Size(82, 23);
            lblMK.TabIndex = 10;
            lblMK.Text = "Mật khẩu";
            // 
            // txtMK
            // 
            txtMK.BackColor = Color.FromArgb(247, 234, 214);
            txtMK.BorderStyle = BorderStyle.None;
            txtMK.Font = new Font("Segoe UI", 11F);
            txtMK.ForeColor = Color.FromArgb(78, 49, 41);
            txtMK.Location = new Point(70, 292);
            txtMK.Name = "txtMK";
            txtMK.Size = new Size(360, 25);
            txtMK.TabIndex = 11;
            txtMK.UseSystemPasswordChar = true;
            // 
            // pnlMKUnderline
            // 
            pnlMKUnderline.BackColor = Color.FromArgb(118, 74, 61);
            pnlMKUnderline.Location = new Point(70, 315);
            pnlMKUnderline.Name = "pnlMKUnderline";
            pnlMKUnderline.Size = new Size(400, 1);
            pnlMKUnderline.TabIndex = 12;
            // 
            // lblNLMK
            // 
            lblNLMK.AutoSize = true;
            lblNLMK.Font = new Font("Segoe UI", 10F);
            lblNLMK.ForeColor = Color.FromArgb(78, 49, 41);
            lblNLMK.Location = new Point(70, 330);
            lblNLMK.Name = "lblNLMK";
            lblNLMK.Size = new Size(151, 23);
            lblNLMK.TabIndex = 15;
            lblNLMK.Text = "Nhập lại mật khẩu";
            // 
            // txtNLMK
            // 
            txtNLMK.BackColor = Color.FromArgb(247, 234, 214);
            txtNLMK.BorderStyle = BorderStyle.None;
            txtNLMK.Font = new Font("Segoe UI", 11F);
            txtNLMK.ForeColor = Color.FromArgb(78, 49, 41);
            txtNLMK.Location = new Point(70, 352);
            txtNLMK.Name = "txtNLMK";
            txtNLMK.Size = new Size(360, 25);
            txtNLMK.TabIndex = 16;
            txtNLMK.UseSystemPasswordChar = true;
            // 
            // pnlNLMKUnderline
            // 
            pnlNLMKUnderline.BackColor = Color.FromArgb(118, 74, 61);
            pnlNLMKUnderline.Location = new Point(70, 375);
            pnlNLMKUnderline.Name = "pnlNLMKUnderline";
            pnlNLMKUnderline.Size = new Size(400, 1);
            pnlNLMKUnderline.TabIndex = 17;
            // 
            // btnĐK
            // 
            btnĐK.BackColor = Color.FromArgb(133, 181, 100);
            btnĐK.FlatAppearance.BorderSize = 0;
            btnĐK.FlatStyle = FlatStyle.Flat;
            btnĐK.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnĐK.ForeColor = Color.White;
            btnĐK.Location = new Point(70, 425);
            btnĐK.Name = "btnĐK";
            btnĐK.Size = new Size(400, 50);
            btnĐK.TabIndex = 20;
            btnĐK.Text = "ĐĂNG KÝ";
            btnĐK.UseVisualStyleBackColor = false;
            btnĐK.Click += btnĐK_Click;
            // 
            // lblDaCo
            // 
            lblDaCo.AutoSize = true;
            lblDaCo.Font = new Font("Segoe UI", 10F);
            lblDaCo.ForeColor = Color.FromArgb(78, 49, 41);
            lblDaCo.Location = new Point(133, 544);
            lblDaCo.Name = "lblDaCo";
            lblDaCo.Size = new Size(138, 23);
            lblDaCo.TabIndex = 21;
            lblDaCo.Text = "Đã có tài khoản?";
            // 
            // btnShow1
            // 
            btnShow1.BackColor = Color.Transparent;
            btnShow1.FlatAppearance.BorderSize = 0;
            btnShow1.FlatStyle = FlatStyle.Flat;
            btnShow1.Location = new Point(435, 289);
            btnShow1.Name = "btnShow1";
            btnShow1.Size = new Size(30, 35);
            btnShow1.TabIndex = 13;
            btnShow1.Text = "👁";
            btnShow1.UseVisualStyleBackColor = false;
            btnShow1.Click += btnShow1_Click;
            // 
            // btnHide1
            // 
            btnHide1.BackColor = Color.Transparent;
            btnHide1.FlatAppearance.BorderSize = 0;
            btnHide1.FlatStyle = FlatStyle.Flat;
            btnHide1.Location = new Point(435, 289);
            btnHide1.Name = "btnHide1";
            btnHide1.Size = new Size(30, 23);
            btnHide1.TabIndex = 14;
            btnHide1.Text = "👁‍🗨";
            btnHide1.UseVisualStyleBackColor = false;
            btnHide1.Visible = false;
            btnHide1.Click += btnHide1_Click;
            // 
            // btnShow2
            // 
            btnShow2.BackColor = Color.Transparent;
            btnShow2.FlatAppearance.BorderSize = 0;
            btnShow2.FlatStyle = FlatStyle.Flat;
            btnShow2.Location = new Point(435, 349);
            btnShow2.Name = "btnShow2";
            btnShow2.Size = new Size(30, 36);
            btnShow2.TabIndex = 18;
            btnShow2.Text = "👁";
            btnShow2.UseVisualStyleBackColor = false;
            btnShow2.Click += btnShow2_Click;
            // 
            // btnHide2
            // 
            btnHide2.BackColor = Color.Transparent;
            btnHide2.FlatAppearance.BorderSize = 0;
            btnHide2.FlatStyle = FlatStyle.Flat;
            btnHide2.Location = new Point(435, 349);
            btnHide2.Name = "btnHide2";
            btnHide2.Size = new Size(30, 23);
            btnHide2.TabIndex = 19;
            btnHide2.Text = "👁‍🗨";
            btnHide2.UseVisualStyleBackColor = false;
            btnHide2.Visible = false;
            btnHide2.Click += btnHide2_Click;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // frmRegister
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 217, 181);
            ClientSize = new Size(900, 600);
            Controls.Add(pnlLeft);
            Controls.Add(pnlRight);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "frmRegister";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Chess - Đăng Ký";
            Load += frmDK_Load;
            pnlLeft.ResumeLayout(false);
            pnlLeft.PerformLayout();
            pnlRight.ResumeLayout(false);
            pnlRight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
        }

        private Panel pnlLeft;
        private Panel pnlRight;
        private Label lblQuote;
        private Label lblTieuDe;
        private Label lblEmail;
        private TextBox txtEmail;
        private Panel pnlEmailUnderline;
        private Label lblTenHienThi;
        private TextBox txtTenHienThi;
        private Panel pnlTenHienThiUnderline;
        private Label lblTK;
        private TextBox txtTK;
        private Panel pnlTKUnderline;
        private Label lblMK;
        private TextBox txtMK;
        private Panel pnlMKUnderline;
        private Button btnShow1;
        private Button btnHide1;
        private Label lblNLMK;
        private TextBox txtNLMK;
        private Panel pnlNLMKUnderline;
        private Button btnShow2;
        private Button btnHide2;
        private Button btnĐK;
        private Label lblDaCo;
        private ErrorProvider errorProvider1;
        private Button btnĐN;
        private Label lblKingIcon;
    }
}
