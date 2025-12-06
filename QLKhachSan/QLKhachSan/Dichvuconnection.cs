using System;
using System.Data;
using System.Data.SqlClient;

namespace QLKhachSan
{
    internal class Dichvuconnection
    {

        private readonly string connecting =
            @"Data Source=HUAL;Initial Catalog=qlkhachsan;Integrated Security=True;TrustServerCertificate=True";


        public DataTable LayDanhSach()
        {
            var dt = new DataTable();
            using (var conn = new SqlConnection(connecting))

            using (var da = new SqlDataAdapter("SELECT madv AS N'Mã DV', tendv AS N'Tên Dịch Vụ', dongia AS N'Đơn Giá' FROM dichvu", conn))
            {
                da.Fill(dt);
            }
            return dt;
        }


        public DataTable GetAllMaHD()
        {
            var dt = new DataTable();
            using (var conn = new SqlConnection(connecting))

            using (var da = new SqlDataAdapter("SELECT mahd AS N'Mã HĐ' FROM datphong ORDER BY mahd", conn))
            {
                da.Fill(dt);
            }
            return dt;
        }

        public DataTable GetAllMaPhong()
        {
            var dt = new DataTable();
            using (var conn = new SqlConnection(connecting))

            using (var da = new SqlDataAdapter("SELECT maph AS N'Mã Phòng' FROM phong ORDER BY maph", conn))
            {
                da.Fill(dt);
            }
            return dt;
        }

