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
    public class EditAccountViewModel : BaseViewModel
    {
        private readonly MusicPlayerService _musicPlayerService;
        public EditAccountViewModel(MusicPlayerService musicPlayerService)
        {
            
            _musicPlayerService = musicPlayerService;
            Artist = _musicPlayerService.GetAllArtists().FirstOrDefault(x => x.UserId == AccountService.ActiveUser.Id);
            ArtistId = Artist.Id;
            ArtistName = Artist.Name;
            ArtistCountry = Artist.Country;
            YearOfBirth = Artist.YearOfBirth;
            EditArtistCommand = new RelayCommand(EditArtist, CanEditArtist);
        }

        public Artist Artist { get { return _artist; } set { _artist = value; OnPropertyChanged(); } }
        private Artist _artist;

        public int ArtistId { get; set; }

        public string ArtistName { get; set; }


        public string ArtistCountry { get; set;}

        
        public int YearOfBirth { get; set; }

        //AddArtistCommand
        public ICommand EditArtistCommand { get; }
        public void EditArtist()
        {
            _musicPlayerService.EditArtist(ArtistId, ArtistName, ArtistCountry, YearOfBirth);
        }
        public bool CanEditArtist()
        {
            return ArtistName.Length > 0 && ArtistCountry.Length > 0 && YearOfBirth > 0;
        }
    }
}
