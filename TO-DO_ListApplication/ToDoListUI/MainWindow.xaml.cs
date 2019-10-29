using BLL;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ToDoListUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            User usr = new User() { UserName = txtRegUserName.Text, Password = txtRegPassword.Password.GetHashString() };
            var result = UOW.Instance.RepUser.Add(usr);
            List listWindows = new List(usr, this);
            ClearTextBox();
            listWindows.Show();
            this.Hide();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var pass = txtPassword.Password.GetHashString();
            var user = UOW.Instance.RepUser.Get(x => x.UserName == txtUserName.Text && x.Password == pass);

            if (user.Count == 0)
            {
                MessageBox.Show("Hatalı");
                return;
            }
            List listWindows = new List(user[0], this);
            ClearTextBox();
            listWindows.Show();
            this.Hide();
        }

        private void ClearTextBox()
        {
            txtPassword.Password = string.Empty;
            txtUserName.Text = string.Empty;
            txtRegPassword.Password = string.Empty;
            txtRegUserName.Text = string.Empty;
        }
    }
}
