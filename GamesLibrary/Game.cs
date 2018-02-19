using DevExpress.Mvvm;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GamesLibrary
{
    public class Game : ViewModelBase
    {
        Process GameProcess;
        public bool GameStarted
        {
            get => !GameProcess?.HasExited == true;
        }

        public void StopGame()
        {
            GameProcess?.Kill();
            RaisePropertyChanged("GameStarted");
        }

        public async void StartGame()
        {
            if (!Config.FileExists(GamePath))
            {
                Consts.Config.Games.Remove(this);
                return;
            }

            if (!GameProcess?.HasExited == true)
                return;

            GameProcess = Process.Start(gamePath, Arguments);
            RaisePropertyChanged("GameStarted");
            await Task.Run(() =>
            {
                while (true)
                {
                    if (GameProcess.HasExited)
                        break;

                    gameTime++;
                    RaisePropertyChanged("GameTime");
                    Thread.Sleep(1000);
                }
            });

            gameTime = -1;
            RaisePropertyChanged("GameTime");

            if (!GameProcess.HasExited)
                GameProcess.Kill();
            GameProcess = null;
            RaisePropertyChanged("GameStarted");
        }

        int gameTime = -1;
        public string GameTime => Helper.GetTime(gameTime);

        public ImageSource Image { get; set; }

        string name;
        [JsonProperty("name")]
        public string Name
        {
            get => name;
            set
            {
                name = value;
                RaisePropertyChanged();
            }
        }

        [JsonProperty("args")]
        public string Arguments { get; set; } = "";

        string gamePath;
        [JsonProperty("gamePath")]
        public string GamePath
        {
            get => gamePath;
            set
            {
                gamePath = value;
                if (Config.FileExists(value))
                    Image = Icon.ExtractAssociatedIcon(gamePath).ToImageSource();
                RaisePropertiesChanged("GamePath", "Name", "Image");
            }
        }
    }
}
