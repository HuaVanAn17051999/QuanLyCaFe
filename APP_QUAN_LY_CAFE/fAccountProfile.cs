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
    public partial class fAccountProfile : Form
    {
        private Account loginAccount;
        public Account LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; ChangeAccount(LoginAccount); }
        }
        public fAccountProfile(Account acc)
        {
            InitializeComponent();
            this.LoginAccount = acc;
        }



        #region Method
        void ChangeAccount(Account acc)
        {
            txtUsername.Text = LoginAccount.UserName;
            txtDisplayName.Text = LoginAccount.DisplayName;
        }
        void UpdateAccount()
        {
            string UserName = txtUsername.Text;
            string DisplayName = txtDisplayName.Text;
            string Password = txtoldPass.Text;
            string NewPass = txtNewPass.Text;
            string ConfigPass = txtConfigPass.Text;

            if (!NewPass.Equals(ConfigPass))
            {
                MessageBox.Show("Vui lòng nhập lại mật khẩu đúng với mật khẩu cũ.");
            }
            else
            {
                if(AccountDAO.Instance.UpdateAccount(UserName, DisplayName, Password, NewPass))
                {
                    MessageBox.Show("Cập nhật thành công.");
                    if (updateAccounts != null)
                        updateAccounts(this, new AccountEvent(AccountDAO.Instance.GetAccountByUsername(UserName)));
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại.");
                }
            }
        }
        #endregion

        #region Event
        private event EventHandler<AccountEvent> updateAccounts;
        public event EventHandler<AccountEvent> UpdateAccounts
        {
            add { updateAccounts += value; }
            remove { updateAccounts -= value; }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateAccount();
        }
        #endregion

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
    public class AccountEvent : EventArgs 
    {
        private Account acc;
        public Account Acc
        {
            get { return acc; }
            set { acc = value; }
        }
        public AccountEvent(Account acc)
        {
            this.Acc = acc;
        }

    }

}
