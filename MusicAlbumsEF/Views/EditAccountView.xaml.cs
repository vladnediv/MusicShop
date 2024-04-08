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
    /// Interaction logic for AddArtistView.xaml
    /// </summary>
    public partial class EditAccountView : Window
    {
        public EditAccountView(EditAccountViewModel addArtistViewModel)
        {
            DataContext = addArtistViewModel;
            InitializeComponent();
        }
    }
}
