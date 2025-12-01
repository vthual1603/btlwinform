using QlKhachSan;
using System;
using System.Data;
using System.Windows.Forms;

namespace QLKhachSan
{
    public partial class BookingDesk : Form
    {
        public BookingDesk()
        {
            InitializeComponent();
            this.Load += BookingDesk_Load;
        }

        //================ FORM LOAD ================

        private void BookingDesk_Load(object sender, EventArgs e)
        {
            string err;
            if (!BookingConnection.TestConnection(out err))
            {
                MessageBox.Show("Không kết nối được CSDL: " + err,
                    "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);

                btnThem.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;

                return;
            }

            LoadPhongToGrid();
            LoadDatPhongToGrid();
        }

        //================ LOAD DATA GRID ================

        private void LoadPhongToGrid()
        {
            try
            {
                DataTable dt = BookingConnection.GetAvailableRooms();
                dgvPhong.DataSource = dt;

                dgvPhong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvPhong.ReadOnly = true;
                dgvPhong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvPhong.MultiSelect = false;

                if (dgvPhong.Columns["MaPhong"] != null) dgvPhong.Columns["MaPhong"].HeaderText = "Mã phòng";
                if (dgvPhong.Columns["LoaiPhong"] != null) dgvPhong.Columns["LoaiPhong"].HeaderText = "Loại phòng";
                if (dgvPhong.Columns["DienTich"] != null) dgvPhong.Columns["DienTich"].HeaderText = "Diện tích";
                if (dgvPhong.Columns["TinhTrang"] != null) dgvPhong.Columns["TinhTrang"].HeaderText = "Tình trạng";
                if (dgvPhong.Columns["GiaPhong"] != null) dgvPhong.Columns["GiaPhong"].HeaderText = "Giá phòng";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDatPhongToGrid()
        {
            try
            {
                DataTable dt = BookingConnection.GetAllBookings();
                dgvDatPhong.DataSource = dt;

                dgvDatPhong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvDatPhong.ReadOnly = true;
                dgvDatPhong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvDatPhong.MultiSelect = false;

                if (dgvDatPhong.Columns["MaHD"] != null)
                {
                    dgvDatPhong.Columns["MaHD"].HeaderText = "Mã HĐ";
                    // Có thể ẩn hoặc hiện tùy bạn, hiện lên để dễ test
                    // dgvDatPhong.Columns["MaHD"].Visible = false; 
                }
                if (dgvDatPhong.Columns["MaKH"] != null) dgvDatPhong.Columns["MaKH"].HeaderText = "Mã khách hàng";
                if (dgvDatPhong.Columns["MaPhong"] != null) dgvDatPhong.Columns["MaPhong"].HeaderText = "Mã phòng";
                if (dgvDatPhong.Columns["NgayNhan"] != null) dgvDatPhong.Columns["NgayNhan"].HeaderText = "Ngày nhận";
                if (dgvDatPhong.Columns["NgayTra"] != null) dgvDatPhong.Columns["NgayTra"].HeaderText = "Ngày trả";
                if (dgvDatPhong.Columns["HoaDon"] != null) dgvDatPhong.Columns["HoaDon"].HeaderText = "Hóa đơn";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách đặt phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //================ HÀM THU THẬP & KIỂM TRA DỮ LIỆU ================

        private bool ThuThapDuLieu(
            out string maHD,
            out string maKH,
            out string maPhong,
            out DateTime ngayNhan,
            out DateTime ngayTra)
        {
            maHD = (txtMaHoaDon.Text ?? "").Trim();
            maKH = (txtMaKH.Text ?? "").Trim();
            maPhong = (txtMaPhong.Text ?? "").Trim();
            ngayNhan = dtpNgayNhan.Value.Date;
            ngayTra = dtpNgayTra.Value.Date;

            // Kiểm tra khách hàng
            if (string.IsNullOrEmpty(maKH))
            {
                MessageBox.Show("Vui lòng nhập Mã khách hàng.", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Kiểm tra phòng
            if (string.IsNullOrEmpty(maPhong))
            {
                MessageBox.Show("Vui lòng nhập Mã phòng.", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Kiểm tra ngày
            if (ngayTra < ngayNhan)
            {
                MessageBox.Show("Ngày trả phải lớn hơn hoặc bằng Ngày nhận.", "Dữ liệu sai", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Kiểm tra tồn tại khách hàng trong CSDL
            if (!BookingConnection.CustomerExists(maKH))
            {
                MessageBox.Show("Mã khách hàng không tồn tại trong bảng KHACHHANG.", "Sai dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void ClearInputs()
        {
            txtMaHoaDon.Clear();
            txtMaKH.Clear();
            txtMaPhong.Clear();
            dtpNgayNhan.Value = DateTime.Today;
            dtpNgayTra.Value = DateTime.Today;
        }

        //================ NÚT THÊM ================
        // Logic: Dùng BookingConnection.ThemDatPhong
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ThuThapDuLieu(out string maHD, out string maKH, out string maPhong,
                               out DateTime ngayNhan, out DateTime ngayTra))
                return;

            // Nếu chưa có mã hóa đơn, tự tạo mã mới
            if (string.IsNullOrEmpty(maHD))
                maHD = BookingConnection.GenerateNewMaHoaDon();

            try
            {
                // GỌI HÀM THÊM MỚI
                BookingConnection.ThemDatPhong(maHD, maKH, maPhong, ngayNhan, ngayTra);

                // Đổ lại mã HD vừa tạo lên textbox để người dùng biết
                txtMaHoaDon.Text = maHD;

                // Tải lại dữ liệu
                LoadPhongToGrid();
                LoadDatPhongToGrid();

                MessageBox.Show("Đã thêm đặt phòng thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm đặt phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //================ NÚT SỬA ================
        // Logic: Dùng BookingConnection.SuaDatPhong
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!ThuThapDuLieu(out string maHD, out string maKH, out string maPhong,
                               out DateTime ngayNhan, out DateTime ngayTra))
                return;

            // Sửa bắt buộc phải có Mã hóa đơn
            if (string.IsNullOrEmpty(maHD))
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần sửa (hoặc nhập Mã hóa đơn).", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult ret = MessageBox.Show(
               $"Bạn có muốn cập nhật ngày/giá cho phòng {maPhong} thuộc hóa đơn {maHD} không?",
               "Xác nhận sửa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ret != DialogResult.Yes) return;

            try
            {
                // GỌI HÀM SỬA
                // Lưu ý: Hàm SuaDatPhong KHÔNG cần makh, vì nó chỉ update ngày và tiền dựa trên mahd và maph
                BookingConnection.SuaDatPhong(maHD, maPhong, ngayNhan, ngayTra);

                LoadPhongToGrid();
                LoadDatPhongToGrid();

                MessageBox.Show("Đã cập nhật thông tin đặt phòng.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa đặt phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //================ NÚT XÓA ================
        // Logic: Dùng BookingConnection.XoaDatPhong (Giữ nguyên)
        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maHD = (txtMaHoaDon.Text ?? "").Trim();
            string maPhong = (txtMaPhong.Text ?? "").Trim();

            if (string.IsNullOrEmpty(maHD) || string.IsNullOrEmpty(maPhong))
            {
                MessageBox.Show("Vui lòng chọn dòng cần xóa ở bảng bên phải hoặc nhập đủ Mã hóa đơn và Mã phòng.",
                    "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult ret = MessageBox.Show(
                $"Bạn có chắc muốn hủy đặt phòng {maPhong} của hóa đơn {maHD}?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ret != DialogResult.Yes) return;

            try
            {
                // GỌI HÀM XÓA
                BookingConnection.XoaDatPhong(maHD, maPhong);

                ClearInputs();
                LoadPhongToGrid();
                LoadDatPhongToGrid();

                MessageBox.Show("Đã hủy đặt phòng thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa đặt phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //================ CHUYỂN FORM (THANH TOÁN / DỊCH VỤ) ================

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            // Giả sử nút Đăng ký dùng để chuyển sang Form Dịch vụ như code cũ
            ChuyenFormDichVu();
        }

        private void btnDichvu_Click(object sender, EventArgs e)
        {
            ChuyenFormDichVu();
        }

        private void ChuyenFormDichVu()
        {
            MainForm mainForm = this.ParentForm as MainForm;

            if (mainForm != null)
            {
                try
                {
                    mainForm.OpenChildForm(new DichvuForm());
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi mở form Dịch vụ: " + ex.Message, "Lỗi");
                }
            }
        }

        //================ SỰ KIỆN CLICK GRIDVIEW ================

        // Chọn phòng trống -> Đổ mã phòng lên TextBox
        private void dgvPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvPhong.Rows[e.RowIndex];
            object val = row.Cells["MaPhong"].Value;
            txtMaPhong.Text = val == null ? "" : val.ToString();
        }

        // Chọn đặt phòng -> Đổ dữ liệu lên TextBox để Sửa/Xóa
        private void dgvDatPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvDatPhong.Rows[e.RowIndex];

            if (row.Cells["MaHD"].Value != null)
                txtMaHoaDon.Text = row.Cells["MaHD"].Value.ToString();

            if (row.Cells["MaKH"].Value != null)
                txtMaKH.Text = row.Cells["MaKH"].Value.ToString();

            if (row.Cells["MaPhong"].Value != null)
                txtMaPhong.Text = row.Cells["MaPhong"].Value.ToString();

            if (row.Cells["NgayNhan"].Value != null &&
                DateTime.TryParse(row.Cells["NgayNhan"].Value.ToString(), out DateTime nn))
                dtpNgayNhan.Value = nn;

            if (row.Cells["NgayTra"].Value != null &&
                DateTime.TryParse(row.Cells["NgayTra"].Value.ToString(), out DateTime nt))
                dtpNgayTra.Value = nt;
        }

        //================ CÁC HANDLER THỪA (Do Designer tạo) ================
        private void lblMaKH_Click_1(object sender, EventArgs e) { }
        private void lblNgayNhan_Click_1(object sender, EventArgs e) { }
        private void dtpNgayNhan_ValueChanged_1(object sender, EventArgs e) { }
        private void lblMaHoaDon_Click(object sender, EventArgs e) { }
        private void textBox2_TextChanged_1(object sender, EventArgs e) { }
        private void lblMaPhong_Click_1(object sender, EventArgs e) { }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e) { }
    }
}