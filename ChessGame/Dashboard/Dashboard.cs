using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessGame
{
    public partial class frmDashboard : Form
    {
        public string DisplayName;
        public int Elo;
        public int UserId;
        public string Email;
        public string Username;
        private System.Windows.Forms.Timer onlineUpdateTimer; 

        public frmDashboard()
        {
            InitializeComponent();
        }

        private void toolStripContainer1_TopToolStripPanel_Click(object sender, EventArgs e) { }
        private void toolStripButton1_Click(object sender, EventArgs e) { }
        private void toolStripMenuItem1_Click(object sender, EventArgs e) { }
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e) { }

        private void btnChoi_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Match().ShowDialog();
            this.Show();
        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {
            lblUsername.Text = DisplayName;
            lblUserRank.Text = $"Rating: {Elo}";
            LoadOnlineUsers();

            // Thiết lập timer realtime 500ms
            onlineUpdateTimer = new System.Windows.Forms.Timer();
            onlineUpdateTimer.Interval = 500;
            onlineUpdateTimer.Tick += OnlineUpdateTimer_Tick;
            onlineUpdateTimer.Start();
        }

        private void OnlineUpdateTimer_Tick(object sender, EventArgs e)
        {
            LoadOnlineUsers();
        }

        private void btnBan_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Friend().ShowDialog();
            this.Show();
        }

        private void btnBXH_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Ranking().ShowDialog();
            this.Show();
        }

        private void btnLichSu_Click(object sender, EventArgs e)
        {
            this.Hide();
            new History().ShowDialog();
            this.Show();
        }

        private void btnĐX_Click(object sender, EventArgs e)
        {
            TCPClient client = new TCPClient();
            client.Connect();
            var request = new
            {
                action = "LOGOUT",
                username = Username
            };
            string response = client.SendRequest(request);
            client.Disconnect();
            MessageBox.Show("Đăng xuất thành công!");
            this.Hide();
            frmLogin login = new frmLogin();
            login.ShowDialog();
            this.Close();
        }

        private void btnCaiDat_Click(object sender, EventArgs e)
        {
            AccountSetting frm = new AccountSetting();
            frm.UserId = this.UserId;
            frm.CurrentDisplayName = this.DisplayName;
            frm.CurrentEmail = this.Email;
            this.Hide();
            frm.ShowDialog();
            this.Show();
            if (frm.IsUpdated)
            {
                this.DisplayName = frm.CurrentDisplayName;
                this.Email = frm.CurrentEmail;
                lblUsername.Text = this.DisplayName;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (onlineUpdateTimer != null)
                onlineUpdateTimer.Stop();
            Application.Exit();
        }

        private void LoadOnlineUsers()
        {
            try
            {
                TCPClient client = new TCPClient();
                client.Connect();
                var request = new
                {
                    action = "GET_ONLINE_USERS"
                };
                string response = client.SendRequest(request);
                client.Disconnect();

                var obj = System.Text.Json.JsonDocument.Parse(response);
                if (obj.RootElement.GetProperty("success").GetBoolean())
                {
                    var users = obj.RootElement.GetProperty("users").EnumerateArray();
                    lstOnlinePlayers.Items.Clear();
                    int count = 0;
                    foreach (var u in users)
                    {
                        string displayName = u.GetProperty("displayName").GetString();
                        string username = u.GetProperty("username").GetString();
                        lstOnlinePlayers.Items.Add($"{displayName} ({username})");
                        count++;
                    }
                    lblOnlineCount.Text = "Có " + count.ToString() + " người chơi đang online";
                }
                else
                {
                    lblOnlineCount.Text = "Có 0 người chơi đang online";
                    lstOnlinePlayers.Items.Clear();
                }
            }
            catch
            {
                lblOnlineCount.Text = "Có 0 người chơi đang online";
                lstOnlinePlayers.Items.Clear();
            }
        }

        private void lblOnlineCount_Click(object sender, EventArgs e)
        {
            LoadOnlineUsers();
        }
        private void lstOnlinePlayers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
