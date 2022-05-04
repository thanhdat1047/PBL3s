using QuanLyThuVienSach.DTO.DTO_ADMIN;
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

        public void DeleteBill_DAL(int ID)
        {
           
            string query = $"Delete from ChiTietHoaDon where MaHoaDon = {ID}";
            DBHelper.Instance.ExecuteDB(query);

            string query1 = $"Delete from HoaDon where MaHoaDon = {ID}";
            DBHelper.Instance.ExecuteDB(query1);
        }

   
        public List<Bill_Detail> GetBill_Detail(int MaHoaDon)
        {
            List<Bill_Detail> data = new List<Bill_Detail>();
            string query = $"select * from ChiTietHoaDon where MaHoaDon = {MaHoaDon}";
            foreach (DataRow i in DBHelper.Instance.GetRecord(query).Rows)
            {
                data.Add(Bill_DetailByDataRow(i));
            }
            return data;
        }

        public Bill_Detail Bill_DetailByDataRow(DataRow i)
        {
            return new Bill_Detail
            {
                MaHoaDon = Convert.ToInt32(i["MaHoaDon"].ToString()),
                MaSach = Convert.ToInt32(i["MaSach"].ToString()),
                SoLuong = Convert.ToInt32(i["SoLuong"]),
            };
        }
        public double GetMucGiamGia(int MaSach)
        {
            double mucgiamgia = 1;
            foreach (var u in DAL_SachKhuyenMai.Instance.GetAllSachKM())
            {
                if (u.MaSach == MaSach)
                {
                    mucgiamgia = u.MucGiamGia;
                    break;
                }
            }
            return mucgiamgia;
        }
    }
}
