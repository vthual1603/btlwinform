using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using QlKhachSan; 

namespace QLKhachSan
{
    public enum XoaTrangThaiDV
    {
        ThanhCong,
        KhongTonTai,
        CoSuDung
    }

    public partial class Dichvu : Form
    {
        public Dichvu()
        {
            InitializeComponent();
            TaiDanhSachDichVu();
        }

        private SqlConnection GetConnection()
        {
            
            return QlKhachSan.Connection.GetSqlConnection();
        }

        public DataTable LayDanhSach()
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = GetConnection())
            {
                try
                {
               
                    string query = "SELECT madv AS N'Mã DV', tendv AS N'Tên DV', donvitinh AS N'ĐVT', dongia AS N'Đơn Giá' FROM dichvu";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.Fill(dataTable);
                }
                catch (Exception ex) { MessageBox.Show("Lỗi lấy danh sách: " + ex.Message); }
            }
            return dataTable;
        }

      
        public DataTable LayThongTinMotDichVu(string maDV)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = GetConnection())
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM dichvu WHERE madv = @madv";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@madv", maDV);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);
                    }
                }
                catch { }
            }
            return dt;
        }

      
        public bool KiemTraCoTrongChiTietDichVu(string maDV)
        {
            using (SqlConnection connection = GetConnection())
            {
                try
                {
                    connection.Open();
                   
                    string query = "SELECT COUNT(*) FROM ctdichvu WHERE madv = @madv";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@madv", maDV);
                        int count = (int)cmd.ExecuteScalar();
                        return count > 0; 
                    }
                }
                catch { return true; }
            }
        }

      
        public bool InsertDichVu(string ma, string ten, string dvt, float gia)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO dichvu (madv, tendv, donvitinh, dongia) VALUES (@ma, @ten, @dvt, @gia)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ma", ma);
                    cmd.Parameters.AddWithValue("@ten", ten);
                    cmd.Parameters.AddWithValue("@dvt", dvt);
                    cmd.Parameters.AddWithValue("@gia", gia);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

     
        public bool UpdateDichVu(string ma, string ten, string dvt, float gia)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = "UPDATE dichvu SET tendv = @ten, donvitinh = @dvt, dongia = @gia WHERE madv = @ma";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ma", ma);
                    cmd.Parameters.AddWithValue("@ten", ten);
                    cmd.Parameters.AddWithValue("@dvt", dvt);
                    cmd.Parameters.AddWithValue("@gia", gia);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
     public bool DeleteDichVu(string ma)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM dichvu WHERE madv = @ma";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ma", ma);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public DataTable TimKiem(string tuKhoa)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = GetConnection())
            {
                string query = "SELECT madv AS N'Mã DV', tendv AS N'Tên DV', donvitinh AS N'ĐVT', dongia AS N'Đơn Giá' FROM dichvu WHERE madv LIKE @tk OR tendv LIKE @tk";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@tk", "%" + tuKhoa + "%");
                da.Fill(dt);
            }
            return dt;
        }


        private void TaiDanhSachDichVu()
        {
            dgvDichVu.DataSource = LayDanhSach();
        }

        private void ResetForm()
        {
            txtMaDV.Clear(); txtTenDV.Clear(); txtDVT.Clear(); txtDonGia.Clear();
            txtMaDV.Focus();
        }

      
        private void dgvDichVu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDichVu.Rows[e.RowIndex];
                if (row.Cells[0].Value != null)
                {
                    txtMaDV.Text = row.Cells[0].Value.ToString();
                    txtTenDV.Text = row.Cells[1].Value.ToString();
                    txtDVT.Text = row.Cells[2].Value.ToString();
                    txtDonGia.Text = row.Cells[3].Value.ToString();
                }
            }
        }


        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaDV.Text) || string.IsNullOrWhiteSpace(txtTenDV.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã và Tên dịch vụ!");
                return;
            }

  
            if (LayThongTinMotDichVu(txtMaDV.Text).Rows.Count > 0)
            {
                MessageBox.Show("Mã dịch vụ này đã tồn tại!");
                return;
            }

            float gia = 0;

         
            bool isNumber = float.TryParse(txtDonGia.Text, out gia);
            if (!isNumber || gia <= 0)
            {
                MessageBox.Show("Đơn giá phải là số và lớn hơn 0!");
                return;
            }
         

            try
            {
                if (InsertDichVu(txtMaDV.Text, txtTenDV.Text, txtDVT.Text, gia))
                {
                    MessageBox.Show("Thêm thành công!");
                    TaiDanhSachDichVu();
                    ResetForm();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi thêm: " + ex.Message); }
        }


        private void btnSua_Click(object sender, EventArgs e)
        {
            string maDV = txtMaDV.Text;
            if (string.IsNullOrWhiteSpace(maDV))
            {
                MessageBox.Show("Vui lòng nhập Mã Dịch Vụ cần sửa!");
                return;
            }

      
            DataTable dtCu = LayThongTinMotDichVu(maDV);
            if (dtCu.Rows.Count == 0)
            {
                MessageBox.Show("Mã dịch vụ không tồn tại để sửa!");
                return;
            }
            DataRow rowCu = dtCu.Rows[0];

      
            string tenMoi = string.IsNullOrWhiteSpace(txtTenDV.Text) ? rowCu["tendv"].ToString() : txtTenDV.Text;
            string dvtMoi = string.IsNullOrWhiteSpace(txtDVT.Text) ? rowCu["donvitinh"].ToString() : txtDVT.Text;

            float giaMoi;
            if (string.IsNullOrWhiteSpace(txtDonGia.Text))
            {
           
                float.TryParse(rowCu["dongia"].ToString(), out giaMoi);
            }
            else
            {
              
                if (!float.TryParse(txtDonGia.Text, out giaMoi) || giaMoi <= 0)
                {
                    MessageBox.Show("Đơn giá mới phải là số và lớn hơn 0!");
                    return;
                }
                
            }

            try
            {
                if (UpdateDichVu(maDV, tenMoi, dvtMoi, giaMoi))
                {
                    MessageBox.Show("Sửa thành công!");
                    TaiDanhSachDichVu();
                    ResetForm();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi sửa: " + ex.Message); }
        }

 
        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maDV = txtMaDV.Text;
            if (string.IsNullOrWhiteSpace(maDV))
            {
                MessageBox.Show("Vui lòng nhập Mã Dịch Vụ cần xóa!");
                return;
            }

          
            if (LayThongTinMotDichVu(maDV).Rows.Count == 0)
            {
                MessageBox.Show("Mã dịch vụ không tồn tại!");
                return;
            }

     
            if (KiemTraCoTrongChiTietDichVu(maDV))
            {
                MessageBox.Show("Không thể xóa! Dịch vụ này đang có trong Chi Tiết Sử Dụng Dịch Vụ (ctdichvu).", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

          
            if (MessageBox.Show("Bạn chắc chắn muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    if (DeleteDichVu(maDV))
                    {
                        MessageBox.Show("Xóa thành công!");
                        TaiDanhSachDichVu();
                        ResetForm();
                    }
                }
                catch (Exception ex) { MessageBox.Show("Lỗi xóa: " + ex.Message); }
            }
        }
    }
}