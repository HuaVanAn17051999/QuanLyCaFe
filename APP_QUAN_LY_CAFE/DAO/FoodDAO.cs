using APP_QUAN_LY_CAFE.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP_QUAN_LY_CAFE.DAO
{
    class FoodDAO
    {
        private static FoodDAO instance;
        public static FoodDAO Instance
        {
            get { if (instance == null) instance = new FoodDAO(); return FoodDAO.instance; }
            private set { FoodDAO.instance = value; }
        }
        private FoodDAO() { }
        public List<Food> GetListFood()
        {
            List<Food> listFood = new List<Food>();

            string query = "SELECT * FROM Food";
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                listFood.Add(food);
            }
            return listFood;
        }
        public List<Food> SearchFoodByName(string FoodName)
        {
            List<Food> listFood = new List<Food>();

            string query = string.Format("select * from Food where name like N'%{0}%'", FoodName);
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                listFood.Add(food);
            }
            return listFood;
        }
        public List<Food> GetFoodByCategoryID(int id)
        {
            List<Food> list = new List<Food>();
            string query = "SELECT * FROM Food WHERE idCategory = " + id;
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                list.Add(food);
            }
            return list;
        }
        public bool InsertFood(int idCategory, string name, float price)
        {
            string query = string.Format("INSERT INTO Food (idCategory, name, price) VALUES ({0},N'{1}',{2})", idCategory,name,price);
            int result = (int)DataProvider.Instance.ExcuteNonQuery(query);
            return result > 0;
        }
        public bool UpdateFood(int id, int idCategory, string name, float price)
        {
            string query = string.Format("UPDATE Food SET idCategory = {0} , name = N'{1}', price = {2} WHERE id = {3}", idCategory, name, price, id);
            int result = (int)DataProvider.Instance.ExcuteNonQuery(query);
            return result > 0;
        }
        public bool DeleteFood(int idFood)
        {
            BillInfoDAO.Instance.DeleteBillInfoByFoodID(idFood);

            string query = string.Format("DELETE Food WHERE id = {0}", idFood);
            int result = (int)DataProvider.Instance.ExcuteNonQuery(query);

            return result > 0;


        }
      


    }
}
