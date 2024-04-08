using MusicAlbumsEF.Commands;
using MusicAlbumsEF.Infrastructure;
using MusicAlbumsEF.Models;
using MusicAlbumsEF.Services;
using MusicAlbumsEF.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;

namespace MusicAlbumsEF.ViewModels
{
    public class AlbumViewModel : BaseViewModel
    {
        private readonly MusicPlayerService _musicPlayerService;
        private readonly AccountService _accountService;
        public AlbumViewModel(MusicPlayerService musicPlayerService, AccountService accountService)
        {
            _accountService = accountService;
            _musicPlayerService = musicPlayerService;
            Artist = _musicPlayerService.GetAllArtists().FirstOrDefault(x => x.UserId == AccountService.ActiveUser.Id);
            Albums = new ObservableCollection<Album>();
            //Commands
            OpenAlbumCommand = new RelayCommand(OpenAlbum);
            AddAlbumCommand = new RelayCommand(AddAlbum);
            EditAccountCommand = new RelayCommand(EditAccount);
            LogoutCommand = new RelayCommand(Logout);
        }

        public Artist Artist { get { return _artist; } set { _artist = value; OnPropertyChanged(); } }
        private Artist _artist;

        //test code
        public string ArtistName
        {
            get { return Artist.Name; }
            set { ArtistName = value; OnPropertyChanged(); }
        }
        public string Email;
        //test code

        public User User
        {
            get { return _User; }
            set
            {
                _User = value;
                var Artist = _musicPlayerService.GetAllArtists().FirstOrDefault(x => x.UserId == User.Id);
                if(Artist != null)
                {
                    var albums = _musicPlayerService.GetAllAlbumsByArtist(Artist.Id);
                    Albums = new ObservableCollection<Album>(albums);
                }
                else { Albums = new ObservableCollection<Album>(); }
            }
        }
        private User _User;

        //Album filtering
        private string _SearchText;
        public string SearchText
        {
            get { return _SearchText; }
            set {
                if (_SearchText != value)
                {
                    _SearchText = value;
                    OnPropertyChanged();
                    FilterAlbums(_SearchText);
                }
            }
        }
        public ComboBoxItem SelectedOption { get; set; }
        private void FilterAlbums(string SearchText)
        {
            if (SelectedOption != null)
            {
                if (SelectedOption.Content.ToString() == "Name")
                {
                    var albums = _musicPlayerService.GetAllAlbumsByArtist(Artist.Id).Where(album => album.Name.Contains(SearchText)).ToList();
                    Albums = new ObservableCollection<Album>(albums);
                }
                else if (SelectedOption.Content.ToString() == "Genre")
                {
                    var albums = _musicPlayerService.GetAllAlbumsByArtist(Artist.Id).Where(album => album.Genre.Contains(SearchText)).ToList();
                    Albums = new ObservableCollection<Album>(albums);
                }
            }
            else System.Windows.MessageBox.Show("Select a search option!");
        }


        //Albums View
        private ObservableCollection<Album> _Albums;
        public ObservableCollection<Album> Albums { get { return _Albums; } set { _Albums = value; OnPropertyChanged(); } }





        //Open Album
        public ICommand OpenAlbumCommand { get; }
        public int AlbumId { get; set; }
        public void OpenAlbum()
        {
            //Here open new window to view songs in the album
            var album = _musicPlayerService.GetAlbum(AlbumId);
            album.Tracks = new ObservableCollection<Track>(_musicPlayerService.GetTracksById(AlbumId).OrderByDescending(x => x.PlaceInOrder).Reverse());
            var trackViewModel = (TrackViewModel)AppServiceProvider.ServiceProvider.GetService(typeof(TrackViewModel));
            trackViewModel.Album = album;
            var windowTrack = (TrackView)AppServiceProvider.ServiceProvider.GetService(typeof(TrackView));
            trackViewModel.CloseAction += () => { windowTrack.Close(); };
            windowTrack.DataContext = trackViewModel;
            windowTrack.ShowDialog();

            Albums = new ObservableCollection<Album>(_musicPlayerService.GetAllAlbumsByArtist(Artist.Id));
        }





        //Add Album
        public ICommand AddAlbumCommand { get; }
        public void AddAlbum()
        {
            var ViewModel = (AddAlbumViewModel)AppServiceProvider.ServiceProvider.GetService(typeof(AddAlbumViewModel));
            ViewModel.SelectedArtist = _musicPlayerService.GetAllArtists().FirstOrDefault(x => x.UserId == AccountService.ActiveUser.Id);
            var window = (AddAlbumView)AppServiceProvider.ServiceProvider.GetService(typeof(AddAlbumView));
            window.DataContext = ViewModel;
            window.ShowDialog();
            var Artist = _musicPlayerService.GetAllArtists().FirstOrDefault(x => x.UserId == AccountService.ActiveUser.Id);
            if (Artist != null)
            {
                Albums = new ObservableCollection<Album>(_musicPlayerService.GetAllAlbumsByArtist(Artist.Id));
            }
            else Albums = new ObservableCollection<Album>();
        }

        //Add Artist
        public ICommand EditAccountCommand { get; }
        public void EditAccount()
        {
            var window = (EditAccountView)AppServiceProvider.ServiceProvider.GetService(typeof(EditAccountView));
            window.ShowDialog();
        }

        //Logout
        public Action Close { get; set; }
        public ICommand LogoutCommand { get; }
        public void Logout()
        {
            var window = (LoginView)AppServiceProvider.ServiceProvider.GetService(typeof(LoginView));
            System.Windows.Application.Current.Windows[0].Close();
            window.ShowDialog();
        }

    }
}