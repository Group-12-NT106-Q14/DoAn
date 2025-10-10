namespace chess
{
    partial class frmRegister
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRegister));
            btnĐK = new Button();
            lblEmail = new Label();
            lblTK = new Label();
            lblMK = new Label();
            lblNLMK = new Label();
            txtEmail = new TextBox();
            txtTK = new TextBox();
            txtMK = new TextBox();
            txtNLMK = new TextBox();
            btnShow1 = new Button();
            btnShow2 = new Button();
            btnHide1 = new Button();
            btnHide2 = new Button();
            errorProvider1 = new ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // btnĐK
            // 
            btnĐK.BackColor = Color.Lime;
            btnĐK.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnĐK.Location = new Point(334, 430);
            btnĐK.Margin = new Padding(3, 4, 3, 4);
            btnĐK.Name = "btnĐK";
            btnĐK.Size = new Size(115, 48);
            btnĐK.TabIndex = 0;
            btnĐK.Text = "Đăng Kí";
            btnĐK.UseVisualStyleBackColor = false;
            btnĐK.Click += btnĐK_Click;
            // 
            // lblEmail
            // 
            lblEmail.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblEmail.Location = new Point(63, 91);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(132, 29);
            lblEmail.TabIndex = 1;
            lblEmail.Text = "Email";
            lblEmail.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTK
            // 
            lblTK.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTK.Location = new Point(63, 166);
            lblTK.Name = "lblTK";
            lblTK.Size = new Size(132, 29);
            lblTK.TabIndex = 2;
            lblTK.Text = "Tài Khoản";
            lblTK.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblMK
            // 
            lblMK.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblMK.Location = new Point(63, 249);
            lblMK.Name = "lblMK";
            lblMK.Size = new Size(132, 29);
            lblMK.TabIndex = 3;
            lblMK.Text = "Mật Khẩu";
            lblMK.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblNLMK
            // 
            lblNLMK.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNLMK.Location = new Point(63, 341);
            lblNLMK.Name = "lblNLMK";
            lblNLMK.Size = new Size(191, 29);
            lblNLMK.TabIndex = 4;
            lblNLMK.Text = "Nhập Lại Mật Khẩu";
            lblNLMK.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtEmail
            // 
            txtEmail.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtEmail.Location = new Point(292, 86);
            txtEmail.Margin = new Padding(3, 4, 3, 4);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(268, 27);
            txtEmail.TabIndex = 5;
            // 
            // txtTK
            // 
            txtTK.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtTK.Location = new Point(292, 161);
            txtTK.Margin = new Padding(3, 4, 3, 4);
            txtTK.Name = "txtTK";
            txtTK.Size = new Size(268, 27);
            txtTK.TabIndex = 6;
            // 
            // txtMK
            // 
            txtMK.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtMK.Location = new Point(292, 244);
            txtMK.Margin = new Padding(3, 4, 3, 4);
            txtMK.Name = "txtMK";
            txtMK.Size = new Size(268, 27);
            txtMK.TabIndex = 7;
            // 
            // txtNLMK
            // 
            txtNLMK.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtNLMK.Location = new Point(292, 336);
            txtNLMK.Margin = new Padding(3, 4, 3, 4);
            txtNLMK.Name = "txtNLMK";
            txtNLMK.Size = new Size(268, 27);
            txtNLMK.TabIndex = 8;
            // 
            // btnShow1
            // 
            btnShow1.FlatStyle = FlatStyle.Flat;
            btnShow1.Image = (Image)resources.GetObject("btnShow1.Image");
            btnShow1.Location = new Point(521, 242);
            btnShow1.Margin = new Padding(3, 4, 3, 4);
            btnShow1.Name = "btnShow1";
            btnShow1.Size = new Size(39, 31);
            btnShow1.TabIndex = 9;
            btnShow1.UseVisualStyleBackColor = true;
            btnShow1.Click += button2_Click;
            // 
            // btnShow2
            // 
            btnShow2.FlatStyle = FlatStyle.Flat;
            btnShow2.Image = (Image)resources.GetObject("btnShow2.Image");
            btnShow2.Location = new Point(521, 336);
            btnShow2.Margin = new Padding(3, 4, 3, 4);
            btnShow2.Name = "btnShow2";
            btnShow2.Size = new Size(39, 31);
            btnShow2.TabIndex = 10;
            btnShow2.UseVisualStyleBackColor = true;
            btnShow2.Click += btnShow2_Click;
            // 
            // btnHide1
            // 
            btnHide1.FlatStyle = FlatStyle.Flat;
            btnHide1.Image = (Image)resources.GetObject("btnHide1.Image");
            btnHide1.Location = new Point(521, 242);
            btnHide1.Margin = new Padding(3, 4, 3, 4);
            btnHide1.Name = "btnHide1";
            btnHide1.Size = new Size(39, 31);
            btnHide1.TabIndex = 11;
            btnHide1.UseVisualStyleBackColor = true;
            btnHide1.Click += btnHide1_Click;
            // 
            // btnHide2
            // 
            btnHide2.FlatStyle = FlatStyle.Flat;
            btnHide2.Image = (Image)resources.GetObject("btnHide2.Image");
            btnHide2.Location = new Point(521, 336);
            btnHide2.Margin = new Padding(3, 4, 3, 4);
            btnHide2.Name = "btnHide2";
            btnHide2.Size = new Size(39, 31);
            btnHide2.TabIndex = 12;
            btnHide2.UseVisualStyleBackColor = true;
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
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(800, 562);
            Controls.Add(btnHide2);
            Controls.Add(btnHide1);
            Controls.Add(btnShow2);
            Controls.Add(btnShow1);
            Controls.Add(txtNLMK);
            Controls.Add(txtMK);
            Controls.Add(txtTK);
            Controls.Add(txtEmail);
            Controls.Add(lblNLMK);
            Controls.Add(lblMK);
            Controls.Add(lblTK);
            Controls.Add(lblEmail);
            Controls.Add(btnĐK);
            Margin = new Padding(3, 4, 3, 4);
            Name = "frmRegister";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Register";
            Load += frmDK_Load;
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnĐK;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblTK;
        private System.Windows.Forms.Label lblMK;
        private System.Windows.Forms.Label lblNLMK;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtTK;
        private System.Windows.Forms.TextBox txtNLMK;
        private System.Windows.Forms.Button btnShow1;
        private System.Windows.Forms.Button btnShow2;
        private System.Windows.Forms.Button btnHide1;
        private System.Windows.Forms.Button btnHide2;
        private System.Windows.Forms.TextBox txtMK;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}