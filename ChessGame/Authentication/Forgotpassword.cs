using System;
using System.Text.Json;
using System.Windows.Forms;

namespace ChessGame
{
    public partial class frmForgotpassword : Form
    {
        public frmForgotpassword()
        {
            InitializeComponent();
        }
        private void btnGuiMa_Click(object sender, EventArgs e)
        {
            string email = txtQuenMK.Text.Trim();
            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Vui lòng nhập email của bạn!");
                return;
            }
            TCPClient client = new TCPClient();
            client.Connect();
            var request = new
            {
                action = "REQUEST_OTP",
                email = email
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
                    lblHuongDan.Text = "Mã xác nhận đã được gửi! Nhập mã 6 số ở dưới rồi nhấn Tiếp tục.";
                    txtMaXacNhan.Focus();
                }
            }
    }
        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            string email = txtQuenMK.Text.Trim();
            string otp = txtMaXacNhan.Text.Trim();
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(otp))
            {
                MessageBox.Show("Vui lòng nhập email và mã xác nhận!");
                return;
            }
            TCPClient client = new TCPClient();
            client.Connect();
            var otpRequest = new
            {
                action = "VERIFY_OTP",
                email = email,
                otp = otp
            };
            string response = client.SendRequest(otpRequest);
            client.Disconnect();
            using (JsonDocument doc = JsonDocument.Parse(response))
            {
                JsonElement root = doc.RootElement;
                bool success = root.GetProperty("success").GetBoolean();
                string message = root.GetProperty("message").GetString();
                MessageBox.Show(message);
                if (success)
                {
                    Recovery recovery = new Recovery();
                    recovery.UserEmail = email;
                    this.Close();
                    recovery.ShowDialog();
                }
            }
        }
    }
}
