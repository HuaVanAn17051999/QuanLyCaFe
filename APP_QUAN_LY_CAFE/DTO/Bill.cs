using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP_QUAN_LY_CAFE.DTO
{
    public class Bill
    {
        public int ID { get; private set; }
        public DateTime? DateCheckIN { get; private set; }
        public DateTime? DateCheckOUT { get; private set; }
        public int IDTable { get; private set; }
        public int Status { get; private set; }
        public int Discount { get; private set; }
        public Bill (int id, DateTime? dateCheckIn, DateTime? dateCheckOut, int idTable, int status, int discount)
        {
            this.ID = id;
            this.DateCheckIN = dateCheckIn;
            this.DateCheckIN = dateCheckOut;
            this.IDTable = idTable;
            this.Status = status;
            this.Discount = discount;
        }
        public Bill(DataRow row)
        {
            this.ID = (int)row["id"];
            this.DateCheckIN = (DateTime?)row["DateCheckIn"];

            var dateCheckOutTemp = row["DateCheckOut"];
            if(dateCheckOutTemp.ToString() != "")
            {
                this.DateCheckOUT = (DateTime?)dateCheckOutTemp;
            }

            this.IDTable = (int)row["idTabe"];
            this.Status = (int)row["status"];
            if (row["discount"].ToString() != "")
            {
            this.Discount = (int)row["discount"];
            }
        }

    }
}
