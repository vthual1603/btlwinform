using QlKhachSan;
using System;
using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;
using System.Drawing;

using System.Windows.Forms;
using static QLKhachSan.KhachHangconnection;

namespace QLKhachSan
{
    public partial class CustomerForm : Form
    {

        private KhachHangconnection crudkhachhang;
        public CustomerForm()
        {
            InitializeComponent();
            cmbGioiTinh.SelectedIndex = 0;

            crudkhachhang = new KhachHangconnection();
            try
            {
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }
        public void LoadData()
        {

            DataTable danhSachKhachHang = crudkhachhang.LayDanhSach();

            dataGridView1.DataSource = danhSachKhachHang;
        }


        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(txtmakh.Text) || string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Mã khách hàng và Họ tên không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                KhachHang khachHangMoi = new KhachHang();
                khachHangMoi.MaKH = txtmakh.Text.Trim();
                khachHangMoi.HoTen = txtName.Text.Trim();
                khachHangMoi.NamSinh = dtpDate.Value;
                khachHangMoi.SoDienThoai = txtphone.Text.Trim();
                khachHangMoi.DiaChi = txtaddress.Text.Trim();
                khachHangMoi.GioiTinh = cmbGioiTinh.SelectedItem.ToString();

                if (crudkhachhang.Them(khachHangMoi))
                {
                    //MessageBox.Show("Thêm khách hàng thành công!");

                    LoadData();
                    ResetControls();
                }
                else
                {
                    MessageBox.Show("Thêm thất bại!");
                }

            }

            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601)
                {
                    MessageBox.Show("Mã khách hàng này đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Lỗi cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }





        private void ResetControls()
        {
            txtmakh.Clear();
            txtName.Clear();
            txtphone.Clear();
            txtaddress.Clear();
            dtpDate.Value = DateTime.Now;
            cmbGioiTinh.SelectedIndex = 0;

            txtmakh.Focus();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string maKH = txtmakh.Text.Trim();
            if (string.IsNullOrEmpty(maKH))
            {
                MessageBox.Show("Vui lòng nhập Mã khách hàng !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {

                KhachHang kh = crudkhachhang.TimKiem(maKH);


                if (kh == null)
                {
                    MessageBox.Show("Không tìm thấy khách hàng !", "Lỗi");
                    return;
                }

                if (!string.IsNullOrWhiteSpace(txtName.Text)) kh.HoTen = txtName.Text.Trim();
                if (!string.IsNullOrWhiteSpace(txtphone.Text)) kh.SoDienThoai = txtphone.Text.Trim();
                if (!string.IsNullOrWhiteSpace(txtaddress.Text)) kh.DiaChi = txtaddress.Text.Trim();
                if (dtpDate.Checked)
                {
                    kh.NamSinh = dtpDate.Value;
                }

                kh.GioiTinh = cmbGioiTinh.SelectedItem.ToString();

             
                if (crudkhachhang.Sua(kh))
                {
                    //MessageBox.Show("Cập nhật thông tin thành công!", "Thành công");
                    LoadData();
                    ResetControls();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại!", "Lỗi");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string maKH = txtmakh.Text.Trim();
            if (string.IsNullOrEmpty(maKH))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
          
                XoaTrangThai trangThai = crudkhachhang.Xoa(maKH);

                switch (trangThai)
                {
                    case XoaTrangThai.ThanhCong:
                        MessageBox.Show("Xóa khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        ResetControls();
                        break;

                    case XoaTrangThai.KhongTonTai:
                        MessageBox.Show("Khách hàng không tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case XoaTrangThai.CoDatPhong:
                        MessageBox.Show("Không thể xóa khách hàng này vì họ đã có hợp đồng đặt phòng!", "Lỗi Ràng Buộc", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        break;

                    default:
                        MessageBox.Show("Đã xảy ra lỗi trong quá trình xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // Lấy mã khách hàng từ textbox dùng để tìm kiếm
            string maKH = txtmakh.Text.Trim();
            if (string.IsNullOrEmpty(maKH))
            {
                MessageBox.Show("Vui lòng nhập Mã khách hàng cần tìm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
             
                KhachHang kh = crudkhachhang.TimKiem(maKH);

                if (kh != null) // Nếu tìm thấy khách hàng
                {
                   
                    List<KhachHang> ketQua = new List<KhachHang>();
                  
                    ketQua.Add(kh);

                 
                    dataGridView1.DataSource = ketQua;
                }
                else
                {
                    dataGridView1.DataSource = null; 
                    MessageBox.Show("Không tìm thấy khách hàng có mã này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefesh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
            if (e.RowIndex >= 0)
            {
                
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

               
                txtmakh.Text = row.Cells[0].Value?.ToString();

                txtName.Text = row.Cells[1].Value?.ToString();

                if (row.Cells[2].Value != null && row.Cells[2].Value != DBNull.Value)
                {
                    dtpDate.Value = Convert.ToDateTime(row.Cells[2].Value);
                }

               
                cmbGioiTinh.Text = row.Cells[3].Value?.ToString();

             
                txtaddress.Text = row.Cells[4].Value?.ToString();

        
                txtphone.Text = row.Cells[5].Value?.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm mainForm = this.ParentForm as MainForm;

            if (mainForm != null)
            {
                try
                {
                 
                    mainForm.OpenChildForm(new DatPhongForm());

                   
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi mở form : " + ex.Message, "Lỗi");
                }
            }
        }
    }
}
