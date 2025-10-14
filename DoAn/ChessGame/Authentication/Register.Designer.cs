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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            pnlLeft = new Panel();
            picLogo = new PictureBox();
            lblQuote = new Label();
            pnlRight = new Panel();
            lblTieuDe = new Label();
            lblEmail = new Label();
            txtEmail = new TextBox();
            pnlEmailUnderline = new Panel();
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
            btnĐN = new Button();
            pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            pnlRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // pnlLeft
            // 
            pnlLeft.BackColor = Color.FromArgb(118, 74, 61);
            pnlLeft.Controls.Add(picLogo);
            pnlLeft.Controls.Add(lblQuote);
            pnlLeft.Dock = DockStyle.Left;
            pnlLeft.Location = new Point(0, 0);
            pnlLeft.Name = "pnlLeft";
            pnlLeft.Size = new Size(360, 600);
            pnlLeft.TabIndex = 0;
            // 
            // picLogo
            // 
            picLogo.BackColor = Color.Transparent;
            picLogo.Location = new Point(105, 180);
            picLogo.Name = "picLogo";
            picLogo.Size = new Size(150, 150);
            picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            picLogo.TabIndex = 0;
            picLogo.TabStop = false;
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
            // lblTieuDe
            // 
            lblTieuDe.AutoSize = true;
            lblTieuDe.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTieuDe.ForeColor = Color.FromArgb(78, 49, 41);
            lblTieuDe.Location = new Point(180, 40);
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
            lblEmail.Location = new Point(70, 110);
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
            txtEmail.Location = new Point(70, 132);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(400, 25);
            txtEmail.TabIndex = 2;
            // 
            // pnlEmailUnderline
            // 
            pnlEmailUnderline.BackColor = Color.FromArgb(118, 74, 61);
            pnlEmailUnderline.Location = new Point(70, 155);
            pnlEmailUnderline.Name = "pnlEmailUnderline";
            pnlEmailUnderline.Size = new Size(400, 1);
            pnlEmailUnderline.TabIndex = 3;
            // 
            // lblTK
            // 
            lblTK.AutoSize = true;
            lblTK.Font = new Font("Segoe UI", 10F);
            lblTK.ForeColor = Color.FromArgb(78, 49, 41);
            lblTK.Location = new Point(70, 175);
            lblTK.Name = "lblTK";
            lblTK.Size = new Size(82, 23);
            lblTK.TabIndex = 4;
            lblTK.Text = "Tài khoản";
            // 
            // txtTK
            // 
            txtTK.BackColor = Color.FromArgb(247, 234, 214);
            txtTK.BorderStyle = BorderStyle.None;
            txtTK.Font = new Font("Segoe UI", 11F);
            txtTK.ForeColor = Color.FromArgb(78, 49, 41);
            txtTK.Location = new Point(70, 197);
            txtTK.Name = "txtTK";
            txtTK.Size = new Size(400, 25);
            txtTK.TabIndex = 5;
            // 
            // pnlTKUnderline
            // 
            pnlTKUnderline.BackColor = Color.FromArgb(118, 74, 61);
            pnlTKUnderline.Location = new Point(70, 220);
            pnlTKUnderline.Name = "pnlTKUnderline";
            pnlTKUnderline.Size = new Size(400, 1);
            pnlTKUnderline.TabIndex = 6;
            // 
            // lblMK
            // 
            lblMK.AutoSize = true;
            lblMK.Font = new Font("Segoe UI", 10F);
            lblMK.ForeColor = Color.FromArgb(78, 49, 41);
            lblMK.Location = new Point(70, 240);
            lblMK.Name = "lblMK";
            lblMK.Size = new Size(82, 23);
            lblMK.TabIndex = 7;
            lblMK.Text = "Mật khẩu";
            // 
            // txtMK
            // 
            txtMK.BackColor = Color.FromArgb(247, 234, 214);
            txtMK.BorderStyle = BorderStyle.None;
            txtMK.Font = new Font("Segoe UI", 11F);
            txtMK.ForeColor = Color.FromArgb(78, 49, 41);
            txtMK.Location = new Point(70, 262);
            txtMK.Name = "txtMK";
            txtMK.Size = new Size(360, 25);
            txtMK.TabIndex = 8;
            txtMK.UseSystemPasswordChar = true;
            // 
            // pnlMKUnderline
            // 
            pnlMKUnderline.BackColor = Color.FromArgb(118, 74, 61);
            pnlMKUnderline.Location = new Point(70, 285);
            pnlMKUnderline.Name = "pnlMKUnderline";
            pnlMKUnderline.Size = new Size(400, 1);
            pnlMKUnderline.TabIndex = 9;
            // 
            // lblNLMK
            // 
            lblNLMK.AutoSize = true;
            lblNLMK.Font = new Font("Segoe UI", 10F);
            lblNLMK.ForeColor = Color.FromArgb(78, 49, 41);
            lblNLMK.Location = new Point(70, 305);
            lblNLMK.Name = "lblNLMK";
            lblNLMK.Size = new Size(151, 23);
            lblNLMK.TabIndex = 12;
            lblNLMK.Text = "Nhập lại mật khẩu";
            // 
            // txtNLMK
            // 
            txtNLMK.BackColor = Color.FromArgb(247, 234, 214);
            txtNLMK.BorderStyle = BorderStyle.None;
            txtNLMK.Font = new Font("Segoe UI", 11F);
            txtNLMK.ForeColor = Color.FromArgb(78, 49, 41);
            txtNLMK.Location = new Point(70, 327);
            txtNLMK.Name = "txtNLMK";
            txtNLMK.Size = new Size(360, 25);
            txtNLMK.TabIndex = 13;
            txtNLMK.UseSystemPasswordChar = true;
            // 
            // pnlNLMKUnderline
            // 
            pnlNLMKUnderline.BackColor = Color.FromArgb(118, 74, 61);
            pnlNLMKUnderline.Location = new Point(70, 350);
            pnlNLMKUnderline.Name = "pnlNLMKUnderline";
            pnlNLMKUnderline.Size = new Size(400, 1);
            pnlNLMKUnderline.TabIndex = 14;
            // 
            // btnĐK
            // 
            btnĐK.BackColor = Color.FromArgb(133, 181, 100);
            btnĐK.FlatAppearance.BorderSize = 0;
            btnĐK.FlatStyle = FlatStyle.Flat;
            btnĐK.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnĐK.ForeColor = Color.White;
            btnĐK.Location = new Point(70, 400);
            btnĐK.Name = "btnĐK";
            btnĐK.Size = new Size(400, 50);
            btnĐK.TabIndex = 17;
            btnĐK.Text = "ĐĂNG KÝ";
            btnĐK.UseVisualStyleBackColor = false;
            btnĐK.Click += btnĐK_Click;
            // 
            // lblDaCo
            // 
            lblDaCo.AutoSize = true;
            lblDaCo.Font = new Font("Segoe UI", 10F);
            lblDaCo.ForeColor = Color.FromArgb(78, 49, 41);
            lblDaCo.Location = new Point(150, 480);
            lblDaCo.Name = "lblDaCo";
            lblDaCo.Size = new Size(138, 23);
            lblDaCo.TabIndex = 18;
            lblDaCo.Text = "Đã có tài khoản?";
            // 
            // btnShow1
            // 
            btnShow1.BackColor = Color.Transparent;
            btnShow1.FlatAppearance.BorderSize = 0;
            btnShow1.FlatStyle = FlatStyle.Flat;
            btnShow1.Location = new Point(435, 259);
            btnShow1.Name = "btnShow1";
            btnShow1.Size = new Size(30, 23);
            btnShow1.TabIndex = 10;
            btnShow1.Text = "👁";
            btnShow1.UseVisualStyleBackColor = false;
            btnShow1.Click += button2_Click;
            // 
            // btnHide1
            // 
            btnHide1.BackColor = Color.Transparent;
            btnHide1.FlatAppearance.BorderSize = 0;
            btnHide1.FlatStyle = FlatStyle.Flat;
            btnHide1.Location = new Point(435, 259);
            btnHide1.Name = "btnHide1";
            btnHide1.Size = new Size(30, 23);
            btnHide1.TabIndex = 11;
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
            btnShow2.Location = new Point(435, 324);
            btnShow2.Name = "btnShow2";
            btnShow2.Size = new Size(30, 23);
            btnShow2.TabIndex = 15;
            btnShow2.Text = "👁";
            btnShow2.UseVisualStyleBackColor = false;
            btnShow2.Click += btnShow2_Click;
            // 
            // btnHide2
            // 
            btnHide2.BackColor = Color.Transparent;
            btnHide2.FlatAppearance.BorderSize = 0;
            btnHide2.FlatStyle = FlatStyle.Flat;
            btnHide2.Location = new Point(435, 324);
            btnHide2.Name = "btnHide2";
            btnHide2.Size = new Size(30, 23);
            btnHide2.TabIndex = 16;
            btnHide2.Text = "👁‍🗨";
            btnHide2.UseVisualStyleBackColor = false;
            btnHide2.Visible = false;
            btnHide2.Click += btnHide2_Click;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // btnĐN
            // 
            btnĐN.BackColor = Color.Transparent;
            btnĐN.FlatAppearance.BorderSize = 0;
            btnĐN.FlatStyle = FlatStyle.Flat;
            btnĐN.Font = new Font("Segoe UI", 10F, FontStyle.Underline);
            btnĐN.ForeColor = Color.FromArgb(78, 49, 41);
            btnĐN.Location = new Point(284, 474);
            btnĐN.Name = "btnĐN";
            btnĐN.Size = new Size(146, 35);
            btnĐN.TabIndex = 19;
            btnĐN.Text = "Đăng nhập ngay";
            btnĐN.UseVisualStyleBackColor = false;
            btnĐN.Click += btnĐN_Click;
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
            Name = "frmRegister";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Chess - Đăng Ký";
            Load += frmDK_Load;
            pnlLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            pnlRight.ResumeLayout(false);
            pnlRight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlLeft;
        private Panel pnlRight;
        private PictureBox picLogo;
        private Label lblQuote;
        private Label lblTieuDe;
        private Label lblEmail;
        private TextBox txtEmail;
        private Panel pnlEmailUnderline;
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
    }
}
