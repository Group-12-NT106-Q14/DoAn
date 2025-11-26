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
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlRoom = new System.Windows.Forms.Panel();
            this.lblPhongTitle = new System.Windows.Forms.Label();
            this.txtSearchRoom = new System.Windows.Forms.TextBox();
            this.lstRooms = new System.Windows.Forms.ListView();
            this.colRoomName = new System.Windows.Forms.ColumnHeader();
            this.colOwner = new System.Windows.Forms.ColumnHeader();
            this.colOwnerElo = new System.Windows.Forms.ColumnHeader();
            this.colPlayers = new System.Windows.Forms.ColumnHeader();
            this.colStatus = new System.Windows.Forms.ColumnHeader();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnCreateRoom = new System.Windows.Forms.Button();
            this.btnJoinRoom = new System.Windows.Forms.Button();
            this.panelCreateRoom = new System.Windows.Forms.Panel();
            this.lblRoomNamePrompt = new System.Windows.Forms.Label();
            this.txtRoomName = new System.Windows.Forms.TextBox();
            this.btnCreateRoomConfirm = new System.Windows.Forms.Button();
            this.btnCancelCreateRoom = new System.Windows.Forms.Button();
            this.pnlRoom.SuspendLayout();
            this.panelCreateRoom.SuspendLayout();
            this.SuspendLayout();
            // 
            // Form (RoomList)
            // 
            this.BackColor = System.Drawing.Color.FromArgb(240, 217, 181);
            this.ClientSize = new System.Drawing.Size(980, 650);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Name = "RoomList";
            this.Text = "Phòng chơi";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(78, 49, 41);
            this.lblTitle.Location = new System.Drawing.Point(45, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(383, 50);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "DANH SÁCH PHÒNG";
            // 
            // pnlRoom
            // 
            this.pnlRoom.BackColor = System.Drawing.Color.FromArgb(118, 74, 61);
            this.pnlRoom.Location = new System.Drawing.Point(30, 90);
            this.pnlRoom.Name = "pnlRoom";
            this.pnlRoom.Size = new System.Drawing.Size(920, 530);
            this.pnlRoom.TabIndex = 1;
            // 
            // lblPhongTitle
            // 
            this.lblPhongTitle.AutoSize = true;
            this.lblPhongTitle.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblPhongTitle.ForeColor = System.Drawing.Color.White;
            this.lblPhongTitle.Location = new System.Drawing.Point(20, 20);
            this.lblPhongTitle.Name = "lblPhongTitle";
            this.lblPhongTitle.Size = new System.Drawing.Size(219, 35);
            this.lblPhongTitle.TabIndex = 0;
            this.lblPhongTitle.Text = "Danh Sách Phòng";
            // 
            // txtSearchRoom
            // 
            this.txtSearchRoom.BackColor = System.Drawing.Color.FromArgb(247, 234, 214);
            this.txtSearchRoom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearchRoom.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtSearchRoom.Location = new System.Drawing.Point(20, 65);
            this.txtSearchRoom.Name = "txtSearchRoom";
            this.txtSearchRoom.PlaceholderText = "Tìm tên phòng hoặc username...";
            this.txtSearchRoom.Size = new System.Drawing.Size(880, 34);
            this.txtSearchRoom.TabIndex = 1;
            // 
            // lstRooms
            // 
            this.lstRooms.BackColor = System.Drawing.Color.FromArgb(247, 234, 214);
            this.lstRooms.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstRooms.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colRoomName,
            this.colOwner,
            this.colOwnerElo,
            this.colPlayers,
            this.colStatus});
            this.lstRooms.Font = new System.Drawing.Font("Segoe UI", 11.5F);
            this.lstRooms.ForeColor = System.Drawing.Color.FromArgb(78, 49, 41);
            this.lstRooms.FullRowSelect = true;
            this.lstRooms.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstRooms.HideSelection = false;
            this.lstRooms.Location = new System.Drawing.Point(20, 110);
            this.lstRooms.MultiSelect = false;
            this.lstRooms.Name = "lstRooms";
            this.lstRooms.Size = new System.Drawing.Size(880, 360);
            this.lstRooms.TabIndex = 2;
            this.lstRooms.UseCompatibleStateImageBehavior = false;
            this.lstRooms.View = System.Windows.Forms.View.Details;
            // 
            // colRoomName
            // 
            this.colRoomName.Text = "Tên phòng";
            this.colRoomName.Width = 310;
            // 
            // colOwner
            // 
            this.colOwner.Text = "Chủ phòng";
            this.colOwner.Width = 230;
            // 
            // colOwnerElo
            // 
            this.colOwnerElo.Text = "Elo";
            this.colOwnerElo.Width = 70;
            // 
            // colPlayers
            // 
            this.colPlayers.Text = "Người";
            this.colPlayers.Width = 80;
            // 
            // colStatus
            // 
            this.colStatus.Text = "Trạng thái";
            this.colStatus.Width = 170;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(160, 106, 88);
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(20, 480);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(150, 40);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.UseVisualStyleBackColor = false;
            // 
            // btnCreateRoom
            // 
            this.btnCreateRoom.BackColor = System.Drawing.Color.FromArgb(133, 181, 100);
            this.btnCreateRoom.FlatAppearance.BorderSize = 0;
            this.btnCreateRoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateRoom.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnCreateRoom.ForeColor = System.Drawing.Color.White;
            this.btnCreateRoom.Location = new System.Drawing.Point(260, 480);
            this.btnCreateRoom.Name = "btnCreateRoom";
            this.btnCreateRoom.Size = new System.Drawing.Size(190, 40);
            this.btnCreateRoom.TabIndex = 4;
            this.btnCreateRoom.Text = "Tạo phòng mới";
            this.btnCreateRoom.UseVisualStyleBackColor = false;
            // 
            // btnJoinRoom
            // 
            this.btnJoinRoom.BackColor = System.Drawing.Color.FromArgb(160, 106, 88);
            this.btnJoinRoom.Enabled = false;
            this.btnJoinRoom.FlatAppearance.BorderSize = 0;
            this.btnJoinRoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnJoinRoom.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnJoinRoom.ForeColor = System.Drawing.Color.White;
            this.btnJoinRoom.Location = new System.Drawing.Point(710, 480);
            this.btnJoinRoom.Name = "btnJoinRoom";
            this.btnJoinRoom.Size = new System.Drawing.Size(190, 40);
            this.btnJoinRoom.TabIndex = 5;
            this.btnJoinRoom.Text = "Vào phòng";
            this.btnJoinRoom.UseVisualStyleBackColor = false;
            // 
            // panelCreateRoom
            // 
            this.panelCreateRoom.BackColor = System.Drawing.Color.FromArgb(118, 74, 61);
            this.panelCreateRoom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCreateRoom.Controls.Add(this.lblRoomNamePrompt);
            this.panelCreateRoom.Controls.Add(this.txtRoomName);
            this.panelCreateRoom.Controls.Add(this.btnCreateRoomConfirm);
            this.panelCreateRoom.Controls.Add(this.btnCancelCreateRoom);
            // đặt giữa panel danh sách
            this.panelCreateRoom.Location = new System.Drawing.Point(200, 165);
            this.panelCreateRoom.Name = "panelCreateRoom";
            this.panelCreateRoom.Size = new System.Drawing.Size(520, 210);
            this.panelCreateRoom.TabIndex = 6;
            this.panelCreateRoom.Visible = false;
            // 
            // lblRoomNamePrompt
            // 
            this.lblRoomNamePrompt.AutoSize = true;
            this.lblRoomNamePrompt.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblRoomNamePrompt.ForeColor = System.Drawing.Color.White;
            this.lblRoomNamePrompt.Location = new System.Drawing.Point(30, 20);
            this.lblRoomNamePrompt.Name = "lblRoomNamePrompt";
            this.lblRoomNamePrompt.Size = new System.Drawing.Size(182, 30);
            this.lblRoomNamePrompt.TabIndex = 0;
            this.lblRoomNamePrompt.Text = "Nhập tên phòng";
            // 
            // txtRoomName
            // 
            this.txtRoomName.BackColor = System.Drawing.Color.FromArgb(247, 234, 214);
            this.txtRoomName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRoomName.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.txtRoomName.Location = new System.Drawing.Point(30, 60);
            this.txtRoomName.Name = "txtRoomName";
            this.txtRoomName.PlaceholderText = "Tên phòng...";
            this.txtRoomName.Size = new System.Drawing.Size(460, 36);
            this.txtRoomName.TabIndex = 1;
            // 
            // btnCreateRoomConfirm
            // 
            this.btnCreateRoomConfirm.BackColor = System.Drawing.Color.FromArgb(133, 181, 100);
            this.btnCreateRoomConfirm.FlatAppearance.BorderSize = 0;
            this.btnCreateRoomConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateRoomConfirm.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.btnCreateRoomConfirm.ForeColor = System.Drawing.Color.White;
            this.btnCreateRoomConfirm.Location = new System.Drawing.Point(120, 125);
            this.btnCreateRoomConfirm.Name = "btnCreateRoomConfirm";
            this.btnCreateRoomConfirm.Size = new System.Drawing.Size(120, 40);
            this.btnCreateRoomConfirm.TabIndex = 2;
            this.btnCreateRoomConfirm.Text = "Tạo";
            this.btnCreateRoomConfirm.UseVisualStyleBackColor = false;
            // 
            // btnCancelCreateRoom
            // 
            this.btnCancelCreateRoom.BackColor = System.Drawing.Color.FromArgb(160, 106, 88);
            this.btnCancelCreateRoom.FlatAppearance.BorderSize = 0;
            this.btnCancelCreateRoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelCreateRoom.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.btnCancelCreateRoom.ForeColor = System.Drawing.Color.White;
            this.btnCancelCreateRoom.Location = new System.Drawing.Point(280, 125);
            this.btnCancelCreateRoom.Name = "btnCancelCreateRoom";
            this.btnCancelCreateRoom.Size = new System.Drawing.Size(120, 40);
            this.btnCancelCreateRoom.TabIndex = 3;
            this.btnCancelCreateRoom.Text = "Huỷ";
            this.btnCancelCreateRoom.UseVisualStyleBackColor = false;
            // 
            // add controls vào form
            // 
            this.pnlRoom.Controls.Add(this.lblPhongTitle);
            this.pnlRoom.Controls.Add(this.txtSearchRoom);
            this.pnlRoom.Controls.Add(this.lstRooms);
            this.pnlRoom.Controls.Add(this.btnRefresh);
            this.pnlRoom.Controls.Add(this.btnCreateRoom);
            this.pnlRoom.Controls.Add(this.btnJoinRoom);
            this.pnlRoom.Controls.Add(this.panelCreateRoom);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pnlRoom);
            this.pnlRoom.ResumeLayout(false);
            this.pnlRoom.PerformLayout();
            this.panelCreateRoom.ResumeLayout(false);
            this.panelCreateRoom.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
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
