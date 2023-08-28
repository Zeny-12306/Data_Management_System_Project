using MySql.Data.MySqlClient;
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
        //public bool is_teacher = false;
        //public bool is_student = false;
        private MySqlConnection connection;
        private string connectionString = "server=localhost;port=3306;user=root;password=zyh0714.;database=test";
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
                MessageBox.Show("账号或密码不正确，请确认后重新登录！");
            }
        }
        private bool IsValidUser(string username, string password)
        {
            connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                string query = "SELECT pwd FROM stu_log_info WHERE Id = @Id"; // Replace with your table name
                
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", username);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (reader.GetString("pwd") == password)
                            {
                                GlobalVariables.is_student = true;
                            }
                        }

                    }
                }
                query = "SELECT pwd FROM teacher_log_info WHERE Id = @Id"; // Replace with your table name

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", username);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if(reader.GetString( "pwd") == password)
                            {
                                GlobalVariables.is_teacher = true;
                            }
                        }

                    }
                }
                if (GlobalVariables.is_student || GlobalVariables.is_teacher) return true;
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return false;
        }
    }
}
