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
    /// Логика взаимодействия для DatePick.xaml
    /// </summary>
    public partial class DatePick : Window
    {
        int ID_Number;
        public DatePick(int id)
        {
            InitializeComponent();
            picker.BlackoutDates.AddDatesInPast();
            ID_Number = id;
            using(HotelEntities db = new HotelEntities())
            {
                IEnumerable<Date> temp = db.Dates.ToList().Where(c => c.ID_Number == ID_Number && c.order_state != "Отменен");

                foreach(var item in temp)
                {
                    DateTime first;
                    if (item.dateFrom >= DateTime.Today)
                    {
                        first = item.dateFrom;
                    }
                    else
                    {
                        first = DateTime.Today;
                    }
                    DateTime last = item.dateTo;
                    CalendarDateRange DateRange = new CalendarDateRange(first,last);
                    picker.BlackoutDates.Add(DateRange);                 
                }
            }
        }

        private void ok_Click(object sender, RoutedEventArgs e)
        {
            List<DateTime> dates = picker.SelectedDates.ToList();
            if(dates.Count > 0)
            {
                using(HotelEntities db = new HotelEntities())
                {
                    Date temp = new Date();
                    temp.ID_Number = ID_Number;
                    temp.ID_Profile = db.Profiles.FirstOrDefault(c => c.Entry.login == User.user.Login).ID_Profile;
                    temp.dateFrom = dates[0];
                    if(temp.dateFrom == DateTime.Today)
                    {
                        temp.order_state = "Выполняется";
                    }
                    else
                    {
                        temp.order_state = "Будет выполнен";
                    }
                    temp.dateTo = dates[dates.Count - 1];
                    db.Dates.Add(temp);
                    db.SaveChanges();         
                }

                string subject = "Бронирование";
                string body = $"Доброго времени суток!\nВы забронировали номер {ID_Number} на даты с {dates.First().ToShortDateString()} по {dates.Last().ToShortDateString()}.\nЗа дополнительной информацией обращайтесь по телефону: 80 (29) 77 77 7 77.\nСпасибо, что выбрали наш отель, мы ценим каждого гостя!";
                Email.Send(subject, body, User.user.Email);
                MessageBox.Show("Даты забронированы!");
                this.DialogResult = true;
            }
            else
            {
                this.DialogResult = false;
            }
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
