using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Логика взаимодействия для Numbers.xaml
    /// </summary>
    public partial class Numbers : UserControl
    {

        private NumbersRepository repository;
        public Numbers()
        {
            InitializeComponent();
            repository = new NumbersRepository();
            DataContext = repository;
            repository.stateFree();
            repository.CheckingIsFree();
            repository.GetNumbers();
            ListNumbers.ItemsSource = repository.numbers;
            repository.Fill();
            placing.ItemsSource = repository.placing;
            comfort.ItemsSource = repository.comfort;
        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            if(isFree.IsChecked == true)
            {
                repository.Search(1);
            }
            else
            {
                repository.Search(0);
            }
            ListNumbers.ItemsSource = repository.tempNumbers;
            Brone.Visibility = Visibility.Collapsed;
        }

        private void Brone_Click(object sender, RoutedEventArgs e)
        {
            DatePick datePick = new DatePick(repository.SelectedItem.ID_Number);
            datePick.ShowDialog();
            repository.CheckingIsFree();
            repository.GetNumbers();
        }

        private void ListNumbers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Brone.Visibility = Visibility.Visible;
            chosen.Visibility = Visibility.Visible;
        }
    }
}
