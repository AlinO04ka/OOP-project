using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void over_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        char sex;

        private void register_Click(object sender, RoutedEventArgs e)
        {
            Regex regex = new Regex(@"^([A-Za-z0-9!@#$%^&*]{8,})$");
            if (!regex.IsMatch(password.Password))
            {
                err.Visibility = Visibility.Visible;
                err.Text = "Пароль слишком простой!";
            }
            else
            {
                RegisterDate register = new RegisterDate(name.Text, surname.Text, middle.Text, sex, login.Text, HashingPassword.HashPassword(password.Password), email.Text);
                Repository repository = new Repository();
                repository.GetUsers();
                var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
                var context = new ValidationContext(register);
                if (!Validator.TryValidateObject(register, context, results, true))
                {
                    err.Visibility = Visibility.Visible;
                    err.Text = results[0].ErrorMessage;
                }
                else
                {
                    Profile_Date NewUser = new Profile_Date(register.name, register.surname, register.middle, register.sex, register.login, register.password, register.email);
                    err.Visibility = Visibility.Collapsed;
                    if ((repository.profile_s.FirstOrDefault(s => s.Login == NewUser.Login) == null) && (NewUser.Login != repository.GetAdmin().login))
                    {
                        repository.createUser(NewUser);
                        repository.Save();
                        repository.Dispose();
                        MessageBox.Show("Успешно зарегистрирован!");
                        Authorize authorize = new Authorize();
                        authorize.Show();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Пользователь с таким ником уже существует!");
                    }
                }
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton pressed = (RadioButton)sender;
            string choise = pressed.Content.ToString();
            if(choise == "М")
            {
                sex = 'М';
            }
            else
            {
                sex = 'Ж';
            }
        }
    }
}
