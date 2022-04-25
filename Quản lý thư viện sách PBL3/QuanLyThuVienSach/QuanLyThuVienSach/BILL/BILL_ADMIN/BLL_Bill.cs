using QuanLyThuVienSach.DAL.DAL_ADMIN;
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

    }
}
