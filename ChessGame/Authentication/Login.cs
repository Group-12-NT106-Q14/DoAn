using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace ChessGame
{
    public partial class frmLogin : Form
    {
        private TCPClient tcpClient;

        public frmLogin()
        {
            InitializeComponent();
        }

        private void lblMoiDen_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (btnShow.Visible == true)
            {
                btnShow.Visible = false;
                btnHide.Visible = true;
                txtMK.PasswordChar = '\0';
            }
            if (btnHide.Visible == true)
            {
                btnHide.Visible = false;
                btnShow.Visible = true;
                txtMK.PasswordChar = '*';
            }
        }

        private void btnĐăngNhập_Click(object sender, EventArgs e)
        {
            try
            {
                tcpClient = new TCPClient();
                tcpClient.Connect();
                var loginRequest = new
                {
                    action = "LOGIN",
                    username = txtTK.Text,
                    password = txtMK.Text
                };
                string responseJson = tcpClient.SendRequest(loginRequest);
                JsonDocument doc = JsonDocument.Parse(responseJson);
                {
                    JsonElement root = doc.RootElement;
                    bool success = root.GetProperty("success").GetBoolean();
                    string message = root.GetProperty("message").GetString();
                    if (success)
                    {
                        MessageBox.Show("Đăng nhập thành công");
                        this.Hide();
                        frmDashboard frm = new frmDashboard();
                        JsonElement userElement = root.GetProperty("user");
                        string displayName = userElement.GetProperty("displayName").GetString();
                        int elo = userElement.GetProperty("elo").GetInt32();
                        int userId = userElement.GetProperty("userId").GetInt32();
                        string email = userElement.GetProperty("email").GetString();
                        frm.DisplayName = displayName;
                        frm.Elo = elo;
                        frm.UserId = userId;
                        frm.Email = email;
                        frm.Username = txtTK.Text;
                        frm.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show(message);
                    }
                }
                tcpClient.Disconnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: Server chưa được bật hoặc đã bảo trì!");
            }
        }
        private void btnĐK_Click(object sender, EventArgs e)
        {
            frmRegister frm = new frmRegister();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }
        private void btnQMK_Click(object sender, EventArgs e)
        {
            frmForgotpassword frm = new frmForgotpassword();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }
        private void txtMK_TextChanged(object sender, EventArgs e)
        {
            txtMK.PasswordChar = '*';
        }
        private void btnHide_Click(object sender, EventArgs e)
        {
            this.btnHide.Visible = false;
            this.btnShow.Visible = true;
            this.txtMK.PasswordChar = '\0';
        }
        private void btnShow_Click(object sender, EventArgs e)
        {
            this.btnShow.Visible = false;
            this.btnHide.Visible = true;
            this.txtMK.PasswordChar = '*';
        }
        private void lblLuu_Click(object sender, EventArgs e)
        {

        }
        private void frmDN_Load(object sender, EventArgs e)
        {

        }
        private void picLogo_Click(object sender, EventArgs e)
        {

        }
    }
}
