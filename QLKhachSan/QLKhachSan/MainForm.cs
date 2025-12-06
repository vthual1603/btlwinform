using QlKhachSan;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLKhachSan
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

        }


        private void MainForm_Load(object sender, EventArgs e)
        {

            OpenChildForm(new CustomerForm());
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private Form activeForm = null;


        public void OpenChildForm(Form childForm)
        {

            if (activeForm != null)
            {
                activeForm.Close();
            }

            activeForm = childForm;

            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;


            pnlContent.Controls.Add(childForm);
            pnlContent.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void ClickCustomer_Click(object sender, EventArgs e)
        {

            OpenChildForm(new CustomerForm());
        }


        private void phongToolStripMenuItem_Click(object sender, EventArgs e)
        {

            OpenChildForm(new PhongForm());
        }

        private void ServiceToolStripMenuItem_Click(object sender, EventArgs e)
        {

            OpenChildForm(new DichvuForm());
        }

        private void ThanhtoanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ThanhtoanForm());
        }

        private void dichvuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Dichvu());
        }

        private void đặtPhòngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new DatPhongForm());
        }
    }
}