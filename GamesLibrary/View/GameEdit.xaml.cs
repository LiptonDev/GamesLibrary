using GamesLibrary.ViewModel;

namespace GamesLibrary.View
{
    /// <summary>
    /// Логика взаимодействия для GameEdit.xaml
    /// </summary>
    public partial class GameEdit
    {
        public GameEdit(Game game)
        {
            InitializeComponent();
            DataContext = new GameEditVM(game);
        }
    }
}
