using System;
using System.Text.Json;
using System.Windows.Forms;

namespace ChessGame
{
    public partial class Recovery : Form
    {
        public string UserEmail { get; set; }
        public Recovery()
        {
            InitializeComponent();
            this.Load += Recovery_Load;
        }

        private void Recovery_Load(object sender, EventArgs e)
        {
            txtMatKhauMoi.UseSystemPasswordChar = true;
            txtNhapLaiMK.UseSystemPasswordChar = true;
            btnShowMKMoi.Visible = true;
            btnHideMKMoi.Visible = false;
            btnShowNLMK.Visible = true;
            btnHideNLMK.Visible = false;
        }

        private void btnShowMKMoi_Click(object sender, EventArgs e)
        {
            txtMatKhauMoi.UseSystemPasswordChar = false;
            btnShowMKMoi.Visible = false;
            btnHideMKMoi.Visible = true;
        }
        private void btnHideMKMoi_Click(object sender, EventArgs e)
        {
            txtMatKhauMoi.UseSystemPasswordChar = true;
            btnHideMKMoi.Visible = false;
            btnShowMKMoi.Visible = true;
        }
        private void btnShowNLMK_Click(object sender, EventArgs e)
        {
            txtNhapLaiMK.UseSystemPasswordChar = false;
            btnShowNLMK.Visible = false;
            btnHideNLMK.Visible = true;
        }
        private void btnHideNLMK_Click(object sender, EventArgs e)
        {
            txtNhapLaiMK.UseSystemPasswordChar = true;
            btnHideNLMK.Visible = false;
            btnShowNLMK.Visible = true;
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            string newPass = txtMatKhauMoi.Text.Trim();
            string confirm = txtNhapLaiMK.Text.Trim();
            if (string.IsNullOrEmpty(newPass))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu mới!");
                return;
            }
            if (newPass.Length < 8)
            {
                MessageBox.Show("Mật khẩu phải có ít nhất 8 ký tự!");
                return;
            }
            if (newPass != confirm)
            {
                MessageBox.Show("Mật khẩu nhập lại không khớp!");
                return;
            }
            TCPClient client = new TCPClient();
            client.Connect();
            var request = new
            {
                action = "RESET_PASSWORD",
                email = UserEmail,
                newPassword = newPass
            };
            string response = client.SendRequest(request);
            client.Disconnect();
            using (JsonDocument doc = JsonDocument.Parse(response))
            {
                JsonElement root = doc.RootElement;
                bool success = root.GetProperty("success").GetBoolean();
                string message = root.GetProperty("message").GetString();
                MessageBox.Show(message);
                if (success)
                {
                    this.Close();
                }
            }
        }
    }
}
