using APP_QUAN_LY_CAFE.DAO;
using APP_QUAN_LY_CAFE.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APP_QUAN_LY_CAFE
{
    public partial class fAdmin : Form
    {
        BindingSource foodList = new BindingSource();
        public Account loginAccount;
        public fAdmin()
        {
            InitializeComponent();
            Loads();

          //  
          // LoadTable();
          //  LoadListFood();
          //  
          //Binding category and loaCategory

            //     BindingCategory();
            //  end
            //Binding Food and loadFood


            //end

            //   BindingTable();
            //  

            //  dgvAccount.DataSource = accountList;
        }
        void Loads()
        {
            dgvFood.DataSource = foodList;
            LoadListBillByDate(dtpkFormDate.Value, dtpkToDate.Value);
            LoadListFood();
            LoadCategory();
            LoadFoodCategory();
            BindingFood();
            LoadAccount();
            BindingAccount();
            LoadCategoryAccount();
            LoadTypeAccount();
            BindingTypeAccount();
        }
        #region Method
        void DeleteAccount(string username)
        {
            if (loginAccount.UserName.Equals(username))
            {
                MessageBox.Show("Tài khoản đang đăng nhập. Không được xóa.");
                return;
            }
            if (AccountDAO.Instance.DeleteAccountType(username))
            {
                MessageBox.Show("Xóa thành công.");
                LoadAccount();
            }
            else
            {
                MessageBox.Show("Xóa thất bại.");
            }
        }
        void LoadListBillByDate (DateTime checkIn, DateTime checkOut)
        {
           dvgBill.DataSource = BillDAO.Instance.GetBillListByDate(checkIn, checkOut);
        }
        void LoadFoodCategory()
        {
            dgvCategory.DataSource = CategoryDAO.Instance.GetListFoddCategory();
        }
        void LoadCategory()
        {
            cbbFoodCategory.DataSource =  CategoryDAO.Instance.GetListFoddCategory();
            cbbFoodCategory.DisplayMember = "name";
        }
        void LoadAccount()
        {
            dgvAccount.DataSource = AccountDAO.Instance.GetListAccount();
        }
        void LoadListFood()
        {
            foodList.DataSource = FoodDAO.Instance.GetListFood();
        }
        void LoadTable()
        {
            dgvTable.DataSource = TableDAO.Instance.LoadTableList();
        }
        void BindingFood()
        {
            txtFoodID.DataBindings.Add(new Binding("text", dgvFood.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txtFoodName.DataBindings.Add(new Binding("text", dgvFood.DataSource, "name", true, DataSourceUpdateMode.Never));
            nmFoodPrice.DataBindings.Add(new Binding("Value", dgvFood.DataSource, "price", true, DataSourceUpdateMode.Never));
        }
        void BindingAccount()
        {
            txtAccountID.DataBindings.Add(new Binding("text", dgvAccount.DataSource, "Username", true, DataSourceUpdateMode.Never));
            txtDisPlayNameAccount.DataBindings.Add(new Binding("text", dgvAccount.DataSource, "DisplayName", true, DataSourceUpdateMode.Never));
        }
        void BindingTable()
        {
            txtTableID.DataBindings.Add(new Binding("text", dgvTable.DataSource, "id"));
            txtTableName.DataBindings.Add(new Binding("text", dgvTable.DataSource, "name"));
            cbbTableStatus.DataBindings.Add(new Binding("text", dgvTable.DataSource, "status"));
        }
        void BindingCategory()
        {
            txtCatogoryID.DataBindings.Add(new Binding("text", dgvCategory.DataSource, "id"));
            txtCategoryName.DataBindings.Add(new Binding("text", dgvCategory.DataSource, "name"));
        }
        void LoadTypeAccount()
        {
            dgvTypeAccount.DataSource = TypeAccountDAO.Intstance.LoadlistAccount();
           
        }
        void LoadCategoryAccount()
        {
            cbbAccountType.DataSource = TypeAccountDAO.Intstance.LoadlistAccount();
            cbbAccountType.DisplayMember = "Name";
        }
        void BindingTypeAccount()
        {
            txtTypeAccountID.DataBindings.Add(new Binding("text", dgvTypeAccount.DataSource, "ID"));
            txtNameTypeAccount.DataBindings.Add(new Binding("text", dgvTypeAccount.DataSource, "Name")); 
        }
        void ResertPass(string username)
        {
            if (AccountDAO.Instance.ResetPassword(username))
            {
                MessageBox.Show("Đặt lại mật khẩu thành công.");
            }
            else
            {
                MessageBox.Show("Đặt lại mật khẩu thất bại.");
            }
        }
        List<Food> SearchFoodByName(string FoodName)
        {
            List<Food> listFood = FoodDAO.Instance.SearchFoodByName(FoodName);
            return listFood;
        }
        #endregion
        #region Event
        private void btnViewBill_Click(object sender, EventArgs e)
        {
            LoadListBillByDate(dtpkFormDate.Value, dtpkToDate.Value);
        }
    
        private void btnSearch_Click(object sender, EventArgs e)
        {
            foodList.DataSource =  SearchFoodByName(txtFoodSearchName.Text);
        }

        private void txtFoodID_TextChanged(object sender, EventArgs e)
        {
            if(dgvFood.SelectedCells.Count > 0)
            {
                int id = (int)dgvFood.SelectedCells[0].OwningRow.Cells["IDCategory"].Value;

                Category category = CategoryDAO.Instance.GetCategoryByID(id);

               // cbbFoodCategory.SelectedItem = category;

                int index = -1;
                int i = 0;
                foreach(Category item in cbbFoodCategory.Items)
                {
                    if(item.ID == category.ID)
                    {
                        index = i;  
                        break;
                    }
                    i++;
                }
                cbbFoodCategory.SelectedIndex = index;
            }
        }
        private void txtAccountID_TextChanged(object sender, EventArgs e)
        {
            if(dgvAccount.SelectedCells.Count > 0)
            {
                int id = (int)dgvAccount.SelectedCells[0].OwningRow.Cells["Type"].Value;

                TypeAccount typeAccount = TypeAccountDAO.Intstance.GetCategoryAccountByID(id);

                int index = -1;
                int i = 0;
                foreach(TypeAccount item in cbbAccountType.Items)
                {
                    if(item.ID == typeAccount.ID)
                    {
                        index = i;
                        break;
                    }
                    i++;
                }

                cbbAccountType.SelectedIndex = index;

            }
        }
        private void btnShowFood_Click(object sender, EventArgs e)
        {
            LoadListFood();

        }
        private void btnAddFood_Click(object sender, EventArgs e)
        {
            string name = txtFoodName.Text;
            int idCategory = (cbbFoodCategory.SelectedItem as Category).ID;
            float price = (float)nmFoodPrice.Value;

            if (FoodDAO.Instance.InsertFood(idCategory, name, price))
            {
                MessageBox.Show("Thêm mới thành công!");
                LoadListFood();
                if (insertFood != null)
                    insertFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Thêm mới thất bại.");
            }
        }
      
        private void btnUpdateFood_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtFoodID.Text);
            string name = txtFoodName.Text;
            int idCategory = (cbbFoodCategory.SelectedItem as Category).ID;
            float price = (float)nmFoodPrice.Value;

            if (FoodDAO.Instance.UpdateFood(id, idCategory, name, price))
            {
                MessageBox.Show("Cập nhật thành công.");
                LoadListFood();
                if (updateFood != null)
                    updateFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại");
            }
            
        }
        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtFoodID.Text);
            
            if (FoodDAO.Instance.DeleteFood(id))
            {
                MessageBox.Show("Xoá thành công.");
                LoadListFood();
                if (deleteFood != null)
                    deleteFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Xóa thất bại");
            }
         
        }
        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            string username = txtAccountID.Text;
            string displayName = txtDisPlayNameAccount.Text;
            int idType = ((cbbAccountType.SelectedItem as TypeAccount).ID);
            if (AccountDAO.Instance.InsertAccountType(username, displayName, idType))
            {
                MessageBox.Show("Thêm mới thành công.");
                LoadAccount();
            }
            else
            {
                MessageBox.Show("Thêm mới thất bai.");
            }
        }
        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            string username = txtAccountID.Text;
            string displayName = txtDisPlayNameAccount.Text;
            int idType = ((cbbAccountType.SelectedItem as TypeAccount).ID);
            if (AccountDAO.Instance.UpdateAccountType(username,displayName, idType))
            {
                MessageBox.Show("Cập nhật thành công.");
                LoadAccount();
            }
            else
            {
                MessageBox.Show("Cập nhật thất bai.");
            }
        }
       
        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            string username = txtAccountID.Text;
            DeleteAccount(username);
        }
        private void txtResertPass_Click(object sender, EventArgs e)
        {
            string username = txtAccountID.Text;
            ResertPass(username);
        }
        private void btnFirstBillPage_Click(object sender, EventArgs e)
        {
            txtPageBill.Text = "1";
        }
        private void btnLastBillPage_Click(object sender, EventArgs e)
        {
            int sumRecord = BillDAO.Instance.GetNumBillListByDate(dtpkFormDate.Value, dtpkToDate.Value);

            int lastPage = sumRecord / 10;

            if (sumRecord % 10 != 0)
                lastPage++;
            txtPageBill.Text = lastPage.ToString();
        }
        private void txtPageBill_TextChanged(object sender, EventArgs e)
        {
            dvgBill.DataSource = BillDAO.Instance.GetBillListByDateAndPage(dtpkFormDate.Value, dtpkToDate.Value, Convert.ToInt32(txtPageBill.Text));
        }


        private event EventHandler insertFood;
        public event EventHandler InsertFood
        {
            add { insertFood += value; }
            remove { insertFood -= value; }
        }

        private event EventHandler updateFood;
        public event EventHandler UpdateFood
        {
            add { updateFood += value; }
            remove { updateFood -= value; }
        }

        private event EventHandler deleteFood;
        public event EventHandler DeleteFood
        {
            add { deleteFood += value; }
            remove { deleteFood -= value; }
        }







        #endregion

        private void btnDeleteTable_Click(object sender, EventArgs e)
        {

        }
    }

}
