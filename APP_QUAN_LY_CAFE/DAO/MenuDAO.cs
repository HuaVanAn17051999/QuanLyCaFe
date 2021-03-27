using APP_QUAN_LY_CAFE.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP_QUAN_LY_CAFE.DAO
{
    class MenuDAO
    {
        private static MenuDAO instance;
        public static MenuDAO Instance
        {
            get { if (instance == null) instance = new MenuDAO(); return MenuDAO.instance; }
            private set { MenuDAO.instance = value; }
        }
        private MenuDAO() { }
        public List<Menus> GetListMenuByTable(int id)
        {
            List<Menus> listMenu = new List<Menus>();
            string query = "USP_GetListMenuByTableID @idTable";
            DataTable da = DataProvider.Instance.ExcuteQuery(query, new object[] { id });
            foreach(DataRow item in da.Rows)
            {
                Menus menu = new Menus(item);
                listMenu.Add(menu);
            }
            return listMenu;

        }
    }
}
