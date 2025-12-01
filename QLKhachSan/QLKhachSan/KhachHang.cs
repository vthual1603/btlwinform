using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLKhachSan
{
    internal class KhachHang

    {
        public string MaKH { get; set; }
        public string HoTen { get; set; }
        public DateTime NamSinh { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChi { get; set; }
        public string GioiTinh { get; set; }

        public KhachHang()
        {
        }

        public KhachHang(string id, string name, DateTime date, string phone, string address, string gender)
        {
            this.MaKH = id;
            this.HoTen = name;
            this.NamSinh = date;
            this.SoDienThoai = phone;
            this.DiaChi = address;
            this.GioiTinh = gender;

        }

       
    }
}
