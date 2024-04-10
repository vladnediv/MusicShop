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
    public class EditUserAccountViewModel : BaseViewModel
    {
        private readonly MusicPlayerService _musicPlayerService;

        public EditUserAccountViewModel(MusicPlayerService musicPlayerService)
        {
            _musicPlayerService = musicPlayerService;
            UserBalance = AccountService.ActiveUser.Balance;
            EditBalanceCommand = new RelayCommand(EditBalance);
        }

        public decimal? UserBalance { get; set; }

        public Action CloseAction { get; set; }
        public ICommand EditBalanceCommand { get; }
        public void EditBalance()
        {
            if (UserBalance == null) { UserBalance = 0; }
            decimal? balance = UserBalance;
            _musicPlayerService.EditBalance(AccountService.ActiveUser.Id, balance);
            CloseAction();
        }
    }
}
