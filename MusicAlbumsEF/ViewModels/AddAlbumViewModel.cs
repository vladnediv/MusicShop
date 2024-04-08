using MusicAlbumsEF.Commands;
using MusicAlbumsEF.Models;
using MusicAlbumsEF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Net.WebRequestMethods;

namespace MusicAlbumsEF.ViewModels
{
    public class AddAlbumViewModel : BaseViewModel
    {
        private readonly MusicPlayerService _musicPlayerService;
        public AddAlbumViewModel(MusicPlayerService musicPlayerService)
        {
            _musicPlayerService = musicPlayerService;
            Artists = new List<Artist>(_musicPlayerService.GetAllArtists());
            Genres = new List<string>() { Album.GenreEnum.Rock.ToString(), Album.GenreEnum.Metal.ToString(), Album.GenreEnum.Indie.ToString(),
            Album.GenreEnum.Jazz.ToString(), Album.GenreEnum.Classic.ToString(), Album.GenreEnum.Pop.ToString() };
            AlbumTitle = "";
            SelectedGenre = "";
            PicturePath = "";
            SelectedArtist = _musicPlayerService.GetAllArtists().FirstOrDefault(x => x.UserId == AccountService.ActiveUser.Id);

            //Commands
            AddAlbumCommand = new RelayCommand(AddNewAlbum, CanAddNewAlbum);
        }

        public string AlbumTitle { get; set; }


        private List<Artist> _Artists;
        public List<Artist> Artists { get { return _Artists; } set { _Artists = value; OnPropertyChanged(); } }
        public Artist SelectedArtist { get; set; }


        public int YearOfPublishing { get; set; }


        public List<string> Genres { get; set; }

        public string SelectedGenre { get; set; }

        public string PicturePath { get; set; }

        public decimal PrimeCost { get; set; }
        public decimal SellPrice { get; set; }


        //AddAlbumCommand
        public ICommand AddAlbumCommand { get; }
        public void AddNewAlbum()
        {
            var artist = _musicPlayerService.GetAllArtists().FirstOrDefault(x => x.UserId == AccountService.ActiveUser.Id);
            var album = new Album()
            {
                Name = AlbumTitle,
                ArtistName = artist.Name,
                Artist = artist,
                PublishingYear = YearOfPublishing,
                Genre = SelectedGenre,
                PicturePath = PicturePath,
                ArtistId = artist.Id,
                Tracks = null,
                //DELETE AFTER
                PrimeCost = PrimeCost,
                SellPrice = SellPrice
                //
        };
            /*SelectedArtist.Albums.Add(album);*/
            _musicPlayerService.AddAlbum(album);
        }
        public bool CanAddNewAlbum()
        {
            if(PicturePath.Length == 0)
            {
                PicturePath = "https://static.vecteezy.com/system/resources/previews/014/166/470/original/headphones-icon-in-simple-style-vector.jpg";
            }
            var album = _musicPlayerService.GetAlbumByName(AlbumTitle);
            var artist = _musicPlayerService.GetAllArtists().FirstOrDefault(x => x.UserId == AccountService.ActiveUser.Id);
            var IfCan = album == null && AlbumTitle.Length > 0 && YearOfPublishing > 0 && YearOfPublishing < 2024 && PrimeCost > 0 && SellPrice > PrimeCost && SelectedGenre.Length > 0;
            return IfCan;
        }

    }
}
