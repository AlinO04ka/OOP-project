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
    /// Логика взаимодействия для AdminNumbers.xaml
    /// </summary>
    public partial class AdminNumbers : UserControl
    {
        public static int tempNumber;
        private NumbersRepository repository;
        public AdminNumbers()
        {
            InitializeComponent();
            repository = new NumbersRepository();
            DataContext = repository;
            repository.GetNumbers();
            ListNumbers.ItemsSource = repository.numbers;
            add.Visibility = Visibility.Visible;
            if (repository.numbers.Count == 0)
            {
                clean.Visibility = Visibility.Visible;
                ico.Visibility = Visibility.Visible;
            }
            else
            {
                icoSmall.Visibility = Visibility.Visible;
            }
        }

        private void ListNumbers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            add.Visibility = Visibility.Collapsed;
            delete.Visibility = Visibility.Visible;
            chosen.Visibility = Visibility.Visible;
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            repository.DeleteNumber(repository.SelectedItem.ID_Number);
            int pos = repository.numbers.IndexOf(repository.SelectedItem);
            repository.numbers.RemoveAt(pos);
            repository.SelectedItem = null;
            delete.Visibility = Visibility.Collapsed;
            add.Visibility = Visibility.Visible;
            chosen.Visibility = Visibility.Hidden;
            if (repository.numbers.Count == 0)
            {
                icoSmall.Visibility = Visibility.Collapsed;
                clean.Visibility = Visibility.Visible;
                ico.Visibility = Visibility.Visible;
            }
            else
            {
                icoSmall.Visibility = Visibility.Visible;
            }
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            AddNumber add = new AddNumber();
            add.ShowDialog();
            if (add.DialogResult == true)
            {
                repository.numbers.Add(repository.GetNumber(tempNumber));
                clean.Visibility = Visibility.Collapsed;
            }
        }
    }
}
