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
            string query = " SELECT Sach.MaSach,TenSach,Theloai,TenTacGia,SolanTaiBan,NamXuatBan,GiaNhap,GiaBan,TongSoLuong" +
                           " FROM dbo.Sach,Kho " +
                           " WHERE Sach.MaSach = Kho.MaSach";
            return DBHelper.Instance.GetRecord(query);
        }
  




        public void UpdateSach_DAL(Sach sach, int ID_Person)
        {
            string query = $" UPDATE Sach SET TenSach = '{sach.TenSach}', Theloai = '{sach.TheLoai}', TenTacGia = '{sach.TenTacGia}', " +
                           $" SolanTaiBan = {sach.SoLanTaiBan}, NamXuatBan = '{sach.NamXuatBan}', GiaNhap = {sach.GiaNhap}, GiaBan = {sach.GiaBan}" +
                           $" WHERE MaSach = {sach.MaSach}";
            DBHelper.Instance.ExecuteDB(query);

            string query1 = $"SELECT TongSoLuong FROM Kho WHERE MaSach = {sach.MaSach}";
            int SoLuong = 0;
            foreach (DataRow i in DBHelper.Instance.GetRecord(query1).Rows)
            {
                SoLuong = Convert.ToInt32(i[0]);
            }

            string query2 = $" UPDATE Kho SET TongSoLuong = {sach.TongSoLuong}" +
                       $" WHERE MaSach = {sach.MaSach}";

            DBHelper.Instance.ExecuteDB(query2);


            if (sach.TongSoLuong > SoLuong)
            {
                string query3 = $"INSERT INTO dbo.LichSuNhapSach VALUES( {sach.MaSach}, {sach.TongSoLuong - SoLuong}, GETDATE(), {ID_Person} )";
                DBHelper.Instance.ExecuteDB(query3);
            }

            if (sach.TongSoLuong > SoLuong)
            {
                string query3 = $"INSERT INTO dbo.LichSuNhapSach VALUES( {sach.MaSach}, {sach.TongSoLuong - SoLuong}, GETDATE(), {ID_Person})";
                DBHelper.Instance.ExecuteDB(query3);
            }

            if (sach.TongSoLuong <= SoLuong)
            {
                int SoLuongMat = (SoLuong - sach.TongSoLuong);
                int ID_LSNS = 0;
                int soluong = 0;

                while (true)
                {
                    string query4 = $"SELECT ID_LichSuNhapSach,SoLuong FROM LichSuNhapSach WHERE MaSach = {sach.MaSach} ORDER BY ID_LichSuNhapSach ASC";
                    foreach (DataRow i in DBHelper.Instance.GetRecord(query4).Rows)
                    {
                        ID_LSNS = (Convert.ToInt32(i[0]));
                        soluong = (Convert.ToInt32(i[1]));
                    }
                    int t = SoLuongMat - soluong;
                    if (t >= 0)
                    {
                        string query5 = $" DELETE from LichSuNhapSach where MaSach = {sach.MaSach} AND ID_LichSuNhapSach = {ID_LSNS}";
                        DBHelper.Instance.ExecuteDB(query5);
                        SoLuongMat = t;

                    }
                    else
                    {
                        string query6 = $" UPDATE LichSuNhapSach SET SoLuong = {-t} WHERE MaSach = {sach.MaSach} AND ID_LichSuNhapSach = {ID_LSNS}";
                        DBHelper.Instance.ExecuteDB(query6);
                        SoLuongMat = 0;
                        break;
                    }

                }

            }

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

        public void AddSach_DAL(Sach sach,int ID_Person)
        {
            string query = $"INSERT INTO dbo.Sach VALUES ( '{sach.TenSach}', '{sach.TheLoai}', '{sach.TenTacGia}', '{sach.SoLanTaiBan}', '{sach.NamXuatBan}', {sach.GiaNhap}, {sach.GiaBan} )";

            DBHelper.Instance.ExecuteDB(query);

            string query1 = "SELECT TOP 1 MaSach FROM Sach ORDER BY MaSach DESC";

            int masach = -1;
            foreach (DataRow i in DBHelper.Instance.GetRecord(query1).Rows)
            {
                masach = Convert.ToInt32(i[0]);
            }

            string query2 = $"INSERT INTO dbo.Kho VALUES ({masach},{sach.TongSoLuong})";
            DBHelper.Instance.ExecuteDB(query2);

            string query3 = $"INSERT INTO dbo.LichSuNhapSach VALUES( {masach}, {sach.TongSoLuong}, GETDATE(), {ID_Person} )";
            DBHelper.Instance.ExecuteDB(query3);
        }

        public DataTable FindSach_DAL(string txt)
        {
            string query = $"SELECT Sach.MaSach, TenSach, TheLoai, TenTacGia, SoLanTaiBan, NamXuatBan, GiaNhap, GiaBan, TongSoLuong" +
                $" FROM Sach, Kho" +
                $" WHERE Sach.MaSach = Kho.MaSach" +
                $" AND TenSach LIKE '%{txt}%'";
            return DBHelper.Instance.GetRecord(query);

        }

    }
}
