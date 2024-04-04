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
using System.Windows.Input;

namespace MusicAlbumsEF.ViewModels
{
    public class TrackViewModel : BaseViewModel
    {
        private readonly MusicPlayerService _musicPlayerService;
        public TrackViewModel(MusicPlayerService musicPlayerService)
        {
            _musicPlayerService = musicPlayerService;
            EditAlbumCommand = new RelayCommand(EditAlbum);
            DeleteAlbumCommand = new RelayCommand(DeleteAlbum);
            EditArtistCommand = new RelayCommand(EditArtist);
            EditSongCommand = new RelayCommand(EditSong);
            DeleteSongCommand = new RelayCommand(DeleteSong);
            DeleteArtistCommand = new RelayCommand(DeleteArtist);
        }
        private Album _Album;
        public Album Album { get { return _Album; } set { if (_Album != value) _Album = value; OnPropertyChanged(); } }

        public Action CloseAction { get; set; }

        //Commands

        public ICommand EditSongCommand { get; }
        private Track _SelectedTrack;
        public Track SelectedTrack { get { return _SelectedTrack; } set { if (_SelectedTrack != value) { _SelectedTrack = value; } } }
        public void EditSong()
        {
            var editTrackViewModel = (EditSongViewModel)AppServiceProvider.ServiceProvider.GetService(typeof(EditSongViewModel));
            editTrackViewModel.TrackToEdit = SelectedTrack;
            var window = (EditSongView)AppServiceProvider.ServiceProvider.GetService(typeof(EditSongView));
            editTrackViewModel.CloseAction += () => { window.Close(); };
            window.DataContext = editTrackViewModel;
            window.ShowDialog();   
        }

        public ICommand DeleteSongCommand { get; }
        public void DeleteSong()
        {
            var CanDelete = SelectedTrack != null && _musicPlayerService.GetTrackById(SelectedTrack.Id) != null;
            if (CanDelete)
            {
                _musicPlayerService.DeleteTrack(SelectedTrack);
                Album.Tracks = new ObservableCollection<Track>(_musicPlayerService.GetTracksById(Album.Id).ToList());
            }
        }


        public ICommand EditAlbumCommand { get; }
        public void EditAlbum()
        {
            var editAlbumViewModel = (EditAlbumViewModel)AppServiceProvider.ServiceProvider.GetService(typeof(EditAlbumViewModel));
            editAlbumViewModel.AlbumToEdit = Album;
            var window = (EditAlbumView)AppServiceProvider.ServiceProvider.GetService(typeof(EditAlbumView));
            editAlbumViewModel.CloseAction += () => { window.Close(); };
            window.DataContext = editAlbumViewModel;
            window.ShowDialog();
        }

           


        public ICommand DeleteAlbumCommand { get; }
        public void DeleteAlbum()
        {
            _musicPlayerService.DeleteAlbum(Album);
            CloseAction.Invoke();
        }


        public ICommand EditArtistCommand { get; }
        public void EditArtist()
        {
            var editArtistViewModel = (EditArtistViewModel)AppServiceProvider.ServiceProvider.GetService(typeof(EditArtistViewModel));
            editArtistViewModel.ArtistToEdit = _musicPlayerService.GetArtist(Album.ArtistId);
            var window = (EditArtistView)AppServiceProvider.ServiceProvider.GetService(typeof(EditArtistView));
            editArtistViewModel.CloseAction += () => { window.Close(); };
            window.DataContext = editArtistViewModel;
            window.ShowDialog();
        }

        
        public ICommand DeleteArtistCommand { get; }
        public void DeleteArtist()
        {
            _musicPlayerService.DeleteArtist(_musicPlayerService.GetArtist(Album.ArtistId));
            CloseAction.Invoke();
        }
    }
}
