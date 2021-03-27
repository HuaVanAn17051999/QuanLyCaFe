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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        bool LOGIN(string username, string password)
        {
            return AccountDAO.Instance.Login(username, password);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            if(LOGIN(username, password))
            {
                Account loginAccount = AccountDAO.Instance.GetAccountByUsername(username);
                fTableManage fTable = new fTableManage(loginAccount);
                this.Hide();
                fTable.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Đăng nhập thất bại.");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
           if( MessageBox.Show("Bạn muốn thoát chương trình.","Thông báo", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes)
           {
                e.Cancel = true;
           }
        }
    }
}
