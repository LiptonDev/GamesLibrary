using MahApps.Metro.Controls.Dialogs;

namespace GamesLibrary
{
    static class Consts
    {
        public static MainWindow MainWindow { get; set; }

        public static MetroDialogSettings DialogSettings = new MetroDialogSettings
        {
            ColorScheme = MetroDialogColorScheme.Accented,
            AffirmativeButtonText = "Да",
            NegativeButtonText = "Нет"
        };

        public static Config Config { get; set; }
    }
}
