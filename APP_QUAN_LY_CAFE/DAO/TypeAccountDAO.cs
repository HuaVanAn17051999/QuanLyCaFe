using APP_QUAN_LY_CAFE.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP_QUAN_LY_CAFE.DAO
{
    class TypeAccountDAO
    {
        private static TypeAccountDAO intstance;
        public static TypeAccountDAO Intstance
        {
            get { if (intstance == null) intstance = new TypeAccountDAO(); return TypeAccountDAO.intstance; }
            private set { TypeAccountDAO.intstance = value; }
        }
        private TypeAccountDAO() { }
        public List<TypeAccount> LoadlistAccount()
        {
            List<TypeAccount> typeAccount = new List<TypeAccount>();
            string query = "select * from TypeAccount";
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            foreach(DataRow item in data.Rows)
            {
                TypeAccount account = new TypeAccount(item);
                typeAccount.Add(account);
            }
            return typeAccount;
        }
        public TypeAccount GetCategoryAccountByID(int id)
        {
            TypeAccount typeAccount = null;

            string query = "select * from TypeAccount where Type = " + id;

            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                typeAccount = new TypeAccount(item);
                return typeAccount;
            }
            return typeAccount;
        }
    }
}
