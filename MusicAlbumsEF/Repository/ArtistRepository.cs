using MusicAlbumsEF.Context;
using MusicAlbumsEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicAlbumsEF.Repository
{
    public class ArtistRepository : IRepository<Artist>
    {
        private readonly MusicDbContext _context;
        public ArtistRepository(MusicDbContext context)
        {
            _context = context;
        }

        public void Add(Artist item)
        {
            _context.Artists.Add(item);
            _context.SaveChanges();
        }
        public void Delete(Artist item)
        {
            _context.Artists.Remove(item);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            var item = _context.Artists.FirstOrDefault(x => x.Id == id);
            if (item != null) _context.Artists.Remove(item);
            _context.SaveChanges();
        }
        public Artist Get(int id)
        {
            var item = _context.Artists.FirstOrDefault(x => x.Id == id);
            return item;
        }
        public ICollection<Artist> GetAll()
        {
            return _context.Artists.ToList();
        }
        public void Update(Artist item)
        {
            _context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
