using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP_QUAN_LY_CAFE.DTO
{
    public class Account
    {
        public string UserName { get; private set; }
        public string DisplayName { get; private set; }
        public string PassWord { get; private set; }
        public int Type { get; private set; }
        public Account(string username , string displaynamne, int type, string password = null)
        {
            this.UserName = username;
            this.DisplayName = displaynamne;
            this.Type = type;
            this.PassWord = password;
        }
        public Account(DataRow row)
        {
            this.UserName = row["Username"].ToString();
            this.DisplayName = row["DisplayName"].ToString();   
            this.Type = (int)row["Type"];
            this.PassWord = row["PassWord"].ToString();
        }
    }
}
