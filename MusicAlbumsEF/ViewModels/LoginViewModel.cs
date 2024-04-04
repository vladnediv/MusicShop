using Microsoft.VisualBasic.Logging;
using Microsoft.Win32;
using MusicAlbumsEF.Commands;
using MusicAlbumsEF.Infrastructure;
using MusicAlbumsEF.Services;
using MusicAlbumsEF.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace MusicAlbumsEF.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly AccountService _accountService;
        private readonly PasswordHasher _passwordHasher;

        public Action CloseAction { get; set; }
        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        public LoginViewModel(AccountService authentificationService, PasswordHasher passwordHasher)
        {
            _accountService = authentificationService;
            _passwordHasher = passwordHasher;
            LoginCommand = new RelayCommand(Login, CanLogin);
            RegisterCommand = new RelayCommand(Register, CanRegister);
        }

        private string email;
        private string password;

        public string Email { get { return email; } set { email = value; OnPropertyChanged(); } }

        public string Password { get { return password; } set { password = value; OnPropertyChanged(); } }

        private bool CanLogin()
        {
            return !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password);
        }


        private void Login()
        {
            var res = _accountService.AuthentificatedUser(Email, _passwordHasher.HasPassword(Password));
            if (res == true)
            {
                var user = _accountService.GetUser(Email);

                //TO EDIT: DIFFERENT WINDOWS IF USER OR ADMIN
                //IF ADMIN:
                if(user.IsArtist) {
                    var window = (AlbumView)AppServiceProvider.ServiceProvider.GetService(typeof(AlbumView));
                    var viewModel = (AlbumViewModel)AppServiceProvider.ServiceProvider.GetService(typeof(AlbumViewModel));
                    viewModel.User = user;
                    window.DataContext = viewModel;
                    CloseAction();
                    window.Show();
                }
                //TO EDIT:
                //IF USER:
                else
                {
                    var window = (UserAlbumView)AppServiceProvider.ServiceProvider.GetService(typeof(UserAlbumView));
                    var viewModel = (UserAlbumViewModel)AppServiceProvider.ServiceProvider.GetService(typeof(UserAlbumViewModel));
                    viewModel.User = user;
                    window.DataContext = viewModel;
                    CloseAction();
                    window.Show();
                }

            }
            else MessageBox.Show("User not Found", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private bool CanRegister()
        {
            return true;
        }

        private void Register()
        {
            //OPEN REGISTER WINDOW EDIT
            ((RegisterView)AppServiceProvider.ServiceProvider.GetService(typeof(RegisterView))).Show();
            CloseAction();
        }


    }
}
