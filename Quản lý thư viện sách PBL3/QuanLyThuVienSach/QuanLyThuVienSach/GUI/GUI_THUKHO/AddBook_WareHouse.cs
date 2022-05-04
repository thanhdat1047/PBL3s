using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyThuVienSach.BILL.BLL_THUKHO;
using QuanLyThuVienSach.DTO.DTO_THUKHO;

namespace QuanLyThuVienSach.GUI.GUI_THUKHO
{
    public partial class AddBook_WareHouse : Form
    {
        public delegate void Mydel();
        public Mydel d { get; set; }
        public int MaSach { get; set; } 

        public AddBook_WareHouse(int masach)
        {
            InitializeComponent();
            MaSach = masach;
            GUI();
        }

        public void GUI()
        {
            if(MaSach !=  0)
            {
             
                var t = BLL_KhoSach.Instance.GetSachByMaSach(MaSach);               
                txtNameBook.Text = t.TenSach;
                txtTacGia.Text = t.TenTacGia;
                txtTheLoai.Text = t.TheLoai;
                txtSoLanTaiBan.Text = t.SoLanTaiBan.ToString();
                txtSoLuong.Text = t.TongSoLuong.ToString();
                txtGiaMua.Text = t.GiaNhap.ToString();
                txtGiaBan.Text = t.GiaBan.ToString();
                txtNamxuatban.Text = t.NamXuatBan;
            }
        }
        private void bt_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bt_OK_Click(object sender, EventArgs e)
        {
            SachView1 s = new SachView1
            {
                //MaSach = Convert.ToInt32(txtIdBook.Text),
                MaSach = MaSach,
                TenSach = txtNameBook.Text,
                TenTacGia = txtTacGia.Text,
                TheLoai = txtTheLoai.Text,
                SoLanTaiBan = txtSoLanTaiBan.Text,
                NamXuatBan = txtNamxuatban.Text,
                GiaBan = Convert.ToInt32(txtGiaBan.Text),
                GiaNhap = Convert.ToInt32(txtGiaMua.Text),
                TongSoLuong = Convert.ToInt32(txtSoLuong.Text),


            };

            BLL_KhoSach.Instance.AddorUpdate(s);
            d();
            this.Close();
        }
    }
}
