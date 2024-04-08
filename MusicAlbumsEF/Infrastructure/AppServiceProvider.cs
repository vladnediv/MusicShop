﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MusicAlbumsEF.Context;
using MusicAlbumsEF.Repository;
using MusicAlbumsEF.Services;
using MusicAlbumsEF.ViewModels;
using MusicAlbumsEF.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicAlbumsEF.Infrastructure
{
    public static class AppServiceProvider
    {
        public static ServiceProvider ServiceProvider {  get; private set; }


        public static void Initialize()
        {
            var services = new ServiceCollection();

            //DbContext configuration / Connecting to the sql server
            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("AppSettings.json").Build();
            string connectionString = configuration.GetConnectionString("MusicDatabase");
            services.AddDbContext<MusicDbContext>(options =>  options.UseSqlServer(connectionString));

            //Views
            services.AddTransient<AlbumView, AlbumView>();
            services.AddTransient<AddAlbumView, AddAlbumView>();
            services.AddTransient<EditAccountView, EditAccountView>();
            services.AddTransient<AddTrackView, AddTrackView>();
            services.AddTransient<TrackView, TrackView>();
            services.AddTransient<EditAlbumView, EditAlbumView>();
            services.AddTransient<EditArtistView, EditArtistView>();
            services.AddTransient<EditSongView, EditSongView>();
            services.AddTransient<LoginView, LoginView>();
            services.AddTransient<RegisterView, RegisterView>();
            services.AddTransient<UserAlbumView, UserAlbumView>();


            //Repository
            services.AddTransient<AlbumRepository, AlbumRepository>();
            services.AddTransient<ArtistRepository, ArtistRepository>();
            services.AddTransient<TrackRepository, TrackRepository>();
            services.AddTransient<UserRepository, UserRepository>();


            //Services
            services.AddTransient<MusicPlayerService, MusicPlayerService>();
            services.AddTransient<AccountService, AccountService>();
            services.AddTransient<PasswordHasher, PasswordHasher>();


            //ViewModels
            services.AddTransient<AlbumViewModel, AlbumViewModel>();
            services.AddTransient<AddAlbumViewModel, AddAlbumViewModel>();
            services.AddTransient<EditAccountViewModel, EditAccountViewModel>();
            services.AddTransient<AddTrackViewModel, AddTrackViewModel>();
            services.AddTransient<TrackViewModel, TrackViewModel>();
            services.AddTransient<EditSongViewModel, EditSongViewModel>();
            services.AddTransient<EditAlbumViewModel, EditAlbumViewModel>();
            services.AddTransient<EditArtistViewModel, EditArtistViewModel>();
            services.AddTransient<LoginViewModel, LoginViewModel>();
            services.AddTransient<RegisterViewModel, RegisterViewModel>();
            services.AddTransient<UserAlbumViewModel, UserAlbumViewModel>();


            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
 