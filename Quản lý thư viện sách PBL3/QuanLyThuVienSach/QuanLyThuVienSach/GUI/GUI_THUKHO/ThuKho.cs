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
using QuanLyThuVienSach.BILL.BLL_THUKHO;
using QuanLyThuVienSach.DTO.DTO_THUKHO;


namespace QuanLyThuVienSach.GUI.GUI_THUKHO
{
    public partial class ThuKho : Form
    {
        public int ID_AccountL { get; set; }
        public int ID_PersonL { get; set; }

        public ThuKho(int ID_account, int ID_person)
        {
            InitializeComponent();
            ID_AccountL = ID_account;
            ID_PersonL = ID_person;
            MovePanel(bt_Resume);
            SetResume(BLL_MemberTK.Instance.GetMemberByID(ID_PersonL, ID_AccountL));
        }


        #region RESEME
        private void bt_EditResume_Click(object sender, EventArgs e)
        {

            EditResume f = new EditResume(ID_PersonL);
            f.d = new EditResume.Mydel(ReloadResume);
            f.ShowDialog();
        }

        private void ReloadResume()
        {
            SetResume(BLL_MemberTK.Instance.GetMemberByID(ID_PersonL, ID_AccountL));
        }
        private void bt_ChangePassword_Click(object sender, EventArgs e)
        {
            new FormChangePassword(ID_AccountL).ShowDialog();
        }

        private void SetResume(Member member)
        {

            //LableQ.Text = member.Name_Person;
            //tb_NameResume.Text = member.Name_Person;
            //tb_PhoneResume.Text = member.PhoneNumber;
            //tb_PositionResume.Text = member.Name_Position;
            //tb_AnddressResume.Text = member.Address;
            //tb_EmailResume.Text = member.Email;
            //DateTime d = member.DateOfBirth;
            //tb_DateOfBirthResume.Text = $"{d.Day}/{d.Month}/{d.Year}";
            //tb_GenderResume.Text = member.Gender;

        }
        private void bt_Resume_Click(object sender, EventArgs e)
        {
            MovePanel(bt_Resume);
            Page_ThuKho.SetPage("Resume");

        }

        #endregion


        public ThuKho()
        {
            InitializeComponent();
            MovePanel(bt_Resume);
        }
        private void MovePanel(Control c)
        {
            Panel_THUKHO.Height = (c.Height - 15);
            Panel_THUKHO.Top = (c.Top + 8);
        }

        private void bt_LogOut_Click(object sender, EventArgs e)
        {
            Application.Exit();
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


        //TAB_MANAGEBOOK

        private void bt_AddBook_Click(object sender, EventArgs e)
        {
            new FormAddBooks().ShowDialog();
        }


        //TAB_WAREHOUSE

        private void bt_WareHouse_Click(object sender, EventArgs e)
        {
            MovePanel(bt_WareHouse);
            Page_ThuKho.SetPage("WareHouse");
            Show();

        }
        public void Show()
        {
            dgv1.DataSource = BLL_KhoSach.Instance.GetSachView1();
        }
        private void bt_AddBook_WareHouse_Click(object sender, EventArgs e)
        {
            AddBook_WareHouse f = new AddBook_WareHouse(0);
            f.d = new AddBook_WareHouse.Mydel(Show);
            f.ShowDialog();

        }

        private void bunifuButton21_Click(object sender, EventArgs e)
        {
            //edit 
            if (dgv1.SelectedRows.Count == 1)
            {
                int MaSach = Convert.ToInt32((dgv1.SelectedRows[0].Cells["MaSach"].Value.ToString()));
                AddBook_WareHouse f = new AddBook_WareHouse(MaSach);
                f.d = new AddBook_WareHouse.Mydel(Show);
                f.ShowDialog();
            }

        }

        private void bunifuButton22_Click(object sender, EventArgs e)
        {
            DialogResult re = MessageBox.Show("Are you sure dalete Sach ? ", "Delete", MessageBoxButtons.OKCancel);
            if (re == DialogResult.OK)
            {
                if (dgv1.SelectedRows.Count > 0)
                {
                    foreach(DataGridViewRow i in dgv1.SelectedRows)
                    {
                        BLL_KhoSach.Instance.DelSV(Convert.ToInt32(i.Cells["MaSach"].Value.ToString()));
                        Show();

                    }
                }
            }
        }

        private void bunifuButton24_Click(object sender, EventArgs e)
        {
            //sort

           
        }

        private void btnSearchWareHouse_Click(object sender, EventArgs e)
        {
            //search
            if(txtSearchWH.Text != "")
            {
                dgv1.DataSource = BLL_KhoSach.Instance.SearchWH(txtSearchWH.Text);
            }
            else
            {
                Show();
            }
        }
    }
}