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
    /// GradeAnalysingPage.xaml 的交互逻辑
    /// </summary>
    public partial class GradeAnalysingPage : Page
    {
        private MySqlConnection connection;
        private string connectionString = "server=localhost;port=3306;user=root;password=zyh0714.;database=test";
        public GradeAnalysingPage()
        {
            InitializeComponent();


            try
            {
                TestRangeDialog dialog = new TestRangeDialog();
                if (dialog.ShowDialog() == true)
                {
                    int start = dialog.StartNumber;
                    int end = dialog.EndNumber;



                    List<int> testLabels = new List<int>();
                    for (int i = start; i <= end; i++) testLabels.Add(i);

                    SearchDialog searchDialog = new SearchDialog();
                    searchDialog.ShowDialog();
                    connection = new MySqlConnection(connectionString);
                    connection.Open();
                    string selectQuery = "SELECT ";
                    for (int i = start; i <= end; i++)
                    {
                        selectQuery += " grade" + i.ToString() + ",";
                    }
                    selectQuery += "Name FROM student WHERE ";
                    string conStr = "";
                    if (searchDialog.Id_search == true) conStr += " AND Id = @Id";
                    if (searchDialog.Grade_search == true) conStr += " AND Grade = @Grade";
                    if (searchDialog.Age_search == true) conStr += " AND Age = @Age";
                    if (searchDialog.Age_search == true) conStr += " AND Sex = @Sex";
                    if (searchDialog.Name_search == true) conStr += " AND Name = @Name";
                    conStr += ";";
                    conStr = conStr.Substring(4);
                    selectQuery += conStr;

                    gradeChart.Series = new LiveCharts.SeriesCollection();

                    using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Id", searchDialog.txtId.Text);
                        command.Parameters.AddWithValue("@Age", (searchDialog.txtAge.Text)); // Replace with the desired student Id
                        command.Parameters.AddWithValue("@Name", searchDialog.txtName.Text); // Replace with the desired student Id
                        command.Parameters.AddWithValue("@Grade", (searchDialog.txtAge.Text)); // Replace with the desired student Id
                        command.Parameters.AddWithValue("@Sex", searchDialog.cmbSex.SelectedItem.ToString().Substring(searchDialog.cmbSex.SelectedItem.ToString().Length - 1)); // Replace with the desired student Id


                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                List<int> gradeValues = new List<int>();
                                for (int i = start; i <= end; i++)
                                {
                                    gradeValues.Add(reader.GetInt32("grade" + i.ToString()));
                                }
                                gradeChart.Series.Add(
                                    new LiveCharts.Wpf.LineSeries
                                    {
                                        Title = reader.GetString("name"),
                                        Values = new LiveCharts.ChartValues<int>(gradeValues),
                                        LineSmoothness = 0
                                    }
                                    );
                            }
                        }
                    }

                    gradeChart.AxisX.Add(new LiveCharts.Wpf.Axis
                    {
                        Title = "考试编号",
                        Labels = testLabels.ConvertAll(x => x.ToString())
                    });
                    gradeChart.AxisY.Add(new LiveCharts.Wpf.Axis
                    {
                        Title = "分数"
                    });
                }
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
