using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessGame
{
    public partial class frmRegister : Form
    {
        private TCPClient tcpClient;
        public frmRegister()
        {
            InitializeComponent();
            this.AcceptButton = btnĐK;
        }
        private bool IsValidEmail(string email)
        {
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }
        private void btnĐK_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (txtTK.Text == "")
            {
                errorProvider1.SetError(txtTK, "Vui lòng nhập tài khoản");
            }
            if (txtEmail.Text == "")
            {
                errorProvider1.SetError(txtEmail, "Vui lòng nhập email");
            }
            if (!IsValidEmail(txtEmail.Text))
            {
                errorProvider1.SetError(txtEmail, "Email không hợp lệ");
            }
            if (txtMK.Text == "")
            {
                errorProvider1.SetError(txtMK, "Vui lòng nhập mật khẩu");
            }
            if (txtNLMK.Text == "")
            {
                errorProvider1.SetError(txtNLMK, "Vui lòng nhập lại mật khẩu");
            }
            if (txtMK.Text.Length < 8)
            {
                errorProvider1.SetError(txtMK, "Mật khẩu phải lớn hơn 8 ký tự");
            }
            if (txtMK.Text != txtNLMK.Text)
            {
                errorProvider1.SetError(txtNLMK, "Mật khẩu không khớp");
            }
            if (txtTK.Text != "" && txtMK.Text != "" && txtNLMK.Text != "" &&
                txtMK.Text.Length >= 8 && txtMK.Text == txtNLMK.Text &&
                IsValidEmail(txtEmail.Text))
            {
                try
                {
                    tcpClient = new TCPClient();
                    tcpClient.Connect();
                    var registerRequest = new
                    {
                        action = "REGISTER",
                        email = txtEmail.Text,
                        displayName = txtTenHienThi.Text,
                        username = txtTK.Text,
                        password = txtMK.Text
                    };
                    string responseJson = tcpClient.SendRequest(registerRequest);
                    using (JsonDocument doc = JsonDocument.Parse(responseJson))
                    {
                        JsonElement root = doc.RootElement;
                        bool success = root.GetProperty("success").GetBoolean();
                        string message = root.GetProperty("message").GetString();
                        if (success)
                        {
                            MessageBox.Show("Đăng ký thành công");
                            this.Close();
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
                    MessageBox.Show($"Lỗi: Server chưa được bật hoặc bảo trì.");
                }
            }
            else
            {
                MessageBox.Show("Đăng ký thất bại");
            }
        }
        private void btnShow1_Click(object sender, EventArgs e)
        {
            txtMK.UseSystemPasswordChar = false;
            btnShow1.Visible = false;
            btnHide1.Visible = true;
        }
        private void btnHide1_Click(object sender, EventArgs e)
        {
            txtMK.UseSystemPasswordChar = true;
            btnHide1.Visible = false;
            btnShow1.Visible = true;
        }
        private void btnShow2_Click(object sender, EventArgs e)
        {
            txtNLMK.UseSystemPasswordChar = false;
            btnShow2.Visible = false;
            btnHide2.Visible = true;
        }
        private void btnHide2_Click(object sender, EventArgs e)
        {
            txtNLMK.UseSystemPasswordChar = true;
            btnHide2.Visible = false;
            btnShow2.Visible = true;
        }
        private void frmDK_Load(object sender, EventArgs e)
        {
            txtMK.UseSystemPasswordChar = true;
            txtNLMK.UseSystemPasswordChar = true;
            btnShow1.Visible = true;
            btnHide1.Visible = false;
            btnShow2.Visible = true;
            btnHide2.Visible = false;
        }
        private void btnĐN_Click(object sender, EventArgs e)
        {
            this.Hide();
            //new frmLogin().ShowDialog();
        }
    }
}
