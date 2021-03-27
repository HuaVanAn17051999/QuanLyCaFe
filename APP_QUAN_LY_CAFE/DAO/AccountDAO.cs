using APP_QUAN_LY_CAFE.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace APP_QUAN_LY_CAFE.DAO
{
    class AccountDAO
    {
        private static AccountDAO instance;
        public static AccountDAO Instance
        {
            get { if (instance == null) instance = new AccountDAO(); return AccountDAO.instance; }
            private set { AccountDAO.instance = value; }
        }
        private AccountDAO() { }
        public bool Login(string username, string password)
        {
            byte[] temp = ASCIIEncoding.ASCII.GetBytes(password);
            byte[] hasData = new MD5CryptoServiceProvider().ComputeHash(temp);

            string hassPass = "";
            foreach(byte item in hasData)
            {
                hassPass += item;
            }

            //var list = hasData.ToList();
            //list.Reverse();
            string query = "USP_LOGIN @username , @password";
            DataTable dt = DataProvider.Instance.ExcuteQuery(query,new object[] { username, hassPass /*password*/ });
            return dt.Rows.Count > 0;

        }
        public Account GetAccountByUsername(string username)
        {
            
            string query = "select * from Account Where Username = " + username;
            DataTable data = DataProvider.Instance.ExcuteQuery("select * from Account Where Username = '" + username + "'");

            foreach(DataRow item in data.Rows)
            {
                return new Account(item);
            }
            return null;
        }
        public bool UpdateAccount(string Username , string DisplayName, string Pass, string NewPass)
        {
            int result = DataProvider.Instance.ExcuteNonQuery("EXEC USP_UpdateAccount @userName , @displayName , @Password , @NewPassword", new object[] { Username, DisplayName, Pass, NewPass });
            return result > 0;
        }      
        public DataTable GetListAccount()
        {
            return DataProvider.Instance.ExcuteQuery("select Username, DisplayName, Type from Account ");
        }
        public bool InsertAccountType(string username, string displayName, int Type)
        {
            string query = string.Format("INSERT INTO Account(Username, DisplayName, Type) VALUES(N'{0}', N'{1}',{2})", username, displayName, Type);

            int result = (int)DataProvider.Instance.ExcuteNonQuery(query);
            return result > 0;
        }
        public bool UpdateAccountType(string username, string displayName, int Type)
        {
            string query = string.Format("UPDATE Account SET DisplayName = N'{0}', Type = {1} WHERE Username = N'{2}'", displayName, Type, username);
            int result = (int)DataProvider.Instance.ExcuteNonQuery(query);

            return result > 0;
        }
        public bool DeleteAccountType(string username)
        {
            string query = string.Format("DELETE  Account WHERE Username = N'{0}'", username);
            int result = DataProvider.Instance.ExcuteNonQuery(query);
            return result > 0;
        }
        public bool ResetPassword(string username)
        {
            string query = string.Format("UPDATE Account SET PassWord = N'0' Where Username = {0}", username);
            int result = DataProvider.Instance.ExcuteNonQuery(query);
            return result > 0;
        }
    }
}
