using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLKhachSan
{
    public static class BookingConnection
    {
        private const string ConnectionString =
            @"Data Source=HUAL;Initial Catalog=qlkhachsan;Integrated Security=True;TrustServerCertificate=True";
       
        //---------- HÀM CƠ BẢN ----------

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        public static bool TestConnection(out string errorMessage)
        {
            errorMessage = null;
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                }
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public static DataTable ExecuteQuery(string sql, params SqlParameter[] parameters)
        {
            var dt = new DataTable();
            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = new SqlCommand(sql, conn))
            using (var ad = new SqlDataAdapter(cmd))
            {
                if (parameters != null && parameters.Length > 0)
                    cmd.Parameters.AddRange(parameters);
                conn.Open();
                ad.Fill(dt);
            }
            return dt;
        }

        public static object ExecuteScalar(string sql, params SqlParameter[] parameters)
        {
            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                if (parameters != null && parameters.Length > 0)
                    cmd.Parameters.AddRange(parameters);
                conn.Open();
                return cmd.ExecuteScalar();
            }
        }

        //---------- LẤY DỮ LIỆU ----------

        public static DataTable GetAvailableRooms()
        {
            const string sql = @"
                SELECT  maph      AS MaPhong,
                        loaiph    AS LoaiPhong,
                        dientich  AS DienTich,
                        tinhtrang AS TinhTrang,
                        giaph     AS GiaPhong
                FROM phong
                WHERE LTRIM(RTRIM(ISNULL(tinhtrang, ''))) = N'còn';";
            return ExecuteQuery(sql);
        }

        public static bool CustomerExists(string maKH)
        {
            const string sql = "SELECT COUNT(*) FROM khachhang WHERE makh = @makh";
            var obj = ExecuteScalar(sql, new SqlParameter("@makh", maKH));
            return (obj != null && obj != DBNull.Value && Convert.ToInt32(obj) > 0);
        }

        public static string GenerateNewMaHoaDon()
        {
            const string sql = @"SELECT ISNULL(MAX(CAST(SUBSTRING(mahd, 3, 10) AS INT)), 0) + 1 FROM datphong;";
            var obj = ExecuteScalar(sql);
            int next = (obj == null || obj == DBNull.Value) ? 1 : Convert.ToInt32(obj);
            return "HD" + next.ToString("D3");
        }

        public static DataTable GetAllBookings()
        {
            const string sql = @"
                SELECT  dp.mahd        AS MaHD,
                        dp.makh        AS MaKH,
                        ct.maph        AS MaPhong,
                        dp.ngaybatdau  AS NgayNhan,
                        dp.ngayketthuc AS NgayTra,
                        ct.thanhtoan   AS HoaDon
                FROM datphong dp
                JOIN ctdatphong ct ON dp.mahd = ct.mahd;";
            return ExecuteQuery(sql);
        }

        //===============================================================
        // 1. CHỨC NĂNG THÊM MỚI (INSERT)
        //===============================================================
        public static void ThemDatPhong(string mahd, string makh, string maph, DateTime ngayBatDau, DateTime ngayKetThuc)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    // B1: Kiểm tra tình trạng phòng
                    string sqlCheckPhong = "SELECT tinhtrang, giaph FROM phong WHERE maph = @maph";
                    string tinhTrang = "";
                    int giaPh = 0;

                    using (var cmd = new SqlCommand(sqlCheckPhong, conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@maph", maph);
                        using (var rd = cmd.ExecuteReader())
                        {
                            if (rd.Read())
                            {
                                tinhTrang = rd["tinhtrang"].ToString().Trim().ToLower();
                                giaPh = Convert.ToInt32(rd["giaph"]);
                            }
                            else throw new Exception("Mã phòng không tồn tại.");
                        }
                    }

                    if (tinhTrang == "hết" || tinhTrang == "het")
                        throw new Exception($"Phòng {maph} đã có người thuê (Tình trạng: hết). Vui lòng chọn phòng khác.");

                    // B2: Kiểm tra hóa đơn (Logic gộp hóa đơn)
                    string sqlCheckHD = "SELECT makh FROM datphong WHERE mahd = @mahd";
                    object existMakh = null;
                    using (var cmd = new SqlCommand(sqlCheckHD, conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@mahd", mahd);
                        existMakh = cmd.ExecuteScalar();
                    }

                    bool hdTonTai = (existMakh != null && existMakh != DBNull.Value);

                    // Nếu HD tồn tại, bắt buộc Khách hàng phải trùng khớp
                    if (hdTonTai)
                    {
                        if (existMakh.ToString().Trim() != makh.Trim())
                            throw new Exception($"Mã hóa đơn {mahd} đã tồn tại của khách hàng {existMakh}. Không thể thêm cho khách {makh}.");
                    }
                    else
                    {
                        // Nếu HD chưa tồn tại -> Tạo bảng datphong trước
                        string sqlInsertHD = "INSERT INTO datphong(mahd, makh, ngaybatdau, ngayketthuc) VALUES(@mahd, @makh, @nbd, @nkt)";
                        using (var cmd = new SqlCommand(sqlInsertHD, conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@mahd", mahd);
                            cmd.Parameters.AddWithValue("@makh", makh);
                            cmd.Parameters.AddWithValue("@nbd", ngayBatDau.Date);
                            cmd.Parameters.AddWithValue("@nkt", ngayKetThuc.Date);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // B3: Tính tiền
                    int soNgay = (ngayKetThuc.Date - ngayBatDau.Date).Days;
                    if (soNgay <= 0) soNgay = 1;
                    int thanhToan = giaPh * soNgay;

                    // B4: Thêm vào chi tiết đặt phòng (ctdatphong)
                    string sqlInsertCT = "INSERT INTO ctdatphong(mahd, maph, songaythue, thanhtoan) VALUES(@mahd, @maph, @sn, @tt)";
                    using (var cmd = new SqlCommand(sqlInsertCT, conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@mahd", mahd);
                        cmd.Parameters.AddWithValue("@maph", maph);
                        cmd.Parameters.AddWithValue("@sn", soNgay);
                        cmd.Parameters.AddWithValue("@tt", thanhToan);
                        cmd.ExecuteNonQuery();
                    }

                    // B5: Cập nhật phòng sang trạng thái 'hết'
                    string sqlUpdatePhong = "UPDATE phong SET tinhtrang = N'hết' WHERE maph = @maph";
                    using (var cmd = new SqlCommand(sqlUpdatePhong, conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@maph", maph);
                        cmd.ExecuteNonQuery();
                    }

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }

        //===============================================================
        // 2. CHỨC NĂNG SỬA (UPDATE)
        //===============================================================
        public static void SuaDatPhong(string mahd, string maph, DateTime ngayBatDau, DateTime ngayKetThuc)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    // B1: Kiểm tra xem dòng đặt phòng này có tồn tại không
                    string sqlCheck = "SELECT COUNT(*) FROM ctdatphong WHERE mahd = @mahd AND maph = @maph";
                    using (var cmd = new SqlCommand(sqlCheck, conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@mahd", mahd);
                        cmd.Parameters.AddWithValue("@maph", maph);
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        if (count == 0)
                            throw new Exception("Không tìm thấy thông tin đặt phòng để sửa (Sai mã HD hoặc Mã phòng).");
                    }

                    // B2: Lấy giá phòng hiện tại để tính lại tiền
                    string sqlGia = "SELECT giaph FROM phong WHERE maph = @maph";
                    int giaPh = 0;
                    using (var cmd = new SqlCommand(sqlGia, conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@maph", maph);
                        object obj = cmd.ExecuteScalar();
                        if (obj != null) giaPh = Convert.ToInt32(obj);
                    }

                    // B3: Tính toán lại
                    int soNgay = (ngayKetThuc.Date - ngayBatDau.Date).Days;
                    if (soNgay <= 0) soNgay = 1;
                    int thanhToanMoi = giaPh * soNgay;

                    // B4: Cập nhật bảng ctdatphong
                    string sqlUpdateCT = @"UPDATE ctdatphong 
                                           SET songaythue = @sn, thanhtoan = @tt 
                                           WHERE mahd = @mahd AND maph = @maph";
                    using (var cmd = new SqlCommand(sqlUpdateCT, conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@sn", soNgay);
                        cmd.Parameters.AddWithValue("@tt", thanhToanMoi);
                        cmd.Parameters.AddWithValue("@mahd", mahd);
                        cmd.Parameters.AddWithValue("@maph", maph);
                        cmd.ExecuteNonQuery();
                    }

                    // B5: Cập nhật ngày trong bảng datphong (nếu muốn đồng bộ ngày tổng của hóa đơn)
                    // Lưu ý: Nếu hóa đơn có nhiều phòng, việc update ngày chung này có thể ảnh hưởng logic,
                    // nhưng với bài toán cơ bản thì thường update theo lần sửa cuối.
                    string sqlUpdateHD = "UPDATE datphong SET ngaybatdau = @nbd, ngayketthuc = @nkt WHERE mahd = @mahd";
                    using (var cmd = new SqlCommand(sqlUpdateHD, conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@nbd", ngayBatDau.Date);
                        cmd.Parameters.AddWithValue("@nkt", ngayKetThuc.Date);
                        cmd.Parameters.AddWithValue("@mahd", mahd);
                        cmd.ExecuteNonQuery();
                    }

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }

        //===============================================================
        // 3. CHỨC NĂNG XÓA (DELETE)
        //===============================================================
        public static void XoaDatPhong(string mahd, string maph)
        {
            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                // Sử dụng Transaction để đảm bảo xóa sạch hoặc không xóa gì cả
                cmd.CommandText = @"
                    BEGIN TRAN;
                        -- 1. Xóa các dịch vụ đi kèm (nếu có) của phòng này trong hóa đơn này
                        DELETE FROM ctdichvu WHERE mahd = @mahd AND maph = @maph;

                        -- 2. Xóa chi tiết đặt phòng
                        DELETE FROM ctdatphong WHERE mahd = @mahd AND maph = @maph;

                        -- 3. Nếu hóa đơn này không còn phòng nào nữa -> Xóa luôn Hóa đơn cha
                        DELETE FROM datphong 
                        WHERE mahd = @mahd
                          AND NOT EXISTS (SELECT 1 FROM ctdatphong WHERE mahd = @mahd);

                        -- 4. Trả lại trạng thái phòng thành 'còn'
                        UPDATE phong SET tinhtrang = N'còn' WHERE maph = @maph;
                    COMMIT TRAN;";

                cmd.Parameters.AddWithValue("@mahd", mahd);
                cmd.Parameters.AddWithValue("@maph", maph);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Nếu lỗi hệ thống SQL thì throw ra
                    throw new Exception("Lỗi khi xóa: " + ex.Message);
                }
            }
        }
    }
}