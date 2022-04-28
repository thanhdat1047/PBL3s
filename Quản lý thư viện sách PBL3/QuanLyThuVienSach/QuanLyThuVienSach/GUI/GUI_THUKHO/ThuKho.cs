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

namespace QuanLyThuVienSach.GUI.GUI_THUKHO
{
    public partial class ThuKho : Form
    {
        public ThuKho()
        {
            InitializeComponent();
        }
        #region Movepanel
        private void MovePanel(Control c)
        {
            Panel_THUKHO.Height = (c.Height - 15);
            Panel_THUKHO.Top = (c.Top + 8);
        }

        private void bt_LogOut_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bt_Resume_Click(object sender, EventArgs e)
        {
            MovePanel(bt_Resume);
            Page_ThuKho.SetPage("Resume");

        }

        private void bt_WareHouse_Click(object sender, EventArgs e)
        {
            MovePanel(bt_WareHouse);
            Page_ThuKho.SetPage("WareHouse");

        }

        private void bt_ManageBook_Click(object sender, EventArgs e)
        {
            MovePanel(bt_ManageBook);
            Page_ThuKho.SetPage("ManageBook");

        }

        private void bt_History_Click(object sender, EventArgs e)
        {
            MovePanel(bt_History);
            Page_ThuKho.SetPage("History");

        }
        #endregion


        #region TAB_RESUME

        private void bt_EditResume_Click(object sender, EventArgs e)
        {
            new EditResume().ShowDialog();
        }

        private void bt_ChangePassword_Click(object sender, EventArgs e)
        {
            new FormChangePassword().ShowDialog();
        }
        #endregion

        #region TAB_MANAGEBOOK

        private void bt_AddBook_Click(object sender, EventArgs e)
        {
            new FormAddBooks().ShowDialog();
        }
        #endregion

        #region Tab_WareHouse
        private void bt_AddBook_WareHouse_Click(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
