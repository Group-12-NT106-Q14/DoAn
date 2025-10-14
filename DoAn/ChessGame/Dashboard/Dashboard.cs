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
        public frmDashboard()
        {
            InitializeComponent();
        }

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



        private void btnChoi_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Match().ShowDialog();
        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {

        }

        private void btnMay_Click(object sender, EventArgs e)
        {
            this.Hide();
            new AI().Show();
        }

        private void btnBan_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Room().Show();
        }

        private void btnBXH_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Ranking().Show();
        }

        private void btnLichSu_Click(object sender, EventArgs e)
        {
            this.Hide();
            new History().Show();
        }

        private void btnĐX_Click(object sender, EventArgs e)
        {

        }

        private void btnCaiDat_Click(object sender, EventArgs e)
        {
            this.Hide();
            new AccountSetting().Show();
        }
    }
}
