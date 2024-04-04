using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicAlbumsEF.Models
{
    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public int YearOfBirth { get; set; }
        public List<Album>? Albums { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
    