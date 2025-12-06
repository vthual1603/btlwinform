using QLKhachSan;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QlKhachSan
{
    public partial class ThanhtoanForm : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=HUAL;Initial Catalog=qlkhachsan;Integrated Security=True;Encrypt=False;");

        public ThanhtoanForm()
        {
            InitializeComponent();

            this.Load += Form1_Load;

            button1.Click += Button1_Click;
            dataGridView1.CellClick += DataGridView1_CellClick;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadComboBox();
        }

        private void LoadComboBox()
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("--- ALL ---");

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT mahd FROM datphong", conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBox1.Items.Add(dr["mahd"].ToString());
                }
                dr.Close();
                conn.Close();

                comboBox1.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load ComboBox: " + ex.Message);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string mahd = comboBox1.Text.Trim();
            LoadHoaDon(mahd);
        }

        private void LoadHoaDon(string mahd = "")
        {
            string sql;
            if (mahd == "" || mahd == "--- ALL ---")
                sql = "SELECT mahd, makh, ngaybatdau, ngayketthuc FROM datphong";
            else
                sql = "SELECT mahd, makh, ngaybatdau, ngayketthuc FROM datphong WHERE mahd = @mahd";

            DataTable dt = new DataTable();
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                if (mahd != "" && mahd != "--- ALL ---")
                    cmd.Parameters.AddWithValue("@mahd", mahd);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            dataGridView1.AutoGenerateColumns = false;

            dataGridView1.DataSource = dt;

            dataGridView1.Columns["Column1"].DataPropertyName = "mahd";
            dataGridView1.Columns["Column2"].DataPropertyName = "makh";
            dataGridView1.Columns["Column3"].DataPropertyName = "ngaybatdau";
            dataGridView1.Columns["Column4"].DataPropertyName = "ngayketthuc";

            dataGridView2.DataSource = null;
            dataGridView3.DataSource = null;
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string mahd = dataGridView1.Rows[e.RowIndex].Cells["Column1"].Value.ToString();
                LoadChiTietDatPhong(mahd);
                LoadChiTietDichVu(mahd);
                loadtongtienthanhtoan(mahd);
            }
        }

        private void LoadChiTietDatPhong(string mahd)
        {
            string sql = @"SELECT ctdatphong.maph AS [Mã Phòng], phong.giaph AS [Giá Phòng], ctdatphong.songaythue AS [Số Ngày Thuê], ctdatphong.thanhtoan AS [Thanh Toán] FROM ctdatphong
            JOIN phong ON ctdatphong.maph = phong.maph
            WHERE ctdatphong.mahd = @mahd";


            DataTable dt = new DataTable();
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@mahd", mahd);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            dataGridView2.DataSource = dt;
        }


        private void LoadChiTietDichVu(string mahd)
        {
            string sql = @"SELECT ctdichvu.madv AS [Mã Dịch Vụ], dichvu.tendv AS [Tên Dịch Vụ],  dichvu.dongia AS [Đơn Giá], ctdichvu.soluong AS [Số Lượng], ctdichvu.thanhtien AS [Thành Tiền] FROM ctdichvu
JOIN dichvu ON ctdichvu.madv = dichvu.madv
WHERE ctdichvu.mahd = @mahd";


            DataTable dt = new DataTable();
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@mahd", mahd);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            dataGridView3.DataSource = dt;
        }

        private void loadtongtienthanhtoan(string mahd)
        {
            string sql = @"SELECT ctdatphong.mahd, 
       SUM(ctdatphong.thanhtoan) + COALESCE(SUM(ctdichvu.thanhtien), 0) AS tongtien
FROM ctdatphong
LEFT JOIN ctdichvu ON ctdatphong.mahd = ctdichvu.mahd
WHERE ctdatphong.mahd = @mahd
GROUP BY ctdatphong.mahd;";
            DataTable dt = new DataTable();
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@mahd", mahd);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            textBox1.DataBindings.Clear();
            textBox1.DataBindings.Add("Text", dt, "tongtien");

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Đã thanh toán xong!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    MessageBox.Show("Lỗi : " + ex.Message, "Lỗi");
                }
            }

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click_1(object sender, EventArgs e)
        {

        }
    }
}