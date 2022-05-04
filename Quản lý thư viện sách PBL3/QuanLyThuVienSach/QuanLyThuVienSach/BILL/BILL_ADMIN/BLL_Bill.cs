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
    internal class BLL_Bill
    {
        private static BLL_Bill _Instance;
        public static BLL_Bill Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_Bill();
                }
                return _Instance;
            }
            private set { }
        }

        public DataTable GetAllBill_BLL()
        {
            return DAL_Bill.Instance.GetAllBill_DAL();
        }
        public DataTable GetAllBill_BLL(DateTime from , DateTime to)
        {
            return DAL_Bill.Instance.GetAllBill_DAL(from,to);
        }

        public DataTable GetBillByID_BLL(int ID)
        {
            return DAL_Bill.Instance.GetBillByID_DAL(ID);
        }

        public void DeleteBill_BLL(List<int> list)
        {
            foreach(int id in list)
            {
                DAL_Bill.Instance.DeleteBill_DAL(id);
            }    

        }
   
        public List<Bill_Detail_View> GetBill_Detail_Views(int MaHoaDon)
        {
            List<Bill_Detail_View> list = new List<Bill_Detail_View>();
            foreach (Bill_Detail i in DAL_Bill.Instance.GetBill_Detail(MaHoaDon))
            {
                Bill_Detail_View bdv = new Bill_Detail_View();
                bdv.MaHoaDon = MaHoaDon;
                bdv.MaSach = i.MaSach;
                bdv.SoLuong = i.SoLuong;
                foreach (Sach sach in DAL_Sach.Instance.GetAllSach())
                {
                    if(sach.MaSach == bdv.MaSach)
                    {
                        bdv.TenSach = sach.TenSach;
                        bdv.MucGiamGia = DAL_Bill.Instance.GetMucGiamGia(sach.MaSach);
                        bdv.Total = (sach.GiaBan * bdv.MucGiamGia * bdv.SoLuong);
                        break;
                    }
                    ;
                } 
                list.Add(bdv);
            }    
            return list;
        }

   

    }
}
