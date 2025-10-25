namespace ChessGame
{
    partial class frmForgotpassword
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
            lblQuenMK = new Label();
            txtQuenMK = new TextBox();
            pnlEmailUnderline = new Panel();
            btnGuiMa = new Button();
            lblMaXacNhan = new Label();
            txtMaXacNhan = new TextBox();
            pnlMaUnderline = new Panel();
            lblHuongDan = new Label();
            btnXacNhan = new Button();
            SuspendLayout();
            // 
            // lblTieuDe
            // 
            lblTieuDe.AutoSize = true;
            lblTieuDe.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTieuDe.ForeColor = Color.FromArgb(78, 49, 41);
            lblTieuDe.Location = new Point(160, 30);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(360, 54);
            lblTieuDe.TabIndex = 0;
            lblTieuDe.Text = "QUÊN MẬT KHẨU";
            // 
            // lblMoTa
            // 
            lblMoTa.Font = new Font("Segoe UI", 11F);
            lblMoTa.ForeColor = Color.FromArgb(78, 49, 41);
            lblMoTa.Location = new Point(100, 95);
            lblMoTa.Name = "lblMoTa";
            lblMoTa.Size = new Size(500, 25);
            lblMoTa.TabIndex = 1;
            lblMoTa.Text = "Nhập email để nhận mã xác nhận khôi phục mật khẩu";
            lblMoTa.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblQuenMK
            // 
            lblQuenMK.AutoSize = true;
            lblQuenMK.Font = new Font("Segoe UI", 11F);
            lblQuenMK.ForeColor = Color.FromArgb(78, 49, 41);
            lblQuenMK.Location = new Point(100, 150);
            lblQuenMK.Name = "lblQuenMK";
            lblQuenMK.Size = new Size(58, 25);
            lblQuenMK.TabIndex = 2;
            lblQuenMK.Text = "Email";
            // 
            // txtQuenMK
            // 
            txtQuenMK.BackColor = Color.FromArgb(247, 234, 214);
            txtQuenMK.BorderStyle = BorderStyle.None;
            txtQuenMK.Font = new Font("Segoe UI", 12F);
            txtQuenMK.ForeColor = Color.FromArgb(78, 49, 41);
            txtQuenMK.Location = new Point(100, 175);
            txtQuenMK.Name = "txtQuenMK";
            txtQuenMK.Size = new Size(500, 27);
            txtQuenMK.TabIndex = 3;
            // 
            // pnlEmailUnderline
            // 
            pnlEmailUnderline.BackColor = Color.FromArgb(118, 74, 61);
            pnlEmailUnderline.Location = new Point(100, 200);
            pnlEmailUnderline.Name = "pnlEmailUnderline";
            pnlEmailUnderline.Size = new Size(500, 1);
            pnlEmailUnderline.TabIndex = 4;
            // 
            // btnGuiMa
            // 
            btnGuiMa.BackColor = Color.FromArgb(133, 181, 100);
            btnGuiMa.FlatAppearance.BorderSize = 0;
            btnGuiMa.FlatStyle = FlatStyle.Flat;
            btnGuiMa.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnGuiMa.ForeColor = Color.White;
            btnGuiMa.Location = new Point(250, 220);
            btnGuiMa.Name = "btnGuiMa";
            btnGuiMa.Size = new Size(233, 45);
            btnGuiMa.TabIndex = 5;
            btnGuiMa.Text = "GỬI MÃ XÁC NHẬN";
            btnGuiMa.UseVisualStyleBackColor = false;
            btnGuiMa.Click += btnGuiMa_Click;
            // 
            // lblMaXacNhan
            // 
            lblMaXacNhan.AutoSize = true;
            lblMaXacNhan.Font = new Font("Segoe UI", 11F);
            lblMaXacNhan.ForeColor = Color.FromArgb(78, 49, 41);
            lblMaXacNhan.Location = new Point(100, 290);
            lblMaXacNhan.Name = "lblMaXacNhan";
            lblMaXacNhan.Size = new Size(125, 25);
            lblMaXacNhan.TabIndex = 6;
            lblMaXacNhan.Text = "Mã Xác Nhận";
            // 
            // txtMaXacNhan
            // 
            txtMaXacNhan.BackColor = Color.FromArgb(247, 234, 214);
            txtMaXacNhan.BorderStyle = BorderStyle.None;
            txtMaXacNhan.Font = new Font("Segoe UI", 12F);
            txtMaXacNhan.ForeColor = Color.FromArgb(78, 49, 41);
            txtMaXacNhan.Location = new Point(100, 315);
            txtMaXacNhan.MaxLength = 6;
            txtMaXacNhan.Name = "txtMaXacNhan";
            txtMaXacNhan.Size = new Size(500, 27);
            txtMaXacNhan.TabIndex = 7;

            // 
            // pnlMaUnderline
            // 
            pnlMaUnderline.BackColor = Color.FromArgb(118, 74, 61);
            pnlMaUnderline.Location = new Point(100, 340);
            pnlMaUnderline.Name = "pnlMaUnderline";
            pnlMaUnderline.Size = new Size(500, 1);
            pnlMaUnderline.TabIndex = 8;
            // 
            // lblHuongDan
            // 
            lblHuongDan.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblHuongDan.ForeColor = Color.FromArgb(118, 74, 61);
            lblHuongDan.Location = new Point(100, 350);
            lblHuongDan.Name = "lblHuongDan";
            lblHuongDan.Size = new Size(500, 20);
            lblHuongDan.TabIndex = 9;
            lblHuongDan.Text = "Nhập mã 6 chữ số được gửi đến email của bạn";
            // 
            // btnXacNhan
            // 
            btnXacNhan.BackColor = Color.FromArgb(133, 181, 100);
            btnXacNhan.FlatAppearance.BorderSize = 0;
            btnXacNhan.FlatStyle = FlatStyle.Flat;
            btnXacNhan.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnXacNhan.ForeColor = Color.White;
            btnXacNhan.Location = new Point(160, 400);
            btnXacNhan.Name = "btnXacNhan";
            btnXacNhan.Size = new Size(408, 50);
            btnXacNhan.TabIndex = 10;
            btnXacNhan.Text = "TIẾP TỤC";
            btnXacNhan.UseVisualStyleBackColor = false;
            btnXacNhan.Click += btnXacNhan_Click;
            // 
            // frmForgotpassword
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 217, 181);
            ClientSize = new Size(700, 500);
            Controls.Add(btnXacNhan);
            Controls.Add(lblHuongDan);
            Controls.Add(pnlMaUnderline);
            Controls.Add(txtMaXacNhan);
            Controls.Add(lblMaXacNhan);
            Controls.Add(btnGuiMa);
            Controls.Add(pnlEmailUnderline);
            Controls.Add(txtQuenMK);
            Controls.Add(lblQuenMK);
            Controls.Add(lblMoTa);
            Controls.Add(lblTieuDe);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "frmForgotpassword";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quên Mật Khẩu";
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTieuDe;
        private Label lblMoTa;
        private Label lblQuenMK;
        private TextBox txtQuenMK;
        private Panel pnlEmailUnderline;
        private Button btnGuiMa;
        private Label lblMaXacNhan;
        private TextBox txtMaXacNhan;
        private Panel pnlMaUnderline;
        private Label lblHuongDan;
        private Button btnXacNhan;
    }
}
