using MusicAlbumsEF.Context;
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
    /// Interaction logic for AlbumView.xaml
    /// </summary>
    public partial class AlbumView : Window
    {
        private readonly AlbumViewModel _albumViewModel;
        public AlbumView(AlbumViewModel albumViewModel)
        {
            _albumViewModel = albumViewModel;
            DataContext = _albumViewModel;
            InitializeComponent();
        }
        private void PassId(object sender, RoutedEventArgs e)
        {
            var Btn = (Button)sender;
            _albumViewModel.AlbumId = (int)Btn.Tag;
            DataContext = _albumViewModel;
            
        }
    }
}
