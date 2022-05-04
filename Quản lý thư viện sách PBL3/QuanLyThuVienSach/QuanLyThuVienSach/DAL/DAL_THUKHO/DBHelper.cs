using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace QuanLyThuVienSach.DAL.DAL_THUKHO
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
                    string s = @"Data Source=LAPTOP-TUNGSDPF\SQLEXPRESS;Initial Catalog=QuanLyThuVienSach;Integrated Security=True";
                    _Instance = new DBHelper(s);
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
            try
            {

                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, cnn);
                cnn.Open();
                dataAdapter.Fill(DT);

            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {

                cnn.Close();

            }
            return DT;

        }
    }
}

