using DevExpress.Mvvm;
using GamesLibrary.View;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;

namespace GamesLibrary.ViewModel
{
    class PlayVM : ViewModelBase
    {
        Game currentGame;
        public Game CurrentGame
        {
            get => currentGame;
            set
            {
                currentGame = value;
                RaisePropertyChanged();
            }
        }

        public ICommand AddGame => new DelegateCommand(() =>
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Filter = "Game *.exe|*.exe"
            };

            var open = fileDialog.ShowDialog();

            if (open == false)
                return;

            Consts.Config.Games.Add(new Game
            {
                GamePath = fileDialog.FileName,
                Name = Path.GetFileNameWithoutExtension(fileDialog.FileName)
            });
        });

        public ICommand DelGame => new DelegateCommand(async () =>
        {
            var res = await Consts.MainWindow.ShowMessageAsync("Удаление игры", $"Вы действительно хотите удалить \"{CurrentGame.Name}\"?", MessageDialogStyle.AffirmativeAndNegative, Consts.DialogSettings);

            if (res == MessageDialogResult.Affirmative)
            {
                CurrentGame.StopGame();
                Consts.Config.Games.Remove(CurrentGame);
            }
        });

        public ICommand RunGame => new DelegateCommand(() =>
        {
            if (!CurrentGame.GameStarted)
                CurrentGame.StartGame();
            else CurrentGame.StopGame();
        });

        public ICommand EditGame => new DelegateCommand(async () =>
        {
            await Consts.MainWindow.ShowMetroDialogAsync(new GameEdit(currentGame), Consts.DialogSettings);
        });

        public ObservableCollection<Game> Games => Consts.Config.Games;
    }
}
