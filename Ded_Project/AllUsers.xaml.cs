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
    /// Логика взаимодействия для AllUsers.xaml
    /// </summary>
    public partial class AllUsers : UserControl
    {
        private Repository repository;
        public AllUsers()
        {
            InitializeComponent();
            repository = new Repository();
            DataContext = repository;
            repository.GetUsers();
            ListUsers.ItemsSource = repository.profile_s;
            if (repository.profile_s.Count == 0)
            {
                clean.Visibility = Visibility.Visible;
                ico.Visibility = Visibility.Visible;
            }
            else
                icoSmall.Visibility = Visibility.Visible;
        }

        private void ListUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            delete.Visibility = Visibility.Visible;
            chosen.Visibility = Visibility.Visible;
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            repository.DeleteUser(repository.SelectedItem.Login);
            int pos = repository.profile_s.IndexOf(repository.SelectedItem);
            repository.profile_s.RemoveAt(pos);
            repository.SelectedItem = null;
            delete.Visibility = Visibility.Collapsed;
            chosen.Visibility = Visibility.Hidden;
            textSe.Text = "";

            if (repository.profile_s.Count == 0)
            {
                icoSmall.Visibility = Visibility.Collapsed;
                clean.Visibility = Visibility.Visible;
                ico.Visibility = Visibility.Visible;
            }
            else
                icoSmall.Visibility = Visibility.Visible;
        }

        int tempC = 0;
        private void textSe_TextChanged(object sender, TextChangedEventArgs e)
        {      
            if (textSe.Text != "")
            {
                if (textSe.Text.Length > tempC)
                {
                    foreach (var user in repository.profile_s)
                    {
                        if (!user.Login.StartsWith(textSe.Text))
                        {
                            repository.profile_Temp.Add(user);
                        }
                    }
                    foreach (var user in repository.profile_Temp)
                    {
                        if (repository.profile_s.Contains(user))
                        {
                            repository.profile_s.Remove(user);
                        }
                    }
                }
                else
                {
                    foreach (var user in repository.profile_Temp)
                    {
                        if (user.Login.StartsWith(textSe.Text))
                        {
                            repository.profile_s.Add(user);
                        }
                    }
                    foreach (var user in repository.profile_s)
                    {
                        if (repository.profile_Temp.Contains(user))
                        {
                            repository.profile_Temp.Remove(user);
                        }
                    }
                }
            }
            else
            {
                foreach (var user in repository.profile_Temp)
                {
                    repository.profile_s.Add(user);
                }
                repository.profile_Temp.Clear();
            }
            tempC = textSe.Text.Length;
        }
    }
}
