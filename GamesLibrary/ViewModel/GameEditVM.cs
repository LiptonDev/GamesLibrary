using DevExpress.Mvvm;
using MahApps.Metro.Controls.Dialogs;
using System.ComponentModel;

namespace GamesLibrary.ViewModel
{
    class GameEditVM : ViewModelBase, IDataErrorInfo
    {
        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(Name):
                        if (Name.StringIsEmpty())
                            return "Введите название игры";
                        break;
                }

                return string.Empty;
            }
        }

        public string Name { get; set; }
        public string Args { get; set; }

        public string Error { get; set; }

        Game game;
        public GameEditVM(Game game)
        {
            this.game = game;
            Name = game.Name;
            Args = game.Arguments;
        }

        public ICommand<CustomDialog> Save => new DelegateCommand<CustomDialog>(x =>
        {
            game.Name = Name;
            game.Arguments = Args;
            Consts.Config.Save();
            Close.Execute(x);
        });

        public ICommand<CustomDialog> Close => new DelegateCommand<CustomDialog>(async x =>
        {
            await Consts.MainWindow.HideMetroDialogAsync(x);
        });
    }
}
