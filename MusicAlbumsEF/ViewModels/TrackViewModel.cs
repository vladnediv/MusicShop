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
            Discount = 10;
            _musicPlayerService = musicPlayerService;
            EditAlbumCommand = new RelayCommand(EditAlbum);
            DeleteAlbumCommand = new RelayCommand(DeleteAlbum);
            EditArtistCommand = new RelayCommand(EditArtist);
            EditSongCommand = new RelayCommand(EditSong, IsSongChosen);
            DeleteSongCommand = new RelayCommand(DeleteSong, IsSongChosen);
            SetDiscountCommand = new RelayCommand(SetDiscount);
            IncrementDiscountCommand = new RelayCommand(IncrementDiscount);
            AddSongCommand = new RelayCommand(AddSong);
        }

        private Album _Album;
        public Album Album { get { return _Album; } set { if (_Album != value) _Album = value; OnPropertyChanged(); } }

        public ObservableCollection<Track> Songs { get { return _Songs; } set { _Songs = value; OnPropertyChanged(); } }
        private ObservableCollection<Track> _Songs;

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
        public bool IsSongChosen()
        {
            return SelectedTrack != null;
        }

        public ICommand AddSongCommand { get; }
        public void AddSong()
        {
            var window = ((AddTrackView)AppServiceProvider.ServiceProvider.GetService(typeof(AddTrackView)));
            //edit


            var viewModel = ((AddTrackViewModel)AppServiceProvider.ServiceProvider.GetService(typeof(AddTrackViewModel)));
            viewModel.SelectedAlbum = Album;
            window.DataContext = viewModel;


            //edit
            window.ShowDialog();

            Album.Tracks = new ObservableCollection<Track>(_musicPlayerService.GetTracksById(Album.Id).ToList());
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
            CloseAction();
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

        //Discount
        private int _Discount;
        public int Discount { get { return _Discount; } set { if (value >= 0 && value <= 100) { _Discount = value; OnPropertyChanged(); } } }

        public ICommand IncrementDiscountCommand { get; }
        public void IncrementDiscount()
        {
            if (Discount + 10 <= 100)
            {
                Discount += 10;
            }
        }
        public ICommand SetDiscountCommand { get; }
        public void SetDiscount()
        {
            var newPrice = Album.SellPrice - Album.SellPrice / 100 * Discount;
            if(newPrice > Album.PrimeCost)
            {
                _musicPlayerService.EditAlbum(Album.Id, Album.Name, Album.ArtistName, Album.PublishingYear, Album.PrimeCost, newPrice, Album.Genre);
                CloseAction();
            }
            else
            {
                MessageBox.Show("Sell price should be bigger than prime cost!");
            }
        }
    }
}
