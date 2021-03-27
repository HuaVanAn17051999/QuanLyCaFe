using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP_QUAN_LY_CAFE.DTO
{
    public class Menus
    {
        public string FoodName { get; private set; }
        public int Count { get; private set; }
        public float Price { get; private set; }
        public float TotalPrice { get; private set; }
        public Menus (string foodName, int count , float price, float totalPrice)
        {
            this.FoodName = foodName;
            this.Count = count;
            this.Price = price;
            this.TotalPrice = totalPrice;
        }
        public Menus(DataRow row)
        {
            this.FoodName = row["name"].ToString();
            this.Count = (int)row["count"];
            this.Price = (float)Convert.ToDouble(row["price"]);
            this.TotalPrice = (float)Convert.ToDouble(row["TotalPrice"]); ;
        }
    }
}
