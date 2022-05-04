using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyThuVienSach.DTO.DTO_THUKHO;
using QuanLyThuVienSach.DAL.DAL_THUKHO;

namespace QuanLyThuVienSach.BILL.BLL_THUKHO
{
    public class BLL_KhoSach
    {

        private static BLL_KhoSach _Instance;
        public static BLL_KhoSach Instance
        {
            get
            {
                if (_Instance == null)
                {

                    _Instance = new BLL_KhoSach();
                }
                return _Instance;

            }
            private set { }

        }
        private BLL_KhoSach()
        {

        }

        public List<SachView1> GetSachView1()
        {
            List<SachView1> data = new List<SachView1>();

            foreach (var i in DAL_KhoSach.Instance.GetAllSach())
            {
                int SoLuong = 0;
                foreach (var s in DAL_KhoSach.Instance.GetAllKho())
                {
                    if (i.MaSach == s.MaSach)
                    {
                        SoLuong = s.TongSoLuong;
                        break;
                    }
                }


                data.Add(new SachView1
                {
                    MaSach = i.MaSach,
                    TenSach = i.TenSach,
                    TheLoai = i.TheLoai,
                    TenTacGia = i.TenTacGia,
                    SoLanTaiBan = i.SoLanTaiBan,
                    NamXuatBan = i.NamXuatBan,
                    GiaNhap = i.GiaNhap,
                    GiaBan = i.GiaBan,
                    TongSoLuong = SoLuong
                });
            }

          
            return data;    
      

        }

        public SachView1 GetSachByMaSach(int MaSach)
        {
            SachView1 s = new SachView1();
            s = null;
            foreach(var i in GetSachView1())
            {
                if(MaSach == i.MaSach)
                {
                    s = i;
                    break;
                }
            }
            return s; 
        }

        public bool Check (int MaSach)
        {
            if(GetSachByMaSach(MaSach) != null )
            {
                return true;    
            }
            else
            {
                return false;
            }
        }

        public void AddorUpdate(SachView1 s)
        {
            if(Check(s.MaSach))
            {
                DAL_KhoSach.Instance.UpdateSach(s);
            }
            else
            {
                DAL_KhoSach.Instance.AddSach(s);
            }
        }

        public void DelSV(int MaSach)
        {
            DAL_KhoSach.Instance.DelSach(MaSach);   
        }

        public List<SachView1> SearchWH (string txt)
        {
            List<SachView1> data = new List<SachView1>();
            foreach(SachView1 s  in GetSachView1())
            {
                if(s.TenSach == txt)
                {
                    data.Add(s);
                }
            }
            return data;
        }

        public List<SachView1> SortWH (string CBB)
        {
            List<SachView1> data = new List<SachView1>();
            data = GetSachView1();
            if(CBB.CompareTo("Ten Sach") == 0)
            {
                //data.Sort(
                //    (x, y) =>
                //    {
                //        if (x.TenSach == y.TenSach) return 0;

                //    }

                //    );
            }
        }

    }
}
