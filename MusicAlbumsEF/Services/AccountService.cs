using MusicAlbumsEF.Models;
using MusicAlbumsEF.Repository;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicAlbumsEF.Services
{
    public class AccountService
    {
        private readonly UserRepository _userRepository;

        public AccountService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool RegisterUser(string password, string email, string name = "")
        {
            var IsExistsUser = _userRepository.GetUserByMail(email);
            if (IsExistsUser != null)
            {
                return false;
            }

            var user = new User() { Password = password, Name = name, Email = email, IsArtist = false };

            _userRepository.Add(user);

            return true;
        }

        public bool RegisterArtist(string password, string email, string name = "")
        {
            var IsExistsUser = _userRepository.GetUserByMail(email);
            if (IsExistsUser != null)
            {
                return false;
            }

            var user = new User() { Password = password, Name = name, Email = email, IsArtist = true };

            _userRepository.Add(user);

            return true;
        }

        public User GetUser(string mail)
        {
            return _userRepository.GetUserByMail(mail);
        }

        public bool AuthentificatedUser(string mail, string password)
        {
            var user = _userRepository.GetUserByMail(mail);
            if (user != null && user.Password == password) { return true; }

            return false;
        }
    }
}
