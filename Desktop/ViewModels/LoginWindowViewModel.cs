using Desktop.BaseViewModels;
using Desktop.Windows;
using Parcer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Desktop.ViewModels
{
    public class LoginWindowViewModel: NotifyPropertyChangedBase
    {
        private ProductContext _context;
        private List<Users> _users;
        private LoginWindow _loginWindow;
        public LoginWindowViewModel(ProductContext context,LoginWindow logWind)
        {
            _context = context;
            _users = context.Users.ToList();
            _loginWindow= logWind;
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
        public ICommand LoginCommand => new RelayCommand(x =>
        {
            var user = _users.Find(u=>u.Login==this.Login && u.Password==this.Password);
            if (user!=null)
            {
                var mainWindow=new MainWindow(user);
                mainWindow.Show();
                _loginWindow.Close();
            }
            else
            {
                MessageBox.Show("Invalid password!");
            }
        },
        x=>true);
        public ICommand RegistrationCommand => new RelayCommand(x =>
        {
            var registrationWind = new RegistrationWindow();
            registrationWind.Show();
            _loginWindow.Close();
        },
        x => true);
    }
}
