namespace ChessGame
{
    partial class Room
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
            this.components = new System.ComponentModel.Container();

            // Top Panel - Create/Join Room
            pnlTop = new Panel();
            lblTitle = new Label();
            btnCreateRoom = new Button();
            pnlCreateRoom = new Panel();
            lblRoomName = new Label();
            txtRoomName = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            chkPrivate = new CheckBox();
            btnCreate = new Button();
            btnCancelCreate = new Button();

            // Left Panel - Available Rooms
            pnlRooms = new Panel();
            lblRoomList = new Label();
            txtSearchRoom = new TextBox();
            lstRooms = new ListBox();
            btnRefreshRooms = new Button();

            // Right Panel - Online Friends
            pnlFriends = new Panel();
            lblFriendsTitle = new Label();
            lstOnlineFriends = new ListBox();
            btnInvite = new Button();

            pnlTop.SuspendLayout();
            pnlCreateRoom.SuspendLayout();
            pnlRooms.SuspendLayout();
            pnlFriends.SuspendLayout();
            this.SuspendLayout();

            // 
            // Room Form
            // 
            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(240, 217, 181);
            this.ClientSize = new Size(1200, 700);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Room";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Phòng Chơi - Game Room";

            // 
            // pnlTop (Top Panel)
            // 
            pnlTop.BackColor = Color.FromArgb(118, 74, 61);
            pnlTop.Location = new Point(20, 20);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(1160, 100);
            pnlTop.TabIndex = 0;
            pnlTop.Controls.Add(lblTitle);
            pnlTop.Controls.Add(btnCreateRoom);

            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(30, 30);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(320, 37);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "🏠 Danh Sách Phòng";

            // 
            // btnCreateRoom
            // 
            btnCreateRoom.BackColor = Color.FromArgb(133, 181, 100);
            btnCreateRoom.FlatStyle = FlatStyle.Flat;
            btnCreateRoom.FlatAppearance.BorderSize = 0;
            btnCreateRoom.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnCreateRoom.ForeColor = Color.White;
            btnCreateRoom.Location = new Point(900, 25);
            btnCreateRoom.Name = "btnCreateRoom";
            btnCreateRoom.Size = new Size(230, 50);
            btnCreateRoom.TabIndex = 1;
            btnCreateRoom.Text = "➕ TẠO PHÒNG MỚI";
            btnCreateRoom.UseVisualStyleBackColor = false;

            // 
            // pnlCreateRoom (Create Room Form - Initially Hidden)
            // 
            pnlCreateRoom.BackColor = Color.FromArgb(160, 106, 88);
            pnlCreateRoom.Location = new Point(300, 200);
            pnlCreateRoom.Name = "pnlCreateRoom";
            pnlCreateRoom.Size = new Size(600, 350);
            pnlCreateRoom.TabIndex = 1;
            pnlCreateRoom.Visible = false;
            pnlCreateRoom.Controls.Add(lblRoomName);
            pnlCreateRoom.Controls.Add(txtRoomName);
            pnlCreateRoom.Controls.Add(lblPassword);
            pnlCreateRoom.Controls.Add(txtPassword);
            pnlCreateRoom.Controls.Add(chkPrivate);
            pnlCreateRoom.Controls.Add(btnCreate);
            pnlCreateRoom.Controls.Add(btnCancelCreate);

            // 
            // lblRoomName
            // 
            lblRoomName.AutoSize = true;
            lblRoomName.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            lblRoomName.ForeColor = Color.White;
            lblRoomName.Location = new Point(50, 40);
            lblRoomName.Name = "lblRoomName";
            lblRoomName.Size = new Size(120, 25);
            lblRoomName.TabIndex = 0;
            lblRoomName.Text = "Tên Phòng:";

            // 
            // txtRoomName
            // 
            txtRoomName.BackColor = Color.FromArgb(247, 234, 214);
            txtRoomName.BorderStyle = BorderStyle.None;
            txtRoomName.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txtRoomName.Location = new Point(50, 75);
            txtRoomName.Name = "txtRoomName";
            txtRoomName.Size = new Size(500, 22);
            txtRoomName.TabIndex = 1;

            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            lblPassword.ForeColor = Color.White;
            lblPassword.Location = new Point(50, 120);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(210, 25);
            lblPassword.TabIndex = 2;
            lblPassword.Text = "Mật Khẩu (tùy chọn):";

