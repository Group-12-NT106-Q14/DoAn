namespace ChessGame
{
    partial class RoomList
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RoomList));
            lblTitle = new Label();
            pnlRoom = new Panel();
            lblPhongTitle = new Label();
            txtSearchRoom = new TextBox();
            lstRooms = new ListView();
            colRoomName = new ColumnHeader();
            colOwner = new ColumnHeader();
            colOwnerElo = new ColumnHeader();
            colPlayers = new ColumnHeader();
            colStatus = new ColumnHeader();
            btnRefresh = new Button();
            btnCreateRoom = new Button();
            btnJoinRoom = new Button();
            panelCreateRoom = new Panel();
            lblRoomNamePrompt = new Label();
            txtRoomName = new TextBox();
            btnCreateRoomConfirm = new Button();
            btnCancelCreateRoom = new Button();
            pnlRoom.SuspendLayout();
            panelCreateRoom.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(78, 49, 41);
            lblTitle.Location = new Point(45, 30);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(383, 50);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "DANH SÁCH PHÒNG";
            // 
            // pnlRoom
            // 
            pnlRoom.BackColor = Color.FromArgb(118, 74, 61);
            pnlRoom.Controls.Add(lblPhongTitle);
            pnlRoom.Controls.Add(txtSearchRoom);
            pnlRoom.Controls.Add(lstRooms);
            pnlRoom.Controls.Add(btnRefresh);
            pnlRoom.Controls.Add(btnCreateRoom);
            pnlRoom.Controls.Add(btnJoinRoom);
            pnlRoom.Controls.Add(panelCreateRoom);
            pnlRoom.Location = new Point(30, 90);
            pnlRoom.Name = "pnlRoom";
            pnlRoom.Size = new Size(920, 530);
            pnlRoom.TabIndex = 1;
            // 
            // lblPhongTitle
            // 
            lblPhongTitle.AutoSize = true;
            lblPhongTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblPhongTitle.ForeColor = Color.White;
            lblPhongTitle.Location = new Point(20, 20);
            lblPhongTitle.Name = "lblPhongTitle";
            lblPhongTitle.Size = new Size(219, 35);
            lblPhongTitle.TabIndex = 0;
            lblPhongTitle.Text = "Danh Sách Phòng";
            // 
            // txtSearchRoom
            // 
            txtSearchRoom.BackColor = Color.FromArgb(247, 234, 214);
            txtSearchRoom.BorderStyle = BorderStyle.FixedSingle;
            txtSearchRoom.Font = new Font("Segoe UI", 12F);
            txtSearchRoom.Location = new Point(20, 65);
            txtSearchRoom.Name = "txtSearchRoom";
            txtSearchRoom.PlaceholderText = "Tìm tên phòng hoặc username...";
            txtSearchRoom.Size = new Size(880, 34);
            txtSearchRoom.TabIndex = 1;
            // 
            // lstRooms
            // 
            lstRooms.BackColor = Color.FromArgb(247, 234, 214);
            lstRooms.BorderStyle = BorderStyle.FixedSingle;
            lstRooms.Columns.AddRange(new ColumnHeader[] { colRoomName, colOwner, colOwnerElo, colPlayers, colStatus });
            lstRooms.Font = new Font("Segoe UI", 11.5F);
            lstRooms.ForeColor = Color.FromArgb(78, 49, 41);
            lstRooms.FullRowSelect = true;
            lstRooms.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            lstRooms.Location = new Point(20, 110);
            lstRooms.MultiSelect = false;
            lstRooms.Name = "lstRooms";
            lstRooms.Size = new Size(880, 360);
            lstRooms.TabIndex = 2;
            lstRooms.UseCompatibleStateImageBehavior = false;
            lstRooms.View = View.Details;
            // 
            // colRoomName
            // 
            colRoomName.Text = "Tên phòng";
            colRoomName.Width = 310;
            // 
            // colOwner
            // 
            colOwner.Text = "Chủ phòng";
            colOwner.Width = 230;
            // 
            // colOwnerElo
            // 
            colOwnerElo.Text = "Elo";
            colOwnerElo.Width = 70;
            // 
            // colPlayers
            // 
            colPlayers.Text = "Người";
            colPlayers.Width = 80;
            // 
            // colStatus
            // 
            colStatus.Text = "Trạng thái";
            colStatus.Width = 170;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.FromArgb(160, 106, 88);
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(20, 480);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(150, 40);
            btnRefresh.TabIndex = 3;
            btnRefresh.Text = "Làm mới";
            btnRefresh.UseVisualStyleBackColor = false;
            // 
            // btnCreateRoom
            // 
            btnCreateRoom.BackColor = Color.FromArgb(133, 181, 100);
            btnCreateRoom.FlatAppearance.BorderSize = 0;
            btnCreateRoom.FlatStyle = FlatStyle.Flat;
            btnCreateRoom.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnCreateRoom.ForeColor = Color.White;
            btnCreateRoom.Location = new Point(260, 480);
            btnCreateRoom.Name = "btnCreateRoom";
            btnCreateRoom.Size = new Size(190, 40);
            btnCreateRoom.TabIndex = 4;
            btnCreateRoom.Text = "Tạo phòng mới";
            btnCreateRoom.UseVisualStyleBackColor = false;
            // 
            // btnJoinRoom
            // 
            btnJoinRoom.BackColor = Color.FromArgb(160, 106, 88);
            btnJoinRoom.Enabled = false;
            btnJoinRoom.FlatAppearance.BorderSize = 0;
            btnJoinRoom.FlatStyle = FlatStyle.Flat;
            btnJoinRoom.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnJoinRoom.ForeColor = Color.White;
            btnJoinRoom.Location = new Point(710, 480);
            btnJoinRoom.Name = "btnJoinRoom";
            btnJoinRoom.Size = new Size(190, 40);
            btnJoinRoom.TabIndex = 5;
            btnJoinRoom.Text = "Vào phòng";
            btnJoinRoom.UseVisualStyleBackColor = false;
            // 
            // panelCreateRoom
            // 
            panelCreateRoom.BackColor = Color.FromArgb(118, 74, 61);
            panelCreateRoom.BorderStyle = BorderStyle.FixedSingle;
            panelCreateRoom.Controls.Add(lblRoomNamePrompt);
            panelCreateRoom.Controls.Add(txtRoomName);
            panelCreateRoom.Controls.Add(btnCreateRoomConfirm);
            panelCreateRoom.Controls.Add(btnCancelCreateRoom);
            panelCreateRoom.Location = new Point(200, 165);
            panelCreateRoom.Name = "panelCreateRoom";
            panelCreateRoom.Size = new Size(520, 210);
            panelCreateRoom.TabIndex = 6;
            panelCreateRoom.Visible = false;
            // 
            // lblRoomNamePrompt
            // 
            lblRoomNamePrompt.AutoSize = true;
            lblRoomNamePrompt.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblRoomNamePrompt.ForeColor = Color.White;
            lblRoomNamePrompt.Location = new Point(30, 20);
            lblRoomNamePrompt.Name = "lblRoomNamePrompt";
            lblRoomNamePrompt.Size = new Size(182, 30);
            lblRoomNamePrompt.TabIndex = 0;
            lblRoomNamePrompt.Text = "Nhập tên phòng";
            // 
            // txtRoomName
            // 
            txtRoomName.BackColor = Color.FromArgb(247, 234, 214);
            txtRoomName.BorderStyle = BorderStyle.FixedSingle;
            txtRoomName.Font = new Font("Segoe UI", 13F);
            txtRoomName.Location = new Point(30, 60);
            txtRoomName.Name = "txtRoomName";
            txtRoomName.PlaceholderText = "Tên phòng...";
            txtRoomName.Size = new Size(460, 36);
            txtRoomName.TabIndex = 1;
            // 
            // btnCreateRoomConfirm
            // 
            btnCreateRoomConfirm.BackColor = Color.FromArgb(133, 181, 100);
            btnCreateRoomConfirm.FlatAppearance.BorderSize = 0;
            btnCreateRoomConfirm.FlatStyle = FlatStyle.Flat;
            btnCreateRoomConfirm.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            btnCreateRoomConfirm.ForeColor = Color.White;
            btnCreateRoomConfirm.Location = new Point(120, 125);
            btnCreateRoomConfirm.Name = "btnCreateRoomConfirm";
            btnCreateRoomConfirm.Size = new Size(120, 40);
            btnCreateRoomConfirm.TabIndex = 2;
            btnCreateRoomConfirm.Text = "Tạo";
            btnCreateRoomConfirm.UseVisualStyleBackColor = false;
            // 
            // btnCancelCreateRoom
            // 
            btnCancelCreateRoom.BackColor = Color.FromArgb(160, 106, 88);
            btnCancelCreateRoom.FlatAppearance.BorderSize = 0;
            btnCancelCreateRoom.FlatStyle = FlatStyle.Flat;
            btnCancelCreateRoom.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            btnCancelCreateRoom.ForeColor = Color.White;
            btnCancelCreateRoom.Location = new Point(280, 125);
            btnCancelCreateRoom.Name = "btnCancelCreateRoom";
            btnCancelCreateRoom.Size = new Size(120, 40);
            btnCancelCreateRoom.TabIndex = 3;
            btnCancelCreateRoom.Text = "Huỷ";
            btnCancelCreateRoom.UseVisualStyleBackColor = false;
            // 
            // RoomList
            // 
            BackColor = Color.FromArgb(240, 217, 181);
            ClientSize = new Size(980, 650);
            Controls.Add(lblTitle);
            Controls.Add(pnlRoom);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "RoomList";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Phòng chơi";
            pnlRoom.ResumeLayout(false);
            pnlRoom.PerformLayout();
            panelCreateRoom.ResumeLayout(false);
            panelCreateRoom.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlRoom;
        private System.Windows.Forms.Label lblPhongTitle;
        private System.Windows.Forms.TextBox txtSearchRoom;
        private System.Windows.Forms.ListView lstRooms;
        private System.Windows.Forms.ColumnHeader colRoomName;
        private System.Windows.Forms.ColumnHeader colOwner;
        private System.Windows.Forms.ColumnHeader colOwnerElo;
        private System.Windows.Forms.ColumnHeader colPlayers;
        private System.Windows.Forms.ColumnHeader colStatus;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnCreateRoom;
        private System.Windows.Forms.Button btnJoinRoom;
        private System.Windows.Forms.Panel panelCreateRoom;
        private System.Windows.Forms.Label lblRoomNamePrompt;
        private System.Windows.Forms.TextBox txtRoomName;
        private System.Windows.Forms.Button btnCreateRoomConfirm;
        private System.Windows.Forms.Button btnCancelCreateRoom;
    }
}
