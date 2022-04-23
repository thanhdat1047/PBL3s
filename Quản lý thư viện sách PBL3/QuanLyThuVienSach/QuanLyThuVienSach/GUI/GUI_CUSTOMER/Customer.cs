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

namespace QuanLyThuVienSach.GUI.GUI_CUSTOMER
{
    public partial class Customer : Form
    {
        public Customer()
        {
            InitializeComponent();
        }
        private void MovePanel(Control c)
        {
            Panel_Admin.Height = (c.Height - 15);
            Panel_Admin.Top = (c.Top + 8);
        }

        private void bt_SearchBooks_Click(object sender, EventArgs e)
        {
            MovePanel(bt_SearchBooks);
            Page_Customer.SetPage("SearchBooks");
        }

        private void bt_Personalinformation_Click(object sender, EventArgs e)
        {
            MovePanel(bt_Personalinformation);
            Page_Customer.SetPage("Personalinformation");
        }

        private void bt_LogOut_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bt_Payments_Click(object sender, EventArgs e)
        {
            MovePanel(bt_Payments);
            Page_Customer.SetPage("Payments");
        }

        private void bt_Transactionhistory_Click(object sender, EventArgs e)
        {
            MovePanel(bt_Transactionhistory);
            Page_Customer.SetPage("Transaction history");
        }

        private void bt_ResetPassword_Click(object sender, EventArgs e)
        {
            new FormChangePassword().ShowDialog();
        }

        private void bt_bookinformation_Click(object sender, EventArgs e)
        {
            new BookInformation().ShowDialog();
        }

        private void bunifuButton25_Click(object sender, EventArgs e)
        {
           // choose
        }
    }
}
