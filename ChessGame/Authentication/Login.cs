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
            txtMK.UseSystemPasswordChar = true;
            btnShow.Visible = true;
            btnHide.Visible = false;
            this.AcceptButton = btnĐăngNhập;
        }

        private void lblMoiDen_Click(object sender, EventArgs e)
        {

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

                    // NEW: đọc cờ alreadyOnline (nếu server có gửi)
                    bool alreadyOnline = false;
                    if (root.TryGetProperty("alreadyOnline", out JsonElement aoElement) &&
                        aoElement.ValueKind == System.Text.Json.JsonValueKind.True ||
                        aoElement.ValueKind == System.Text.Json.JsonValueKind.False)
                    {
                        try
                        {
                            alreadyOnline = aoElement.GetBoolean();
                        }
                        catch { alreadyOnline = false; }
                    }

                    if (success)
                    {
                        string notify = "Đăng nhập thành công.";
                        if (alreadyOnline)
                        {
                            notify += "\n\nLưu ý: Tài khoản này hiện cũng đang đăng nhập ở một nơi khác.";
                        }

                        MessageBox.Show(
                            notify,
                            "Đăng nhập",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );

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
                        MessageBox.Show(message, "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                tcpClient.Disconnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Server chưa được bật hoặc đã bảo trì");
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
        private void btnHide_Click(object sender, EventArgs e)
        {
            txtMK.UseSystemPasswordChar = true;
            btnHide.Visible = false;
            btnShow.Visible = true;
        }
        private void btnShow_Click(object sender, EventArgs e)
        {
            txtMK.UseSystemPasswordChar = false;
            btnShow.Visible = false;
            btnHide.Visible = true;
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

        private void pnlRight_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
