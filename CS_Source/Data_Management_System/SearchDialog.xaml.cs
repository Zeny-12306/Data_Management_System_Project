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
    /// SearchDialog.xaml 的交互逻辑
    /// </summary>
    public partial class SearchDialog : Window
    {


        public bool Id_search = false;
        public bool Name_search = false;
        public bool Sex_search = false;
        public bool Age_search = false;
        public bool Grade_search = false;

        public SearchDialog()
        {
            InitializeComponent();


            cmbSex.SelectedIndex = 0;
        }

        private void SearchConfirmButton_Click(object sender, RoutedEventArgs e)
        {


            if (txtName.Text != "") Name_search = true;
            if (txtId.Text != "") Id_search = true;
            if (txtAge.Text != "") Age_search = true;
            if (txtName.Text != "") Name_search = true;
            if (txtGrade.Text != "") Grade_search = true;

            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }


    }
}
