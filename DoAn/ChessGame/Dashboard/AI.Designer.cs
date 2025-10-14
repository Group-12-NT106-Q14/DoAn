namespace ChessGame
{
    partial class AI
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
            pnlMain = new Panel();
            lblTitle = new Label();
            lblDescription = new Label();
            pnlDifficulty = new Panel();
            lblDifficultyTitle = new Label();
            rbEasy = new RadioButton();
            lblEasyDesc = new Label();
            rbMedium = new RadioButton();
            lblMediumDesc = new Label();
            rbHard = new RadioButton();
            lblHardDesc = new Label();
            pnlColor = new Panel();
            lblColorTitle = new Label();
            rbWhite = new RadioButton();
            rbBlack = new RadioButton();
            rbRandom = new RadioButton();
            btnStart = new Button();
            pnlMain.SuspendLayout();
            pnlDifficulty.SuspendLayout();
            pnlColor.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMain
            // 
            pnlMain.BackColor = Color.FromArgb(240, 217, 181);
            pnlMain.Controls.Add(lblTitle);
            pnlMain.Controls.Add(lblDescription);
            pnlMain.Controls.Add(pnlDifficulty);
            pnlMain.Controls.Add(pnlColor);
            pnlMain.Controls.Add(btnStart);
            pnlMain.Location = new Point(20, 20);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new Size(960, 610);
            pnlMain.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(78, 49, 41);
            lblTitle.Location = new Point(300, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(429, 62);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "🤖 CHƠI VỚI MÁY";
            // 
            // lblDescription
            // 
            lblDescription.Font = new Font("Segoe UI", 12F);
            lblDescription.ForeColor = Color.FromArgb(118, 74, 61);
            lblDescription.Location = new Point(200, 80);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(600, 30);
            lblDescription.TabIndex = 1;
            lblDescription.Text = "Chọn độ khó và màu quân cờ để bắt đầu chơi với máy";
            lblDescription.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlDifficulty
            // 
            pnlDifficulty.BackColor = Color.FromArgb(118, 74, 61);
            pnlDifficulty.Controls.Add(lblDifficultyTitle);
            pnlDifficulty.Controls.Add(rbEasy);
            pnlDifficulty.Controls.Add(lblEasyDesc);
            pnlDifficulty.Controls.Add(rbMedium);
            pnlDifficulty.Controls.Add(lblMediumDesc);
            pnlDifficulty.Controls.Add(rbHard);
            pnlDifficulty.Controls.Add(lblHardDesc);
            pnlDifficulty.Location = new Point(38, 146);
            pnlDifficulty.Name = "pnlDifficulty";
            pnlDifficulty.Size = new Size(420, 247);
            pnlDifficulty.TabIndex = 3;
            // 
            // lblDifficultyTitle
            // 
            lblDifficultyTitle.AutoSize = true;
            lblDifficultyTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblDifficultyTitle.ForeColor = Color.White;
            lblDifficultyTitle.Location = new Point(20, 20);
            lblDifficultyTitle.Name = "lblDifficultyTitle";
            lblDifficultyTitle.Size = new Size(230, 37);
            lblDifficultyTitle.TabIndex = 0;
            lblDifficultyTitle.Text = "🎯 Chọn Độ Khó";
            // 
            // rbEasy
            // 
            rbEasy.AutoSize = true;
            rbEasy.Checked = true;
            rbEasy.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            rbEasy.ForeColor = Color.White;
            rbEasy.Location = new Point(30, 70);
            rbEasy.Name = "rbEasy";
            rbEasy.Size = new Size(198, 32);
            rbEasy.TabIndex = 1;
            rbEasy.TabStop = true;
            rbEasy.Text = "😊 Dễ (Beginner)";
            rbEasy.UseVisualStyleBackColor = true;
            // 
            // lblEasyDesc
            // 
            lblEasyDesc.AutoSize = true;
            lblEasyDesc.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblEasyDesc.ForeColor = Color.FromArgb(255, 223, 186);
            lblEasyDesc.Location = new Point(50, 98);
            lblEasyDesc.Name = "lblEasyDesc";
            lblEasyDesc.Size = new Size(212, 20);
            lblEasyDesc.TabIndex = 2;
            lblEasyDesc.Text = "Phù hợp cho người mới bắt đầu";
            // 
            // rbMedium
            // 
            rbMedium.AutoSize = true;
            rbMedium.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            rbMedium.ForeColor = Color.White;
            rbMedium.Location = new Point(30, 130);
            rbMedium.Name = "rbMedium";
            rbMedium.Size = new Size(261, 32);
            rbMedium.TabIndex = 3;
            rbMedium.Text = "🙂 Trung Bình (Normal)";
            rbMedium.UseVisualStyleBackColor = true;
            // 
            // lblMediumDesc
            // 
            lblMediumDesc.AutoSize = true;
            lblMediumDesc.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblMediumDesc.ForeColor = Color.FromArgb(255, 223, 186);
            lblMediumDesc.Location = new Point(50, 158);
            lblMediumDesc.Name = "lblMediumDesc";
            lblMediumDesc.Size = new Size(241, 20);
            lblMediumDesc.TabIndex = 4;
            lblMediumDesc.Text = "Thử thách cho người chơi tầm trung";
            // 
            // rbHard
            // 
            rbHard.AutoSize = true;
            rbHard.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            rbHard.ForeColor = Color.White;
            rbHard.Location = new Point(30, 190);
            rbHard.Name = "rbHard";
            rbHard.Size = new Size(170, 32);
            rbHard.TabIndex = 5;
            rbHard.Text = "😤 Khó (Hard)";
            rbHard.UseVisualStyleBackColor = true;
            // 
            // lblHardDesc
            // 
            lblHardDesc.AutoSize = true;
            lblHardDesc.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblHardDesc.ForeColor = Color.FromArgb(255, 223, 186);
            lblHardDesc.Location = new Point(50, 218);
            lblHardDesc.Name = "lblHardDesc";
            lblHardDesc.Size = new Size(171, 20);
            lblHardDesc.TabIndex = 6;
            lblHardDesc.Text = "Dành cho người chơi giỏi";
            // 
            // pnlColor
            // 
            pnlColor.BackColor = Color.FromArgb(118, 74, 61);
            pnlColor.Controls.Add(lblColorTitle);
            pnlColor.Controls.Add(rbWhite);
            pnlColor.Controls.Add(rbBlack);
            pnlColor.Controls.Add(rbRandom);
            pnlColor.Location = new Point(500, 146);
            pnlColor.Name = "pnlColor";
            pnlColor.Size = new Size(420, 200);
            pnlColor.TabIndex = 4;
            // 
            // lblColorTitle
            // 
            lblColorTitle.AutoSize = true;
            lblColorTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblColorTitle.ForeColor = Color.White;
            lblColorTitle.Location = new Point(20, 20);
            lblColorTitle.Name = "lblColorTitle";
            lblColorTitle.Size = new Size(307, 37);
            lblColorTitle.TabIndex = 0;
            lblColorTitle.Text = "♟️ Chọn Màu Quân Cờ";
            // 
            // rbWhite
            // 
            rbWhite.AutoSize = true;
            rbWhite.Checked = true;
            rbWhite.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            rbWhite.ForeColor = Color.White;
            rbWhite.Location = new Point(30, 70);
            rbWhite.Name = "rbWhite";
            rbWhite.Size = new Size(275, 32);
            rbWhite.TabIndex = 1;
            rbWhite.TabStop = true;
            rbWhite.Text = "⚪ Quân Trắng (Đi trước)";
            rbWhite.UseVisualStyleBackColor = true;
            // 
            // rbBlack
            // 
            rbBlack.AutoSize = true;
            rbBlack.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            rbBlack.ForeColor = Color.White;
            rbBlack.Location = new Point(30, 110);
            rbBlack.Name = "rbBlack";
            rbBlack.Size = new Size(240, 32);
            rbBlack.TabIndex = 2;
            rbBlack.Text = "⚫ Quân Đen (Đi sau)";
            rbBlack.UseVisualStyleBackColor = true;
            // 
            // rbRandom
            // 
            rbRandom.AutoSize = true;
            rbRandom.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            rbRandom.ForeColor = Color.White;
            rbRandom.Location = new Point(30, 150);
            rbRandom.Name = "rbRandom";
            rbRandom.Size = new Size(181, 32);
            rbRandom.TabIndex = 3;
            rbRandom.Text = "🎲 Ngẫu Nhiên";
            rbRandom.UseVisualStyleBackColor = true;
            // 
            // btnStart
            // 
            btnStart.BackColor = Color.FromArgb(133, 181, 100);
            btnStart.FlatAppearance.BorderSize = 0;
            btnStart.FlatStyle = FlatStyle.Flat;
            btnStart.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            btnStart.ForeColor = Color.White;
            btnStart.Location = new Point(500, 364);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(420, 80);
            btnStart.TabIndex = 5;
            btnStart.Text = "▶️ BẮT ĐẦU TRẬN ĐẤU";
            btnStart.UseVisualStyleBackColor = false;
            // 
            // AI
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 217, 181);
            ClientSize = new Size(1000, 650);
            Controls.Add(pnlMain);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "AI";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Chơi Với Máy - AI Challenge";
            pnlMain.ResumeLayout(false);
            pnlMain.PerformLayout();
            pnlDifficulty.ResumeLayout(false);
            pnlDifficulty.PerformLayout();
            pnlColor.ResumeLayout(false);
            pnlColor.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlMain;
        private Label lblTitle;
        private Label lblDescription;
        private Panel pnlDifficulty;
        private Label lblDifficultyTitle;
        private RadioButton rbEasy;
        private Label lblEasyDesc;
        private RadioButton rbMedium;
        private Label lblMediumDesc;
        private RadioButton rbHard;
        private Label lblHardDesc;
        private Panel pnlColor;
        private Label lblColorTitle;
        private RadioButton rbWhite;
        private RadioButton rbBlack;
        private RadioButton rbRandom;
        private Button btnStart;
    }
}
