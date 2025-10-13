using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Giao_Diện_Đăng_Nhập_Cờ_Vua
{
    public partial class frmDashboard : Form
    {
        public frmDashboard()
        {
            InitializeComponent();
        }
        private bool isCollapsed = false; // Trạng thái thu gọn hoặc bung ra

        private void toolStripContainer1_TopToolStripPanel_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Thu gọn hoặc bung ra MenuStrip
            if (isCollapsed)
            {
                menuStrip1.Visible = true; // Hiện ra
                btnThuGon.Text = "Thu Gọn"; // Đổi tên nút
            }
            else
            {
                menuStrip1.Visible = false; // Thu gọn
                btnThuGon.Text = "Hiện Ra"; // Đổi tên nút
            }

            // Đổi trạng thái
            isCollapsed = !isCollapsed;
        }

        private void btnChoi_Click(object sender, EventArgs e)
        {

        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {

        }
    }
}
