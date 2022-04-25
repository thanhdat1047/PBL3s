using QuanLyThuVienSach.BILL.BILL_ADMIN;
using QuanLyThuVienSach.DTO.DTO_ADMIN;
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

namespace QuanLyThuVienSach.GUI
{
    public partial class Admin : Form
    {
        int movex, movey, move;
        public int ID_AccountL { get; set; }
        public int ID_PersonL { get; set; }
        public Admin()
        {
            InitializeComponent();
            MovePanel(bt_Resume);
        }
        public Admin(int ID_account, int ID_person)
        {
            InitializeComponent();
            ID_AccountL = ID_account;
            ID_PersonL = ID_person;
            MovePanel(bt_Resume);
            SetResume(BLL_Member.Instance.GetMemberByID(ID_PersonL,ID_AccountL));
        }

        //-------------------------------------------------------------- SET DESIGN

        #region SetDesign
        private void MovePanel(Control c)
        {
            Panel_Admin.Height = (c.Height - 15);
            Panel_Admin.Top = (c.Top + 8);
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            move = 1;
            movex = e.X;
            movey = e.Y;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (move == 1)
            {
                this.SetDesktopLocation(MousePosition.X - movex, MousePosition.Y - movey);
            }
        }
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            move = 0;
        }
        private void bt_LogOut_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion
  
        //------------------------------------------------------------------ RESUME

        #region RESEME
        private void bt_EditResume_Click(object sender, EventArgs e)
        {
           
            EditResume f = new EditResume(ID_PersonL);
            f.d = new EditResume.Mydel(ReloadResume);
            f.ShowDialog();
        }

        private void ReloadResume()
        {
        SetResume(BLL_Member.Instance.GetMemberByID(ID_PersonL, ID_AccountL));
        }
        private void bt_ChangePassword_Click(object sender, EventArgs e)
        {
            new FormChangePassword(ID_AccountL).ShowDialog();     
        }
        private void bt_Resume_Click(object sender, EventArgs e)
        {
            ReloadResume();
            MovePanel(bt_Resume);
            Page_Admin.SetPage("Resume");
        }
        private void SetResume(Member member)
        {
            LableQ.Text = member.Name_Person;
            tb_NameResume.Text = member.Name_Person;
            tb_PhoneResume.Text = member.PhoneNumber;
            tb_PositionResume.Text = member.Name_Position;
            tb_AnddressResume.Text = member.Address;
            tb_EmailResume.Text = member.Email;
            DateTime d = member.DateOfBirth;
            tb_DateOfBirthResume.Text = $"{d.Day}/{d.Month}/{d.Year}";

            if (member.Gender)
            {
                tb_GenderResume.Text = "Male";
            }
            else
            {
                tb_GenderResume.Text = "Female";
            }

        }
        #endregion

        //-------------------------------------------------------------- MEMBER

        #region Member
        private void cbb_Positon_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void bt_Members_Click(object sender, EventArgs e)
        {
            MovePanel(bt_Members);
            Page_Admin.SetPage("Members");
            ShowMembers();
        }
        public void ShowMembers()
        {
            DataGridView_Members.DataSource = BLL_Member.Instance.GetAllMembers_BLL();
            DataGridView_Members.Columns[0].HeaderText = "ID";
            DataGridView_Members.Columns[1].HeaderText = "Name";
            DataGridView_Members.Columns[1].Width = 120;
            DataGridView_Members.Columns[2].HeaderText = "      Gender";
            DataGridView_Members.Columns[3].HeaderText = "Date of Birth";
            DataGridView_Members.Columns[4].HeaderText = "Address";
            DataGridView_Members.Columns[5].HeaderText = "Email";
            DataGridView_Members.Columns[5].Width = 200;
            DataGridView_Members.Columns[6].HeaderText = "Phone number";
            DataGridView_Members.Columns[6].Width = 100;
            DataGridView_Members.Columns[7].HeaderText = "ID Account";
            DataGridView_Members.Columns[8].HeaderText = "User name";
            DataGridView_Members.Columns[9].HeaderText = "Password";
            DataGridView_Members.Columns[10].HeaderText = "Position";
        }
        
