using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyThuVienSach.DTO.DTO_THUKHO;
using System.Data;

namespace QuanLyThuVienSach.DAL.DAL_THUKHO
{
     class DAL_KhoSach
    {
        private static DAL_KhoSach _Instance; 
        public static DAL_KhoSach Instance
        {
            get
            {
                if(_Instance == null)
                {
                    _Instance = new DAL_KhoSach();  
                }
                return _Instance;
            }
            private set { }
        }
        private DAL_KhoSach()
        {

        }


        public List<Sach> GetAllSach()
        {
            List<Sach> data = new List<Sach>();
            string query = "select * from Sach";
            foreach(DataRow i in DBHelper.Instance.GetRecord(query).Rows)
            {
                data.Add(GetSachByDataRow(i));
            }
            return data;
                    
        }
        public Sach GetSachByDataRow(DataRow i)
        {
            return new Sach
            {
            
                MaSach = Convert.ToInt32( i["MaSach"].ToString()),
                TenSach = i["TenSach"].ToString(), 
                TheLoai = i["TheLoai"].ToString(), 
                TenTacGia = i["TenTacGia"].ToString(), 
                SoLanTaiBan = i["SolanTaiBan"].ToString(),
                NamXuatBan = i["NamXuatBan"].ToString(), 
                GiaNhap =  Convert.ToInt32( i["GiaNhap"].ToString() ),
                GiaBan = Convert.ToInt32(i["GiaBan"].ToString())


            };
        }

        public List<Kho> GetAllKho()
        {
            List<Kho> data = new List<Kho>();
            string query = "select * from Kho";
            foreach (DataRow i in DBHelper.Instance.GetRecord(query).Rows)
            {
                data.Add(GetKhoByDataRow(i));
            }
            return data;

        }
        public Kho GetKhoByDataRow(DataRow i)
        {
            return new Kho
            {

                MaSach = Convert.ToInt32(i["MaSach"].ToString()),
                TongSoLuong = Convert.ToInt32(i["TongSoLuong"].ToString())
            };
        }

        public void AddSach(SachView1 s)
        {
            string query = $"INSERT INTO dbo.Sach VALUES ( '{s.TenSach}', '{s.TheLoai}', '{s.TenTacGia}','{s.SoLanTaiBan}', '{s.NamXuatBan}', {s.GiaNhap}, {s.GiaBan} )";

            DBHelper.Instance.ExecuteDB(query);

            string query1 = "SELECT TOP 1 MaSach FROM Sach ORDER BY MaSach DESC";

            int masach = -1;
            foreach (DataRow i in DBHelper.Instance.GetRecord(query1).Rows)
            {
                masach = Convert.ToInt32(i[0]);
            }


            string query2 = $"INSERT INTO dbo.Kho VALUES ({masach},{s.TongSoLuong})";
            DBHelper.Instance.ExecuteDB(query2);
            
            string query3 = $"INSERT INTO dbo.LichSuNhapSach VALUES( {masach}, {s.TongSoLuong}, GETDATE(), {3} )";
            DBHelper.Instance.ExecuteDB(query3);

        }

        public void UpdateSach(SachView1 s)
        {
                string query = $" UPDATE Sach SET TenSach = '{s.TenSach}', Theloai = '{s.TheLoai}', TenTacGia = '{s.TenTacGia}', " +
                               $" SolanTaiBan = {s.SoLanTaiBan}, NamXuatBan = '{s.NamXuatBan}', GiaNhap = {s.GiaNhap}, GiaBan = {s.GiaBan}" +
                               $" WHERE MaSach = {s.MaSach}";
                DBHelper.Instance.ExecuteDB(query);

                string query1 = $"SELECT TongSoLuong FROM Kho WHERE MaSach = {s.MaSach}";
                int SoLuongKho = 0;
                foreach (DataRow i in DBHelper.Instance.GetRecord(query1).Rows)
                {
                    SoLuongKho = Convert.ToInt32(i[0]);
                }

               

                if (s.TongSoLuong > SoLuongKho)
                {
                    string query3 = $"INSERT INTO dbo.LichSuNhapSach VALUES( {s.MaSach}, {s.TongSoLuong - SoLuongKho}, GETDATE(), {3} )";
                    DBHelper.Instance.ExecuteDB(query3);
                }

              

                if (s.TongSoLuong <= SoLuongKho)
                {
                    int SoLuongMat = (SoLuongKho - s.TongSoLuong);
                    int ID_LSNS = 0;
                    int soluong = 0;

                    while (true)
                    {
                        string query4 = $"SELECT ID_LichSuNhapSach,SoLuong FROM LichSuNhapSach WHERE MaSach = {s.MaSach} ORDER BY ID_LichSuNhapSach ASC";
                        foreach (DataRow i in DBHelper.Instance.GetRecord(query4).Rows)
                        {
                            ID_LSNS = (Convert.ToInt32(i[0]));
                            soluong = (Convert.ToInt32(i[1]));
                        }
                        int t = SoLuongMat - soluong;
                        if (t >= 0)
                        {
                            string query5 = $" DELETE from LichSuNhapSach where MaSach = {s.MaSach} AND ID_LichSuNhapSach = {ID_LSNS}";
                            DBHelper.Instance.ExecuteDB(query5);
                            SoLuongMat = t;

                        }
                        else
                        {
                            string query6 = $" UPDATE LichSuNhapSach SET SoLuong = {-t} WHERE MaSach = {s.MaSach} AND ID_LichSuNhapSach = {ID_LSNS}";
                            DBHelper.Instance.ExecuteDB(query6);
                            SoLuongMat = 0;
                            break;
                        }

                    }

                }
                string query2 = $" UPDATE Kho SET TongSoLuong = {s.TongSoLuong}" +
                      $" WHERE MaSach = {s.MaSach}";

                DBHelper.Instance.ExecuteDB(query2);



        }

        public void DelSach (int Id_Book)
        {
            string query1 = $" DELETE from LichSuNhapSach where MaSach = {Id_Book}";
            DBHelper.Instance.ExecuteDB(query1);

            string query2 = $" DELETE from Kho where MaSach = {Id_Book}";
            DBHelper.Instance.ExecuteDB(query2);

            string query3 = $" DELETE from Sach where MaSach = {Id_Book}";
            DBHelper.Instance.ExecuteDB(query3);
        }
     }
}
