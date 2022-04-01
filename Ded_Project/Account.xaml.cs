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
    /// Логика взаимодействия для Account.xaml
    /// </summary>
    public partial class Account : UserControl
    {
        private Repository repository;
        MainWindow window;
        public Account(MainWindow mainWindow)
        {
            InitializeComponent();
            DataContext = User.user;
            window = mainWindow;
            repository = new Repository();
        }

        private void addPicture_Click(object sender, RoutedEventArgs e)
        {
            Image.Open();
            
            User.user.Image = repository.GetUser(User.user.Login).Image;
        }

        private void changeDate_Click(object sender, RoutedEventArgs e)
        {
            end.Visibility = Visibility.Visible;
            changeDate.IsEnabled = false;
            delete.IsEnabled = false;
            addPicture.IsEnabled = false;
            var bc = new BrushConverter();
            name.IsReadOnly = false;
            name.Cursor = Cursors.IBeam;
            name.Background = (Brush)bc.ConvertFrom("#bbc8ba");

            surname.IsReadOnly = false;
            surname.Cursor = Cursors.IBeam;
            surname.Background = (Brush)bc.ConvertFrom("#bbc8ba");

            middle.IsReadOnly = false;
            middle.Cursor = Cursors.IBeam;
            middle.Background = (Brush)bc.ConvertFrom("#bbc8ba");
        }

        private void end_Click(object sender, RoutedEventArgs e)
        {
            changeDate.IsEnabled = true;
            delete.IsEnabled = true;
            addPicture.IsEnabled = true;
            end.Visibility = Visibility.Collapsed;
            Repository repository = new Repository();

            name.IsReadOnly = true;
            name.Cursor = Cursors.Arrow;
            var bc = new BrushConverter();
            name.Background = (Brush)bc.ConvertFrom("#f2f1ef");      
            
            surname.IsReadOnly = true;
            surname.Cursor = Cursors.Arrow;
            surname.Background = (Brush)bc.ConvertFrom("#f2f1ef");

            middle.IsReadOnly = true;
            middle.Cursor = Cursors.Arrow;
            middle.Background = (Brush)bc.ConvertFrom("#f2f1ef");

            email.IsReadOnly = true;
            email.Cursor = Cursors.Arrow;
            email.Background = (Brush)bc.ConvertFrom("#f2f1ef");
            if(email.Text == "")
            {
                email.Text = $"Email: {User.user.Email}";
            }
            
            repository.updateUser(User.user);
            repository.Save();
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            Sure sure = new Sure();
            sure.ShowDialog();
            if(sure.DialogResult == true)
            {
                repository.DeleteUser(User.user.Login);
                Authorize authorize = new Authorize();
                authorize.Show();
                window.Close();
            }
        }
    }
}
