using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductProject
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmProducts MyProduct = new frmProducts();
            MyProduct.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmUsers MyUser = new frmUsers();
            MyUser.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmLogin myLog = new frmLogin();
            myLog.Show();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }
    }
}
