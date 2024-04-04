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
using System.Windows.Forms;
using System.Windows.Input;

namespace MusicAlbumsEF.ViewModels
{
    public class AlbumViewModel : BaseViewModel
    {
        private readonly MusicPlayerService _musicPlayerService;
        public AlbumViewModel(MusicPlayerService musicPlayerService)
        {
            _musicPlayerService = musicPlayerService;

            Albums = new ObservableCollection<Album>();
            //Commands
            OpenAlbumCommand = new RelayCommand(OpenAlbum);
            AddAlbumCommand = new RelayCommand(AddAlbum);
            AddArtistCommand = new RelayCommand(AddArtist);
            AddTrackCommand = new RelayCommand(AddTrack);
        }

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
        private void FilterAlbums(string name)
        {
            var Artist = _musicPlayerService.GetAllArtists().FirstOrDefault(x => x.UserId == User.Id);
            if(Artist != null)
            {
                var albums = _musicPlayerService.GetAllAlbumsByArtist(Artist.Id).Where(album => album.Name.Contains(name)).ToList();
                Albums = new ObservableCollection<Album>(albums);
            }
            else { Albums = new ObservableCollection<Album>(); }
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
            var window = (TrackView)AppServiceProvider.ServiceProvider.GetService(typeof(TrackView));
            trackViewModel.CloseAction += () => { window.Close(); };
            window.DataContext = trackViewModel;
            window.ShowDialog();

            var Artist = _musicPlayerService.GetAllArtists().FirstOrDefault(x => x.UserId == User.Id);

            if (Artist != null)
            {
                Albums = new ObservableCollection<Album>(_musicPlayerService.GetAllAlbumsByArtist(Artist.Id));
            }
            else Albums = new ObservableCollection<Album>();
        }





        //Add Album
        public ICommand AddAlbumCommand { get; }
        public void AddAlbum()
        {
            var ViewModel = (AddAlbumViewModel)AppServiceProvider.ServiceProvider.GetService(typeof(AddAlbumViewModel));
            ViewModel.SelectedArtist = _musicPlayerService.GetAllArtists().FirstOrDefault(x => x.UserId == User.Id);
            var window = (AddAlbumView)AppServiceProvider.ServiceProvider.GetService(typeof(AddAlbumView));
            window.DataContext = ViewModel;
            window.ShowDialog();
            var Artist = _musicPlayerService.GetAllArtists().FirstOrDefault(x => x.UserId == User.Id);
            if (Artist != null)
            {
                Albums = new ObservableCollection<Album>(_musicPlayerService.GetAllAlbumsByArtist(Artist.Id));
            }
            else Albums = new ObservableCollection<Album>();
        }

        //Add Artist
        public ICommand AddArtistCommand { get; }
        public void AddArtist()
        {
            var window = (AddArtistView)AppServiceProvider.ServiceProvider.GetService(typeof(AddArtistView));
            window.ShowDialog();
        }

        //Add Track
        public ICommand AddTrackCommand { get; }
        public void AddTrack()
        {
            var window = (AddTrackView)AppServiceProvider.ServiceProvider.GetService(typeof(AddTrackView));
            window.ShowDialog();
        }

    }
}