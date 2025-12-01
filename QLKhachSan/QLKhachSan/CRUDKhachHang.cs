using System;
using System.Data;
using System.Data.SqlClient;

namespace QLKhachSan
{
    
    public enum XoaTrangThai
    {
        ThanhCong,
        KhongTonTai,
        CoDatPhong
    }

    internal class CRUDKhachHang
    {
        private string connecting = @"Data Source=localhost,1433;Initial Catalog=qlkhachsan;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        
        public bool KiemTraLogin(string username, string password)
        {
          
            using (SqlConnection connection = new SqlConnection(connecting))
            {
               
                    connection.Open();
                    string query = "SELECT COUNT(1) FROM taikhoan WHERE username = @username AND pass = @password";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return (count > 0);
                    }
               
            }
        }

        public DataTable LayDanhSach()
        {
           
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(connecting))
            {
                try
                {
                    string query = "SELECT makh AS N'Mã Khách Hàng', tenkh AS N'Tên Khách Hàng', namsinh AS N'Năm Sinh', gioitinh AS N'Giới Tính', diachi AS N'Địa Chỉ', sdt AS N'Số Điện Thoại' FROM KhachHang";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi khi lấy danh sách khách hàng: " + ex.Message);
                }
            }
            return dataTable;
        }

        public bool Them(KhachHang kh)
        {
            // Logic đã đúng
            using (SqlConnection connection = new SqlConnection(connecting))
            {
                try
                {
                    connection.Open();
                    string query = @"INSERT INTO KhachHang (makh, tenkh, namsinh, sdt, diachi, gioitinh) 
                                     VALUES (@makh, @tenkh, @namsinh, @sdt, @diachi, @gioitinh)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@makh", kh.MaKH);
                        command.Parameters.AddWithValue("@tenkh", kh.HoTen);
                        command.Parameters.AddWithValue("@namsinh", kh.NamSinh);
                        command.Parameters.AddWithValue("@sdt", kh.SoDienThoai);
                        command.Parameters.AddWithValue("@diachi", kh.DiaChi);
                        command.Parameters.AddWithValue("@gioitinh", kh.GioiTinh);

                        int result = command.ExecuteNonQuery();
                        return result > 0;
                    }
                }
                catch (SqlException)
                {
                    throw;
                }
            }
        }

        public bool Sua(KhachHang kh)
        {
            // Logic đã đúng
            using (SqlConnection connection = new SqlConnection(connecting))
            {
                connection.Open();
                string query = @"UPDATE KhachHang 
                                 SET tenkh = @tenkh, 
                                     namsinh = @namsinh, 
                                     sdt = @sdt, 
                                     diachi = @diachi, 
                                     gioitinh = @gioitinh 
                                 WHERE makh = @makh";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@tenkh", kh.HoTen);
                    command.Parameters.AddWithValue("@namsinh", kh.NamSinh);
                    command.Parameters.AddWithValue("@sdt", kh.SoDienThoai);
                    command.Parameters.AddWithValue("@diachi", kh.DiaChi);
                    command.Parameters.AddWithValue("@gioitinh", kh.GioiTinh);
                    command.Parameters.AddWithValue("@makh", kh.MaKH);

                    int result = command.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }

        public KhachHang TimKiem(string maKH)
        {
            // Logic đã đúng
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(connecting))
            {
                string query = "SELECT * FROM KhachHang WHERE makh = @makh";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@makh", maKH);
                adapter.Fill(dataTable);
            }

            if (dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];
                // Cần đảm bảo KhachHang là một lớp/struct có thể khởi tạo
                KhachHang kh = new KhachHang
                {
                    MaKH = row["makh"].ToString(),
                    HoTen = row["tenkh"].ToString(),
                    NamSinh = Convert.ToDateTime(row["namsinh"]),
                    SoDienThoai = row["sdt"].ToString(),
                    DiaChi = row["diachi"].ToString(),
                    GioiTinh = row["gioitinh"].ToString()
                };
                return kh;
            }
            return null;
        }

        public bool KiemTraCoDatPhong(string maKH)
        {
            using (SqlConnection connection = new SqlConnection(connecting))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM datphong WHERE makh = @makh";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@makh", maKH);
                        int count = (int)command.ExecuteScalar();
                        return count > 0;
                    }
                }
                catch (Exception)
                {
                    // Lỗi kết nối/DB: Trả về true để đảm bảo không xóa trong điều kiện không ổn định (an toàn)
                    return true;
                }
            }
        }

        /// <summary>
        /// Kiểm tra khách hàng có tồn tại trong bảng khachhang hay không.
        /// </summary>
        /// <returns>True nếu mã khách hàng tồn tại.</returns>
        public bool KiemTraTonTaiKhachHang(string maKH)
        {
            using (SqlConnection connection = new SqlConnection(connecting))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM khachhang WHERE makh = @makh";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@makh", maKH);
                        int count = (int)command.ExecuteScalar();
                        return count > 0;
                    }
                }
                catch (Exception)
                {
                    // Lỗi kết nối/DB: Giả định khách hàng không tồn tại.
                    return false;
                }
            }
        }

        /// <summary>
        /// Xóa khách hàng sau khi kiểm tra ràng buộc.
        /// </summary>
        /// <returns>Mã trạng thái xóa chi tiết.</returns>
        public XoaTrangThai Xoa(string maKH)
        {
            // 1. Kiểm tra sự tồn tại
            if (!KiemTraTonTaiKhachHang(maKH))
            {
                return XoaTrangThai.KhongTonTai;
            }

            // 2. Kiểm tra ràng buộc đặt phòng
            if (KiemTraCoDatPhong(maKH))
            {
                return XoaTrangThai.CoDatPhong;
            }

            // 3. Tiến hành xóa
            using (SqlConnection connection = new SqlConnection(connecting))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM khachhang WHERE makh = @makh";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@makh", maKH);
                        int result = command.ExecuteNonQuery();

                        return (result > 0) ? XoaTrangThai.ThanhCong : XoaTrangThai.KhongTonTai;
                    }
                }
                catch (Exception)
                {
                    return XoaTrangThai.KhongTonTai;
                }
            }
        }
    }
}