using System;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ChessGame
{
    public partial class AccountSetting : Form
    {
        public int UserId;
        public string CurrentDisplayName;
        public string CurrentEmail;
        public bool IsUpdated = false;
        public AccountSetting()
        {
            InitializeComponent();
            this.Load += AccountSetting_Load;
            btnShowMKMoi.Click += btnShowMKMoi_Click;
            btnShowNLMK.Click += btnShowNLMK_Click;
        }

        private void AccountSetting_Load(object sender, EventArgs e)
        {
            txtUsername.Text = CurrentDisplayName;
            txtEmail.Text = CurrentEmail;
            txtMatKhauMoi.Text = "";
            txtNhapLaiMK.Text = "";
        }

        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Tên hiển thị không được để trống!");
                return;
            }
            if (!IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Email không hợp lệ!");
                return;
            }
            bool passwordChanged = !string.IsNullOrWhiteSpace(txtMatKhauMoi.Text);
            if (passwordChanged)
            {
                if (txtMatKhauMoi.Text.Length < 8)
                {
                    MessageBox.Show("Mật khẩu phải có ít nhất 8 ký tự!");
                    return;
                }
                if (txtMatKhauMoi.Text != txtNhapLaiMK.Text)
                {
                    MessageBox.Show("Mật khẩu không khớp!");
                    return;
                }
            }
            TCPClient tcpClient = new TCPClient();
            tcpClient.Connect();
            object updateRequest;
            if (passwordChanged)
            {
                updateRequest = new
                {
                    action = "UPDATE_ACCOUNT",
                    userId = UserId,
                    displayName = txtUsername.Text,
                    email = txtEmail.Text,
                    password = txtMatKhauMoi.Text
                };
            }
            else
            {
                updateRequest = new
                {
                    action = "UPDATE_ACCOUNT",
                    userId = UserId,
                    displayName = txtUsername.Text,
                    email = txtEmail.Text
                };
            }
            string response = tcpClient.SendRequest(updateRequest);
            tcpClient.Disconnect();
            CurrentDisplayName = txtUsername.Text;
            CurrentEmail = txtEmail.Text;
            IsUpdated = true;
            MessageBox.Show("Cập nhật thành công!");
            this.Close();
        }
        private void btnShowMKMoi_Click(object sender, EventArgs e)
        {
            txtMatKhauMoi.UseSystemPasswordChar = !txtMatKhauMoi.UseSystemPasswordChar;
        }
        private void btnShowNLMK_Click(object sender, EventArgs e)
        {
            txtNhapLaiMK.UseSystemPasswordChar = !txtNhapLaiMK.UseSystemPasswordChar;
        }
        private void txtUsername_TextChanged(object sender, EventArgs e) 
        { 

        }
        private void txtEmail_TextChanged(object sender, EventArgs e) 
        {

        }
        private void txtMatKhauMoi_TextChanged(object sender, EventArgs e) 
        {

        }
        private void txtNhapLaiMK_TextChanged(object sender, EventArgs e) 
        { 

        }
    }
}
