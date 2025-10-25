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
    public partial class frmLogin : Form
    {
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
            if (txtTK.Text == "admin" && txtMK.Text == "admin")
            {
                MessageBox.Show("Đăng nhập thành công");
                this.Hide();
                frmDashboard frm = new frmDashboard();
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu");
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

        private void pnlRight_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
