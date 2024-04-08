using MusicAlbumsEF.Models;
using MusicAlbumsEF.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicAlbumsEF.Services
{
    public class MusicPlayerService
    {
        private readonly ArtistRepository _ArtistRepository;
        private readonly AlbumRepository _AlbumRepository;
        private readonly TrackRepository _TrackRepository;

        public MusicPlayerService(ArtistRepository artistRepository, AlbumRepository albumRepository, TrackRepository trackRepository)
        {
            _AlbumRepository = albumRepository;
            _ArtistRepository = artistRepository;
            _TrackRepository = trackRepository;
        }

        //Service for Albums
        public void AddAlbum(Album item)
        {
            _AlbumRepository.Add(item);
        }
        public void DeleteAlbum(Album item)
        {
            _AlbumRepository.Delete(item);
        }
        public ICollection<Album> GetAllAlbums()
        {
            return _AlbumRepository.GetAll();
        }

        public ICollection<Album> GetAllAlbumsByArtist(int ArtistId) 
        {
            var albums = _AlbumRepository.GetAll().Where(x => x.ArtistId == ArtistId).ToList();
            if (albums != null) return albums;
            else return new List<Album>();
        }
        public Album GetAlbum(int id)
        {
            return _AlbumRepository.Get(id);
        }
        public Album GetAlbumByName(string name)
        {
            return _AlbumRepository.GetAll().FirstOrDefault(a => a.Name == name);
        }
        public void EditAlbum(int id, string Name, string ArtistName, int PublishingYear, decimal PrimeCost, decimal SellPrice, string Genre)
        {
            _AlbumRepository.Get(id).Name = Name;
            _AlbumRepository.Get(id).ArtistName = ArtistName;
            _AlbumRepository.Get(id).PublishingYear = PublishingYear;
            _AlbumRepository.Get(id).PrimeCost = PrimeCost;
            _AlbumRepository.Get(id).SellPrice = SellPrice;
            _AlbumRepository.Get(id).Genre = Genre;
            _AlbumRepository.Update(_AlbumRepository.Get(id));
        }


        //Service for Artists
        public void AddArtist(Artist artist)
        {
            _ArtistRepository.Add(artist);
        }
        public void DeleteArtist(Artist artist)
        {
            _ArtistRepository.Delete(artist);
        }
        public ICollection<Artist> GetAllArtists() 
        {
            return _ArtistRepository.GetAll();
        }
        public Artist GetArtist(int id)
        {
            return _ArtistRepository.Get(id);
        }
        public Artist GetArtistByName(string Name)
        {
            return _ArtistRepository.GetAll().FirstOrDefault(x => x.Name == Name);
        }
        public void EditArtist(int id, string Name, string Country, int YearOfBirth)
        {
            _ArtistRepository.Get(id).Name = Name;
            _ArtistRepository.Get(id).Country = Country;
            _ArtistRepository.Get(id).YearOfBirth = YearOfBirth;
            _ArtistRepository.Update(_ArtistRepository.Get(id));
        }



        //Service for Tracks
        public void AddTrack(Track track)
        {
            _TrackRepository.Add(track);
        }
        public void DeleteTrack(Track track)
        {
            _TrackRepository.Delete(track);
        }
        public Track GetTrackById(int id)
        {
            return _TrackRepository.Get(id);
        }
        public ICollection<Track> GetTracksById(int id)
        {
            return _TrackRepository.GetAllById(id);
        }
        public void EditTrack(int id, string Title, string Duration, int PlaceInOrder)
        {
            _TrackRepository.Get(id).Title = Title;
            _TrackRepository.Get(id).Duration = Duration;
            _TrackRepository.Get(id).PlaceInOrder = PlaceInOrder;
            _TrackRepository.Update(_TrackRepository.Get(id));
        }
    }
}
