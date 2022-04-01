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
    /// Логика взаимодействия для Authorize.xaml
    /// </summary>
    public partial class Authorize : Window
    {
        public Authorize()
        {
            InitializeComponent();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void over_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void enter_Click(object sender, RoutedEventArgs e)
        {
            Repository repository = new Repository();
            if(login.Text != "" && password.Password != "")
            {
                Admin temp = repository.GetAdmin();
                if (temp != null)
                {
                    if (login.Text == temp.login)
                    {
                        if (HashingPassword.VerifyHashedPassword(temp.password, password.Password))
                        {
                            WindowAdmin windowAdmin = new WindowAdmin();
                            windowAdmin.Show();
                            Close();                            
                        }
                        else
                        {
                            MessageBox.Show("Пароль введен неверно!");
                        }
                    }
                    else 
                        temp = null;
                }
                if (temp == null)
                {
                    User.user = repository.GetUser(login.Text);
                    if (User.user != null)
                    {
                        if (HashingPassword.VerifyHashedPassword(User.user.Password, password.Password))
                        {
                            MainWindow mainWindow = new MainWindow();
                            mainWindow.Show();
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("Пароль введен неверно!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Такого пользователя нет!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Введены не все данные!");
            }
        }

        private void register_Click(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
            Close();
        }
    }
}
