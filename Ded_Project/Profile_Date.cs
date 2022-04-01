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
using System.ComponentModel.DataAnnotations;
using Microsoft.Win32;
using System.IO;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Ded_Project
{
    class Profile_Date : INotifyPropertyChanged
    {
        public Profile_Date(string name, string surname, string middle, char sex, string login, string password, string email)
        {
            this.name = name;
            this.surname = surname;
            this.middle = middle;
            this.login = login;
            this.email = email;
            this.password = password;
            this.sex = sex;
        }
        public Profile_Date(string name, string surname, string middle, char sex, string login, string password, string email, byte[] image)
        {
            this.Name = name;
            this.Surname = surname;
            this.Middle = middle;
            this.Login = login;
            this.Email = email;
            this.Password = password;
            if (image != null)
            {
                this.Image = Ded_Project.Image.LoadImage(image);
            }
            this.Sex = sex;
        }

        string name;
        string surname;
        string middle;
        string login;
        string password;
        string email;
        char sex;
        BitmapImage image = null;


        public string Name
        {
            set
            {
                Regex regex = new Regex(@"^([А-Я]{1})([а-я]*)$");
                if(regex.IsMatch(value))
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
                else
                {
                    name = name;
                }
            }
            get { return name; }
        }

        public string Surname
        {
            set
            {
                Regex regex = new Regex(@"^([А-Я]{1})([а-я]*)$");
                if (regex.IsMatch(value))
                {
                    surname = value;
                    OnPropertyChanged("Surname");
                }
                else
                {
                    surname = surname;
                }
            }
            get { return surname; }
        }

        public string Middle
        {
            set
            {
                Regex regex = new Regex(@"^([А-Я]{1})([а-я]*)$");
                if (regex.IsMatch(value))
                {
                    middle = value;
                    OnPropertyChanged("Middle");
                }
                else
                {
                    middle = middle;
                }
            }
            get { return middle; }
        }

        public string Login
        {
            set
            {
                login = value;
                OnPropertyChanged("Login");
            }
            get { return login; }
        }

        public string Password
        {
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
            get { return password; }
        }

        public string Email
        {
            set
            {
                Regex regex = new Regex(@"^\S{2,30}[@]{1}(gmail|mail)(\.ru|\.com)$");
                if (regex.IsMatch(value))
                {
                    email = value;
                    OnPropertyChanged("Email");
                }
                else
                {
                    email = email;
                }
            }
            get { return email; }
        }

        public char Sex
        {
            set
            {
                sex = value;
                OnPropertyChanged("Sex");
            }
            get { return sex; }
        }

        public BitmapImage Image
        {
            set
            {
                image = value;
                OnPropertyChanged("Image");
            }
            get { return image; }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public static char Change(int? value)
        {
            if (value == 1)
            {
                return 'М';
            }
            else return 'Ж';
        }
    }
}
