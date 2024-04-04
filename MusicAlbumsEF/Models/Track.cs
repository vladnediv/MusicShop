using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicAlbumsEF.Models
{
    public class Track
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Duration { get; set; }
        public int PlaceInOrder { get; set; }
        public Album Album { get; set; }
        public int AlbumId { get; set; }
    }
}
