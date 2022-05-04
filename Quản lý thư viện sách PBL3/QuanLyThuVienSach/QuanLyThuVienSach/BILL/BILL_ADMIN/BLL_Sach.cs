using QuanLyThuVienSach.DAL.DAL_ADMIN;
using QuanLyThuVienSach.DTO.DTO_ADMIN;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVienSach.BILL.BILL_ADMIN
{
    class BLL_Sach
    {
        private static BLL_Sach _Instance;
        public static BLL_Sach Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_Sach();
                }
                return _Instance;
            }
            private set { }
        }
        private BLL_Sach()
        {
        }

        public List<Sach> GetAllSach_BLL()
        {
            List<Sach> list = new List<Sach>();
            foreach (DataRow i in DAL_Sach.Instance.GetAllSach_DAL().Rows)
            {
                list.Add(new Sach

                { 
                    MaSach = Convert.ToInt32(i[0]),
                    TenSach = i[1].ToString(),
                    TheLoai = i[2].ToString(),
                    TenTacGia = i[3].ToString(),
                    SoLanTaiBan = Convert.ToInt32(i[4].ToString()),
                    NamXuatBan = Convert.ToString(i[5]),
                    GiaNhap = Convert.ToDouble(i[6].ToString()),
                    GiaBan = Convert.ToDouble(i[7].ToString()),
                    TongSoLuong = Convert.ToInt32(i[8]),

                }); ;
            }
            return list;
        }

       
        public Sach GetSachByID(int ID_Sach)
        {
            Sach sach = new Sach();
            foreach (Sach i in GetAllSach_BLL())
            { 
                if(ID_Sach == i.MaSach)
                {
                    sach = i;
                }
            }
            return sach;
        }

        public void UpdateSach_BLL(Sach sach, int ID_Person)
        {
            DAL_Sach.Instance.UpdateSach_DAL(sach, ID_Person);
        }
        public void DelSach_BLL(List<string> MaSach)
        {
            foreach (string i in MaSach)
            {
                DAL_Sach.Instance.DeleteSach_DAL(i);
            }
        }

        public void AddSach_BLL(Sach sach,int ID_Person)
        {
            DAL_Sach.Instance.AddSach_DAL(sach,ID_Person);
        }

        public DataTable FindSach_BLL(string txt)
        {
           return DAL_Sach.Instance.FindSach_DAL(txt);
        }

        public List<int> GetAllMaSach_BLL()
        { 
            List<int> list = new List<int>();
            foreach (Sach i in GetAllSach_BLL())
            { 
                list.Add(i.MaSach);
            }
            return list;
        }

    }
}
