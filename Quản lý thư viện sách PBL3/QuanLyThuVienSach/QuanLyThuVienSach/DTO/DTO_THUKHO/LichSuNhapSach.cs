using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVienSach.DTO.DTO_THUKHO
{
    public class LichSuNhapSach
    {
        public int ID_LichSuNhapSach { get; set; } 
        public int MaSach { get; set; }
        public int SoLuong { get; set; }
        public DateTime NgayNhap { get; set; }  
        public int ID_Person { get; set; }  

    }
}
