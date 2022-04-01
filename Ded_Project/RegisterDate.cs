using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Ded_Project
{
    class RegisterDate
    {
        public RegisterDate(string name, string surname, string middle, char sex, string login, string password, string email)
        {
            this.name = name;
            this.surname = surname;
            this.middle = middle;
            this.login = login;
            this.email = email;
            this.password = password;
            this.sex = sex;
        }

        [Required(ErrorMessage = "Это поле обязательно")]
        [RegularExpression(@"^([А-Я]{1})([а-я]*)$", ErrorMessage = "Данные введено неверно!")]
        public string name { private set; get; }
        [Required(ErrorMessage = "Это поле обязательно")]
        [RegularExpression(@"^([А-Я]{1})([а-я]*)$", ErrorMessage = "Данные введено неверно!")]
        public string surname { private set; get; }
        [Required(ErrorMessage = "Это поле обязательно")]
        [RegularExpression(@"^([А-Я]{1})([а-я]*)$", ErrorMessage = "Данные введено неверно!")]
        public string middle { private set; get; }
        [Required(ErrorMessage = "Это поле обязательно")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Невалидный логин")]
        public string login { private set; get; }
        [Required(ErrorMessage = "Это поле обязательно")]
        public string password { private set; get; }
        [Required(ErrorMessage = "Это поле обязательно")]
        [RegularExpression(@"^\S{2,30}[@]{1}(gmail|mail)(\.ru|\.com)$", ErrorMessage = "Неверный адрес почты!")]
        public string email { private set; get; }
        public char sex { private set; get; }
        public BitmapImage image { private set; get; } = null;
    }
}
