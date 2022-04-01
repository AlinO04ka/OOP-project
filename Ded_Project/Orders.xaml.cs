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
    /// Логика взаимодействия для Orders.xaml
    /// </summary>
    public partial class Orders : UserControl
    {
        private NumbersRepository repository;
        public Orders()
        {
            InitializeComponent();
            repository = new NumbersRepository();
            DataContext = repository;
            repository.GetNumbers(User.user.Login);
            repository.getStates();
            ListNumbers.ItemsSource = repository.numbers;
            if (repository.numbers.Count == 0)
            {
                clean.Visibility = Visibility.Visible;
                ico.Visibility = Visibility.Visible;
                labelsearch.Visibility = Visibility.Collapsed;
            }
            else
            {
                icoSmall.Visibility = Visibility.Visible;
                labelsearch.Visibility = Visibility.Visible;
            }           
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            repository.SelectedItem.state = "Отменен";
            repository.updateState(repository.SelectedItem);
            string subject = "Отмена";
            string body = $"Доброго времени суток! \n Вы отменили бронирование номера {repository.SelectedItem.ID_Number}.\n Ждем Вас в нашем отеле!";
            Email.Send(subject, body, User.user.Email);
            repository.SelectedItem = null;
            back.Visibility = Visibility.Collapsed;
            chosen.Visibility = Visibility.Hidden;           
        }

        private void ListNumbers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (repository.SelectedItem != null)
            {
                back.Visibility = Visibility.Visible;
                chosen.Visibility = Visibility.Visible;
                var bc = new BrushConverter();
                back.IsEnabled = true;
                back.Content = "Отменить бронь";
                from.Visibility = Visibility.Visible;
                to.Visibility = Visibility.Visible;
                switch (repository.SelectedItem.state)
                {
                    case "Выполнен":
                        st.Foreground = (Brush)bc.ConvertFrom("#43a047");
                        back.IsEnabled = false;
                        back.Content = "Выполнен";
                        break;
                    case "Будет выполнен":
                        st.Foreground = (Brush)bc.ConvertFrom("#1e88e5");
                        break;
                    case "Выполняется":
                        st.Foreground = (Brush)bc.ConvertFrom("#ffb74d");
                        break;
                    case "Отменен":
                        back.IsEnabled = false;
                        back.Content = "Отменен";
                        from.Visibility = Visibility.Hidden;
                        to.Visibility = Visibility.Hidden;
                        st.Foreground = (Brush)bc.ConvertFrom("#b71c1c");
                        break;
                    default:
                        break;
                }
            }
        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            repository.lookStates();
            ListNumbers.ItemsSource = repository.tempNumbers;
        }
    }
}
