﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVienSach.DAL.DAL_ADMIN
{
    internal class DBHelper
    {
        private static DBHelper _Instance;
        public static DBHelper Instance
        {
            get
            {
                if (_Instance == null)
                {
                    //string cnnstring = @"Data Source=DESKTOP-BP0TSS8;Initial Catalog=QuanLyThuVienSach;Integrated Security=True";
                    string cnnstring = @"Data Source=LAPTOP-TUNGSDPF\SQLEXPRESS;Initial Catalog=QLSVNEW;Integrated Security=True";
                    _Instance = new DBHelper(cnnstring);
                }
                return _Instance;

            }
            private set { }

        }

        private SqlConnection cnn;


        private DBHelper(string s)
        {
            cnn = new SqlConnection(s);
        }

        public void ExecuteDB(string query)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(query, cnn);
                cnn.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Loi");
                Console.WriteLine(ex.Message.ToString());
            }
            finally
            {
                cnn.Close();
            }
        }

        public DataTable GetRecord(string query)
        {
            DataTable DT = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(query, cnn);
            cnn.Open();
            dataAdapter.Fill(DT);
            cnn.Close();
            return DT;

        }
    }

}
