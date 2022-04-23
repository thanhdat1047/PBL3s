﻿using QuanLyThuVienSach.BILL.BLL_Login;
using QuanLyThuVienSach.DTO.DTO_Login;
using QuanLyThuVienSach.GUI;
using QuanLyThuVienSach.GUI.GUI_THUKHO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace QuanLyThuVienSach
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        private void tb_PASSWORD_Click(object sender, EventArgs e)
        {
            if (tb_USERNAME.Text == "")
            {
                tb_USERNAME.PlaceholderText = "USERNAME OR EMAIL";

            }
            tb_PASSWORD.PlaceholderText = "";
            tb_PASSWORD.PasswordChar = '•';
        }

        private void bt_Show_MouseDown(object sender, MouseEventArgs e)
        {
            if (tb_PASSWORD.Text != "")
            {
                tb_PASSWORD.PasswordChar = '\0';
            }
        }
        private void bt_Show_MouseUp(object sender, MouseEventArgs e)
        {  
            if (tb_PASSWORD.Text != "")
            {
                tb_PASSWORD.PasswordChar = '•';
            }
        }

        private void lb_SighUp_Click(object sender, EventArgs e)
        {
            this.Hide();
            new SighUp().ShowDialog();
        }

        private void bt_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tb_USERNAME_Click(object sender, EventArgs e)
        {
            tb_USERNAME.PlaceholderText = "";
            if(tb_PASSWORD.Text =="")
            {
                tb_PASSWORD.PasswordChar = '\0';
                tb_PASSWORD.PlaceholderText = "PASSWORD";
            }
          
        }

        private void bt_LogIn_Click(object sender, EventArgs e)
        {
            string username = tb_USERNAME.Text;
            string password = tb_PASSWORD.Text;
            foreach (Account i in BLL_Login.Instance.GetAllAccount_BLL())
            {
                if (username == i.UserName && password == i.Password)
                {
                    if (i.ID_Position == 1)
                    {
                        this.Hide();
                        new Admin(i.ID_Account,i.ID_Person).ShowDialog();
                    }
                    if (i.ID_Position == 2)
                    {
                        this.Hide();
                        new ThuKho().ShowDialog();
                    }
                    if (i.ID_Position == 3)
                    {
                    }
                    if (i.ID_Position == 4)
                    {
                    }
                    else
                    {
                        break;
                    }

                }

            }

           
        }
    }
}
