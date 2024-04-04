using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace MusicAlbumsEF.Models
{
    public class Album
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Artist Artist { get; set; }
        public string ArtistName { get; set; }
        public int ArtistId { get; set; }
        public int PublishingYear { get; set; }
        public enum GenreEnum { Jazz, Metal, Classic, Rock, Indie, Pop };
        public string Genre { get; set; }
        public ObservableCollection<Models.Track>? Tracks { get; set; }
        public decimal PrimeCost { get; set; }
        public decimal SellPrice { get; set; }
        public string PicturePath { get; set; }
    }
}
