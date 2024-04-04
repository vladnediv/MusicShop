using MusicAlbumsEF.Commands;
using MusicAlbumsEF.Models;
using MusicAlbumsEF.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace MusicAlbumsEF.ViewModels
{
    public class AddTrackViewModel : BaseViewModel
    {
        private readonly MusicPlayerService _musicPlayerService;
        public AddTrackViewModel(MusicPlayerService musicPlayerService)
        {
            _musicPlayerService = musicPlayerService;
            SongTitle = "";
            SongDuration = "";
            Albums = new List<Album>(_musicPlayerService.GetAllAlbums());
            AddTrackCommand = new RelayCommand(AddTrack, CanAddTrack);
        }
        public string SongTitle { get; set; }


        public string SongDuration { get; set; }


        public int PlaceInAlbum { get; set; }


        public List<Album> Albums { get; set; }
        public Album SelectedAlbum { get; set; }


        //AddTrackCommand
        public ICommand AddTrackCommand { get; }
        public void AddTrack()
        {
            var track = new Track() { Album = SelectedAlbum, Title = SongTitle, Duration = SongDuration, AlbumId = SelectedAlbum.Id, PlaceInOrder = PlaceInAlbum };
            _musicPlayerService.GetAlbum(SelectedAlbum.Id).Tracks = new ObservableCollection<Track>(_musicPlayerService.GetTracksById(SelectedAlbum.Id));
            _musicPlayerService.AddTrack(track);
        }
        public bool CanAddTrack()
        {
            if (SelectedAlbum != null)
            {
                if (SelectedAlbum.Tracks == null)
                {
                    SelectedAlbum.Tracks = new ObservableCollection<Track>();
                }
            }
            return SongTitle.Length > 0 && SongDuration.Length > 0 && PlaceInAlbum > 0 && SelectedAlbum != null;
        }
    }
}
