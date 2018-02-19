using System.Windows;

namespace GamesLibrary
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Consts.Config = Config.LoadConfig();
            Consts.Config.CheckGames();
            base.OnStartup(e);
        }
    }
}
