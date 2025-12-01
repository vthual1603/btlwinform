// === DichvuForm.cs (FIXED FOR VIETNAMESE ALIASES) ===
using QlKhachSan;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace QLKhachSan
{
    public partial class DichvuForm : Form
    {
        private readonly Dichvuconnection dv = new Dichvuconnection();

        public DichvuForm()
        {
            InitializeComponent();
            this.Load += DichvuForm_Load;
        }

        private void DichvuForm_Load(object sender, EventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;

            // Wire events

            btnThem.Click += btnThem_Click;

            cboMaHD.SelectedIndexChanged += (s, ev) =>
            {
                // Lọc danh sách phòng theo HĐ
                var dtPhong = dv.GetMaPhongByMaHD(cboMaHD.SelectedValue?.ToString() ?? "");
                if (dtPhong != null && dtPhong.Rows.Count > 0)
                {
                    // Đảm bảo dùng TÊN CỘT CÓ DẤU [Mã Phòng]
                    cboMaPhong.DisplayMember = "Mã Phòng";
                    cboMaPhong.ValueMember = "Mã Phòng";
                    cboMaPhong.DataSource = dtPhong;
                }
                LoadCTDV();
            };
            cboMaPhong.SelectedIndexChanged += (s, ev) => LoadCTDV();

            // Click dòng bảng trái -> đổ mã DV vào textbox
            dgvDichVu.CellClick += (s, ev) =>
            {
                if (ev.RowIndex < 0) return;
                var row = dgvDichVu.Rows[ev.RowIndex];

                // Đảm bảo dùng TÊN CỘT CÓ DẤU [Mã DV]
                if (dgvDichVu.Columns.Contains("Mã DV"))
                    txtMaDV.Text = row.Cells["Mã DV"]?.Value?.ToString();
                else
                    txtMaDV.Text = row.Cells[0]?.Value?.ToString();
            };

            // Nạp nguồn combobox + danh mục dịch vụ
            LoadCombos();
            LoadDanhMucDichVu();
            LoadCTDV(); // nạp lưới phải lần đầu
        }

        // ===== COMBOS =====
        private void LoadCombos()
        {
            // Đảm bảo dùng TÊN CỘT CÓ DẤU [Mã HĐ]
            // Hóa đơn
            cboMaHD.DisplayMember = "Mã HĐ";
            cboMaHD.ValueMember = "Mã HĐ";
            cboMaHD.DataSource = dv.GetAllMaHD();

            // Đảm bảo dùng TÊN CỘT CÓ DẤU [Mã Phòng]
            // Phòng (ban đầu tất cả)
            cboMaPhong.DisplayMember = "Mã Phòng";
            cboMaPhong.ValueMember = "Mã Phòng";
            cboMaPhong.DataSource = dv.GetAllMaPhong();
        }

        // ===== BẢNG TRÁI =====
        private void LoadDanhMucDichVu()
        {
            dgvDichVu.AutoGenerateColumns = true;
            dgvDichVu.ReadOnly = true;
            dgvDichVu.AllowUserToAddRows = false;
            dgvDichVu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDichVu.DataSource = dv.LayDanhSach();
        }

        // ===== NÚT THÊM (UPSERT) (Giữ nguyên) =====
        private void btnThem_Click(object sender, EventArgs e)
        {
            var mahd = cboMaHD.SelectedValue?.ToString();
            var maph = cboMaPhong.SelectedValue?.ToString();
            var madv = txtMaDV.Text.Trim();

            if (string.IsNullOrWhiteSpace(mahd) || string.IsNullOrWhiteSpace(maph) || string.IsNullOrWhiteSpace(madv))
            {
                MessageBox.Show("Vui lòng chọn Dịch vụ và số lượng.");
                return;
            }
            if (!int.TryParse(txtSoLuong.Text.Trim(), out int sl) || sl <= 0)
            {
                MessageBox.Show("Số lượng phải là số nguyên dương.");
                return;
            }

            try
            {
                // Insert nếu chưa có, nếu trùng khóa thì Update
                try { dv.InsertCTDV(mahd, maph, madv, sl); }
                catch (System.Data.SqlClient.SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
                { dv.UpdateCTDV(mahd, maph, madv, sl); }

                LoadCTDV(); // 🔁 nạp lại toàn bộ danh sách của (HĐ, Phòng)
                MessageBox.Show("Đã thêm/cập nhật dịch vụ.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm dịch vụ: " + ex.Message);
            }
        }

        // ===== NÚT XÁC NHẬN: cập nhật số lượng (Giữ nguyên) =====


        // ===== LƯỚI PHẢI: DỊCH VỤ ĐÃ ĐẶT (lọc theo (HĐ, Phòng)) (Giữ nguyên) =====
        private void LoadCTDV()
        {
            var mahd = cboMaHD.SelectedValue?.ToString();
            var maph = cboMaPhong.SelectedValue?.ToString();
            if (string.IsNullOrWhiteSpace(mahd) || string.IsNullOrWhiteSpace(maph)) return;

            var dt = dv.GetCTDichVu(mahd, maph); // chỉ phòng đang chọn
            txtDvDaDat.AutoGenerateColumns = true;
            txtDvDaDat.ReadOnly = true;
            txtDvDaDat.AllowUserToAddRows = false;
            txtDvDaDat.DataSource = dt;
        }

        private void DichvuForm_Load_1(object sender, EventArgs e)
        {

        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            MainForm mainForm = this.ParentForm as MainForm;

            if (mainForm != null)
            {
                try
                {
                    // 2. Gọi hàm OpenChildForm của MainForm để mở ThanhToanForm
                    // Dùng constructor mặc định: new ThanhToanForm()
                    mainForm.OpenChildForm(new ThanhtoanForm());

                    // 3. Đóng Form Dịch Vụ hiện tại (tùy chọn, nếu muốn thay thế hẳn Form hiện tại)
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi mở form Thanh toán: " + ex.Message, "Lỗi");
                }
            }
        }
    }
}