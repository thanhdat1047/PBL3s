using QuanLyThuVienSach.BILL.BLL_Login;
using QuanLyThuVienSach.DAL.DAL_Login;
using QuanLyThuVienSach.DTO.DTO_Login;
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
    public partial class SighUpNext : Form
    {
        public int ID_Account { get; set; }
        public SighUpNext()
        {
            InitializeComponent();
        }

        public SighUpNext(int ID_account)
        {
            InitializeComponent();
            ID_Account = ID_account;
        }

        private void bt_Back_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("     Are you sure Back ?", "Confirm delete", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                DAL_Login.Instance.DeleteAccount_DAL(ID_Account);
                this.Close();
                new SighUp().Show();
            }
                  
        }

        private void bt_SighUp_Click(object sender, EventArgs e)
        {
            BLL_Login.Instance.AddPerson(GetPerson());

        }

        private Person GetPerson()
        {
            Person person = new Person();
            person.Name_Person = tb_Name.Text;
            person.Email = tb_Email.Text;
            person.Address = tb_Anddress.Text;
            person.PhoneNumber = tb_Phone.Text;
            person.Gender = RadioButton_Male.Checked;
            person.DateOfBirth = DatePicker_DateOfBirth.Value;
            return person;

        }
    }
}
