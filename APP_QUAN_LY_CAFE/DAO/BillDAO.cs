using APP_QUAN_LY_CAFE.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP_QUAN_LY_CAFE.DAO
{
    class BillDAO
    {
        private static BillDAO instance;
        public static BillDAO Instance
        {
            get { if (instance == null) instance = new BillDAO(); return BillDAO.instance; }
            private set { BillDAO.instance = value; }
        }
        private BillDAO()
        {

        }
        public int GetUncheckBillTableID(int id)
        {
            string query = "SELECT * FROM Bill WHERE idTabe = " + id + " AND status = 0";
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            if (data.Rows.Count > 0)
            {
                Bill bill = new Bill(data.Rows[0]);
                return bill.ID;
            }
            return -1;
        }
        public void CheckOut(int id, int discount, float TotalPrice)
        {
            
            string query = "UPDATE Bill SET dateCheckOut = GETDATE(), TotalPrice =" + TotalPrice + " , status = 1, " + "Discount =" + discount + "WHERE id = " + id;
            DataProvider.Instance.ExcuteNonQuery(query);
        }
        public void InsertBill(int id)
        {
            DataProvider.Instance.ExcuteNonQuery("USP_InsertBill @idTable ", new object[] { id});

        }
        public int GetMaxIDBill()
        {
            try
            {
                return (int)DataProvider.Instance.ExcuteScalar("select MAX(id) from Bill");
            }
            catch
            {
                return 1;
            }
        }
        public DataTable GetBillListByDate(DateTime CheckIn, DateTime CheckOut)
        {
            string query = "USP_GetListBillByDate @checkIn , @checkOut ";
            return  DataProvider.Instance.ExcuteQuery(query, new object[] { CheckIn, CheckOut });
        }
        public DataTable GetBillListByDateAndPage(DateTime CheckIn, DateTime CheckOut, int page)
        {
            string query = "USP_GetListBillByDateAndPage @checkIn , @checkOut , @page ";
            return DataProvider.Instance.ExcuteQuery(query, new object[] { CheckIn, CheckOut,page });
        }
        public int GetNumBillListByDate(DateTime CheckIn, DateTime CheckOut)
        {
            string query = "USP_GetNumBillByDate @checkIn , @checkOut ";
            return (int)DataProvider.Instance.ExcuteNonQuery(query, new object[] { CheckIn, CheckOut });
        }
    }
    
}
