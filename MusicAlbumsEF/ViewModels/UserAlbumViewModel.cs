using MusicAlbumsEF.Models;
using MusicAlbumsEF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicAlbumsEF.ViewModels
{
    public class UserAlbumViewModel : BaseViewModel
    {
        private readonly AccountService _accountService;
        private readonly PasswordHasher _passwordHasher;

        public User User { get; set; }
        public UserAlbumViewModel(AccountService accountService, PasswordHasher passwordHasher)
        {
            _accountService = accountService;
            _passwordHasher = passwordHasher;
        }
    }
}
