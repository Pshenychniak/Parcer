using Desktop.BaseViewModels;
using Desktop.Windows;
using Parcer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Desktop.ViewModels
{
    public class RegistrationWindowViewModel: NotifyPropertyChangedBase
    {
        private ProductContext _context;
        private RegistrationWindow _registrationWindow;
        public RegistrationWindowViewModel(ProductContext context, RegistrationWindow regWind)
        {
            _context = context;
            _registrationWindow = regWind;
        }
        private string _login;
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }
        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        private string _confirmPassword;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }
        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        private string _phone;
        public string Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }
        private DateTime _birsday;
        public DateTime Birsday
        {
            get => _birsday;
            set
            {
                _birsday = value;
                OnPropertyChanged(nameof(Birsday));
            }
        }
        public ICommand RegistrationCommand => new RelayCommand(x =>
        {
            if (IsValidUser())
            {
                _context.Users.Add(new Users()
                {
                    Login = this.Login,
                    Password = this.Password,
                    Name = this.Name,
                    LastName = this.LastName,                    
                    Mail = this.Email,
                    Phone = this.Phone,
                    Birsday = this.Birsday
                });
                _context.SaveChanges();
                new LoginWindow().Show();
                _registrationWindow.Close();
            }
        },
        x => true);
        public ICommand GoToLoginWindow => new RelayCommand(x =>
        {           
                new LoginWindow().Show();
                _registrationWindow.Close();            
        },
        x => true);
        
        private bool IsValidUser()
        {
            if (Password != ConfirmPassword)
                return false;
            else if (String.IsNullOrEmpty(Password)
                || String.IsNullOrEmpty(ConfirmPassword)
                || String.IsNullOrEmpty(Login)
                || String.IsNullOrEmpty(Name)
                || String.IsNullOrEmpty(LastName)
                || String.IsNullOrEmpty(Phone)
                || String.IsNullOrEmpty(Email))
                return false;
            return true;
        }
    }
}
