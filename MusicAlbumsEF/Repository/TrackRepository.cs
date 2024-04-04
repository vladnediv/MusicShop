using MusicAlbumsEF.Context;
using MusicAlbumsEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicAlbumsEF.Repository
{
    public class TrackRepository : IRepository<Track>
    {
        private readonly MusicDbContext _context;
        public TrackRepository(MusicDbContext context)
        {
            _context = context;
        }

        public void Add(Track item)
        {
            _context.Tracks.Add(item);
            _context.SaveChanges();
        }
        public void Delete(Track item)
        {
            _context.Tracks.Remove(item);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            var item = _context.Tracks.FirstOrDefault(x => x.Id == id);
            if (item != null) { _context.Tracks.Remove(item); }
            _context.SaveChanges();
        }
        public Track Get(int id)
        {
            var item = _context.Tracks.FirstOrDefault(_x => _x.Id == id);
            return item;
        }
        public ICollection<Track> GetAll()
        {
            return _context.Tracks.ToList();
        }
        public ICollection<Track> GetAllById(int id)
        {
            return _context.Tracks.Where(x => x.AlbumId == id).ToList();
        }
        public void Update(Track item)
        {
            _context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }
    }
}