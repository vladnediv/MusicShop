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
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        private readonly LoginViewModel _loginViewModel;
        public LoginView(LoginViewModel loginViewModel)
        {
            InitializeComponent();
            this.DataContext = loginViewModel;
            _loginViewModel = loginViewModel;
            _loginViewModel.CloseAction = () => { this.Close(); };
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _loginViewModel.Password = (sender as PasswordBox).Password;
        }
    }
}
