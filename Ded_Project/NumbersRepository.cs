using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Ded_Project
{
    class NumbersRepository : INumberRepository<TempNumber>, INotifyPropertyChanged
    {
        private ObservableCollection<Item> mPlacing;
        private HashSet<Item> mCheckedPlaces;

        public IEnumerable<Item> placing { get { return mPlacing; } }

        private ObservableCollection<Item> mComfort;
        private HashSet<Item> mCheckedComfort;

        public IEnumerable<Item> comfort { get { return mComfort; } }

        private HashSet<Element> mCheckedStates;
        private ObservableCollection<Element> mStates;
        public IEnumerable<Element> States { get { return mStates; } }

        private string placingtext;
        public string placingText
        {
            set
            {
                if (value != null && value.IndexOf("Item") != -1) return;
                placingtext = value;
                OnPropertyChanged("placingText");
            }
            get
            {
                return placingtext;
            }
        }

        private string comforttext;
        public string comfortText
        {
            set
            {
                if (value != null && value.IndexOf("Item") != -1) return;
                comforttext = value;
                OnPropertyChanged("comfortText");
            }
            get
            {
                return comforttext;
            }
        }

        private TempNumber selectedItem = null;
        public TempNumber SelectedItem
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

        public void getStates()
        {
            mStates = new ObservableCollection<Element>();
            mCheckedStates = new HashSet<Element>();
            mStates.CollectionChanged += State_CollectionChanged;

            foreach (var item in db.States)
            {
                mStates.Add(new Element(item.order_state));
            }
        }

        private void State_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (Element item in e.OldItems)
                {
                    item.PropertyChanged -= State_PropertyChanged;
                    mCheckedStates.Remove(item);
                }
            }
            if (e.NewItems != null)
            {
                foreach (Element item in e.NewItems)
                {
                    item.PropertyChanged += State_PropertyChanged;
                    if (item.IsChecked) mCheckedStates.Add(item);
                }
            }
            UpdateState();
        }

        public void lookStates()
        {
            if (mCheckedStates.Count == 0)
            {
                tempNumbers = new ObservableCollection<TempNumber>(numbers);
            }
            else
            {
                List<TempNumber> tempOrd = new List<TempNumber>();
                foreach (var item in mCheckedStates)
                {
                    tempOrd.AddRange(numbers.Where(s => s.state == item.State));
                }
                tempNumbers = new ObservableCollection<TempNumber>(tempOrd);
            }
        }

        private void State_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsChecked")
            {
                Element item = (Element)sender;
                if (item.IsChecked)
                {
                    mCheckedStates.Add(item);
                }
                else
                {
                    mCheckedStates.Remove(item);
                }
                UpdateState();
            }
        }

        private void UpdateState()
        {
            switch (mCheckedStates.Count)
            {
                case 0:
                    stateText = " Все";
                    break;
                case 1:
                    stateText = mCheckedStates.First().State;
                    break;
                default:
                    stateText = " Выбрано несколько";
                    break;
            }
        }

        private string statetext;
        public string stateText
        {
            set
            {
                if (value != null && value.IndexOf("Element") != -1) return;
                statetext = value;
                OnPropertyChanged("stateText");
            }
            get
            {
                return statetext;
            }
        }

        public bool DropOpen
        {
            get { return dropopen; }
            set { dropopen = value; OnPropertyChanged("stateText"); }
        }
        private bool dropopen = false;

        HotelEntities db;
        public NumbersRepository()
        {
            db = new HotelEntities();
        }

        public ObservableCollection<TempNumber> numbers { private set; get; }
        public ObservableCollection<TempNumber> tempNumbers { private set; get; }

        public void Search(int isFree)
        {
            List<TempNumber> tempNumb = new List<TempNumber>();
            if (mCheckedComfort.Count > mCheckedPlaces.Count && mCheckedPlaces.Count != 0)
            {
                foreach (var cf in mCheckedComfort)
                {
                    foreach (var cp in mCheckedPlaces)
                    {
                        tempNumb.AddRange(numbers.Where(c => c.comfort_type == cf.Comfort && c.place_type == cp.Place));
                    }
                }
            }
            else if (mCheckedPlaces.Count > mCheckedComfort.Count && mCheckedComfort.Count != 0)
            {
                foreach (var cp in mCheckedPlaces)
                {
                    foreach (var cf in mCheckedComfort)
                    {
                        tempNumb.AddRange(numbers.Where(c => c.comfort_type == cf.Comfort && c.place_type == cp.Place));
                    }

                }
            }
            else if ((mCheckedComfort.Count == mCheckedPlaces.Count) && (mCheckedPlaces.Count != 0) && (mCheckedComfort.Count != 0))
            {
                foreach (var cp in mCheckedPlaces)
                {
                    foreach (var cf in mCheckedComfort)
                    {
                        tempNumb.AddRange(numbers.Where(c => c.comfort_type == cf.Comfort && c.place_type == cp.Place));

                    }
                }
            }
            else if(mCheckedComfort.Count == 0 && mCheckedPlaces.Count != 0)
            {
                foreach (var cp in mCheckedPlaces)
                {
                    tempNumb.AddRange(numbers.Where(c => c.place_type == cp.Place));
                }
            }
            else if (mCheckedPlaces.Count == 0 && mCheckedComfort.Count != 0)
            {
                foreach (var cp in mCheckedComfort)
                {
                    tempNumb.AddRange(numbers.Where(c => c.comfort_type == cp.Comfort));
                }
            }
            else if ((mCheckedPlaces.Count == 0) && (mCheckedComfort.Count == 0))
            {
                tempNumb = new List<TempNumber>(numbers);           
            }
            IsFreeNow(tempNumb, isFree);
        }

        public void IsFreeNow(List<TempNumber> temp, int isfree)
        {
            if (isfree == 1)
                tempNumbers = new ObservableCollection<TempNumber>(temp.Where(c => c.isFree == 1));
            else
                tempNumbers = new ObservableCollection<TempNumber>(temp);
        }

        public void stateFree()
        {
            foreach(var numb in db.Numbers)
            {
                numb.isFree = 1;

                updateNumber(numb);
            }
            Save();
        }

        public void CheckingIsFree()
        {
            foreach(var date in db.Dates)
            {
                if(DateTime.Today >= date.dateFrom && DateTime.Today <= date.dateTo && date.order_state != "Отменен")
                {
                    date.Number.isFree = 0;
                    updateNumber(date.Number);                  
                }
            }
            Save();           
        }

        public void Fill()
        {
            mPlacing = new ObservableCollection<Item>();
            mCheckedPlaces = new HashSet<Item>();
            mPlacing.CollectionChanged += Placing_CollectionChanged;

            mComfort = new ObservableCollection<Item>();
            mCheckedComfort = new HashSet<Item>();
            mComfort.CollectionChanged += Comfort_CollectionChanged;

            foreach (var item in db.Comforts)
            {
                mComfort.Add(new Item(item.Comfort1, item.Comfort1));
            }

            foreach (var item in db.Placings)
            {
                mPlacing.Add(new Item(item.Place));
            }
        }

        private void Placing_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (Item item in e.OldItems)
                {
                    item.PropertyChanged -= Place_PropertyChanged;
                    mCheckedPlaces.Remove(item);
                }
            }
            if (e.NewItems != null)
            {
                foreach (Item item in e.NewItems)
                {
                    item.PropertyChanged += Place_PropertyChanged;
                    if (item.IsChecked) mCheckedPlaces.Add(item);
                }
            }
            UpdatePlaceText();
        }

        private void Comfort_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (Item item in e.OldItems)
                {
                    item.PropertyChanged -= Comfort_PropertyChanged;
                    mCheckedComfort.Remove(item);
                }
            }
            if (e.NewItems != null)
            {
                foreach (Item item in e.NewItems)
                {
                    item.PropertyChanged += Comfort_PropertyChanged;
                    if (item.IsChecked) mCheckedComfort.Add(item);
                }
            }
            UpdateComfortText();
        }

        private void Place_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsChecked")
            {
                Item item = (Item)sender;
                if (item.IsChecked)
                {
                    mCheckedPlaces.Add(item);
                }
                else
                {
                    mCheckedPlaces.Remove(item);
                }
                UpdatePlaceText();
            }
        }
        private void Comfort_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsChecked")
            {
                Item item = (Item)sender;
                if (item.IsChecked)
                {
                    mCheckedComfort.Add(item);
                }
                else
                {
                    mCheckedComfort.Remove(item);
                }
                UpdateComfortText();
            }
        }

        private void UpdatePlaceText()
        {
            switch (mCheckedPlaces.Count)
            {
                case 0:
                    placingText = " Выберите:";
                    break;
                case 1:
                    placingText = mCheckedPlaces.First().Place;
                    break;
                default:
                    placingText = " Выбрано несколько";
                    break;
            }
        }
        private void UpdateComfortText()
        {
            switch (mCheckedComfort.Count)
            {
                case 0:
                    comfortText = " Выберите:";
                    break;
                case 1:
                    comfortText = mCheckedComfort.First().Comfort;
                    break;
                default:
                    comfortText = " Выбрано несколько";
                    break;
            }
        }

        public List<Placing> places;
        public List<Comfort> comf;

        public void plc()
        {
            places = new List<Placing>();
            comf = new List<Comfort>();
            foreach(var item in db.Placings)
            {
                places.Add(item);
            }
            foreach(var item in db.Comforts)
            {
                comf.Add(item);
            }
        }

        public void createNumber(TempNumber number)/////создание
        {
            Number number1 = new Number();
            number1.comfort_type = number.comfort_type;
            number1.isFree = number.isFree;
            number1.place_type = number.place_type;
            number1.imageNumber = Image.Convert(number.imageNumber);
            number1.ID_Number = number.ID_Number;
            db.Numbers.Add(number1);
            Save();
        }

        public void DeleteNumber(int id)
        {
            Number numb = db.Numbers.FirstOrDefault(c => c.ID_Number == id);
            if(numb != null)
            {
                db.Numbers.Remove(numb);
                Save();
            }
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

        public TempNumber GetNumber(int id)
        {
            Number item = db.Numbers.FirstOrDefault(c => c.ID_Number == id);
            if (item != null)
            {
                return new TempNumber(item.ID_Number, item.place_type, item.comfort_type, item.isFree, Image.LoadImage(item.imageNumber));
            }
            else
                return null;
        }

        public void GetNumbers()
        {
            numbers = new ObservableCollection<TempNumber>();
            foreach(var item in db.Numbers)
            {
                numbers.Add(new TempNumber(item.ID_Number, item.place_type, item.comfort_type, item.isFree, Image.LoadImage(item.imageNumber)));
            }          
        }

        public void GetNumbers(string login)
        {
            Entry user = db.Entries.FirstOrDefault(c => c.login == login);
            numbers = new ObservableCollection<TempNumber>();
            foreach (var item in db.Dates.Where(c => c.ID_Profile == user.ID_Profile))
            {              
                numbers.Add(new TempNumber(item.Number.ID_Number, item.Number.place_type, item.Number.comfort_type, item.Number.isFree, Image.LoadImage(item.Number.imageNumber), item.dateFrom.ToShortDateString(), item.dateTo.ToShortDateString(), item.ID_Date, item.order_state));
            }
        }

        public void GetNumbersWithDate(string login)
        {
            Entry user = db.Entries.FirstOrDefault(c => c.login == login);
            numbers = new ObservableCollection<TempNumber>();
            foreach (var item in db.Dates.Where(c => c.ID_Profile == user.ID_Profile))
            {
                 numbers.Add(new TempNumber(item.Number.ID_Number, item.Number.place_type, item.Number.comfort_type, item.Number.isFree, Image.LoadImage(item.Number.imageNumber), item.dateFrom.ToShortDateString(), item.dateTo.ToShortDateString(), item.ID_Date, item.order_state, item.dateFrom, item.dateTo));
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void updateNumber(Number number)
        {
            db.Entry(number).State = System.Data.Entity.EntityState.Modified;
        }

        public void updateNumber(TempNumber number)
        {
            Number temp = db.Numbers.FirstOrDefault(c => c.ID_Number == number.ID_Number);
            temp.isFree = number.isFree;
            temp.imageNumber = Image.Convert(number.imageNumber);
            temp.place_type = number.place_type;
            temp.comfort_type = number.comfort_type;
            
            db.Entry(temp).State = System.Data.Entity.EntityState.Modified;
        }

        public void updateState(TempNumber number)
        {
            Date temp = db.Dates.FirstOrDefault(c => c.ID_Date == number.orderNumber);
            temp.order_state = number.state;
            
            db.Entry(temp).State = System.Data.Entity.EntityState.Modified;
            Save();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public bool DropOpenPlace
        {
            get { return dropopenplace; }
            set { dropopenplace = value; OnPropertyChanged("placingText"); }
        }
        private bool dropopenplace = false;

        public bool DropOpenComfort
        {
            get { return dropopencomfort; }
            set { dropopencomfort = value; OnPropertyChanged("comfortText"); }
        }
        private bool dropopencomfort = false;
    }

    internal class Element : INotifyPropertyChanged
    {
        public string State { get; private set; }      

        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                OnPropertyChanged("IsChecked");
            }
        }

        private bool _isChecked;

        public Element(string state)
        {
            State = state;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }

    internal class Item : INotifyPropertyChanged
    {
        public string Place { get; private set; }

        public string Comfort { get; private set; }

        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                OnPropertyChanged("IsChecked");
            }
        }

        private bool _isChecked;

        public Item(string place)
        {
            Place = place;
        }
        public Item(string place,string comfort)
        {
            Comfort = comfort;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
