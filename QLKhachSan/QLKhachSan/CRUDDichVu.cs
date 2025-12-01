using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using QlKhachSan; // Để sử dụng Connection.cs

namespace QLKhachSan
{
    public enum XoaTrangThaiDV
    {
        ThanhCong,
        KhongTonTai,
        CoSuDung
    }

    public partial class CRUDDichVu : Form
    {
        // Giả định InitializeComponent() và các controls (txtMaDV, txtTenDV, txtDVT, txtDonGia, dgvDichVu) đã tồn tại
        public CRUDDichVu()
        {
            InitializeComponent();
            TaiDanhSachDichVu();
        }

        private SqlConnection GetConnection()
        {
            // Thay thế bằng cách gọi class Connection thực tế của bạn
            // Ví dụ: return Connection.GetSqlConnection();
            // TẠM THỜI GIẢ ĐỊNH có class Connection và hàm GetSqlConnection()
            return QlKhachSan.Connection.GetSqlConnection();
        }

        // ==================== 1. CÁC HÀM HỖ TRỢ (DAO) ====================

        // Lấy danh sách hiển thị lên DataGridView
        public DataTable LayDanhSach()
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = GetConnection())
            {
                try
                {
                    // Query theo file SQL: madv, tendv, donvitinh, dongia
                    string query = "SELECT madv AS N'Mã DV', tendv AS N'Tên DV', donvitinh AS N'ĐVT', dongia AS N'Đơn Giá' FROM dichvu";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.Fill(dataTable);
                }
                catch (Exception ex) { MessageBox.Show("Lỗi lấy danh sách: " + ex.Message); }
            }
            return dataTable;
        }

        // Lấy thông tin chi tiết của 1 dịch vụ (Để phục vụ logic Sửa: lấy dữ liệu cũ)
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

        // Kiểm tra xem dịch vụ có tồn tại trong bảng ctdichvu không (Logic Xóa)
        public bool KiemTraCoTrongChiTietDichVu(string maDV)
        {
            using (SqlConnection connection = GetConnection())
            {
                try
                {
                    connection.Open();
                    // Theo file SQL: bảng liên kết là 'ctdichvu'
                    string query = "SELECT COUNT(*) FROM ctdichvu WHERE madv = @madv";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@madv", maDV);
                        int count = (int)cmd.ExecuteScalar();
                        return count > 0; // Trả về true nếu đã có trong chi tiết (không được xóa)
                    }
                }
                catch { return true; } // Nếu lỗi, chặn xóa cho an toàn
            }
        }

        // Hàm thực hiện câu lệnh INSERT
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

        // Hàm thực hiện câu lệnh UPDATE
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

        // Hàm thực hiện câu lệnh DELETE
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

        // ==================== 2. XỬ LÝ GIAO DIỆN (LOGIC CHÍNH) ====================

        private void TaiDanhSachDichVu()
        {
            dgvDichVu.DataSource = LayDanhSach();
        }

        private void ResetForm()
        {
            txtMaDV.Clear(); txtTenDV.Clear(); txtDVT.Clear(); txtDonGia.Clear();
            txtMaDV.Focus();
        }

        // Click bảng đổ dữ liệu lên text box (để tiện sửa/xóa)
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

        // NÚT THÊM (Đã thêm kiểm tra DonGia > 0)
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaDV.Text) || string.IsNullOrWhiteSpace(txtTenDV.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã và Tên dịch vụ!");
                return;
            }

            // Kiểm tra trùng mã
            if (LayThongTinMotDichVu(txtMaDV.Text).Rows.Count > 0)
            {
                MessageBox.Show("Mã dịch vụ này đã tồn tại!");
                return;
            }

            float gia = 0;

            // --- LOGIC KIỂM TRA ĐƠN GIÁ > 0 ---
            bool isNumber = float.TryParse(txtDonGia.Text, out gia);
            if (!isNumber || gia <= 0)
            {
                MessageBox.Show("Đơn giá phải là số và lớn hơn 0!");
                return;
            }
            // ----------------------------------

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

        // NÚT SỬA (LOGIC: Giữ nguyên giá trị cũ nếu để trống - Đã thêm kiểm tra DonGia > 0)
        private void btnSua_Click(object sender, EventArgs e)
        {
            string maDV = txtMaDV.Text;
            if (string.IsNullOrWhiteSpace(maDV))
            {
                MessageBox.Show("Vui lòng nhập Mã Dịch Vụ cần sửa!");
                return;
            }

            // 1. Lấy dữ liệu CŨ từ database
            DataTable dtCu = LayThongTinMotDichVu(maDV);
            if (dtCu.Rows.Count == 0)
            {
                MessageBox.Show("Mã dịch vụ không tồn tại để sửa!");
                return;
            }
            DataRow rowCu = dtCu.Rows[0];

            // 2. So sánh và gán giá trị MỚI
            // Nếu Textbox trống -> Lấy giá trị Cũ. Nếu có chữ -> Lấy giá trị Mới.

            string tenMoi = string.IsNullOrWhiteSpace(txtTenDV.Text) ? rowCu["tendv"].ToString() : txtTenDV.Text;
            string dvtMoi = string.IsNullOrWhiteSpace(txtDVT.Text) ? rowCu["donvitinh"].ToString() : txtDVT.Text;

            float giaMoi;
            if (string.IsNullOrWhiteSpace(txtDonGia.Text))
            {
                // Nếu không nhập giá, lấy giá cũ
                float.TryParse(rowCu["dongia"].ToString(), out giaMoi);
            }
            else
            {
                // --- LOGIC KIỂM TRA GIÁ MỚI > 0 ---
                if (!float.TryParse(txtDonGia.Text, out giaMoi) || giaMoi <= 0)
                {
                    MessageBox.Show("Đơn giá mới phải là số và lớn hơn 0!");
                    return;
                }
                // ----------------------------------
            }

            // 3. Gọi lệnh Update
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

        // NÚT XÓA (LOGIC: Kiểm tra trong bảng ctdichvu)
        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maDV = txtMaDV.Text;
            if (string.IsNullOrWhiteSpace(maDV))
            {
                MessageBox.Show("Vui lòng nhập Mã Dịch Vụ cần xóa!");
                return;
            }

            // 1. Kiểm tra sự tồn tại của Mã
            if (LayThongTinMotDichVu(maDV).Rows.Count == 0)
            {
                MessageBox.Show("Mã dịch vụ không tồn tại!");
                return;
            }

            // 2. Kiểm tra ràng buộc trong ctdichvu
            if (KiemTraCoTrongChiTietDichVu(maDV))
            {
                MessageBox.Show("Không thể xóa! Dịch vụ này đang có trong Chi Tiết Sử Dụng Dịch Vụ (ctdichvu).", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Thực hiện xóa
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