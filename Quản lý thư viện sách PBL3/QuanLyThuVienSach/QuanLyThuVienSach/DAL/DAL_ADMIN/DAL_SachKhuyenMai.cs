using QuanLyThuVienSach.DTO.DTO_ADMIN;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVienSach.DAL.DAL_ADMIN
{
    internal class DAL_SachKhuyenMai
    {
        private static DAL_SachKhuyenMai _Instance;
        public static DAL_SachKhuyenMai Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new DAL_SachKhuyenMai();
                }
                return _Instance;
            }
            private set { }
        }
        private DAL_SachKhuyenMai() { }

        public DataTable getAllSachKhuyenMai()
        {
            string query =
                " Select ID_SachKhuyenMai, Sach.MaSach,MucGiamGia,GiaBan,(GiaBan * MucGiamGia) AS GiaSauKhiGiam,NgayBatDau,NgayKetThuc" +
                " From SachKhuyenMai, Sach" +
                " Where Sach.MaSach = SachKhuyenMai.MaSach";
            return DBHelper.Instance.GetRecord(query);
        }

        public void DeleteSachKhuyenMai_DAL(string ID)
        {
            string query = $" DELETE from SachKhuyenMai where ID_SachKhuyenMai = {ID}";
            DBHelper.Instance.ExecuteDB(query);
        }

        public void UpdateSachKhuyenMai_DAL(SachKhuyenMai sachkhuyenmai)
        {
            string query = $"UPDATE SachKhuyenMai SET MucGiamGia = {sachkhuyenmai.MucGiamGia} , " +
                $" NgayBatDau = CONVERT(DATE,'{sachkhuyenmai.NgayBatDau}'), NgayKetThuc = CONVERT(DATE,'{sachkhuyenmai.NgayKetThuc}'), " +
                $" MaSach = {sachkhuyenmai.MaSach} " +
                $" WHERE ID_SachKhuyenMai = {sachkhuyenmai.ID_SachKhuyenMai}";
            DBHelper.Instance.ExecuteDB(query);

        }

        public void AddSachKhuyenMai_DAL(SachKhuyenMai sachkhuyenmai)
        {
            string query = $"INSERT INTO dbo.SachKhuyenMai VALUES( {sachkhuyenmai.MaSach}, {sachkhuyenmai.MucGiamGia}, " +
                $" CONVERT(DATE,'{sachkhuyenmai.NgayBatDau}'), CONVERT(DATE,'{sachkhuyenmai.NgayKetThuc}'))";
            DBHelper.Instance.ExecuteDB(query);
        
        }


    }
}
