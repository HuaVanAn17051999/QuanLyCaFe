using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP_QUAN_LY_CAFE.DTO
{
    public class TypeAccount
    {
        public int ID { get; private set; }
        public string Name { get; private set; }
        public TypeAccount(int id, string name)
        {
            this.ID = id;
            this.Name = name;
        }
        public TypeAccount(DataRow row)
        {
            this.ID = (int)row["Type"];
            this.Name = row["Name"].ToString();
        }


        
    }
}
