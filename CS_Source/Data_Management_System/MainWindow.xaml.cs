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
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            mainFrame.NavigationService.Navigate(new MainPage());
        }
    }
    public static class WatermarkHelper
    {
        public static readonly DependencyProperty WatermarkProperty =
            DependencyProperty.RegisterAttached(
                "Watermark",
                typeof(string),
                typeof(WatermarkHelper),
                new PropertyMetadata(null, OnWatermarkChanged));

        public static string GetWatermark(DependencyObject obj)
        {
            return (string)obj.GetValue(WatermarkProperty);
        }

        public static void SetWatermark(DependencyObject obj, string value)
        {
            obj.SetValue(WatermarkProperty, value);
        }

        private static void OnWatermarkChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox textBox)
            {
                if (e.NewValue != null)
                {
                    textBox.GotFocus += TextBox_GotFocus;
                    textBox.LostFocus += TextBox_LostFocus;

                    if (string.IsNullOrEmpty(textBox.Text))
                    {
                        textBox.Text = e.NewValue.ToString();
                        textBox.Foreground = new SolidColorBrush(Colors.Gray);
                    }
                }
            }

        }

        private static void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (textBox.Foreground is SolidColorBrush brush && brush.Color == Colors.Gray)
            {
                textBox.Text = "";
                textBox.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private static void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = GetWatermark(textBox);
                textBox.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }
    }
    public class PasswordBoxWithHint : Control
    {
        private PasswordBox passwordBox;
        private TextBox hintTextBox;

        public PasswordBoxWithHint()
        {
            DefaultStyleKey = typeof(PasswordBoxWithHint);
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            passwordBox = GetTemplateChild("PasswordBox") as PasswordBox;
            hintTextBox = GetTemplateChild("HintTextBox") as TextBox;

            if (passwordBox != null && hintTextBox != null)
            {
                passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
                UpdateHintVisibility();
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            UpdateHintVisibility();
        }

        private void UpdateHintVisibility()
        {
            if (passwordBox.SecurePassword.Length == 0)
            {
                hintTextBox.Visibility = Visibility.Visible;
            }
            else
            {
                hintTextBox.Visibility = Visibility.Collapsed;
            }
        }
        public static readonly DependencyProperty PasswordProperty =
           DependencyProperty.Register("Password", typeof(string), typeof(PasswordBoxWithHint), new PropertyMetadata(string.Empty));

        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        public static readonly DependencyProperty HintVisibilityProperty =
            DependencyProperty.Register("HintVisibility", typeof(Visibility), typeof(PasswordBoxWithHint), new PropertyMetadata(Visibility.Visible));

        public Visibility HintVisibility
        {
            get { return (Visibility)GetValue(HintVisibilityProperty); }
            set { SetValue(HintVisibilityProperty, value); }
        }
    }

}