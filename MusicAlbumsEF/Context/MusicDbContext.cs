using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MusicAlbumsEF.Models;
using MusicAlbumsEF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicAlbumsEF.Context
{
    public class MusicDbContext : DbContext
    {
        public MusicDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Read connection string
            var configuration = new ConfigurationBuilder().AddJsonFile("AppSettings.json").Build();
            string connectionString = configuration.GetConnectionString("MusicDatabase");

            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Album->Artist: One-To-Many
            modelBuilder.Entity<Album>().HasOne(album => album.Artist).WithMany(artist => artist.Albums).HasForeignKey(album => album.ArtistId);

            //Track->Album: One-To-Many
            modelBuilder.Entity<Track>().HasOne(album => album.Album).WithMany(tracks => tracks.Tracks).HasForeignKey(album => album.AlbumId);

            //Artist->User: One-To-Many
            modelBuilder.Entity<Artist>().HasOne(user => user.User).WithMany(artist => artist.Artists).HasForeignKey(user => user.UserId);

            //Creating admin if first launch
            modelBuilder.Entity<User>().HasData(new User() { Id = 1, Name = "Admin", Email = "admin@gmail.com", Password = $"{new PasswordHasher().HasPassword("admin")}", IsArtist = true });
            modelBuilder.Entity<Artist>().HasData(new Artist() { Id = 1, Name = "Admin Artist", Country = "America", YearOfBirth = 2024, UserId = 1 });
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
