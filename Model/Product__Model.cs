using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPF_Rozetka.Model
{
    public class Product__Model : INotifyPropertyChanged
    {
        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; OnPropertyChanged("Title"); }
        }
        private double price;

        public double Price
        {
            get { return price; }
            set { price = value; OnPropertyChanged("Price"); }
        }
        private string imgPath;

        public string ImgPath
        {
            get { return imgPath; }
            set {  imgPath = value; OnPropertyChanged("Img"); }
        }

        public static int idCounter = 0;

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string productURL;

        public string ProductURL
        {
            get { return productURL; }
            set { productURL = value; }
        }


        public Product__Model() => id = idCounter++;


        private RelayCommand buyButtonClick;
        
        public RelayCommand BuyButtonClick
        {
            get
            {
                return buyButtonClick ?? (buyButtonClick = new RelayCommand(() =>
                {
                    System.Diagnostics.Process.Start(this.ProductURL);
                }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;  

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
