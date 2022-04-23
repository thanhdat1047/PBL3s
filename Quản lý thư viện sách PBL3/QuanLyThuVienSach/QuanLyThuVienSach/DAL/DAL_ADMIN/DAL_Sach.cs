using QuanLyThuVienSach.DTO.DTO_ADMIN;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVienSach.DAL.DAL_ADMIN
{
    internal class DAL_Sach
    {
        private static DAL_Sach _Instance;
        public static DAL_Sach Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new DAL_Sach();
                }
                return _Instance;
            }
            private set { }
        }
        private DAL_Sach() { }

        public DataTable GetAllSach_DAL()
        {
            string query = " SELECT Sach.MaSach,TenSach,Theloai,TenTacGia,SolanTaiBan,NamXuatBan,GiaNhap,GiaBan,TongSoLuong,SoLuongDaBan " +
                           " FROM dbo.Sach,Kho " +
                           " WHERE Sach.MaSach = Kho.MaSach";
            return DBHelper.Instance.GetRecord(query);
        }

        public void UpdateSach_DAL(Sach sach)
        {
            string query = $" UPDATE Sach SET TenSach = '{sach.TenSach}', Theloai = '{sach.TheLoai}', TenTacGia = '{sach.TenTacGia}', " +
                           $" SolanTaiBan = {sach.SoLanTaiBan}, NamXuatBan = '{sach.NamXuatBan}', GiaNhap = {sach.GiaNhap}, GiaBan = {sach.GiaBan}" +
                           $" WHERE MaSach = {sach.MaSach}";
            DBHelper.Instance.ExecuteDB(query);


            string query1 = $" UPDATE Kho SET TongSoLuong = {sach.TongSoLuong}, SoLuongDaBan = {sach.SoLuongDaBan}" +
                            $" WHERE MaSach = {sach.MaSach}";

            DBHelper.Instance.ExecuteDB(query1);
        }

        public void DeleteSach_DAL(String MaSach)
        {
            string query1 = $" DELETE from LichSuNhapSach where MaSach = {MaSach}";
            DBHelper.Instance.ExecuteDB(query1);

            string query2 = $" DELETE from Kho where MaSach = {MaSach}";
            DBHelper.Instance.ExecuteDB(query2);

            string query3 = $" DELETE from Sach where MaSach = {MaSach}";
            DBHelper.Instance.ExecuteDB(query3);
        }

        public void AddSach_DAL(Sach sach)
        {
            string query = $"INSERT INTO dbo.Sach VALUES ( '{sach.TenSach}', '{sach.TheLoai}', '{sach.TenTacGia}', '{sach.SoLanTaiBan}', '{sach.NamXuatBan}', {sach.GiaNhap}, {sach.GiaBan} )";

            DBHelper.Instance.ExecuteDB(query);

            string query1 = "SELECT TOP 1 MaSach FROM Sach ORDER BY MaSach DESC";

            int masach = -1;
            foreach (DataRow i in DBHelper.Instance.GetRecord(query1).Rows)
            {
                masach = Convert.ToInt32(i[0]);
            }

            string query2 = $"INSERT INTO dbo.Kho VALUES ({masach},{sach.TongSoLuong},{sach.SoLuongDaBan})";
            DBHelper.Instance.ExecuteDB(query2);
        }

        public DataTable FindSach_DAL(string txt)
        {
            string query = $"SELECT Sach.MaSach, TenSach, TheLoai, TenTacGia, SoLanTaiBan, NamXuatBan, GiaNhap, GiaBan, TongSoLuong, SoLuongDaBan" +
                $" FROM Sach, Kho" +
                $" WHERE Sach.MaSach = Kho.MaSach" +
                $" AND TenSach LIKE '%{txt}%'";
            return DBHelper.Instance.GetRecord(query);

        }

    }
}
