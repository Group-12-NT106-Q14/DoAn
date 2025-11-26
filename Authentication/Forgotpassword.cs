using System;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessGame
{
    public partial class frmForgotpassword : Form
    {
        public frmForgotpassword()
        {
            InitializeComponent();

            btnXacNhan.Enabled = false;
            this.AcceptButton = btnGuiMa;
        }

        private async void btnGuiMa_Click(object sender, EventArgs e)
        {
            string email = txtQuenMK.Text.Trim();
            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Vui lòng nhập email của bạn!");
                return;
            }

            btnGuiMa.Enabled = false;
            btnXacNhan.Enabled = false;
            progressSending.Visible = true;
            lblHuongDan.Text = "Đang gửi mã xác nhận, vui lòng chờ...";

            string response = null;

            try
            {
                response = await Task.Run(() =>
                {
                    TCPClient client = new TCPClient();
                    client.Connect();
                    var request = new
                    {
                        action = "REQUEST_OTP",
                        email = email
                    };
                    string res = client.SendRequest(request);
                    client.Disconnect();
                    return res;
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi gửi mã xác nhận.\nVui lòng thử lại sau.\n\nChi tiết: " + ex.Message);
                return;
            }
            finally
            {
                progressSending.Visible = false;
                btnGuiMa.Enabled = true;
            }

            if (string.IsNullOrWhiteSpace(response))
            {
                MessageBox.Show("Không nhận được phản hồi từ server.");
                return;
            }

            try
            {
                using (JsonDocument doc = JsonDocument.Parse(response))
                {
                    JsonElement root = doc.RootElement;
                    bool success = root.GetProperty("success").GetBoolean();
                    string message = root.GetProperty("message").GetString();
                    MessageBox.Show(message);

                    if (success)
                    {
                        lblHuongDan.Text = "Mã xác nhận đã được gửi! Nhập mã 6 số ở dưới rồi nhấn Tiếp tục.";
                        btnXacNhan.Enabled = true;
                        this.AcceptButton = btnXacNhan;
                        txtMaXacNhan.Focus();
                    }
                    else
                    {
                        btnXacNhan.Enabled = false;
                        this.AcceptButton = btnGuiMa;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xử lý phản hồi từ server.\n" + ex.Message);
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
                    this.Hide();
                    recovery.ShowDialog();
                    this.Close();
                }
            }
        }

        private void txtQuenMK_TextChanged(object sender, EventArgs e)
        {
            // Nếu user đổi email, quay lại trạng thái ban đầu
            btnXacNhan.Enabled = false;
            this.AcceptButton = btnGuiMa;
        }
    }
}