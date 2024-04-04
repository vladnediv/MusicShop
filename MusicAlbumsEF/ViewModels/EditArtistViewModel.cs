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
    public class EditArtistViewModel : BaseViewModel
    {
        private readonly MusicPlayerService _musicPlayerService;
        public EditArtistViewModel(MusicPlayerService musicPlayerService)
        {
            _musicPlayerService = musicPlayerService;
            EditArtistCommand = new RelayCommand(EditArtist, CanEditArtist);
        }
        public Artist ArtistToEdit { get; set; }
        public Action CloseAction { get; set; }

        //Commands
        public ICommand EditArtistCommand { get; }
        public bool CanEditArtist()
        {
            return ArtistToEdit.Name.Length > 0 && ArtistToEdit.Country.Length > 0 && ArtistToEdit.YearOfBirth > 0;
        }
        public void EditArtist()
        {
            _musicPlayerService.EditArtist(ArtistToEdit.Id, ArtistToEdit.Name, ArtistToEdit.Country, ArtistToEdit.YearOfBirth);
            CloseAction.Invoke();
        }
    }
}
