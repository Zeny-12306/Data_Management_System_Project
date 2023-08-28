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
    /// TestRangeDialog.xaml 的交互逻辑
    /// </summary>
    public partial class TestRangeDialog : Window
    {
        public int StartNumber { get; private set; }
        public int EndNumber { get; private set; }

        public TestRangeDialog()
        {
            InitializeComponent();
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtStart.Text, out int start) && int.TryParse(txtEnd.Text, out int end))
            {
                StartNumber = start;
                EndNumber = end;
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("请输入有效的数字。");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

    }
}
