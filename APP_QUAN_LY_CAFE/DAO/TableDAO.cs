using APP_QUAN_LY_CAFE.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP_QUAN_LY_CAFE.DAO
{
    class TableDAO
    {
        private static TableDAO instance;
        public static TableDAO Instance 
        {
            get { if (instance == null) instance = new TableDAO();return TableDAO.instance; }
            private set { TableDAO.instance = value; }
        }
        private TableDAO() { }
        public static int TableHeight = 90;
        public static int TableWidth = 90;

        public List<Table> LoadTableList()
        {
            List<Table> listTable = new List<Table>();

            DataTable data = DataProvider.Instance.ExcuteQuery("USP_GetTableList");

            foreach(DataRow item in data.Rows)
            {
                Table table = new Table(item);
                listTable.Add(table);
            }
            return listTable;

        }
            

    }
}
