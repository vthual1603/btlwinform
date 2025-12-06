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

            
            btnThem.Click += btnThem_Click;

            cboMaHD.SelectedIndexChanged += (s, ev) =>
            {
               
                cboMaPhong.DataSource = null;
                cboMaPhong.Items.Clear();

                string maHD = cboMaHD.SelectedValue?.ToString();
                if (!string.IsNullOrEmpty(maHD))
                {
                   
                    var dtPhong = dv.GetMaPhongByMaHD(maHD);

                    if (dtPhong != null && dtPhong.Rows.Count > 0)
                    {
                        cboMaPhong.DisplayMember = "Mã Phòng";
                        cboMaPhong.ValueMember = "Mã Phòng";
                        cboMaPhong.DataSource = dtPhong;

                        cboMaPhong.SelectedIndex = 0;
                    }
                }
               
                LoadCTDV();
            };

        
            cboMaPhong.SelectedIndexChanged += (s, ev) => LoadCTDV();

      
            dgvDichVu.CellClick += (s, ev) =>
            {
                if (ev.RowIndex < 0) return;
                var row = dgvDichVu.Rows[ev.RowIndex];

               
                if (dgvDichVu.Columns.Contains("Mã DV"))
                    txtMaDV.Text = row.Cells["Mã DV"]?.Value?.ToString();
                else
                    txtMaDV.Text = row.Cells[0]?.Value?.ToString();
            };

        
            LoadCombos();           // Chỉ nạp Hợp đồng, Phòng sẽ tự nạp theo sự kiện SelectedIndexChanged của HĐ
            LoadDanhMucDichVu();    // Nạp bảng danh sách dịch vụ (trái)
            LoadCTDV();             // Nạp bảng chi tiết (phải)
        }

     
        private void LoadCombos()
        {
            
            cboMaHD.DisplayMember = "Mã HĐ";
            cboMaHD.ValueMember = "Mã HĐ";
            cboMaHD.DataSource = dv.GetAllMaHD();
        }

  
        private void LoadDanhMucDichVu()
        {
            dgvDichVu.AutoGenerateColumns = true;
            dgvDichVu.ReadOnly = true;
            dgvDichVu.AllowUserToAddRows = false;
            dgvDichVu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDichVu.DataSource = dv.LayDanhSach();
        }


        private void LoadCTDV()
        {
            var mahd = cboMaHD.SelectedValue?.ToString();
           
            var maph = cboMaPhong.SelectedValue?.ToString();

    
            if (string.IsNullOrWhiteSpace(mahd) || string.IsNullOrWhiteSpace(maph))
            {
                txtDvDaDat.DataSource = null;
                return;
            }

            var dt = dv.GetCTDichVu(mahd, maph);
            txtDvDaDat.AutoGenerateColumns = true;
            txtDvDaDat.ReadOnly = true;
            txtDvDaDat.AllowUserToAddRows = false;
            txtDvDaDat.DataSource = dt;
        }


        private void btnThem_Click(object sender, EventArgs e)
        {
            var mahd = cboMaHD.SelectedValue?.ToString();
            var maph = cboMaPhong.SelectedValue?.ToString();
            var madv = txtMaDV.Text.Trim();

            if (string.IsNullOrWhiteSpace(mahd) || string.IsNullOrWhiteSpace(maph) || string.IsNullOrWhiteSpace(madv))
            {
                MessageBox.Show("Vui lòng chọn Hợp đồng, Phòng và Dịch vụ.");
                return;
            }

            if (!int.TryParse(txtSoLuong.Text.Trim(), out int sl) || sl <= 0)
            {
                MessageBox.Show("Số lượng phải là số nguyên dương.");
                return;
            }

            try
            {
                try
                {
                    dv.InsertCTDV(mahd, maph, madv, sl);
                }
                catch (System.Data.SqlClient.SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
                {
                    dv.UpdateCTDV(mahd, maph, madv, sl);
                }

                LoadCTDV(); 
                MessageBox.Show("Đã cập nhật dịch vụ thành công.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message);
            }
        }

     
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            MainForm mainForm = this.ParentForm as MainForm;

            if (mainForm != null)
            {
                try
                {
                    mainForm.OpenChildForm(new ThanhtoanForm());
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