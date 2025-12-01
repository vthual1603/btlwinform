namespace QLKhachSan
{
    partial class CRUDDichVu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvDichVu = new System.Windows.Forms.DataGridView();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.txtDVT = new System.Windows.Forms.TextBox();
            this.lblDienTich = new System.Windows.Forms.Label();
            this.txtDonGia = new System.Windows.Forms.TextBox();
            this.lblDonGia = new System.Windows.Forms.Label();
            this.txtTenDV = new System.Windows.Forms.TextBox();
            this.lblLoaiPhong = new System.Windows.Forms.Label();
            this.lblMaPhong = new System.Windows.Forms.Label();
            this.lblDanhSach = new System.Windows.Forms.Label();
            this.txtMaDV = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDichVu)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDichVu
            // 
            this.dgvDichVu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDichVu.Location = new System.Drawing.Point(160, 266);
            this.dgvDichVu.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvDichVu.Name = "dgvDichVu";
            this.dgvDichVu.RowHeadersWidth = 62;
            this.dgvDichVu.RowTemplate.Height = 28;
            this.dgvDichVu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDichVu.Size = new System.Drawing.Size(620, 411);
            this.dgvDichVu.TabIndex = 32;
            this.dgvDichVu.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDichVu_CellClick);
            // 
            // btnXoa
            // 
            this.btnXoa.BackColor = System.Drawing.Color.Red;
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoa.Location = new System.Drawing.Point(973, 200);
            this.btnXoa.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(155, 42);
            this.btnXoa.TabIndex = 30;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSua.Location = new System.Drawing.Point(973, 123);
            this.btnSua.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(155, 42);
            this.btnSua.TabIndex = 29;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = false;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnThem
            // 
            this.btnThem.BackColor = System.Drawing.Color.Cyan;
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThem.Location = new System.Drawing.Point(973, 60);
            this.btnThem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(155, 42);
            this.btnThem.TabIndex = 28;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // txtDVT
            // 
            this.txtDVT.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDVT.Location = new System.Drawing.Point(698, 61);
            this.txtDVT.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtDVT.Name = "txtDVT";
            this.txtDVT.Size = new System.Drawing.Size(209, 27);
            this.txtDVT.TabIndex = 27;
            // 
            // lblDienTich
            // 
            this.lblDienTich.AutoSize = true;
            this.lblDienTich.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDienTich.Location = new System.Drawing.Point(554, 60);
            this.lblDienTich.Name = "lblDienTich";
            this.lblDienTich.Size = new System.Drawing.Size(133, 32);
            this.lblDienTich.TabIndex = 26;
            this.lblDienTich.Text = "Đơn vị tính";
            this.lblDienTich.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtDonGia
            // 
            this.txtDonGia.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDonGia.Location = new System.Drawing.Point(698, 123);
            this.txtDonGia.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtDonGia.Name = "txtDonGia";
            this.txtDonGia.Size = new System.Drawing.Size(209, 27);
            this.txtDonGia.TabIndex = 25;
            // 
            // lblDonGia
            // 
            this.lblDonGia.AutoSize = true;
            this.lblDonGia.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDonGia.Location = new System.Drawing.Point(554, 113);
            this.lblDonGia.Name = "lblDonGia";
            this.lblDonGia.Size = new System.Drawing.Size(98, 32);
            this.lblDonGia.TabIndex = 24;
            this.lblDonGia.Text = "Đơn giá";
            this.lblDonGia.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtTenDV
            // 
            this.txtTenDV.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenDV.Location = new System.Drawing.Point(301, 118);
            this.txtTenDV.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTenDV.Name = "txtTenDV";
            this.txtTenDV.Size = new System.Drawing.Size(209, 27);
            this.txtTenDV.TabIndex = 21;
            // 
            // lblLoaiPhong
            // 
            this.lblLoaiPhong.AutoSize = true;
            this.lblLoaiPhong.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoaiPhong.Location = new System.Drawing.Point(158, 108);
            this.lblLoaiPhong.Name = "lblLoaiPhong";
            this.lblLoaiPhong.Size = new System.Drawing.Size(137, 32);
            this.lblLoaiPhong.TabIndex = 20;
            this.lblLoaiPhong.Text = "Tên dịch vụ";
            this.lblLoaiPhong.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblMaPhong
            // 
            this.lblMaPhong.AutoSize = true;
            this.lblMaPhong.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaPhong.Location = new System.Drawing.Point(158, 60);
            this.lblMaPhong.Name = "lblMaPhong";
            this.lblMaPhong.Size = new System.Drawing.Size(133, 32);
            this.lblMaPhong.TabIndex = 19;
            this.lblMaPhong.Text = "Mã dịch vụ";
            this.lblMaPhong.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblDanhSach
            // 
            this.lblDanhSach.AutoSize = true;
            this.lblDanhSach.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDanhSach.ForeColor = System.Drawing.Color.Blue;
            this.lblDanhSach.Location = new System.Drawing.Point(153, 202);
            this.lblDanhSach.Name = "lblDanhSach";
            this.lblDanhSach.Size = new System.Drawing.Size(246, 37);
            this.lblDanhSach.TabIndex = 18;
            this.lblDanhSach.Text = "Danh sách dịch vụ\r\n";
            this.lblDanhSach.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtMaDV
            // 
            this.txtMaDV.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaDV.Location = new System.Drawing.Point(301, 66);
            this.txtMaDV.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtMaDV.Name = "txtMaDV";
            this.txtMaDV.Size = new System.Drawing.Size(209, 27);
            this.txtMaDV.TabIndex = 33;
            // 
            // CRUDDichVu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1282, 953);
            this.Controls.Add(this.txtMaDV);
            this.Controls.Add(this.dgvDichVu);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.txtDVT);
            this.Controls.Add(this.lblDienTich);
            this.Controls.Add(this.txtDonGia);
            this.Controls.Add(this.lblDonGia);
            this.Controls.Add(this.txtTenDV);
            this.Controls.Add(this.lblLoaiPhong);
            this.Controls.Add(this.lblMaPhong);
            this.Controls.Add(this.lblDanhSach);
            this.Name = "CRUDDichVu";
            this.Text = "CRUDDichVu";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDichVu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvDichVu;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.TextBox txtDVT;
        private System.Windows.Forms.Label lblDienTich;
        private System.Windows.Forms.TextBox txtDonGia;
        private System.Windows.Forms.Label lblDonGia;
        private System.Windows.Forms.TextBox txtTenDV;
        private System.Windows.Forms.Label lblLoaiPhong;
        private System.Windows.Forms.Label lblMaPhong;
        private System.Windows.Forms.Label lblDanhSach;
        private System.Windows.Forms.TextBox txtMaDV;
    }
}