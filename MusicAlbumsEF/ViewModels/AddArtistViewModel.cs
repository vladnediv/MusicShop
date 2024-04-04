using MusicAlbumsEF.Commands;
using MusicAlbumsEF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MusicAlbumsEF.ViewModels
{
    public class AddArtistViewModel : BaseViewModel
    {
        private readonly MusicPlayerService _musicPlayerService;
        public AddArtistViewModel(MusicPlayerService musicPlayerService)
        {
            _musicPlayerService = musicPlayerService;
            ArtistName = "";
            ArtistCountry = "";
            AddArtistCommand = new RelayCommand(AddArtist, CanAddArtist);
        }

        public string ArtistName { get; set; }


        public string ArtistCountry { get; set;}

        
        public int YearOfBirth { get; set; }

        //AddArtistCommand
        public ICommand AddArtistCommand { get; }
        public void AddArtist()
        {
            _musicPlayerService.AddArtist(new Models.Artist() { Name = ArtistName, Country = ArtistCountry, YearOfBirth = YearOfBirth, Albums = new List<Models.Album>() });
        }
        public bool CanAddArtist()
        {
            var artist = _musicPlayerService.GetArtistByName(ArtistName);
            return artist == null && ArtistName.Length > 0 && ArtistCountry.Length > 0 && YearOfBirth > 0;
        }
    }
}
