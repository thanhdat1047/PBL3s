using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVienSach.DAL.DAL_ADMIN
{
    internal class DAL_Bill
    {
        private static DAL_Bill _Instance;
        public static DAL_Bill Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new DAL_Bill();
                }
                return _Instance;
            }
            private set { }
        }
        private DAL_Bill() { }

        public DataTable GetAllBill_DAL()
        {
            string query = "Select * FROM HoaDon";
            return DBHelper.Instance.GetRecord(query);
        }
        public DataTable GetAllBill_DAL(DateTime from ,DateTime to)
        {
            string query = $"Select * FROM HoaDon WHERE NgayLap >= '{from}' and NgayLap <= '{to}'";
            return DBHelper.Instance.GetRecord(query);
        }
        public DataTable GetBillByID_DAL(int ID)
        {
            string query = $"Select * FROM ChiTietHoaDon WHERE MaHoaDon = {ID}";
            return DBHelper.Instance.GetRecord(query);
        }
    }
}
