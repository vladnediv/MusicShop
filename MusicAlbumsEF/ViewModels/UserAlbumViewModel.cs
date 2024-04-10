using Microsoft.Extensions.DependencyInjection;
using MusicAlbumsEF.Commands;
using MusicAlbumsEF.Infrastructure;
using MusicAlbumsEF.Models;
using MusicAlbumsEF.Services;
using MusicAlbumsEF.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MusicAlbumsEF.ViewModels
{
    public class UserAlbumViewModel : BaseViewModel
    {
        private readonly MusicPlayerService _musicPlayerService;

        public UserAlbumViewModel(MusicPlayerService musicPlayerService)
        {
            _musicPlayerService = musicPlayerService;
            User = AccountService.ActiveUser;
            User.Albums = new ObservableCollection<Album>(_musicPlayerService.GetUserAlbums(User.Id));
            Albums = new System.Collections.ObjectModel.ObservableCollection<Album>(_musicPlayerService.GetAllAlbums());
            AddAlbumCommand = new RelayCommand(BuyAlbum);
            EditAccountCommand = new RelayCommand(EditUserAccount);
            LogoutCommand = new RelayCommand(Logout);
        }

        //Show row 1 with user balance, user name and buttons
        public User User { get { return _user; } set { _user = value; OnPropertyChanged(); } }
        private User _user;

        public decimal? Balance { get { return User.Balance; } set { Balance = User.Balance; OnPropertyChanged(); } }

        public int AmountItems { get { return User.Albums.Count; } }

        //Commands
        public ICommand AddAlbumCommand { get; }
        public void BuyAlbum()
        {
           bool IfAlbumPurchased = false;
           if (_musicPlayerService.GetUserAlbums(User.Id) != null)
            {
                IfAlbumPurchased = _musicPlayerService.GetUserAlbums(User.Id).Any(x => x.Id == AlbumId);
            }
            if (IfAlbumPurchased)
            {
                var psi = new ProcessStartInfo
                {
                    FileName = _musicPlayerService.GetAlbum(AlbumId).WebLink,
                    UseShellExecute = true
                };
                Process.Start(psi);
            }
            if(IfAlbumPurchased == false)
            {
                if (User.Balance >= _musicPlayerService.GetAlbum(AlbumId).SellPrice)
                {
                    User.Balance -= _musicPlayerService.GetAlbum(AlbumId).SellPrice;
                    User.Albums.Add(_musicPlayerService.GetAlbum(AlbumId));
                    _musicPlayerService.BuyAlbum(User.Id, AlbumId);
                    OnPropertyChanged("Balance");
                    OnPropertyChanged("AmountItems");
                }
                else MessageBox.Show("Not enough money on balance!");
            }
            else MessageBox.Show("This Album has already been purchased!");
        }

        public ICommand EditAccountCommand { get; }
        public void EditUserAccount()
        {
            var window = ((EditUserAccountView)AppServiceProvider.ServiceProvider.GetService(typeof(EditUserAccountView)));
            var viewModel = ((EditUserAccountViewModel)AppServiceProvider.ServiceProvider.GetService(typeof(EditUserAccountViewModel)));
            viewModel.CloseAction = () => { window.Close(); };
            window.DataContext = viewModel;
            window.ShowDialog();
        }

       

        //Search for albums by options
        private string _SearchText;
        public string SearchText { get { return _SearchText; } set { _SearchText = value; OnPropertyChanged(); FilterAlbums(_SearchText); } }
        public ComboBoxItem SelectedOption { get; set; }
        private void FilterAlbums(string SearchText)
        {
            if (SelectedOption != null)
            {
                if (SelectedOption.Content.ToString() == "Name")
                {
                    var albums = _musicPlayerService.GetAllAlbums().Where(album => album.Name.Contains(SearchText)).ToList();
                    Albums = new ObservableCollection<Album>(albums);
                }
                else if (SelectedOption.Content.ToString() == "Genre")
                {
                    var albums = _musicPlayerService.GetAllAlbums().Where(album => album.Genre.Contains(SearchText)).ToList();
                    Albums = new ObservableCollection<Album>(albums);
                }
                else if(SelectedOption.Content.ToString() == "Artist")
                {
                    var albums = _musicPlayerService.GetAllAlbums().Where(album => album.ArtistName.Contains(SearchText)).ToList();
                    Albums = new ObservableCollection<Album>(albums);
                }
                else if(SelectedOption.Content.ToString() == "Purchased")
                {
                    var albums = _musicPlayerService.GetUserAlbums(User.Id).ToList();
                    Albums = new ObservableCollection<Album>(albums);
                }
            }
            else System.Windows.MessageBox.Show("Select a search option!");
        }


        public ICommand LogoutCommand { get; }
        public void Logout()
        {
            var window = (LoginView)AppServiceProvider.ServiceProvider.GetService(typeof(LoginView));
            System.Windows.Application.Current.Windows[0].Close();
            window.ShowDialog();
        }

        //Show all albums
        public ObservableCollection<Album> Albums { get { return _albums; } set { if (_albums != value) { _albums = value; OnPropertyChanged(); } } }
        private ObservableCollection<Album> _albums;
        public int AlbumId { get; set; }

        
    }
}
