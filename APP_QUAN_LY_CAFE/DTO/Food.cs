using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP_QUAN_LY_CAFE.DTO
{
    public class Food
    {
        public int ID { get; private set; }
        public string Name { get; private set; }
        public int IDCategory { get; private set; }
        public float Price { get; private set; }
        public Food(int id , string name, int idCategory , float price)
        {
            this.ID = id;
            this.Name = name;
            this.IDCategory = idCategory;
            this.Price = price;
        }
        public Food(DataRow row)
        {
            this.ID = (int)row["id"];
            this.Name = row["name"].ToString();
            this.IDCategory = (int)row["idCategory"];
            this.Price = (float)Convert.ToDouble(row["price"]);
        }
    }
}