        private void btReloand_Member_Click(object sender, EventArgs e)
        {
            ShowMembers();
        }

        private void bt_DeleteMembers_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("      Are you sure delete Person ?", "Confirm delete", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (DataGridView_Members.SelectedRows.Count > 0)
                {
                    List<int> listDel = new List<int>();
                    foreach (DataGridViewRow i in DataGridView_Members.SelectedRows)
                    {
                        listDel.Add(Convert.ToInt32(i.Cells["ID_Person"].Value));
                    }
                    BLL_Member.Instance.DeleteMember_BLL(listDel);
                }
                ShowMembers();
            }
        }
        private void DataGridView_Members_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (DataGridView_Members.SelectedRows.Count != 0)
                {
                    int ID_Account = Convert.ToInt32(DataGridView_Members.SelectedRows[0].Cells["ID_Account"].Value);
                    int ID_Person = Convert.ToInt32(DataGridView_Members.SelectedRows[0].Cells["ID_Person"].Value);
                    Member member = BLL_Member.Instance.GetMemberByID(ID_Person,ID_Account);
                    Set_Member(member);
                }
            }
        }

        private void Set_Member(Member member)
        {
            tb_UserName.Text = member.UserName.ToString();
            tb_Password.Text = member.Password.ToString();
            tb_IDAccount.Text = member.ID_Account.ToString();
            cbb_Positon.Text = cbb_Positon.Items[member.ID_Position - 1].ToString();
            tb_ID.Text = member.ID_Person.ToString();
            tb_Name.Text = member.Name_Person.ToString();
            tb_Anddress.Text = member.Address.ToString();
            tb_Email.Text = member.Email.ToString();
            tb_Phone.Text = member.PhoneNumber.ToString();
            DatePicker_DateOfBirth.Value = member.DateOfBirth;

            if (member.Gender == true)
            {
                RadioButton_Male.Checked = true;
                RadioButton_Female.Checked = false;
            }
            else
            {
                RadioButton_Male.Checked = false;
                RadioButton_Female.Checked = true;
            }

        }
        private void bt_Save_Click(object sender, EventArgs e)
        {
            if(DataGridView_Members.SelectedRows != null)
            {
                BLL_Member.Instance.UpdateMember_BLL(GetMember());
                ShowMembers();

            }     
        }
        private Member GetMember()
        {
            Member member = new Member();
            member.ID_Person = Convert.ToInt32(tb_ID.Text.ToString());
            member.Name_Person = tb_Name.Text.ToString();
            member.Address = tb_Anddress.Text.ToString();
            member.Email = tb_Email.Text.ToString();
            member.PhoneNumber = tb_Phone.Text.ToString();
            member.DateOfBirth = DatePicker_DateOfBirth.Value;

            if (RadioButton_Male.Checked == true)
            {
                member.Gender = true;
            }
            else
            {
                member.Gender = false;
            }
            member.UserName = tb_UserName.Text.ToString();
            member.Password = tb_Password.Text.ToString();
            member.ID_Account = Convert.ToInt32(tb_IDAccount.Text.ToString());
            int i = cbb_Positon.SelectedIndex + 1;
            member.ID_Position = i;

            return member;
        }

        private void bt_AddMember_Click(object sender, EventArgs e)
        {
            FormAddMember f = new FormAddMember();
            f.d = new FormAddMember.Mydel(ShowMembers);
            f.ShowDialog();
        }

        private void Search_Members_Click(object sender, EventArgs e)
        {
            DataGridView_Members.DataSource = BLL_Member.Instance.FindPerson_BLL(tb_SearchMember.Text);
            DataGridView_Members.Columns[0].HeaderText = "ID";
            DataGridView_Members.Columns[1].HeaderText = "Name";
            DataGridView_Members.Columns[1].Width = 120;
            DataGridView_Members.Columns[2].HeaderText = "      Gender";
            DataGridView_Members.Columns[3].HeaderText = "Date of Birth";
            DataGridView_Members.Columns[4].HeaderText = "Address";
            DataGridView_Members.Columns[5].HeaderText = "Email";
            DataGridView_Members.Columns[5].Width = 200;
            DataGridView_Members.Columns[6].HeaderText = "Phone number";
            DataGridView_Members.Columns[6].Width = 100;
            DataGridView_Members.Columns[7].HeaderText = "ID Account";
            DataGridView_Members.Columns[8].HeaderText = "User name";
            DataGridView_Members.Columns[9].HeaderText = "Password";
            DataGridView_Members.Columns[10].HeaderText = "Position";
        }
        #endregion

        //--------------------------------------------------------- BOOK

        #region BOOK
        public void ShowManageBook()
        {
            DataGridView_Book.DataSource = BLL_Sach.Instance.GetAllSach_BLL();
            DataGridView_Book.Columns[0].HeaderText = "ID Book";
            DataGridView_Book.Columns[1].HeaderText = "Name Book";
            DataGridView_Book.Columns[1].Width = 150;
            DataGridView_Book.Columns[2].HeaderText = "Category";
            DataGridView_Book.Columns[2].Width = 100;
            DataGridView_Book.Columns[3].HeaderText = "Author";
            DataGridView_Book.Columns[4].HeaderText = "Number of reprints";
            DataGridView_Book.Columns[5].HeaderText = "Year";
            DataGridView_Book.Columns[6].HeaderText = "Import Price";
            DataGridView_Book.Columns[7].HeaderText = "Selling Price";
            DataGridView_Book.Columns[8].HeaderText = "Book";
        }

        private void bt_ManageBooks_Click(object sender, EventArgs e)
        {
            MovePanel(bt_ManageBooks);
            Page_Admin.SetPage("Manage Books");
            ShowManageBook();
        }
        private void DataGridView_Book_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (DataGridView_Book.SelectedRows.Count != 0)
                {
                    int ID_Sach = Convert.ToInt32(DataGridView_Book.SelectedRows[0].Cells["MaSach"].Value);
                    Sach sach = BLL_Sach.Instance.GetSachByID(ID_Sach);
                    SetDetail_Sach(sach);
                }
            }
        }
        private void SetDetail_Sach(Sach sach)
        {
            tb_Book.Text = sach.TongSoLuong.ToString();
            tb_Cetegory.Text = sach.TheLoai.ToString();
            tb_Author.Text = sach.TenTacGia.ToString();
            tb_IDBook.Text = sach.MaSach.ToString();
            tb_ImportPrice.Text = sach.GiaNhap.ToString();
            tb_SellingPrice.Text = sach.GiaBan.ToString();
            tb_PublishingYear.Text = sach.NamXuatBan.ToString();
            tb_NumberOfReprints.Text = sach.SoLanTaiBan.ToString();
            tb_NameBook.Text = sach.TenSach.ToString();
        }

        private void bt_SaveBook_Click(object sender, EventArgs e)
        {  
            if (DataGridView_Book.SelectedRows != null)
            {
                BLL_Sach.Instance.UpdateSach_BLL(GetSach(),ID_PersonL);
                ShowManageBook();
            }    
        }
        private Sach GetSach()
        {  
            Sach sach = new Sach();
            sach.TongSoLuong = Convert.ToInt32(tb_Book.Text.ToString());
            sach.TheLoai = tb_Cetegory.Text;
            sach.TenTacGia = tb_Author.Text;
            sach.MaSach = Convert.ToInt32(tb_IDBook.Text.ToString());
            sach.GiaNhap = Convert.ToInt32(tb_ImportPrice.Text.ToString());
            sach.GiaBan = Convert.ToUInt32(tb_SellingPrice.Text);
            sach.NamXuatBan = tb_PublishingYear.Text;
            sach.SoLanTaiBan = Convert.ToInt32(tb_NumberOfReprints.Text);
            sach.TenSach = tb_NameBook.Text;
            return sach;
        }

        private void bt_DeleteBook_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("      Are you sure delete Book ?", "Confirm delete", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (DataGridView_Book.SelectedRows.Count > 0)
                {
                    List<string> list = new List<string>();
                    foreach (DataGridViewRow i in DataGridView_Book.SelectedRows)
                    {
                        list.Add(i.Cells["MaSach"].Value.ToString());
                    }
                    BLL_Sach.Instance.DelSach_BLL(list);
                }
                ShowManageBook();
            }

        }

        private void bt_AddBookS_Click(object sender, EventArgs e)
        {
            FormAddBooks f = new FormAddBooks(ID_PersonL);
            f.d = new FormAddBooks.Mydel(ShowManageBook);
            f.ShowDialog();

        }
        private void bt_FindBook_Click(object sender, EventArgs e)
        {
            string txt = tb_SearchBook.Text;
            if (string.IsNullOrEmpty(txt))
            {
                DataGridView_Book.DataSource = BLL_Sach.Instance.FindSach_BLL(txt);
            }
            else
            {
                ShowManageBook();
            }
        }

        private void bt_ReloandBook_Click(object sender, EventArgs e)
        {
            ShowManageBook();
        }

        #endregion

        //------------------------------------------------------------------------- SALE

        #region SALE
        private void bt_AddSale_Click(object sender, EventArgs e)
        {
            FormAddSale f = new FormAddSale();
            f.d = new FormAddSale.Mydel(ShowSachKhuyenMai);
            f.ShowDialog();
        }
        private void bt_Sale_Click(object sender, EventArgs e)
        {
            MovePanel(bt_Sale);
            Page_Admin.SetPage("Sale");
            ShowSachKhuyenMai();
        }

        public void ShowSachKhuyenMai()
        {
            DataGridView_SachKhuyenMai.DataSource = BLL_SachKhuyenMai.Instance.GetAllSachKhuyenMai_DAL();
            DataGridView_SachKhuyenMai.Columns[0].HeaderText = "ID Detail";
            DataGridView_SachKhuyenMai.Columns[1].HeaderText = "ID Book";
            DataGridView_SachKhuyenMai.Columns[2].HeaderText = "Discount";
            DataGridView_SachKhuyenMai.Columns[3].HeaderText = "Price";
            DataGridView_SachKhuyenMai.Columns[4].HeaderText = "Price after discount";
            DataGridView_SachKhuyenMai.Columns[5].HeaderText = "Start";
            DataGridView_SachKhuyenMai.Columns[6].HeaderText = "End";

            List<int> list1 = new List<int>();
            List<int> list2 = new List<int>();
            foreach (int i in BLL_SachKhuyenMai.Instance.GetMaSachBySKM())
            {
                list1.Add(i);
            }

            foreach (int i in BLL_Sach.Instance.GetAllMaSach_BLL())
            {
                list2.Add(i);
            }

            IEnumerable<int> list = list2.Except(list1);

            cb_MaSach.Items.Clear();
            foreach (int i in list)
            {
                cb_MaSach.Items.Add(i);
            }
        }

        private void DataGridView_SachKhuyenMai_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (DataGridView_SachKhuyenMai.SelectedRows.Count != 0)
                {
                    int ID = Convert.ToInt32(DataGridView_SachKhuyenMai.SelectedRows[0].Cells["ID_SachKhuyenMai"].Value);
                    SachKhuyenMai sachKM = BLL_SachKhuyenMai.Instance.GetSachKhuyenMaiByID(ID);
                    SetDetail_SKM(sachKM);

                }
            }
        }
        private void SetDetail_SKM(SachKhuyenMai sachKM)
        {
            tb_IdDetail.Text = sachKM.ID_SachKhuyenMai.ToString();
            cb_MaSach.Text = sachKM.MaSach.ToString();
            tb_Discount.Text = sachKM.MucGiamGia.ToString();
            tb_Price.Text = sachKM.Gia.ToString();
            tb_PriceAfterDiscount.Text = Convert.ToDouble(sachKM.MucGiamGia * sachKM.Gia).ToString();
            DatePicker_StartDetail.Value = sachKM.NgayBatDau;
            DatePicker_EndDetail.Value = sachKM.NgayKetThuc;
        }

        private void bt_DeleteDiscount_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("      Are you sure delete Detail ?", "Confirm delete", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (DataGridView_SachKhuyenMai.SelectedRows.Count > 0)
                {
                    List<string> list = new List<string>();
                    foreach (DataGridViewRow i in DataGridView_SachKhuyenMai.SelectedRows)
                    {
                        list.Add(i.Cells["ID_SachKhuyenMai"].Value.ToString());
                    }
                    BLL_SachKhuyenMai.Instance.DeleteSachKhuyenMai_BLL(list);
                }
                ShowSachKhuyenMai();
            }
        }

        private void bt_SaveDiscount_Click(object sender, EventArgs e)
        {
            if(DataGridView_SachKhuyenMai.SelectedRows != null)
            {
                foreach (int i in BLL_Sach.Instance.GetAllMaSach_BLL())
                {
                    if (i != GetSachKhuyenMai().MaSach)
                    {
                        BLL_SachKhuyenMai.Instance.UpdateSachKhuyenMai_BLL(GetSachKhuyenMai());
                        ShowSachKhuyenMai();
                    }
                }

            }    
      
        }
      
        private SachKhuyenMai GetSachKhuyenMai()
        {
               SachKhuyenMai sachKM = new SachKhuyenMai();
               sachKM.ID_SachKhuyenMai = Convert.ToInt32(tb_IdDetail.Text);
               sachKM.MaSach = Convert.ToInt32(cb_MaSach.Text);   
               sachKM.MucGiamGia = Convert.ToDouble(tb_Discount.Text);
               sachKM.NgayBatDau = DatePicker_StartDetail.Value;
               sachKM.NgayKetThuc = DatePicker_EndDetail.Value;
               sachKM.Gia = Convert.ToInt32(tb_Price.Text);
               return sachKM;
        }

        private void cb_MaSach_SelectedIndexChanged(object sender, EventArgs e)
        {
            tb_Price.Text = (BLL_Sach.Instance.GetSachByID(Convert.ToInt32(cb_MaSach.SelectedItem.ToString())).GiaBan).ToString();
            if (tb_Discount.Text != "")
            {
                double Discount = Convert.ToDouble(tb_Discount.Text);
                double Price = Convert.ToDouble(tb_Price.Text);
                double PriceAfterDiscount = Discount * Price;
                tb_PriceAfterDiscount.Text = PriceAfterDiscount.ToString();
            }                 
        }

        private void bt_ReloandSale_Click(object sender, EventArgs e)
        {
            ShowSachKhuyenMai();
        }

        private void tb_Discount_TextChange(object sender, EventArgs e)
        {
            try
            {
                double Discount = Convert.ToDouble(tb_Discount.Text);
                double Price = Convert.ToDouble(tb_Price.Text);
                double PriceAfterDiscount = Discount * Price;
                tb_PriceAfterDiscount.Text = PriceAfterDiscount.ToString();
            }
            catch (Exception)
            {
                return;
            }

        }

        private void cb_MaSach_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void bt_BookDetails_Click(object sender, EventArgs e)
        {
            int ID_Sach = 0;
            if (DataGridView_SachKhuyenMai.SelectedRows.Count != 0)
            {
                ID_Sach = Convert.ToInt32(DataGridView_SachKhuyenMai.SelectedRows[0].Cells["MaSach"].Value);
            }

            if (ID_Sach != 0 )
            {
                new Book_Details(ID_Sach).ShowDialog();
            }
            else
            {
                MessageBox.Show("Click to Book");
            }
        }

    



        #endregion

        //------------------------------------------------------------------ HISTORY

        #region History
        private void bt_History_Click(object sender, EventArgs e)
        {
            MovePanel(bt_History);
            Page_Admin.SetPage("History");
            ShowHistory();
        }
        private void Reloand_History_Click(object sender, EventArgs e)
        {
            ShowHistory();
        }

        private void ShowHistory()
        {
            DataGridView_History.DataSource = BLL_LichSuNhapSach.Instance.GetAllLichSuNhapSach_BLL();
            DataGridView_History.Columns[0].HeaderText = "ID";
            DataGridView_History.Columns[1].HeaderText = "ID Book";
            DataGridView_History.Columns[2].HeaderText = "Number of Book";
            DataGridView_History.Columns[3].HeaderText = "TIME";
            DataGridView_History.Columns[4].HeaderText = "ID Member";
        }

        private void bunifuButton21_Click(object sender, EventArgs e)
        {
            DateTime from = DatePicker_1History.Value;
            DateTime to = DatePicker_2History.Value;
            if (from <= to)
            {
                DataGridView_History.DataSource = BLL_LichSuNhapSach.Instance.GetAllLichSuNhapSach_BLL(from, to);
                DataGridView_History.Columns[0].HeaderText = "ID";
                DataGridView_History.Columns[1].HeaderText = "ID Book";
                DataGridView_History.Columns[2].HeaderText = "Number of Book";
                DataGridView_History.Columns[3].HeaderText = "TIME";
                DataGridView_History.Columns[4].HeaderText = "ID Member";
            }
            else
            {
                MessageBox.Show("Kiểm tra lại ngày bắt đầu hoặc ngày kết thúc");
            }    

        }
        

        #endregion

        //-------------------------------------------------------------- STATISTICS

        #region STATISTICS
        private void bt_Statistics_Click(object sender, EventArgs e)
        {
            MovePanel(bt_Statistics);
            Page_Admin.SetPage("Statistics");
            ShowStatistics();
        }

        private void Reloand_Statistics_Click(object sender, EventArgs e)
        {
            ShowStatistics();
        }


        private void ShowStatistics()
        {
            Statistics statistics = BLL_Statistics.Instance.GetStatistics();
            tb_SoLuongSachNhap.Text = statistics.SoSachNhap.ToString();
            tb_SoLuongSachBan.Text = statistics.SoSachBan.ToString();
            tb_ChiPhi.Text = statistics.ChiPhi.ToString();
            tb_DoanhThu.Text = statistics.DoanhThu.ToString();
            tb_SoLuongHoaDon.Text = statistics.SoHoaDon.ToString();
            //---DESIGN %
            #region design
            chart1.Series[0].Points.Clear();
            chart2.Series[0].Points.Clear();
            int a = statistics.SoSachBan;
            int b = statistics.SoSachNhap;
            double c = 0;
            if (b != 0 && a < b)
            {
                c = (Math.Round((double)a / b, 3) * 100);
                CircleProgress_SoLuongDaBan.Value = Convert.ToInt32(c);
            }
            if (b != 0 && a > b)
            {
                CircleProgress_SoLuongDaBan.Value = 100;
            }
            else
            {
                CircleProgress_SoLuongDaBan.Value = 0;
            }

            chart1.Series[0].Points.AddXY("Purchased Book", a);
            chart1.Series[0].Points[0].Label = a.ToString();
            chart1.Series[0].Points.AddXY("Total Sold Book", b);
            chart1.Series[0].Points[1].Label = b.ToString();

            decimal x = statistics.DoanhThu;
            decimal y = statistics.ChiPhi;
            double z = 0;
            if (y != 0 && x < y)
            {
                z = (Math.Round(Convert.ToDouble(x / y), 3) * 100);
                CircleProgress_DoanhThu.Value = Convert.ToInt32(z);
            }
            if (y != 0 && x > y)
            {
                CircleProgress_DoanhThu.Value = 100;
            }
            else
            {
                CircleProgress_DoanhThu.Value = 0;
            }
            chart2.Series[0].Points.AddXY("Expense", y);
            chart2.Series[0].Points[0].Label = y.ToString();
            chart2.Series[0].Points.AddXY("Total Revenue", x);
            chart2.Series[0].Points[1].Label = x.ToString();
            #endregion
            //----------
        }
        private void bunifuButton22_Click(object sender, EventArgs e)
        {
            if(DatePickerStatistics_Start.Value <= DatePickerStatistics_End.Value)
            {
                Statistics statistics = BLL_Statistics.Instance.GetStatistics(DatePickerStatistics_Start.Value, DatePickerStatistics_End.Value);
                tb_SoLuongSachNhap.Text = statistics.SoSachNhap.ToString();
                tb_SoLuongSachBan.Text = statistics.SoSachBan.ToString();
                tb_ChiPhi.Text = statistics.ChiPhi.ToString();
                tb_DoanhThu.Text = statistics.DoanhThu.ToString();
                tb_SoLuongHoaDon.Text = statistics.SoHoaDon.ToString();
                //---DESIGN %
                #region design
                chart1.Series[0].Points.Clear();
                chart2.Series[0].Points.Clear();
                int a = statistics.SoSachBan;
                int b = statistics.SoSachNhap;
                double c = 0;
                if (b != 0 && a < b)
                {
                    c = (Math.Round((double)a / b, 3) * 100);
                    CircleProgress_SoLuongDaBan.Value = Convert.ToInt32(c);
                }
                if(b != 0 && a > b)
                {
                    CircleProgress_SoLuongDaBan.Value = 100;
                }    
                else
                {
                    CircleProgress_SoLuongDaBan.Value = 0;
                }

                chart1.Series[0].Points.AddXY("Purchased Book", a);
                chart1.Series[0].Points[0].Label = a.ToString();
                chart1.Series[0].Points.AddXY("Total Sold Book", b);
                chart1.Series[0].Points[1].Label = b.ToString();

                decimal x = statistics.DoanhThu;
                decimal y = statistics.ChiPhi;
                double z = 0;
                if (y != 0 && x < y)
                {
                    z = (Math.Round(Convert.ToDouble(x / y), 3) * 100);
                    CircleProgress_DoanhThu.Value = Convert.ToInt32(z);
                }
                if (y != 0 && x > y)
                {
                    CircleProgress_DoanhThu.Value = 100;

                }
                else
                {
                    CircleProgress_DoanhThu.Value = 0;
                }
                chart2.Series[0].Points.AddXY("Expense", y);
                chart2.Series[0].Points[0].Label = y.ToString();
                chart2.Series[0].Points.AddXY("Total Revenue", x);
                chart2.Series[0].Points[1].Label = x.ToString();
                #endregion
                //----------
            }
            else { MessageBox.Show("Kiểm tra lại ngày bắt đầu hoặc ngày kết thúc"); }

        }


        #endregion

        //------------------------------------------------------------------- BILL

        #region BILL
        private void bt_Bill_Click(object sender, EventArgs e)
        {
            MovePanel(bt_Bill);
            Page_Admin.SetPage("Bill");
            ShowAllBill();
        }

        private void Reloand_Bill_Click(object sender, EventArgs e)
        {
            ShowAllBill();
            DataGridView_DetailBill.DataSource = 0;
        }

        private void bt_Detail_Click(object sender, EventArgs e)
        {
            if (DataGridView_Bill.SelectedRows.Count != 0)
            {
                int MaHoaDon = 0;
                foreach (DataGridViewRow i in DataGridView_Bill.SelectedRows)
                {
                MaHoaDon = Convert.ToInt32(i.Cells["MaHoaDon"].Value);
                }
                DataGridView_DetailBill.DataSource = BLL_Bill.Instance.GetBillByID_BLL(MaHoaDon);
            }  
        }

        private void ShowAllBill()
        {
            DataGridView_Bill.DataSource = BLL_Bill.Instance.GetAllBill_BLL();
            DataGridView_Bill.Columns[0].HeaderText = "ID Bill";
            DataGridView_Bill.Columns[1].HeaderText = "DATE";
            DataGridView_Bill.Columns[2].HeaderText = "Amount";
            DataGridView_Bill.Columns[3].HeaderText = "ID Member";
        }

        private void bt_BillOk_Click(object sender, EventArgs e)
        {
            DateTime from = DatePicker_1Bill.Value;
            DateTime to   = DatePicker_2Bill.Value;

            if(from < to)
            {
                DataGridView_DetailBill.DataSource = 0;
                DataGridView_Bill.DataSource = BLL_Bill.Instance.GetAllBill_BLL(from,to);
                DataGridView_Bill.Columns[0].HeaderText = "ID Bill";
                DataGridView_Bill.Columns[1].HeaderText = "DATE";
                DataGridView_Bill.Columns[2].HeaderText = "Amount";
                DataGridView_Bill.Columns[3].HeaderText = "ID Member";
            } 
            else
            {
                MessageBox.Show("Kiểm tra lại ngày bắt đầu hoặc ngày kết thúc");
            }    
        }


        #endregion


    }
}
