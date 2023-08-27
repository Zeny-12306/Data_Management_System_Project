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
using System.Windows.Shapes;

namespace Data_Management_System
{
    /// <summary>
    /// AddDialog.xaml 的交互逻辑
    /// </summary>
    public partial class AddDialog : Window
    {
        public string EditedName { get; private set; }
        public string EditedSex { get; private set; }
        public int EditedAge { get; private set; }
        public string EditedId { get; private set; }
        public string EditedGrade { get; private set; }

        public AddDialog()
        {
            InitializeComponent();


            cmbSex.SelectedIndex = 0;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Validate input
            if (!int.TryParse(txtAge.Text, out int age))
            {
                MessageBox.Show("Invalid age value.");
                return;
            }

            // Save edited data
            EditedName = txtName.Text;
            EditedSex = cmbSex.SelectedItem.ToString().Substring(cmbSex.SelectedItem.ToString().Length - 1);
            EditedAge = age;
            EditedId = txtId.Text;

            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        public Student GetNewStudent()
        {
            return new Student
            {
                Name = txtName.Text,
                Id = txtId.Text,
                Age = int.Parse(txtAge.Text),
                Sex = cmbSex.SelectedItem.ToString().Substring(cmbSex.SelectedItem.ToString().Length - 1)
            };
        }
    }
}
