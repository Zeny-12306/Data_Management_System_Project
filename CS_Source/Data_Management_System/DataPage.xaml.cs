using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// DataPage.xaml 的交互逻辑
    /// </summary>
    public partial class DataPage : Page
    {

        private ObservableCollection<Student> students;
        private MySqlConnection connection;
        private string connectionString = "server=localhost;port=3306;user=root;password=zyh0714.;database=test";

        public DataPage()
        {
            InitializeComponent();
            students = new ObservableCollection<Student>();
            dataGrid.ItemsSource = students;

            connection = new MySqlConnection(connectionString);
            LoadData();
        }

        private void LoadData()
        {
            students.Clear();

            try
            {
                connection.Open();
                string query = "SELECT Id, Sex, Name, Grade, Age FROM student"; // Replace with your table name

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {


                        while (reader.Read())
                        {
                            students.Add(new Student
                            {
                                Id = reader.GetString("id"),
                                Sex = reader.GetString("sex"),
                                Name = reader.GetString("name"),
                                Grade = reader.GetInt32("grade"),
                                Age = reader.GetInt32("age") // Read age from database
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("确定要进行修改吗？", "确认修改", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Student selectedStudent = (Student)dataGrid.SelectedItem;
                if (selectedStudent != null)
                {
                    EditDialog editDialog = new EditDialog(selectedStudent);
                    if (editDialog.ShowDialog() == true)
                    {
                        // Update the selectedStudent with edited data
                        selectedStudent.Id = editDialog.EditedId; // Optional: Update Id if changed
                        selectedStudent.Name = editDialog.EditedName;
                        selectedStudent.Sex = editDialog.EditedSex;
                        selectedStudent.Age = editDialog.EditedAge;

                        // Update the student's data in the database and then refresh the grid
                        try
                        {
                            connection.Open();
                            string updateQuery = "UPDATE student SET Id = @Id, Name = @Name, Sex = @Sex, Age = @Age WHERE Id = @OldId"; // Replace with appropriate column names
                            using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                            {
                                updateCommand.Parameters.AddWithValue("@Id", selectedStudent.Id);
                                updateCommand.Parameters.AddWithValue("@Name", selectedStudent.Name);
                                updateCommand.Parameters.AddWithValue("@Sex", selectedStudent.Sex);
                                updateCommand.Parameters.AddWithValue("@Age", selectedStudent.Age);
                                updateCommand.Parameters.AddWithValue("@OldId", selectedStudent.Id);
                                updateCommand.ExecuteNonQuery();
                            }

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error updating student: " + ex.Message);
                        }
                        finally
                        {
                            connection.Close();
                            LoadData();
                        }
                    }
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Student selectedStudent = (Student)dataGrid.SelectedItem;
            if (selectedStudent != null)
            {
                MessageBoxResult result = MessageBox.Show("确定要进行删除吗？", "确认删除", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        connection.Open();
                        string deleteQuery = "DELETE FROM student WHERE Id = @Id";
                        using (MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@Id", selectedStudent.Id);
                            deleteCommand.ExecuteNonQuery();
                        }

                        students.Remove(selectedStudent);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting student: " + ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Open a dialog for adding a new student
            AddDialog addDialog = new AddDialog();
            if (addDialog.ShowDialog() == true)
            {
                // Get the newly added student from the dialog and add it to the grid
                Student newStudent = addDialog.GetNewStudent();
                AddStudentToDatabase(newStudent); // Add to the database
                LoadData(); // Refresh the grid
            }
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            // Open a dialog for entering search criteria
            try
            {
                SearchDialog searchDialog = new SearchDialog();
                searchDialog.ShowDialog();
                connection.Open();
                string selectQuery = "SELECT Id, Sex, Name, Grade, Age FROM student WHERE ";
                string conStr = "";
                if (searchDialog.Id_search == true) conStr += " AND Id = @Id";
                if (searchDialog.Grade_search == true) conStr += " AND Grade = @Grade";
                if (searchDialog.Age_search == true) conStr += " AND Age = @Age";
                if (searchDialog.Age_search == true) conStr += " AND Sex = @Sex";
                if (searchDialog.Name_search == true) conStr += " AND Name = @Name";
                conStr += ";";
                conStr = conStr.Substring(4);
                selectQuery += conStr;

                using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", searchDialog.txtId.Text);
                    command.Parameters.AddWithValue("@Age", (searchDialog.txtAge.Text)); // Replace with the desired student Id
                    command.Parameters.AddWithValue("@Name", searchDialog.txtName.Text); // Replace with the desired student Id
                    command.Parameters.AddWithValue("@Grade", (searchDialog.txtAge.Text)); // Replace with the desired student Id
                    command.Parameters.AddWithValue("@Sex", searchDialog.cmbSex.SelectedItem.ToString().Substring(searchDialog.cmbSex.SelectedItem.ToString().Length - 1)); // Replace with the desired student Id


                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        students.Clear();
                        while (reader.Read())
                        {
                            students.Add(new Student
                            {
                                Id = reader.GetString("id"),
                                Sex = reader.GetString("sex"),
                                Name = reader.GetString("name"),
                                Grade = reader.GetInt32("grade"),
                                Age = reader.GetInt32("age") // Read age from database
                            });
                            dataGrid.ItemsSource = students;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching student: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

        }
        private void AddStudentToDatabase(Student student)
        {
            try
            {
                connection.Open();
                string insertQuery = "INSERT INTO Student (Id, Sex, Name, Age) VALUES (@Id, @Sex, @Name, @Age)";

                using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", student.Id);
                    command.Parameters.AddWithValue("@Sex", student.Sex);
                    command.Parameters.AddWithValue("@Name", student.Name);
                    //command.Parameters.AddWithValue("@Grade", student.Grade);
                    command.Parameters.AddWithValue("@Age", student.Age);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Insert successful
                        MessageBox.Show("Insert student successful!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting student: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void SortButton_Click(object sender, RoutedEventArgs e)
        {

            var sortedStudents = students.OrderByDescending(s => s.Grade).ToList();
            students.Clear();
            foreach (var student in sortedStudents)
            {
                students.Add(student);
            }
        }
        private void VerseSort_Click(object sender, RoutedEventArgs e)
        {

            var sortedList = students.OrderBy(s => s.Grade).ToList();
            students.Clear();
            foreach (var student in sortedList)
            {
                students.Add(student);
            }
        }
    }
}
