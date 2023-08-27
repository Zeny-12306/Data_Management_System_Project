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

namespace Data_Management_System
{
    /// <summary>
    /// Log_inPage.xaml 的交互逻辑
    /// </summary>
    public partial class Log_inPage : Page
    {
        public Log_inPage()
        {
            InitializeComponent();
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = user.Text;
            string password = pwd.Password;

            if (IsValidUser(username, password))
            {
                MessageBox.Show("Login successful!");
                App.Current.MainWindow.Content = new MainPage();
                App.Current.MainWindow.Show();
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.");
            }
        }
        private bool IsValidUser(string username, string password)
        {
            // Simulate a valid user for demonstration purposes
            return username == "admin" && password == "password";
        }
    }
}
