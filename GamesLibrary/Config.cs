using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace GamesLibrary
{
    class Config
    {
        static string ConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");
        static bool ConfigExists() => FileExists(ConfigPath);
        public static bool FileExists(string path) => File.Exists(path);

        /// <summary>
        /// Загрузка конфига.
        /// </summary>
        /// <returns></returns>
        public static Config LoadConfig()
        {
            if (!ConfigExists()) return new Config();
            return JsonConvert.DeserializeObject<Config>(File.ReadAllText(ConfigPath));
        }

        public Config()
        {
            Games.CollectionChanged += (s, e) => Save();
        }

        public void CheckGames()
        {
            var delGames = Games.Where(x => !FileExists(x.GamePath)).ToArray();

            foreach (var item in delGames)
            {
                Games.Remove(item);
            }
        }

        /// <summary>
        /// Сохранить список игр.
        /// </summary>
        public void Save() => File.WriteAllText(ConfigPath, JsonConvert.SerializeObject(this));

        /// <summary>
        /// Коллекция игр.
        /// </summary>
        public ObservableCollection<Game> Games { get; set; } = new ObservableCollection<Game>();
    }
}
