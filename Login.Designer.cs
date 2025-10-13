namespace Giao_Diện_Đăng_Nhập_Cờ_Vua
{
    partial class frmLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            lblMoiDen = new Label();
            chkLuu = new CheckBox();
            btnĐăngNhập = new Button();
            btnĐK = new Button();
            btnQMK = new Button();
            txtTK = new TextBox();
            txtMK = new TextBox();
            lblTieuDe = new Label();
            btnShow = new Button();
            btnHide = new Button();
            lblLuu = new Label();
            SuspendLayout();
            // 
            // lblMoiDen
            // 
            lblMoiDen.BackColor = Color.Transparent;
            lblMoiDen.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblMoiDen.ForeColor = Color.White;
            lblMoiDen.Location = new Point(161, 638);
            lblMoiDen.Name = "lblMoiDen";
            lblMoiDen.Size = new Size(149, 54);
            lblMoiDen.TabIndex = 0;
            lblMoiDen.Text = "Mới Đến ?";
            lblMoiDen.TextAlign = ContentAlignment.MiddleCenter;
            lblMoiDen.Click += lblMoiDen_Click;
            // 
            // chkLuu
            // 
            chkLuu.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            chkLuu.Location = new Point(228, 371);
            chkLuu.Margin = new Padding(3, 4, 3, 4);
            chkLuu.Name = "chkLuu";
            chkLuu.Size = new Size(17, 21);
            chkLuu.TabIndex = 1;
            chkLuu.UseVisualStyleBackColor = true;
            chkLuu.CheckedChanged += chkLuu_CheckedChanged;
            // 
            // btnĐăngNhập
            // 
            btnĐăngNhập.BackColor = Color.Lime;
            btnĐăngNhập.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnĐăngNhập.Location = new Point(328, 432);
            btnĐăngNhập.Margin = new Padding(3, 4, 3, 4);
            btnĐăngNhập.Name = "btnĐăngNhập";
            btnĐăngNhập.Size = new Size(173, 60);
            btnĐăngNhập.TabIndex = 2;
            btnĐăngNhập.Text = "Đăng Nhập";
            btnĐăngNhập.UseVisualStyleBackColor = false;
            btnĐăngNhập.Click += btnĐăngNhập_Click;
            // 
            // btnĐK
            // 
            btnĐK.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnĐK.Location = new Point(316, 638);
            btnĐK.Margin = new Padding(3, 4, 3, 4);
            btnĐK.Name = "btnĐK";
            btnĐK.Size = new Size(384, 54);
            btnĐK.TabIndex = 3;
            btnĐK.Text = "Đăng kí - và bắt đầu chơi cờ ngay";
            btnĐK.UseVisualStyleBackColor = true;
            btnĐK.Click += btnĐK_Click;
            // 
            // btnQMK
            // 
            btnQMK.BackColor = Color.PaleTurquoise;
            btnQMK.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnQMK.ForeColor = SystemColors.ActiveCaptionText;
            btnQMK.Location = new Point(489, 356);
            btnQMK.Margin = new Padding(3, 4, 3, 4);
            btnQMK.Name = "btnQMK";
            btnQMK.Size = new Size(188, 48);
            btnQMK.TabIndex = 4;
            btnQMK.Text = "Quên Mật Khẩu";
            btnQMK.UseVisualStyleBackColor = false;
            btnQMK.Click += btnQMK_Click;
            // 
            // txtTK
            // 
            txtTK.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtTK.Location = new Point(246, 200);
            txtTK.Margin = new Padding(3, 4, 3, 4);
            txtTK.Multiline = true;
            txtTK.Name = "txtTK";
            txtTK.Size = new Size(300, 39);
            txtTK.TabIndex = 5;
            // 
            // txtMK
            // 
            txtMK.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtMK.Location = new Point(246, 289);
            txtMK.Margin = new Padding(3, 4, 3, 4);
            txtMK.Multiline = true;
            txtMK.Name = "txtMK";
            txtMK.Size = new Size(300, 39);
            txtMK.TabIndex = 6;
            txtMK.TextChanged += txtMK_TextChanged;
            // 
            // lblTieuDe
            // 
            lblTieuDe.BackColor = Color.Transparent;
            lblTieuDe.Font = new Font("Microsoft Sans Serif", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTieuDe.ForeColor = Color.Lime;
            lblTieuDe.Location = new Point(280, 70);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(139, 58);
            lblTieuDe.TabIndex = 9;
            lblTieuDe.Text = "Chess";
            lblTieuDe.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnShow
            // 
            btnShow.BackColor = Color.DarkGray;
            btnShow.FlatStyle = FlatStyle.Flat;
            btnShow.Image = (Image)resources.GetObject("btnShow.Image");
            btnShow.Location = new Point(509, 289);
            btnShow.Margin = new Padding(3, 4, 3, 4);
            btnShow.Name = "btnShow";
            btnShow.Size = new Size(37, 39);
            btnShow.TabIndex = 10;
            btnShow.Text = "S";
            btnShow.UseVisualStyleBackColor = false;
            btnShow.Click += btnShow_Click;
            // 
            // btnHide
            // 
            btnHide.BackColor = Color.DarkGray;
            btnHide.FlatStyle = FlatStyle.Flat;
            btnHide.Image = (Image)resources.GetObject("btnHide.Image");
            btnHide.Location = new Point(509, 283);
            btnHide.Margin = new Padding(3, 4, 3, 4);
            btnHide.Name = "btnHide";
            btnHide.Size = new Size(37, 39);
            btnHide.TabIndex = 11;
            btnHide.Text = "H";
            btnHide.UseVisualStyleBackColor = false;
            btnHide.Click += btnHide_Click;
            // 
            // lblLuu
            // 
            lblLuu.BackColor = Color.Transparent;
            lblLuu.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblLuu.ForeColor = Color.WhiteSmoke;
            lblLuu.Location = new Point(251, 364);
            lblLuu.Name = "lblLuu";
            lblLuu.Size = new Size(100, 32);
            lblLuu.TabIndex = 12;
            lblLuu.Text = "Nhớ Tôi";
            lblLuu.TextAlign = ContentAlignment.MiddleCenter;
            lblLuu.Click += lblLuu_Click;
            // 
            // frmLogin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(860, 807);
            Controls.Add(lblLuu);
            Controls.Add(btnHide);
            Controls.Add(btnShow);
            Controls.Add(lblTieuDe);
            Controls.Add(txtMK);
            Controls.Add(txtTK);
            Controls.Add(btnQMK);
            Controls.Add(btnĐK);
            Controls.Add(btnĐăngNhập);
            Controls.Add(chkLuu);
            Controls.Add(lblMoiDen);
            Margin = new Padding(3, 4, 3, 4);
            Name = "frmLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login";
            Load += frmDN_Load;
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMoiDen;
        private System.Windows.Forms.CheckBox chkLuu;
        private System.Windows.Forms.Button btnĐăngNhập;
        private System.Windows.Forms.Button btnĐK;
        private System.Windows.Forms.Button btnQMK;
        private System.Windows.Forms.TextBox txtTK;
        private System.Windows.Forms.TextBox txtMK;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Button btnShow;
        private System.Windows.Forms.Button btnHide;
        private System.Windows.Forms.Label lblLuu;
    }
}