        public DataTable GetMaPhongByMaHD(string mahd)
        {
            var dt = new DataTable();
            using (var conn = new SqlConnection(connecting))
            using (var cmd = new SqlCommand(@"
                SELECT c.maph AS N'Mã Phòng'
                FROM ctdatphong c
                WHERE c.mahd = @mahd
                ORDER BY c.maph;", conn))
            {
                cmd.Parameters.AddWithValue("@mahd", mahd);
                using (var da = new SqlDataAdapter(cmd)) da.Fill(dt);
            }
            return dt;
        }


        public DataTable GetCTDichVu(string mahd, string maph)
        {
            var dt = new DataTable();
            using (var conn = new SqlConnection(connecting))
            using (var cmd = new SqlCommand(@"
                SELECT c.mahd AS N'Mã HĐ', 
                       c.maph AS N'Mã Phòng', 
                       c.madv AS N'Mã DV', 
                       d.tendv AS N'Tên Dịch Vụ', 
                       c.soluong AS N'Số Lượng', 
                       d.dongia AS N'Đơn Giá',
                       (c.soluong * d.dongia) AS N'Thành Tiền'
                FROM ctdichvu c
                JOIN dichvu d ON d.madv = c.madv
                WHERE c.mahd = @mahd AND c.maph = @maph
                ORDER BY c.madv;", conn))
            {
                cmd.Parameters.AddWithValue("@mahd", mahd);
                cmd.Parameters.AddWithValue("@maph", maph);
                using (var da = new SqlDataAdapter(cmd)) da.Fill(dt);
            }
            return dt;
        }


        public DataTable GetOneCTDichVu(string mahd, string maph, string madv)
        {
            var dt = new DataTable();
            using (var conn = new SqlConnection(connecting))
            using (var cmd = new SqlCommand(@"
                SELECT c.mahd AS N'Mã HĐ', 
                       c.maph AS N'Mã Phòng', 
                       c.madv AS N'Mã DV', 
                       d.tendv AS N'Tên Dịch Vụ', 
                       c.soluong AS N'Số Lượng', 
                       d.dongia AS N'Đơn Giá',
                       (c.soluong * d.dongia) AS N'Thành Tiền'
                FROM ctdichvu c
                JOIN dichvu d ON d.madv = c.madv
                WHERE c.mahd = @mahd AND c.maph = @maph AND c.madv = @madv;", conn))
            {
                cmd.Parameters.AddWithValue("@mahd", mahd);
                cmd.Parameters.AddWithValue("@maph", maph);
                cmd.Parameters.AddWithValue("@madv", madv);
                using (var da = new SqlDataAdapter(cmd)) da.Fill(dt);
            }
            return dt;
        }




        public int InsertCTDV(string mahd, string maph, string madv, int soluong)
        {
            using (var conn = new SqlConnection(connecting))
            using (var cmd = new SqlCommand(@"
                INSERT INTO ctdichvu(mahd,maph,madv,soluong,thanhtien)
                SELECT @mahd, @maph, @madv, @soluong, @soluong * d.dongia
                FROM dichvu d
                WHERE d.madv = @madv;", conn))
            {
                cmd.Parameters.AddWithValue("@mahd", mahd);
                cmd.Parameters.AddWithValue("@maph", maph);
                cmd.Parameters.AddWithValue("@madv", madv);
                cmd.Parameters.AddWithValue("@soluong", soluong);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public int UpdateCTDV(string mahd, string maph, string madv, int soluong)
        {
            using (var conn = new SqlConnection(connecting))
            using (var cmd = new SqlCommand(@"
                UPDATE ctdichvu 
                SET soluong=@soluong,
                    thanhtien = @soluong * (SELECT dongia FROM dichvu WHERE madv = @madv)
                WHERE mahd=@mahd AND maph=@maph AND madv=@madv;", conn))
            {
                cmd.Parameters.AddWithValue("@mahd", mahd);
                cmd.Parameters.AddWithValue("@maph", maph);
                cmd.Parameters.AddWithValue("@madv", madv);
                cmd.Parameters.AddWithValue("@soluong", soluong);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public int DeleteCTDV(string mahd, string maph, string madv)
        {
            using (var conn = new SqlConnection(connecting))
            using (var cmd = new SqlCommand(@"
                DELETE FROM ctdichvu
                WHERE mahd=@mahd AND maph=@maph AND madv=@madv;", conn))
            {
                cmd.Parameters.AddWithValue("@mahd", mahd);
                cmd.Parameters.AddWithValue("@maph", maph);
                cmd.Parameters.AddWithValue("@madv", madv);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }


        public (string Server, string Database, int SoDV) DebugInfo()
        {
            using (var conn = new SqlConnection(connecting))
            using (var cmd = new SqlCommand(@"
                SELECT @@SERVERNAME AS sv, DB_NAME() AS dbname;
                SELECT COUNT(*) AS sodv FROM dichvu;", conn))
            {
                conn.Open();
                using (var r = cmd.ExecuteReader())
                {
                    string server = "?", db = "?";
                    int so = -1;

                    if (r.Read())
                    {
                        server = r["sv"]?.ToString();
                        db = r["dbname"]?.ToString();
                    }
                    if (r.NextResult() && r.Read())
                    {
                        so = Convert.ToInt32(r["sodv"]);
                    }
                    return (server, db, so);
                }
            }
        }


        public DataTable GetCTDichVu_AllRooms(string mahd)
        {
            var dt = new DataTable();
            using (var conn = new SqlConnection(connecting))
            using (var cmd = new SqlCommand(@"
                SELECT c.mahd AS N'Mã HĐ', 
                       c.maph AS N'Mã Phòng', 
                       c.madv AS N'Mã DV', 
                       d.tendv AS N'Tên Dịch Vụ', 
                       c.soluong AS N'Số Lượng', 
                       d.dongia AS N'Đơn Giá',
                       c.soluong * d.dongia AS N'Thành Tiền'
                FROM    ctdichvu c
                JOIN    dichvu d ON d.madv = c.madv
                WHERE   c.mahd = @mahd
                ORDER BY c.maph, c.madv;", conn))
            {
                cmd.Parameters.AddWithValue("@mahd", mahd);
                using (var da = new SqlDataAdapter(cmd)) da.Fill(dt);
            }
            return dt;
        }
    }
}