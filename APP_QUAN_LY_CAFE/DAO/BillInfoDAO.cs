﻿using APP_QUAN_LY_CAFE.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP_QUAN_LY_CAFE.DAO
{
    public class BillInfoDAO
    {
        private static BillInfoDAO instance;
        public static BillInfoDAO Instance
        {
            get { if (instance == null) instance = new BillInfoDAO(); return BillInfoDAO.instance; }
            private set { BillInfoDAO.instance = value; }
        }
        private BillInfoDAO() { }
        public List<BillInfo> GetListBillInfo(int id)
        {
            List<BillInfo> listbillInfo = new List<BillInfo>();

            string query = "SELECT * FROM BillInfo WHERE idBill = " + id;

            DataTable data = DataProvider.Instance.ExcuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                BillInfo info = new BillInfo(item);
                listbillInfo.Add(info);
            }
            return listbillInfo;
        }
        public void InsertBillInfo(int idBill, int idFood, int count)
        {
            DataProvider.Instance.ExcuteNonQuery("USP_InsertBillInfo @idBill , @idFood , @count", new object[] { idBill, idFood, count });
        }
        public void DeleteBillInfoByFoodID(int idFood)
        {

            string query = string.Format("DELETE BillInfo WHERE idFood = {0}", idFood);
            DataProvider.Instance.ExcuteQuery(query);
        }
    }
}
