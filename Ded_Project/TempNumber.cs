using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;

namespace Ded_Project
{
    class TempNumber
    {
        public TempNumber(int iD_Number, string place_type, string comfort_type, int? isFree, BitmapImage imageNumber)
        {
            ID_Number = iD_Number;
            this.place_type = place_type;
            this.comfort_type = comfort_type;
            this.isFree = isFree;
            this.imageNumber = imageNumber;
        }

        public TempNumber(int iD_Number, string place_type, string comfort_type, int? isFree, BitmapImage imageNumber, string From, string To, int orderNumber,string state)
        {
            ID_Number = iD_Number;
            this.place_type = place_type;
            this.comfort_type = comfort_type;
            this.isFree = isFree;
            this.imageNumber = imageNumber;
            this.From = From;
            this.To = To;
            this.orderNumber = orderNumber;
            this.state = state;
        }

        public TempNumber(int iD_Number, string place_type, string comfort_type, int? isFree, BitmapImage imageNumber, string From, string To, int orderNumber,string state, DateTime DateFrom, DateTime DateTo)
        {
            ID_Number = iD_Number;
            this.place_type = place_type;
            this.comfort_type = comfort_type;
            this.isFree = isFree;
            this.imageNumber = imageNumber;
            this.From = From;
            this.To = To;
            this.orderNumber = orderNumber;
            this.state = state;
            this.DateFrom = DateFrom;
            this.DateTo = DateTo;
        }

        public int ID_Number { get; set; }
        public string place_type { get; set; }
        public string comfort_type { get; set; }
        public Nullable<int> isFree { get; set; }
        public BitmapImage imageNumber { get; set; }

        public string From { get; set; } 
        public string To { get; set; } 
        public DateTime DateFrom { get; set; } 
        public DateTime DateTo { get; set; } 
        public string state { get; set; }

        public int orderNumber { get; set; }
    }
}