using MusicAlbumsEF.Commands;
using MusicAlbumsEF.Models;
using MusicAlbumsEF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MusicAlbumsEF.ViewModels
{
    public class EditSongViewModel : BaseViewModel
    {
        private readonly MusicPlayerService _musicPlayerService;
        public EditSongViewModel(MusicPlayerService musicPlayerService)
        {
            _musicPlayerService = musicPlayerService;
            Albums = new List<Album>(_musicPlayerService.GetAllAlbums());
            EditTrackCommand = new RelayCommand(EditTrack, CanEditTrack);
        }

        public Track TrackToEdit { get; set; }
        public List<Album> Albums { get; set; }

        public Action CloseAction { get; set; }

        //Commands
        public ICommand EditTrackCommand { get; }
        public bool CanEditTrack()
        {
            return TrackToEdit.Title.Length > 0 && TrackToEdit.Duration.Length > 3 && TrackToEdit.PlaceInOrder > 0 && TrackToEdit.PlaceInOrder != null && TrackToEdit.AlbumId > 0;
        }
        public void EditTrack()
        {
            _musicPlayerService.EditTrack(TrackToEdit.Id, TrackToEdit.Title, TrackToEdit.Duration, TrackToEdit.PlaceInOrder);
            CloseAction.Invoke();
        }
    }
}