            // 
            // txtPassword
            // 
            txtPassword.BackColor = Color.FromArgb(247, 234, 214);
            txtPassword.BorderStyle = BorderStyle.None;
            txtPassword.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txtPassword.Location = new Point(50, 155);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(500, 22);
            txtPassword.TabIndex = 3;

            // 
            // chkPrivate
            // 
            chkPrivate.AutoSize = true;
            chkPrivate.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            chkPrivate.ForeColor = Color.White;
            chkPrivate.Location = new Point(50, 200);
            chkPrivate.Name = "chkPrivate";
            chkPrivate.Size = new Size(150, 24);
            chkPrivate.TabIndex = 4;
            chkPrivate.Text = "🔒 Phòng Riêng Tư";
            chkPrivate.UseVisualStyleBackColor = true;

            // 
            // btnCreate
            // 
            btnCreate.BackColor = Color.FromArgb(133, 181, 100);
            btnCreate.FlatStyle = FlatStyle.Flat;
            btnCreate.FlatAppearance.BorderSize = 0;
            btnCreate.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnCreate.ForeColor = Color.White;
            btnCreate.Location = new Point(150, 260);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(150, 50);
            btnCreate.TabIndex = 5;
            btnCreate.Text = "✅ Tạo Phòng";
            btnCreate.UseVisualStyleBackColor = false;

            // 
            // btnCancelCreate
            // 
            btnCancelCreate.BackColor = Color.FromArgb(118, 74, 61);
            btnCancelCreate.FlatStyle = FlatStyle.Flat;
            btnCancelCreate.FlatAppearance.BorderSize = 0;
            btnCancelCreate.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnCancelCreate.ForeColor = Color.White;
            btnCancelCreate.Location = new Point(320, 260);
            btnCancelCreate.Name = "btnCancelCreate";
            btnCancelCreate.Size = new Size(150, 50);
            btnCancelCreate.TabIndex = 6;
            btnCancelCreate.Text = "❌ Hủy";
            btnCancelCreate.UseVisualStyleBackColor = false;

            // 
            // pnlRooms (Available Rooms List)
            // 
            pnlRooms.BackColor = Color.FromArgb(118, 74, 61);
            pnlRooms.Location = new Point(20, 140);
            pnlRooms.Name = "pnlRooms";
            pnlRooms.Size = new Size(750, 540);
            pnlRooms.TabIndex = 2;
            pnlRooms.Controls.Add(lblRoomList);
            pnlRooms.Controls.Add(txtSearchRoom);
            pnlRooms.Controls.Add(lstRooms);
            pnlRooms.Controls.Add(btnRefreshRooms);

            // 
            // lblRoomList
            // 
            lblRoomList.AutoSize = true;
            lblRoomList.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            lblRoomList.ForeColor = Color.White;
            lblRoomList.Location = new Point(20, 20);
            lblRoomList.Name = "lblRoomList";
            lblRoomList.Size = new Size(200, 25);
            lblRoomList.TabIndex = 0;
            lblRoomList.Text = "Phòng Đang Mở";

            // 
            // txtSearchRoom
            // 
            txtSearchRoom.BackColor = Color.FromArgb(247, 234, 214);
            txtSearchRoom.BorderStyle = BorderStyle.None;
            txtSearchRoom.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            txtSearchRoom.Location = new Point(20, 60);
            txtSearchRoom.Name = "txtSearchRoom";
            txtSearchRoom.PlaceholderText = "🔍 Tìm kiếm phòng...";
            txtSearchRoom.Size = new Size(710, 20);
            txtSearchRoom.TabIndex = 1;

            // 
            // lstRooms
            // 
            lstRooms.BackColor = Color.FromArgb(247, 234, 214);
            lstRooms.BorderStyle = BorderStyle.None;
            lstRooms.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            lstRooms.ForeColor = Color.FromArgb(78, 49, 41);
            lstRooms.ItemHeight = 20;
            lstRooms.Location = new Point(20, 95);
            lstRooms.Name = "lstRooms";
            lstRooms.Size = new Size(710, 380);
            lstRooms.TabIndex = 2;

