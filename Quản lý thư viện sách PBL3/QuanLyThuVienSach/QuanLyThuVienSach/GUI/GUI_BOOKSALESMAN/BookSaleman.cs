using QuanLyThuVienSach.GUI.GUI_ADMIN;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuVienSach.GUI.GUI_BOOKSALESMAN
{
    public partial class BookSaleman : Form
    {
        public BookSaleman()
        {
            InitializeComponent();
        }

        private void MovePanel(Control c)
        {
            Panel_Admin.Height = (c.Height - 15);
            Panel_Admin.Top = (c.Top + 8);
        }


        private void bt_Resume_Click(object sender, EventArgs e)
        {
            MovePanel(bt_Resume);
            Page_NhanVienBanSach.SetPage("Resume");
        }

       

        private void btn_History_Click(object sender, EventArgs e)
        {
            MovePanel(btn_History);
            Page_NhanVienBanSach.SetPage("History");
        }

        private void btn_manage_Click_1(object sender, EventArgs e)
        {
            MovePanel(btn_Manage);
            Page_NhanVienBanSach.SetPage("Manage");
        }

        private void btn_Bill_Click_1(object sender, EventArgs e)
        {
            MovePanel(btn_Bill);
            Page_NhanVienBanSach.SetPage("Bill");
        }

      

        private void btn_CreateBill_Click(object sender, EventArgs e)
        {
            new CreateBill().ShowDialog();
        }


        private void btn_Addnew_Click(object sender, EventArgs e)
        {
            new FormAddBooks().ShowDialog(); //admin
        }
        private void btnResetpass_Click(object sender, EventArgs e)
        {
            new FormChangePassword().ShowDialog();//admin
        }

        private void bt_LogOut_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
