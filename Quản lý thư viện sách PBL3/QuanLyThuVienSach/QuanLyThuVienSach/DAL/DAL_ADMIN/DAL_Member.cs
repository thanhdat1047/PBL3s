using QuanLyThuVienSach.DTO.DTO_ADMIN;
using System;
using System.Data;


namespace QuanLyThuVienSach.DAL.DAL_ADMIN
{
    internal class DAL_Member
    {
        private static DAL_Member _Instance;
        public static DAL_Member Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new DAL_Member();
                }
                return _Instance;
            }
            private set { }
        }
        private DAL_Member() { }

        public DataTable GetAllMembers_DAL()
        {
            string query =
                " SELECT ID_Person,Name_Person,Gender,DateOfBirth,Address,Email,PhoneNumber,Person.ID_Account, UserName, Password, Name_Position" +
                " FROM Person,Account,Position" +
                " WHERE Person.ID_Account = Account.ID_Account AND Account.ID_Position = Position.ID_Position ";
            return DBHelper.Instance.GetRecord(query);
        }

        public void DeleteMembers_DAL(int ID_Person)
        {
            string query = $"Select ID_Account from Person where ID_Person = {ID_Person}";
            DataTable dt = DBHelper.Instance.GetRecord(query);

            int ID_Account = 0;
            foreach (DataRow i in dt.Rows)
            {
                ID_Account = Convert.ToInt32(i["ID_Account"]);
            }

            string query1 = $"Delete from Person where ID_Person = {ID_Person}";
            DBHelper.Instance.ExecuteDB(query1);

            string query2 = $"Delete from Account where ID_Account = {ID_Account}";
            DBHelper.Instance.ExecuteDB(query2);
        }

        public void UpdateMember_DAL(Member member)
        {
            string query = $" UPDATE dbo.Person SET Name_Person = '{member.Name_Person}',Gender = '{member.Gender}', DateOfBirth = '{member.DateOfBirth}', Address = '{member.Address}', Email= '{member.Email}', PhoneNumber ='{member.PhoneNumber}'" +
                            $" WHERE ID_Person = {member.ID_Person}; ";
            DBHelper.Instance.ExecuteDB(query);

            string query1 = $" UPDATE dbo.Account SET UserName = '{member.UserName}', Password ='{member.Password}',ID_Position = '{member.ID_Position}' " +
              $" WHERE ID_Account = '{member.ID_Account}'; ";
            DBHelper.Instance.ExecuteDB(query1);            
        }

        public void AddMember_DAL(Member member)
        {
            string query = $"INSERT INTO dbo.Account VALUES( '{member.UserName}', '{member.Password}', {member.ID_Position} )";
            DBHelper.Instance.ExecuteDB(query);

            string query1 = "SELECT TOP 1 ID_Account FROM Account ORDER BY ID_Account DESC";

            int ID_Account = 0;
            foreach (DataRow i in DBHelper.Instance.GetRecord(query1).Rows)
            {
                ID_Account = Convert.ToInt32(i[0]);
            }

            string query2 = $"INSERT INTO dbo.Person VALUES ( '{member.Name_Person}', '{member.Gender}', '{member.DateOfBirth}', '{member.Address}', '{member.Email}', '{member.PhoneNumber}', {ID_Account})";
            DBHelper.Instance.ExecuteDB(query2);
        }

        public int GetID_Position(string txt)
        {
            string query = $" Select ID_Position from Position " +
                           $" Where Name_Position = '{txt}'";
            DataTable dt = DBHelper.Instance.GetRecord(query);

            int ID_Position = 0;
            foreach (DataRow i in dt.Rows)
            {
                ID_Position = Convert.ToInt32(i["ID_Position"]);
            }
            return ID_Position;
        }

        public void UpdatePerson(Person person)
        {
            string query = $" UPDATE dbo.Person SET Name_Person = '{person.Name_Person}',Gender = '{person.Gender}', DateOfBirth = '{person.DateOfBirth}', Address = '{person.Address}', Email= '{person.Email}', PhoneNumber ='{person.PhoneNumber}'" +
                           $" WHERE ID_Person = {person.ID_Person}; ";
            DBHelper.Instance.ExecuteDB(query);
        }
        public void UpdateAccount(Account account)
        {
            string query = $" UPDATE dbo.Account SET UserName = '{account.UserName}', Password ='{account.Password}'" +
                           $" WHERE ID_Account = '{account.ID_Account}'; ";
            DBHelper.Instance.ExecuteDB(query);
        }
        public DataTable FindPerson_DAL(string txt)
        {
            string query =
              " SELECT ID_Person,Name_Person,Gender,DateOfBirth,Address,Email,PhoneNumber,Person.ID_Account, UserName, Password, Name_Position" +
              " FROM Person,Account,Position" +
              " WHERE Person.ID_Account = Account.ID_Account AND Account.ID_Position = Position.ID_Position" +
             $" AND Name_Person LIKE '%{txt}%'  ";
            return DBHelper.Instance.GetRecord(query);

        }



    }
}
