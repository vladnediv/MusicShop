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
    public class EditAlbumViewModel : BaseViewModel
    {
        private readonly MusicPlayerService _musicPlayerService;
        public EditAlbumViewModel(MusicPlayerService musicPlayerService)
        {
            _musicPlayerService = musicPlayerService;
            Artists = new List<Artist>(_musicPlayerService.GetAllArtists());
            Genres = new List<string>() { Album.GenreEnum.Rock.ToString(), Album.GenreEnum.Metal.ToString(), Album.GenreEnum.Indie.ToString(),
            Album.GenreEnum.Jazz.ToString(), Album.GenreEnum.Classic.ToString(), Album.GenreEnum.Pop.ToString() };
            EditAlbumCommand = new RelayCommand(EditAlbum, CanEditAlbum);
        }

        public Album AlbumToEdit { get; set; }

        public List<Artist> Artists {  get; set; }

        public List<string> Genres { get; set; }

        public Action CloseAction { get; set; }

        //Commands
        public ICommand EditAlbumCommand { get; }
        public bool CanEditAlbum()
        {
            var IfCan = AlbumToEdit.Name.Length > 0 && AlbumToEdit.Artist != null && AlbumToEdit.PublishingYear > 0 && AlbumToEdit.PublishingYear < 2024;
            return IfCan;
        }
        public void EditAlbum()
        {
            AlbumToEdit.ArtistName = _musicPlayerService.GetArtist(AlbumToEdit.ArtistId).Name;
            _musicPlayerService.EditAlbum(AlbumToEdit.Id, AlbumToEdit.Name, AlbumToEdit.ArtistName, AlbumToEdit.PublishingYear, AlbumToEdit.Genre);
            CloseAction.Invoke();
        }
    }
}
