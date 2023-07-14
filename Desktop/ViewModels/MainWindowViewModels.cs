using Desktop.BaseViewModels;
using Desktop.Windows;
using Parcer.Models;
using Parcer.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Desktop.ViewModels
{
    public class MainWindowViewModels: NotifyPropertyChangedBase
    {
        private ProductContext _context;
        private Users _activeUser;
        private MainWindow _mainWindow;
        public MainWindowViewModels(ProductContext context,Users user,MainWindow mainWindow) 
        {
            _orderProducts=new ObservableCollection<ProductViewModel>();
            _context = context;
            _activeUser = user;
            _products = context.Products.ToList();
            _mainWindow= mainWindow;
        }
        private List<Product> _products { set; get; }
        public ObservableCollection<ProductViewModel> Products { 
            get
            {
                var colection=new ObservableCollection<ProductViewModel>();
                _products.Where(p=>p.Title.ToLower().Contains(SearchProduct.ToLower())).ToList().ForEach(p => colection.Add(new ProductViewModel(p)));
                return colection;
            }
        }
        public UserViewModel ActiveUser
        {
            get => new UserViewModel(_activeUser);            
        }
        private ProductViewModel _selectedProduct;
        public ProductViewModel SelectedProduct
        {
            get => _selectedProduct; 
            set
            {
                _selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
            }
        }
        private ProductViewModel _orderSelectedProduct;
        public ProductViewModel OrderSelectedProduct
        {
            get => _orderSelectedProduct;
            set
            {
                _orderSelectedProduct = value;
                OnPropertyChanged(nameof(OrderSelectedProduct));
            }
        }
        private string _searchProduct=string.Empty;
        public string SearchProduct
        {
            get => _searchProduct;
            set
            {
                _searchProduct = value;
                OnPropertyChanged(nameof(SearchProduct));
                OnPropertyChanged(nameof(Products));
            }
        }
        public ICommand Exit => new RelayCommand(x =>
        {
            _mainWindow.Close();
        },
       x => true);
        public ICommand LightTheme => new RelayCommand(x =>
        {
            IsLightTheme = true;
            OnPropertyChanged(nameof(Background));
            OnPropertyChanged(nameof(Background2));
            OnPropertyChanged(nameof(ButtonBackground));
            OnPropertyChanged(nameof(Foreground));
        },
       x => true);

        public ICommand DarkTheme => new RelayCommand(x =>
        {
            IsLightTheme = false;
            OnPropertyChanged(nameof(Background));
            OnPropertyChanged(nameof(Background2));
            OnPropertyChanged(nameof(ButtonBackground));
            OnPropertyChanged(nameof(Foreground));
        },
       x => true);
        public ICommand SaveFile => new RelayCommand(x =>
        {
            
            var file = "list" + ".xlsx";
            var temp = new ExcelProdictWriter();
            temp.SaveAs(file,  _context.Products.ToList());
        },
       x => true);
         public ICommand SendFile => new RelayCommand(x =>
        {

            var file = Guid.NewGuid().ToString("D") + ".xlsx";

            var temp = new ExcelProdictWriter();
            temp.SaveAs(file, _context.Products.ToList());

            SendMail.Send(_activeUser.Mail, file);
            Task.Run(() => System.IO.File.Delete(file));

            MessageBox.Show("Файл відправленно вам на пошту!");
        },
       x => true);

        public ICommand ProductOpenInBrowser => new RelayCommand(x =>
        {
            Process process = new Process();
            try
            {
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.FileName = SelectedProduct.FullUrl;
                process.Start();
            }
            catch(Exception ex)
            {

            }
        },
        x => 
        {
            return SelectedProduct!=null;
        });
        private ObservableCollection<ProductViewModel> _orderProducts;
        public ObservableCollection<ProductViewModel> OrderProducts { get { return _orderProducts; } }
        public ICommand AddToOrderCommand => new RelayCommand(x =>
        {
            TotalPrice += SelectedProduct.Price;
            _orderProducts.Add(SelectedProduct);
        },
        x =>
        {
            return true;
        });
        public ICommand RemoveFromOrder => new RelayCommand(x =>
        {
            TotalPrice -= OrderSelectedProduct.Price;
            _orderProducts.Remove(OrderSelectedProduct);
        },
       x =>
       {
           return true;
       });

        
        public ICommand ConfirmOrder => new RelayCommand(x =>
        {
            //var file = Guid.NewGuid().ToString("D") + ".xlsx";
            //List<Product> ordProd = _orderProducts.ToList<Product>();
            //var temp = new ExcelProdictWriter();
            //temp.SaveAs(file, ordProd);

            //SendMail.Send(_activeUser.Mail, file);
            //Task.Run(() => System.IO.File.Delete(file));

            //MessageBox.Show("Файл відправленно вам на пошту!");
        },
        x =>
        {
            return true;
        });
        private decimal _totalPrice;
        public decimal TotalPrice
        {
            get { return _totalPrice; }
            set { 
                _totalPrice = value;
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        private bool IsLightTheme = true;
        public System.Windows.Media.Brush Background
        {
            get
            {
                if (IsLightTheme)
                {
                    return System.Windows.Media.Brushes.LightGray;
                }
                else
                {
                    return System.Windows.Media.Brushes.Black;
                }

            }
        }
        public System.Windows.Media.Brush Background2
        {
            get
            {
                if (IsLightTheme)
                {
                    return System.Windows.Media.Brushes.Silver;
                }
                else
                {
                    return System.Windows.Media.Brushes.DarkSlateBlue;
                }

            }
        }
        public System.Windows.Media.Brush ButtonBackground
        {
            get
            {
                if (IsLightTheme)
                {
                    return System.Windows.Media.Brushes.AliceBlue;
                }
                else
                {
                    return System.Windows.Media.Brushes.CornflowerBlue;
                }

            }
        }
        public System.Windows.Media.Brush Foreground
        {
            get
            {
                if (IsLightTheme)
                {
                    return System.Windows.Media.Brushes.Black;
                }
                else
                {
                    return System.Windows.Media.Brushes.White;
                }

            }
        }

    }
}
