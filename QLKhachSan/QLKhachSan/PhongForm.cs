using QlKhachSan;
using System;
using System.Data;
using System.Windows.Forms;

namespace QLKhachSan
{
    public partial class PhongForm : Form
    {
        private readonly Connection pc = new Connection();

        public PhongForm()
        {
            InitializeComponent();
        }

        private void PhongCRUD_Load(object sender, EventArgs e)
        {
            LoadDuLieuPhong();
            LoadComboBoxMaPhong();
            LoadComboBoxTinhTrang();
            ClearInputs();
        }


        private void LoadDuLieuPhong()
        {
            try
            {
                string query = @"SELECT 
                    maph       AS [Mã phòng],
                    loaiph     AS [Loại phòng],
                    dientich   AS [Diện tích],
                    tinhtrang  AS [Tình trạng],
                    giaph AS [Đơn giá]
                    FROM phong 
                    ORDER BY maph";

                dataGridView1.DataSource = pc.Table(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LoadComboBoxMaPhong()
        {
            cbMaPhong.Items.Clear();
            string[] dsMa = {
                "P101","P102","P103","P104","P105",
                "P201","P202","P203","P204","P205",
                "P301","P302","P303","P304","P305",
                "P401","P402","P403","P404","P405",
                "P501","P502","P503","P504","P505"
            };

            using (var conn = Connection.GetSqlConnection())
            {
                conn.Open();
                foreach (string ma in dsMa)
                {
                    string sql = "SELECT COUNT(*) FROM phong WHERE maph = @maph";
                    using (var cmd = new System.Data.SqlClient.SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maph", ma);
                        if ((int)cmd.ExecuteScalar() == 0)
                            cbMaPhong.Items.Add(ma);
                    }
                }
            }
            if (cbMaPhong.Items.Count > 0) cbMaPhong.SelectedIndex = 0;
        }


        private void LoadComboBoxTinhTrang()
        {
            cbTinhTrang.Items.Clear();
            cbTinhTrang.Items.AddRange(new object[] { "Còn", "Hết" });
        }


        private void cbMaPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMaPhong.SelectedItem == null) return;

            string ma = cbMaPhong.SelectedItem.ToString();

            if (ma.StartsWith("P1"))
            {
                txtLoaiPhong.Text = "Đơn";
                txtDienTich.Text = "25";
                txtDonGia.Text = "250000";
            }
            else if (ma.StartsWith("P2") || ma.StartsWith("P5"))
            {
                txtLoaiPhong.Text = "Đôi";
                txtDienTich.Text = "40";
                txtDonGia.Text = "400000";
            }
            else if (ma.StartsWith("P3") || ma.StartsWith("P4"))
            {
                txtLoaiPhong.Text = "VIP";
                txtDienTich.Text = "50";
                txtDonGia.Text = "600000";
            }


            cbTinhTrang.Text = "Còn";
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dataGridView1.Rows.Count - 1) return;

            DataGridViewRow r = dataGridView1.Rows[e.RowIndex];
            cbMaPhong.Text = r.Cells["Mã phòng"].Value.ToString();
            txtLoaiPhong.Text = r.Cells["Loại phòng"].Value.ToString();
            txtDienTich.Text = r.Cells["Diện tích"].Value.ToString();
            cbTinhTrang.Text = r.Cells["Tình trạng"].Value.ToString();

            string gia = r.Cells["Đơn giá"].Value.ToString();
            txtDonGia.Text = gia.Replace(",", "").Replace(".", "");

            cbMaPhong.Enabled = false;
        }


        private void ClearInputs()
        {
            cbMaPhong.Enabled = true;
            cbMaPhong.SelectedIndex = -1;
            txtLoaiPhong.Clear();
            txtDienTich.Clear();
            txtDonGia.Clear();
            cbTinhTrang.Text = "Còn";
        }


        private bool ValidateInputs(bool isAdding = true)
        {
            if (string.IsNullOrWhiteSpace(cbMaPhong.Text))
            {
                MessageBox.Show("Vui lòng chọn mã phòng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }


        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs(true)) return;

            string sql = $"INSERT INTO phong (maph, loaiph, dientich, tinhtrang, giaph) " +
                         $"VALUES ('{cbMaPhong.Text}', N'{txtLoaiPhong.Text}', {txtDienTich.Text}, N'Còn', {txtDonGia.Text})";

            {
                try
                {
                    pc.Command(sql);

                    LoadDuLieuPhong();
                    LoadComboBoxMaPhong();
                    ClearInputs();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi thêm phòng:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs(false)) return;

            string sql = $"UPDATE phong SET " +
                         $"loaiph = N'{txtLoaiPhong.Text}', " +
                         $"dientich = {txtDienTich.Text}, " +
                         $"tinhtrang = N'{cbTinhTrang.Text}', " +
                         $"giaph = {txtDonGia.Text} " +
                         $"WHERE maph = '{cbMaPhong.Text}'";

            if (MessageBox.Show("Cập nhật thông tin phòng?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    pc.Command(sql);

                    LoadDuLieuPhong();
                    ClearInputs();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn phòng cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maph = dataGridView1.SelectedRows[0].Cells["Mã phòng"].Value.ToString();

            if (MessageBox.Show($"Xóa phòng {maph}?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    pc.Command("DELETE FROM phong WHERE maph = '" + maph + "'");
                    MessageBox.Show("Xóa thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDuLieuPhong();
                    LoadComboBoxMaPhong();
                    ClearInputs();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể xóa (phòng đang được sử dụng?):\n" + ex.Message,
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tk = cbMaPhong.Text.Trim();
            string sql = string.IsNullOrEmpty(tk)
                ? "SELECT maph AS [Mã phòng], loaiph AS [Loại phòng], dientich AS [Diện tích], tinhtrang AS [Tình trạng], FORMAT(giaph,'N0') AS [Đơn giá] FROM phong ORDER BY maph"
                : $"SELECT maph AS [Mã phòng], loaiph AS [Loại phòng], dientich AS [Diện tích], tinhtrang AS [Tình trạng], FORMAT(giaph,'N0') AS [Đơn giá] FROM phong WHERE maph LIKE '%{tk}%' OR loaiph LIKE '%{tk}%' ORDER BY maph";

            dataGridView1.DataSource = pc.Table(sql);
        }
    }
}