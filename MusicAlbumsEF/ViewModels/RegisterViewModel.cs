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
using System.Windows.Forms;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace MusicAlbumsEF.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private readonly AccountService _accountService;
        private readonly MusicPlayerService _musicPlayerService;
        private readonly PasswordHasher _passwordHasher;

        public Action CloseEvent { get; set; }
        public ICommand RegisterUserCommand { get; set; }
        public ICommand RegisterArtistCommand { get; set; }

        public RegisterViewModel(AccountService accountService, PasswordHasher passwordHasher, MusicPlayerService musicPlayerService)
        {
            _accountService = accountService;
            _passwordHasher = passwordHasher;
            RegisterUserCommand = new RelayCommand(RegisterUser, CanRegister);
            RegisterArtistCommand = new RelayCommand(RegisterArtist, CanRegister);
            _musicPlayerService = musicPlayerService;
        }

        private string _password;
        private string _confirmPassword;
        private string _email;
        private string _name;
        private string _country;
        private int _year;
        public string Password { get { return _password; } set { _password = value; OnPropertyChanged(); } }
        public string ConfirmPassword { get { return _confirmPassword; } set { _confirmPassword = value; OnPropertyChanged(); } }
        public string Email { get { return _email; } set { _email = value; OnPropertyChanged(); } }
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged(); } }
        public string Country { get { return _country; } set { _country = value; OnPropertyChanged(); } }
        public int Year { get { return _year; } set { _year = value; OnPropertyChanged(); } }

        private void RegisterUser()
        {
            //HERE OPEN WINDOW FOR USER
            var res = _accountService.RegisterUser(_passwordHasher.HasPassword(_password), Email, Name);
            if (res == true)
            {
                var window = (UserAlbumView)AppServiceProvider.ServiceProvider.GetService(typeof(UserAlbumView));
                var viewModel = (UserAlbumViewModel)AppServiceProvider.ServiceProvider.GetService(typeof(UserAlbumViewModel));
                viewModel.User = _accountService.GetUser(Email);
                AccountService.ActiveUser = _accountService.GetUser(Email);
                window.DataContext = viewModel;
                window.Show();
            }
            CloseEvent();
        }
        private void RegisterArtist()
        {
            //HERE OPEN WINDOW FOR ARTIST
            var res = _accountService.RegisterArtist(_passwordHasher.HasPassword(_password), Email, Name);

            if (res == true)
            {
                var user = _accountService.GetUser(Email);
                _musicPlayerService.AddArtist(new Models.Artist()
                {
                    Albums = new List<Models.Album>(),
                    Country = Country, 
                    Name = user.Name,
                    UserId = user.Id,
                    YearOfBirth = Year
                });

                var window = (AlbumView)AppServiceProvider.ServiceProvider.GetService(typeof(AlbumView));
                var viewModel = (AlbumViewModel)AppServiceProvider.ServiceProvider.GetService(typeof(AlbumViewModel));
                viewModel.User = _accountService.GetUser(Email);
                AccountService.ActiveUser = _accountService.GetUser(Email);
                viewModel.Close += () => { window.Close(); };
                window.DataContext = viewModel;
                window.Show();
            }
            CloseEvent();
        }

        private bool CanRegister()
        {
            if (!String.IsNullOrEmpty(_email) && !string.IsNullOrEmpty(_password))
            {
                if (_password == _confirmPassword)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
