using QuanLyThuVienSach.BILL.BILL_ADMIN;
using QuanLyThuVienSach.DTO.DTO_ADMIN;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuVienSach.GUI.GUI_ADMIN
{
    public partial class FormAddMember : Form
    {
        public delegate void Mydel();
        public Mydel d { get; set; }
        public FormAddMember()
        {
             InitializeComponent();         
        }
     
        private void bt_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bt_AddMember_Click(object sender, EventArgs e)
        {
            Member member = new Member();
            member.UserName = tb_UserName.Text.ToString();
            member.Password = tb_Password.Text.ToString();
            member.ID_Position = comboBox_Position.SelectedIndex + 1;
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

            BLL_Member.Instance.AddMember_BLL(member);
            d();
        }

        private void bt_Clear_Click(object sender, EventArgs e)
        {
            tb_Anddress.Text = "";
            tb_Email.Text = "";
            tb_Name.Text = "";
            tb_Password.Text = "";
            tb_Phone.Text = "";
            tb_UserName.Text = "";
            comboBox_Position.Text = "";
            RadioButton_Femalee.Checked = false ;
            RadioButton_Male.Checked = false;
                
        }
    }
}
