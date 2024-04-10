using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicAlbumsEF.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsArtist { get; set; }
        public List<Artist>? Artists { get; set; }
        public ObservableCollection<Album>? Albums { get; set; }
        public decimal? Balance { get; set; }
    }
}
