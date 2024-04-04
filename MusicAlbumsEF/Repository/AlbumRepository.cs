using MusicAlbumsEF.Context;
using MusicAlbumsEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicAlbumsEF.Repository
{
    public class AlbumRepository : IRepository<Album>
    {
        private readonly MusicDbContext _context;

        public AlbumRepository(MusicDbContext context)
        {
            _context = context;
        }

        public void Add(Album item)
        {
            _context.Albums.Add(item);
            _context.SaveChanges();
        }
        public void Delete(Album item)
        {
            _context.Albums.Remove(item);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            var item = _context.Albums.FirstOrDefault(x => x.Id == id);
            if(item != null) _context.Albums.Remove(item);
            _context.SaveChanges();
        }
        public Album Get(int id)
        {
            var item = _context.Albums.FirstOrDefault(x => x.Id == id);
            return item;
        }
        public ICollection<Album> GetAll()
        {
            return _context.Albums.ToList();
        }
        public void Update(Album item)
        {
            _context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
