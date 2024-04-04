using MusicAlbumsEF.Context;
using MusicAlbumsEF.Infrastructure;
using MusicAlbumsEF.Views;
using System.Configuration;
using System.Data;
using System.Windows;

namespace MusicAlbumsEF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            AppServiceProvider.Initialize();
            var window = (LoginView)AppServiceProvider.ServiceProvider.GetService(typeof(LoginView));
            window.Show();
        }
    }

}
