using System.Drawing;
using System.Windows.Forms;

namespace QLKhachSan
{
    partial class BookingDesk
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblMaKH = new System.Windows.Forms.Label();
            this.lblNgayNhan = new System.Windows.Forms.Label();
            this.txtMaKH = new System.Windows.Forms.TextBox();
            this.dtpNgayNhan = new System.Windows.Forms.DateTimePicker();
            this.lblMaHoaDon = new System.Windows.Forms.Label();
            this.txtMaHoaDon = new System.Windows.Forms.TextBox();
            this.lblMaPhong = new System.Windows.Forms.Label();
            this.txtMaPhong = new System.Windows.Forms.TextBox();
            this.lblNgayTra = new System.Windows.Forms.Label();
            this.dtpNgayTra = new System.Windows.Forms.DateTimePicker();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.lblDangKyPhong = new System.Windows.Forms.Label();
            this.lblPhongTrong = new System.Windows.Forms.Label();
            this.dgvPhong = new System.Windows.Forms.DataGridView();
            this.dgvDatPhong = new System.Windows.Forms.DataGridView();
            this.btnDichvu = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatPhong)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMaKH
            // 
            this.lblMaKH.AutoSize = true;
            this.lblMaKH.Location = new System.Drawing.Point(555, 109);
            this.lblMaKH.Name = "lblMaKH";
            this.lblMaKH.Size = new System.Drawing.Size(65, 23);
            this.lblMaKH.TabIndex = 0;
            this.lblMaKH.Text = "Mã KH:";
            this.lblMaKH.Click += new System.EventHandler(this.lblMaKH_Click_1);
            // 
            // lblNgayNhan
            // 
            this.lblNgayNhan.AutoSize = true;
            this.lblNgayNhan.Location = new System.Drawing.Point(555, 194);
            this.lblNgayNhan.Name = "lblNgayNhan";
            this.lblNgayNhan.Size = new System.Drawing.Size(98, 23);
            this.lblNgayNhan.TabIndex = 1;
            this.lblNgayNhan.Text = "Ngày nhận:";
            this.lblNgayNhan.Click += new System.EventHandler(this.lblNgayNhan_Click_1);
            // 
            // txtMaKH
            // 
            this.txtMaKH.Location = new System.Drawing.Point(677, 106);
            this.txtMaKH.Name = "txtMaKH";
            this.txtMaKH.Size = new System.Drawing.Size(336, 30);
            this.txtMaKH.TabIndex = 2;
            // 
            // dtpNgayNhan
            // 
            this.dtpNgayNhan.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayNhan.Location = new System.Drawing.Point(677, 188);
            this.dtpNgayNhan.MaxDate = new System.DateTime(2050, 10, 1, 0, 0, 0, 0);
            this.dtpNgayNhan.Name = "dtpNgayNhan";
            this.dtpNgayNhan.Size = new System.Drawing.Size(336, 30);
            this.dtpNgayNhan.TabIndex = 3;
            this.dtpNgayNhan.Value = new System.DateTime(2025, 12, 1, 0, 0, 0, 0);
            this.dtpNgayNhan.ValueChanged += new System.EventHandler(this.dtpNgayNhan_ValueChanged_1);
            // 
            // lblMaHoaDon
            // 
            this.lblMaHoaDon.AutoSize = true;
            this.lblMaHoaDon.Location = new System.Drawing.Point(555, 269);
            this.lblMaHoaDon.Name = "lblMaHoaDon";
            this.lblMaHoaDon.Size = new System.Drawing.Size(107, 23);
            this.lblMaHoaDon.TabIndex = 4;
            this.lblMaHoaDon.Text = "Mã hóa đơn:";
            this.lblMaHoaDon.Click += new System.EventHandler(this.lblMaHoaDon_Click);
            // 
            // txtMaHoaDon
            // 
            this.txtMaHoaDon.Location = new System.Drawing.Point(677, 264);
            this.txtMaHoaDon.Name = "txtMaHoaDon";
            this.txtMaHoaDon.Size = new System.Drawing.Size(336, 30);
            this.txtMaHoaDon.TabIndex = 5;
            this.txtMaHoaDon.TextChanged += new System.EventHandler(this.textBox2_TextChanged_1);
            // 
            // lblMaPhong
            // 
            this.lblMaPhong.AutoSize = true;
            this.lblMaPhong.Location = new System.Drawing.Point(555, 150);
            this.lblMaPhong.Name = "lblMaPhong";
            this.lblMaPhong.Size = new System.Drawing.Size(93, 23);
            this.lblMaPhong.TabIndex = 6;
            this.lblMaPhong.Text = "Mã phòng:";
            this.lblMaPhong.Click += new System.EventHandler(this.lblMaPhong_Click_1);
            // 
            // txtMaPhong
            // 
            this.txtMaPhong.Enabled = false;
            this.txtMaPhong.Location = new System.Drawing.Point(677, 147);
            this.txtMaPhong.Name = "txtMaPhong";
            this.txtMaPhong.Size = new System.Drawing.Size(336, 30);
            this.txtMaPhong.TabIndex = 7;
            // 
            // lblNgayTra
            // 
            this.lblNgayTra.AutoSize = true;
            this.lblNgayTra.Location = new System.Drawing.Point(555, 234);
            this.lblNgayTra.Name = "lblNgayTra";
            this.lblNgayTra.Size = new System.Drawing.Size(80, 23);
            this.lblNgayTra.TabIndex = 8;
            this.lblNgayTra.Text = "Ngày trả:";
            // 
            // dtpNgayTra
            // 
            this.dtpNgayTra.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayTra.Location = new System.Drawing.Point(677, 228);
            this.dtpNgayTra.MaxDate = new System.DateTime(2050, 11, 22, 0, 0, 0, 0);
            this.dtpNgayTra.Name = "dtpNgayTra";
            this.dtpNgayTra.Size = new System.Drawing.Size(336, 30);
            this.dtpNgayTra.TabIndex = 9;
            this.dtpNgayTra.Value = new System.DateTime(2025, 12, 1, 0, 0, 0, 0);
            this.dtpNgayTra.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // btnThem
            // 
            this.btnThem.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.btnThem.Location = new System.Drawing.Point(562, 320);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(93, 43);
            this.btnThem.TabIndex = 10;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnSua
            // 
            this.btnSua.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.btnSua.Location = new System.Drawing.Point(661, 320);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(100, 43);
            this.btnSua.TabIndex = 11;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = false;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.btnXoa.Location = new System.Drawing.Point(767, 320);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(100, 42);
            this.btnXoa.TabIndex = 12;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // lblDangKyPhong
            // 
            this.lblDangKyPhong.AutoSize = true;
            this.lblDangKyPhong.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.lblDangKyPhong.Location = new System.Drawing.Point(792, 71);
            this.lblDangKyPhong.Name = "lblDangKyPhong";
            this.lblDangKyPhong.Size = new System.Drawing.Size(157, 23);
            this.lblDangKyPhong.TabIndex = 14;
            this.lblDangKyPhong.Text = "ĐĂNG KÝ PHÒNG ";
            // 
            // lblPhongTrong
            // 
            this.lblPhongTrong.AutoSize = true;
            this.lblPhongTrong.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.lblPhongTrong.Location = new System.Drawing.Point(235, 71);
            this.lblPhongTrong.Name = "lblPhongTrong";
            this.lblPhongTrong.Size = new System.Drawing.Size(118, 23);
            this.lblPhongTrong.TabIndex = 15;
            this.lblPhongTrong.Text = "Phòng Trống ";
            // 
            // dgvPhong
            // 
            this.dgvPhong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPhong.Location = new System.Drawing.Point(48, 109);
            this.dgvPhong.Name = "dgvPhong";
            this.dgvPhong.RowHeadersWidth = 51;
            this.dgvPhong.RowTemplate.Height = 24;
            this.dgvPhong.Size = new System.Drawing.Size(472, 499);
            this.dgvPhong.TabIndex = 16;
            this.dgvPhong.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPhong_CellClick);
            // 
            // dgvDatPhong
            // 
            this.dgvDatPhong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatPhong.Location = new System.Drawing.Point(559, 397);
            this.dgvDatPhong.Name = "dgvDatPhong";
            this.dgvDatPhong.RowHeadersWidth = 51;
            this.dgvDatPhong.RowTemplate.Height = 24;
            this.dgvDatPhong.Size = new System.Drawing.Size(454, 211);
            this.dgvDatPhong.TabIndex = 17;
            this.dgvDatPhong.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDatPhong_CellClick);
            // 
            // btnDichvu
            // 
            this.btnDichvu.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnDichvu.Location = new System.Drawing.Point(873, 320);
            this.btnDichvu.Name = "btnDichvu";
            this.btnDichvu.Size = new System.Drawing.Size(140, 42);
            this.btnDichvu.TabIndex = 18;
            this.btnDichvu.Text = "Dịch vụ";
            this.btnDichvu.UseVisualStyleBackColor = false;
            this.btnDichvu.Click += new System.EventHandler(this.btnDichvu_Click);
            // 
            // BookingDesk
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1282, 953);
            this.Controls.Add(this.btnDichvu);
            this.Controls.Add(this.dgvDatPhong);
            this.Controls.Add(this.dgvPhong);
            this.Controls.Add(this.lblPhongTrong);
            this.Controls.Add(this.lblDangKyPhong);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.dtpNgayTra);
            this.Controls.Add(this.lblNgayTra);
            this.Controls.Add(this.txtMaPhong);
            this.Controls.Add(this.lblMaPhong);
            this.Controls.Add(this.txtMaHoaDon);
            this.Controls.Add(this.lblMaHoaDon);
            this.Controls.Add(this.dtpNgayNhan);
            this.Controls.Add(this.txtMaKH);
            this.Controls.Add(this.lblNgayNhan);
            this.Controls.Add(this.lblMaKH);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "BookingDesk";
            this.Text = "LeTanGUI";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatPhong)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lblMaKH;
        private Label lblNgayNhan;
        private TextBox txtMaKH;
        private DateTimePicker dtpNgayNhan;
        private Label lblMaHoaDon;
        private TextBox txtMaHoaDon;
        private Label lblMaPhong;
        private TextBox txtMaPhong;
        private Label lblNgayTra;
        private DateTimePicker dtpNgayTra;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Label lblDangKyPhong;
        private Label lblPhongTrong;
        private DataGridView dgvPhong;
        private DataGridView dgvDatPhong;
        private Button btnDichvu;
    }
}
