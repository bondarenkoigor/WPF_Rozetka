using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using WPF_Rozetka.Model;
using GalaSoft.MvvmLight.Command;
using System.IO;

namespace WPF_Rozetka.ViewModel
{
    public class MainWindow__ViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Product__Model> products;
        public ObservableCollection<Product__Model> Products
        {
            get { return FilterProducts(); }
            set { products = value; OnPropertyChanged("Products"); }
        }
        public MainWindow__ViewModel()
        {
            this.products = new ObservableCollection<Product__Model>();
            Read();

        }

        private string adminMode;

        public string AdminMode
        {
            get { return adminMode == "True" ? "Visible" : "Hidden"; }
            set { adminMode = value; OnPropertyChanged("AdminMode"); }
        }


        #region filtering
        private string searchBarText;

        public string SearchBarText
        {
            get { return searchBarText; }
            set { searchBarText = value; OnPropertyChanged("Products"); }
        }

        private string sorting;

        public string Sorting
        {
            get { return sorting; }
            set { sorting = value.ToString(); OnPropertyChanged("Products"); }
        }
        private ObservableCollection<Product__Model> FilterProducts()
        {
            var filteredProducts = new ObservableCollection<Product__Model>();

            if (searchBarText != null && searchBarText != String.Empty)
                foreach (var product in products)
                {
                    if (product.Title.Contains(searchBarText)) filteredProducts.Add(product);
                }
            else filteredProducts = this.products;

            if (Sorting == "От дешевых к дорогим") filteredProducts = new ObservableCollection<Product__Model>(filteredProducts.OrderBy(product => product.Price));
            else if (Sorting == "От дорогих к дешевым") filteredProducts = new ObservableCollection<Product__Model>(filteredProducts.OrderByDescending(product => product.Price));

            return filteredProducts;
        }
        #endregion

        #region adding
        private RelayCommand addCommand;

        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ?? (addCommand = new RelayCommand(() =>
                {
                    this.Products.Add(new Product__Model() { Title = this.TitleForAdding, Price = this.PriceForAdding, ProductURL = this.URLForAdding, ImgPath = this.PicURLForAdding });
                }));
            }
        }
        private string titleForAdding;

        public string TitleForAdding
        {
            get { return titleForAdding; }
            set { titleForAdding = value; }
        }

        private double priceForAdding;

        public double PriceForAdding
        {
            get { return priceForAdding; }
            set { priceForAdding = value; }
        }

        private string _URLForAdding;

        public string URLForAdding
        {
            get { return _URLForAdding; }
            set { _URLForAdding = value; }
        }
        private string picURLForAdding;

        public string PicURLForAdding
        {
            get { return picURLForAdding; }
            set { picURLForAdding = value; }
        }



        #endregion

        #region deleting
        private RelayCommand<int> deleteCommand;

        public RelayCommand<int> DeleteCommand
        {
            get
            {
                return deleteCommand ?? (deleteCommand = new RelayCommand<int>((id) =>
                {

                }));
            }
            set { deleteCommand = value; }
        }

        #endregion

        public void Save()
        {
            File.WriteAllText("SaveFile.txt", "");

            foreach (var product in this.Products)
            {
                File.AppendAllText("SaveFile.txt", String.Format("{0}\t{1}\t{2}\t{3}\n", product.Title, product.Price, product.ProductURL, product.ImgPath));
            }
        }

        public void Read()
        {
            if (!File.Exists("SaveFile.txt")) return;

            string[] _products = File.ReadAllLines("SaveFile.txt");
            foreach (string product in _products)
            {
                string[] param = product.Split('\t');
                this.Products.Add(new Product__Model() { Title = param[0], Price = double.Parse(param[1]), ProductURL = param[2], ImgPath = param[3] });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
