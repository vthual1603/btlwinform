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

            childForm.TopLevel = false; // Quan trọng: Không phải là form cấp cao nhất
            childForm.FormBorderStyle = FormBorderStyle.None; // Bỏ viền và thanh tiêu đề
            childForm.Dock = DockStyle.Fill; // Lấp đầy panel

            // Thêm form con vào danh sách controls của panel và hiển thị
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
          
            OpenChildForm(new PhongCRUD());
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
            OpenChildForm(new CRUDDichVu());
        }

        private void đặtPhòngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new BookingDesk());
        }
    }
}