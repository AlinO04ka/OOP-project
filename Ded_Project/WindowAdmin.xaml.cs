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

namespace Ded_Project
{
    /// <summary>
    /// Логика взаимодействия для WindowAdmin.xaml
    /// </summary>
    public partial class WindowAdmin : Window
    {
        public WindowAdmin()
        {
            InitializeComponent();
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((System.Windows.Controls.ListBoxItem)((System.Windows.Controls.ListBox)sender).SelectedItem).Name)
            {
                case "Users":
                    Content.Children.Clear();
                    AllUsers users = new AllUsers();
                    Content.Children.Add(users);
                    break;
                case "Numbers":
                    Content.Children.Clear();
                    AdminNumbers adminNumbers = new AdminNumbers();
                    Content.Children.Add(adminNumbers);
                    break;
                default:
                    break;
            }
        }
    }
}
