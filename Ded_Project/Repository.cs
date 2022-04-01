using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ded_Project
{
    class Repository : IRepository<Profile_Date>, INotifyPropertyChanged
    {
        HotelEntities db;
        public Repository()
        {
            db = new HotelEntities();
        }
        private Profile_Date selectedItem = null;
        public Profile_Date SelectedItem
        {
            set
            {
                selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
            get
            {
                return selectedItem;
            }
        }

        public ObservableCollection<Profile_Date> profile_s { private set; get; }
        public ObservableCollection<Profile_Date> profile_Temp { set; get; } = new ObservableCollection<Profile_Date>();

        public Admin GetAdmin()
        {
            return db.Admins.FirstOrDefault();
        }

        public void GetUsers()
        {
            profile_s = new ObservableCollection<Profile_Date>();
            foreach(var item in db.Profiles)
            {
                profile_s.Add(new Profile_Date(item.name, item.surname, item.middle_name, Profile_Date.Change(item.sexxx), item.Entry.login, item.Entry.password, item.Entry.email, item.imageData));
            }
        }

        public Profile_Date GetUser(string login)
        {
            Profile tempUser = db.Profiles.FirstOrDefault(c => c.Entry.login == login);
            if (tempUser != null)
            {
                return new Profile_Date(tempUser.name, tempUser.surname, tempUser.middle_name, Profile_Date.Change(tempUser.sexxx), tempUser.Entry.login, tempUser.Entry.password, tempUser.Entry.email, tempUser.imageData);
            }
            else return null;          
        }

        public void Search(string login)
        {           
        }

        public void createUser(Profile_Date user)
        {
            Profile temp = new Profile();
            temp.name = user.Name;
            temp.surname = user.Surname;
            temp.middle_name = user.Middle;
            if(user.Sex == 'М')
            {
                temp.sexxx = 1;
            }
            else
            {
                temp.sexxx = 0;
            }
            temp.Entry = new Entry();
            temp.Entry.login = user.Login;
            temp.Entry.password = user.Password;
            temp.Entry.email = user.Email;
            temp.imageData = Image.Convert(@"D:\ФИТ\2ой курс\2ой семестр\ООП\Лучшая курсовая\kursach\Ded_Project\Ded_Project\Resourses\1.png");
            temp.Entry.ID_Profile = temp.ID_Profile;
            db.Profiles.Add(temp);
            db.Entries.Add(temp.Entry);
        }

        public void updateUser(Profile_Date user)
        {
            Profile temp = db.Profiles.FirstOrDefault(c => c.Entry.login == user.Login);
            Entry tempEn = db.Entries.FirstOrDefault(c => c.ID_Profile == temp.ID_Profile);

            temp.name = user.Name;
            temp.surname = user.Surname;
            temp.middle_name = user.Middle;
            if (user.Sex == 'М')
            {
                temp.sexxx = 1;
            }
            else
            {
                temp.sexxx = 0;
            }
            tempEn.login = user.Login;
            tempEn.email = user.Email;
            db.Entry(temp).State = System.Data.Entity.EntityState.Modified;
            db.Entry(tempEn).State = System.Data.Entity.EntityState.Modified;
        }

        public void DeleteUser(string login)
        {
            Profile user = db.Profiles.FirstOrDefault(c => c.Entry.login == login);
            Entry entry = db.Entries.FirstOrDefault(c => c.ID_Profile == user.ID_Profile);
            if(user != null)
            {
                db.Entries.Remove(entry);
                db.Profiles.Remove(user);
                Save();
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
