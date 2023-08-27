using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace Data_Management_System
{
    /// <summary>
    /// MainPage.xaml 的交互逻辑
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private static readonly Dictionary<Type, Page> bufferedPages =
            new Dictionary<Type, Page>();

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (navMenu.SelectedItem is ListBoxItem item)
            {
                string tag = item.Tag as string;

                if (!string.IsNullOrEmpty(tag))
                {
                    Type type = Type.GetType($"Data_Management_System.{tag}");
                    if (type != null)
                    {
                        if (!bufferedPages.TryGetValue(type, out Page? page))
                        {
                            page = bufferedPages[type] = Activator.CreateInstance(type) as Page ?? throw new Exception("this would never happen");
                        }
                        appFrame.Navigate(page);
                    }
                }
            }
        }
    }
}
