using Desktop.BaseViewModels;
using Parcer.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.ViewModels
{
    public class ProductViewModel: NotifyPropertyChangedBase
    {
        public Product Product { get; set; }
        public ProductViewModel(Product product) 
        {
            Product = product;
        }
        public string ImgUrl
        {
            get => Product.ImgUrl;
            set
            {
                Product.ImgUrl = value;
                OnPropertyChanged(nameof(ImgUrl));
            }
        }
        public string FullUrl
        {
            get => Product.FullUrl;
            set
            {
                Product.FullUrl = value;
                OnPropertyChanged(nameof(FullUrl));
            }
        }
        public string Title {
            get => Product.Title;
                set {
                Product.Title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        public decimal Price
        {
            get => Product.Price;
            set
            {
                Product.Price = value;
                OnPropertyChanged(nameof(Price));
                OnPropertyChanged(nameof(PriceColor));
            }
        }
        public System.Windows.Media.Brush PriceColor
        {
            get
            {
                if (Price>10)
                {
                    return System.Windows.Media.Brushes.Red;
                }
                return System.Windows.Media.Brushes.Green;

            }
        }
        public string Code
        {
            get => Product.Code;
            set
            {
                Product.Code = value;
                OnPropertyChanged(nameof(Code));
            }
        }
        public string Description
        {
            get => Product.Description;
            set
            {
                Product.Description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public string Available
        {
            get => Product.Available? "in stock" : "out of stock";
            set
            {
                //Product.Available = value;
                OnPropertyChanged(nameof(Available));
                OnPropertyChanged(nameof(AvailableColor));
            }
        }
        public System.Windows.Media.Brush AvailableColor
        {
            get
            {
                if(Available== "in stock")
                {
                    return System.Windows.Media.Brushes.Green;
                }
                else
                {
                    return System.Windows.Media.Brushes.Red;
                }
                
            }
        }
    }
}
