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
    /// Логика взаимодействия для AddNumber.xaml
    /// </summary>
    public partial class AddNumber : Window
    {
        private NumbersRepository repository;
        private string chosePlace = "";
        private string choseComf = "";
        private BitmapImage img = null;
        private int tempNumb;
        public AddNumber()
        {
            InitializeComponent();
            repository = new NumbersRepository();
            DataContext = repository;
            repository.GetNumbers();
            repository.plc();
            comfort.ItemsSource = repository.comf;
            place.ItemsSource = repository.places;
        }

        private void place_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            Placing selectedItem = (Placing)comboBox.SelectedItem;
            chosePlace = selectedItem.Place;
        }

        private void comfort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            Comfort selectedItem = (Comfort)comboBox.SelectedItem;
            choseComf = selectedItem.Comfort1;
        }

        private void photo_Click(object sender, RoutedEventArgs e)
        {
            img = Image.OpenNumb();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if((number.Text != "") && (int.TryParse(number.Text,out tempNumb)) && (img != null) && (choseComf != "") && (chosePlace !=""))
            {
                if(repository.GetNumber(tempNumb) == null)
                {
                    TempNumber temp = new TempNumber(tempNumb, chosePlace, choseComf, 1, img);
                    repository.createNumber(temp);
                    MessageBox.Show("Добавлено!");
                    AdminNumbers.tempNumber = tempNumb;
                    this.DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Такой номер уже есть!");
                }
            }
            else
            {
                MessageBox.Show("Данные введены неверно!");
            }
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
