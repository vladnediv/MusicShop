using MusicAlbumsEF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MusicAlbumsEF.Views
{
    /// <summary>
    /// Interaction logic for UserAlbumView.xaml
    /// </summary>
    public partial class UserAlbumView : Window
    {
        private readonly UserAlbumViewModel _userAlbumViewModel;
        public UserAlbumView(UserAlbumViewModel userAlbumViewModel)
        {
            InitializeComponent();
            _userAlbumViewModel = userAlbumViewModel;
        }

        public void PassId(object sender, RoutedEventArgs e)
        {
            var Btn = (Button)sender;
            _userAlbumViewModel.AlbumId = (int)Btn.Tag;
            DataContext = _userAlbumViewModel;

        }
    }
}
