using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace QLKhachSan
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            string username = txtusername.Text.Trim();
            string password = txtpassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Khởi tạo đối tượng CRUD
            CRUDKhachHang crud = new CRUDKhachHang();

            try
            {
                // 3. Gọi hàm kiểm tra đăng nhập từ lớp CRUD
                bool dangNhapThanhCong = crud.KiemTraLogin(username, password);

                // 4. Kiểm tra kết quả
                if (dangNhapThanhCong)
                {
                    // Đăng nhập thành công
                    MainForm frmMain = new MainForm();
                    frmMain.Show();
                    this.Hide();
                }
                else
                {
                    // Đăng nhập thất bại
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Bắt và hiển thị lỗi nếu có vấn đề về kết nối hoặc SQL
                MessageBox.Show("Đã xảy ra lỗi kết nối với cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void chkHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHienMatKhau.Checked)
            {
                
                txtpassword.PasswordChar = '\0'; 
            }
            else
            {
                
                txtpassword.PasswordChar = '*';
            }
        }

      
    }
}