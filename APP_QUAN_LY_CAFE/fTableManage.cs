using APP_QUAN_LY_CAFE.DAO;
using APP_QUAN_LY_CAFE.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APP_QUAN_LY_CAFE
{
    public partial class fTableManage : Form
    {
        private Account loginAccount;
        public Account LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; ChangeAccount(loginAccount.Type); }
        }
        public fTableManage( Account acc)
        {
            InitializeComponent();

            this.LoginAccount = acc;
            LoadTable();
            LoadCategory();
        }
        #region Method
        void ChangeAccount (int Type)
        {
            adminToolStripMenuItem.Enabled = Type == 1;
            thongtinTk.Text += "(" + LoginAccount.DisplayName + ")";
        }
        void LoadTable()
        {
            List<Table> tableList = TableDAO.Instance.LoadTableList();

            foreach (Table item in tableList)
            {
                Button btn = new Button() { Width = TableDAO.TableWidth, Height = TableDAO.TableHeight };
                btn.Text = item.Name + Environment.NewLine + item.Status;
                btn.Click += btn_Click;
                btn.Tag = item;
                switch (item.Status)
                {
                    case "Trống":
                        btn.BackColor = Color.Aqua;
                        break;
                    default:
                        btn.BackColor = Color.Yellow;
                        break;
                }
                flpTable.Controls.Add(btn);
            }
        }
        void LoadCategory()
        {
            List<Category> list = CategoryDAO.Instance.GetListFoddCategory();
            cbbCategory.DataSource = list;
            cbbCategory.DisplayMember = "name";
        }
        void LoadFoodListByCategory(int id)
        {
            List<Food> data = FoodDAO.Instance.GetFoodByCategoryID(id);
            cbbFodd.DataSource = data;
            cbbFodd.DisplayMember = "name";
        }
        void ShowBill(int id)
        {
            flpTable.Controls.Clear();
            listViewBill.Items.Clear();
            List<Menus> listMenu = MenuDAO.Instance.GetListMenuByTable(id);
            float TotalPrice = 0;
            foreach(Menus item in listMenu)
            {
                ListViewItem lisvItem = new ListViewItem(item.FoodName.ToString());
                lisvItem.SubItems.Add(item.Count.ToString());
                lisvItem.SubItems.Add(item.Price.ToString());
                lisvItem.SubItems.Add(item.TotalPrice.ToString());
                TotalPrice += item.TotalPrice;
                listViewBill.Items.Add(lisvItem);
            }
            CultureInfo culture = new CultureInfo("vi-VN");
            txtTotalPrice.Text = TotalPrice.ToString("c", culture);

        }
        #endregion

        #region Event
        private void cbbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;
            ComboBox cbb = sender as ComboBox;
            if (cbb.SelectedItem == null)
                return;
            Category selected = cbb.SelectedItem as Category;
            id = selected.ID;
            LoadFoodListByCategory(id);

        }
        private void btn_Click(object sender, EventArgs e)
        {
            int tabledID = ((sender as Button).Tag as Table).ID;
            listViewBill.Tag = (sender as Button).Tag;
            ShowBill(tabledID);
            LoadTable();
        }
        private void btnAddFood_Click(object sender, EventArgs e)
        {
            Table table = listViewBill.Tag as Table;
            if(table == null)
            {
                MessageBox.Show("Hãy chọn bàn.");
                return;
            }
            int idBill = BillDAO.Instance.GetUncheckBillTableID(table.ID);
            int foodID = (cbbFodd.SelectedItem as Food).ID;
            int count = (int)nmCount.Value;

            if (idBill == -1)
            {
                BillDAO.Instance.InsertBill(table.ID);
                BillInfoDAO.Instance.InsertBillInfo(BillDAO.Instance.GetMaxIDBill(), foodID, count);
            }
            else
            {
                BillInfoDAO.Instance.InsertBillInfo(idBill, foodID, count);
            }
            ShowBill(table.ID);
            LoadTable();
        }
        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            Table table = listViewBill.Tag as Table;

            int idBill = BillDAO.Instance.GetUncheckBillTableID(table.ID);
            int discount = (int)nmdiscount.Value;
            float totalPrice = (float)Convert.ToDouble(txtTotalPrice.Text.Split(',')[0]);
            float finalTotalPrice = totalPrice - (totalPrice / 100) * discount;

            if (idBill != -1)
            {
                if (MessageBox.Show(string.Format("Bạn có chắc thanh toán hóa đơn cho bàn {0}\nTổng tiền - (Tổng tiền / 100) x Giảm giá = {1} - ({1} / 100) x {2} = {3} ", table.Name, totalPrice, discount, finalTotalPrice), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    BillDAO.Instance.CheckOut(idBill, discount, finalTotalPrice);
                    ShowBill(table.ID);
                    LoadTable();
                }
            }
        }
        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAdmin admin = new fAdmin();
            admin.loginAccount = LoginAccount;
            admin.InsertFood += f_InserFood;
            admin.UpdateFood += f_UpdateFood;
            admin.DeleteFood += f_DeleteFood;
            this.Hide();
            admin.ShowDialog();
            this.Show();
        }

        private void f_DeleteFood(object sender, EventArgs e)
        {
            LoadFoodListByCategory((cbbCategory.SelectedItem as Category).ID);
            if (listViewBill.Tag != null)
                ShowBill((listViewBill.Tag as Table).ID);
            LoadTable();
        }

        private void f_UpdateFood(object sender, EventArgs e)
        {
            LoadFoodListByCategory((cbbCategory.SelectedItem as Category).ID);
            if (listViewBill.Tag != null)
                ShowBill((listViewBill.Tag as Table).ID);
            LoadTable();
        }

        private void f_InserFood(object sender, EventArgs e)
        {
            LoadFoodListByCategory((cbbCategory.SelectedItem as Category).ID);
            if (listViewBill.Tag != null)
                ShowBill((listViewBill.Tag as Table).ID);
        }

        private void ThongTincanhanMenuItem_Click(object sender, EventArgs e)
        {
            fAccountProfile fAccount = new fAccountProfile(LoginAccount);
            fAccount.UpdateAccounts += fAccount_UpdateAccount;
            fAccount.ShowDialog();
        }

        private void fAccount_UpdateAccount(object sender, EventArgs e)
        {
         //   thongtinTk.Text += "(" + e. + ")";
        }

        private void DangXuatMenuItem_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.Show();
            

        }
        private void thanhToánToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnCheckOut_Click(this, new EventArgs());
        }
        private void thêmMónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnAddFood_Click(this, new EventArgs());
        }
        #endregion


    }
}