            // 
            // btnRefreshRooms
            // 
            btnRefreshRooms.BackColor = Color.FromArgb(133, 181, 100);
            btnRefreshRooms.FlatStyle = FlatStyle.Flat;
            btnRefreshRooms.FlatAppearance.BorderSize = 0;
            btnRefreshRooms.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            btnRefreshRooms.ForeColor = Color.White;
            btnRefreshRooms.Location = new Point(20, 490);
            btnRefreshRooms.Name = "btnRefreshRooms";
            btnRefreshRooms.Size = new Size(710, 40);
            btnRefreshRooms.TabIndex = 3;
            btnRefreshRooms.Text = "🔄 Làm Mới";
            btnRefreshRooms.UseVisualStyleBackColor = false;

            // 
            // pnlFriends (Online Friends)
            // 
            pnlFriends.BackColor = Color.FromArgb(118, 74, 61);
            pnlFriends.Location = new Point(790, 140);
            pnlFriends.Name = "pnlFriends";
            pnlFriends.Size = new Size(390, 540);
            pnlFriends.TabIndex = 3;
            pnlFriends.Controls.Add(lblFriendsTitle);
            pnlFriends.Controls.Add(lstOnlineFriends);
            pnlFriends.Controls.Add(btnInvite);

            // 
            // lblFriendsTitle
            // 
            lblFriendsTitle.AutoSize = true;
            lblFriendsTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            lblFriendsTitle.ForeColor = Color.White;
            lblFriendsTitle.Location = new Point(20, 20);
            lblFriendsTitle.Name = "lblFriendsTitle";
            lblFriendsTitle.Size = new Size(200, 25);
            lblFriendsTitle.TabIndex = 0;
            lblFriendsTitle.Text = "🟢 Bạn Bè Online";

            // 
            // lstOnlineFriends
            // 
            lstOnlineFriends.BackColor = Color.FromArgb(247, 234, 214);
            lstOnlineFriends.BorderStyle = BorderStyle.None;
            lstOnlineFriends.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            lstOnlineFriends.ForeColor = Color.FromArgb(78, 49, 41);
            lstOnlineFriends.ItemHeight = 20;
            lstOnlineFriends.Location = new Point(20, 60);
            lstOnlineFriends.Name = "lstOnlineFriends";
            lstOnlineFriends.Size = new Size(350, 415);
            lstOnlineFriends.TabIndex = 1;

            // 
            // btnInvite
            // 
            btnInvite.BackColor = Color.FromArgb(133, 181, 100);
            btnInvite.FlatStyle = FlatStyle.Flat;
            btnInvite.FlatAppearance.BorderSize = 0;
            btnInvite.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            btnInvite.ForeColor = Color.White;
            btnInvite.Location = new Point(20, 490);
            btnInvite.Name = "btnInvite";
            btnInvite.Size = new Size(350, 40);
            btnInvite.TabIndex = 2;
            btnInvite.Text = "✉️ Mời Vào Phòng";
            btnInvite.UseVisualStyleBackColor = false;

            this.Controls.Add(pnlCreateRoom);
            this.Controls.Add(pnlFriends);
            this.Controls.Add(pnlRooms);
            this.Controls.Add(pnlTop);

            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            pnlCreateRoom.ResumeLayout(false);
            pnlCreateRoom.PerformLayout();
            pnlRooms.ResumeLayout(false);
            pnlRooms.PerformLayout();
            pnlFriends.ResumeLayout(false);
            pnlFriends.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private Panel pnlTop;
        private Label lblTitle;
        private Button btnCreateRoom;
        private Panel pnlCreateRoom;
        private Label lblRoomName;
        private TextBox txtRoomName;
        private Label lblPassword;
        private TextBox txtPassword;
        private CheckBox chkPrivate;
        private Button btnCreate;
        private Button btnCancelCreate;
        private Panel pnlRooms;
        private Label lblRoomList;
        private TextBox txtSearchRoom;
        private ListBox lstRooms;
        private Button btnRefreshRooms;
        private Panel pnlFriends;
        private Label lblFriendsTitle;
        private ListBox lstOnlineFriends;
        private Button btnInvite;
    }
}
