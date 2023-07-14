using Desktop.BaseViewModels;
using Parcer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.ViewModels
{
    public class UserViewModel: NotifyPropertyChangedBase
    {
        public Users User { get; set; }
        public UserViewModel(Users Users) { User = Users; }

        public string Login
        {
            get => User.Login;
            set
            {
                User.Login = value;
                OnPropertyChanged(nameof(Login));
            }
        }
        public string Name
        {
            get => User.Name;
            set
            {
                User.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string LastName
        {
            get => User.LastName;
            set
            {
                User.LastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }
        public string Mail
        {
            get => User.Mail;
            set
            {
                User.Mail = value;
                OnPropertyChanged(nameof(Mail));
            }
        }
        public string Phone
        {
            get => User.Phone;
            set
            {
                User.Phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }
        public string Birsday
        {
            get => User.Birsday.Value.ToString("d");
            set
            {
                //User.Birsday = value;
                OnPropertyChanged(nameof(Birsday));
            }
        }
    }
}
