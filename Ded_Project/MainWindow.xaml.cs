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

namespace Ded_Project
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            image.DataContext = User.user;

            NumbersRepository rep = new NumbersRepository();
            rep.GetNumbersWithDate(User.user.Login);
            foreach(var item in rep.numbers)
            {
                if (item.state != "Отменен")
                {
                    if (DateTime.Today >= item.DateFrom && DateTime.Today <= item.DateTo)
                    {
                        item.state = "Выполняется";
                        rep.updateState(item);
                    }
                    else if (DateTime.Today > item.DateTo)
                    {
                        item.state = "Выполнен";
                        rep.updateState(item);
                    }
                    else if (DateTime.Today < item.DateFrom)
                    {
                        item.state = "Будет выполнен";
                        rep.updateState(item);
                    }
                }
            }
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Select_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((System.Windows.Controls.ListBoxItem)((System.Windows.Controls.ListBox)sender).SelectedItem).Name)
            {
                case "Profile":
                    Content.Children.Clear();
                    Account account = new Account(this);
                    Content.Children.Add(account);
                    break;
                case "Orders":
                    Content.Children.Clear();
                    Orders orders = new Orders();
                    Content.Children.Add(orders);
                    break;
                case "Numbers":
                    Content.Children.Clear();
                    Numbers NumbersList = new Numbers();
                    Content.Children.Add(NumbersList);
                    break;
                case "Map":
                    Content.Children.Clear();
                    MapControl map = new MapControl();
                    map.LoadMap(25.75885376775984, -80.19227842161001);
                    Content.Children.Add(map);
                    break;
                default:
                    break;
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Authorize authorize = new Authorize();
            authorize.Show();
            Close();
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
