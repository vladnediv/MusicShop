using MusicAlbumsEF.Context;
using MusicAlbumsEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicAlbumsEF.Repository
{
    public class UserRepository : IRepository<User>
    {
        private readonly MusicDbContext _context;

        public UserRepository(MusicDbContext context)
        {
            _context = context;
        }
        public void Add(User item)
        {
            _context.Users.Add(item);
            _context.SaveChanges();
        }

        public void Delete(int Id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == Id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public void Delete(User item)
        {
            throw new NotImplementedException();
        }

        public User Get(int Id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == Id);
            return user;
        }

        public User GetUserByMail(string mail)
        {
            return _context.Users.FirstOrDefault(x => x.Email == mail);
        }

        public ICollection<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public void Update(User item)
        {
            _context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
