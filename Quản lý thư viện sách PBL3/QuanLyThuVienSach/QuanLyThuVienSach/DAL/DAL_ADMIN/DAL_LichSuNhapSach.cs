using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVienSach.DAL.DAL_ADMIN
{
    internal class DAL_LichSuNhapSach
    {
        private static DAL_LichSuNhapSach _Instance;
        public static DAL_LichSuNhapSach Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new DAL_LichSuNhapSach();
                }
                return _Instance;
            }
            private set { }
        }
        private DAL_LichSuNhapSach() { }

        public DataTable GetAllLichSuNhapSach_DAL()
        {
            string query = "Select * From LichSuNhapSach";
            return DBHelper.Instance.GetRecord(query);
        }
        public DataTable GetAllLichSuNhapSach_DAL(DateTime From ,DateTime To)
        {
            string query = $"Select * From LichSuNhapSach WHERE NgayNhap >= '{From}' and NgayNhap <= '{To}' ";
            return DBHelper.Instance.GetRecord(query);
        }

    }
}
