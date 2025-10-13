namespace Giao_Diện_Đăng_Nhập_Cờ_Vua
{
    partial class frmForgotpassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmForgotpassword));
            txtQuenMK = new TextBox();
            lblQuenMK = new Label();
            btnGui = new Button();
            SuspendLayout();
            // 
            // txtQuenMK
            // 
            txtQuenMK.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtQuenMK.Location = new Point(363, 205);
            txtQuenMK.Margin = new Padding(3, 4, 3, 4);
            txtQuenMK.Name = "txtQuenMK";
            txtQuenMK.Size = new Size(365, 27);
            txtQuenMK.TabIndex = 0;
            txtQuenMK.TextChanged += txtQuenMK_TextChanged;
            // 
            // lblQuenMK
            // 
            lblQuenMK.BackColor = Color.Transparent;
            lblQuenMK.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblQuenMK.Location = new Point(12, 209);
            lblQuenMK.Name = "lblQuenMK";
            lblQuenMK.Size = new Size(334, 29);
            lblQuenMK.TabIndex = 1;
            lblQuenMK.Text = "Nhập Email hoặc số điện thoại";
            // 
            // btnGui
            // 
            btnGui.BackColor = Color.SpringGreen;
            btnGui.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGui.Location = new Point(304, 356);
            btnGui.Margin = new Padding(3, 4, 3, 4);
            btnGui.Name = "btnGui";
            btnGui.Size = new Size(122, 41);
            btnGui.TabIndex = 2;
            btnGui.Text = "Gửi";
            btnGui.UseVisualStyleBackColor = false;
            // 
            // frmForgotpassword
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(800, 562);
            Controls.Add(btnGui);
            Controls.Add(lblQuenMK);
            Controls.Add(txtQuenMK);
            ForeColor = SystemColors.ControlText;
            Margin = new Padding(3, 4, 3, 4);
            Name = "frmForgotpassword";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Forgotpassword";
            Load += frmQuenMk_Load;
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtQuenMK;
        private System.Windows.Forms.Label lblQuenMK;
        private System.Windows.Forms.Button btnGui;
    }
}