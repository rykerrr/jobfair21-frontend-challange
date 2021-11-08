using System;
using System.IO;
using System.Linq;
using Platformer.JobFair.UI.MainMenu;
using Platformer.JobFair.Utility;
using UnityEngine;

namespace Platformer.JobFair.SaveLoad
{
    /// <summary>
    /// Main class for saving/loading highscore data with JSON
    /// </summary>
    public static class JSONSaveLoadManager
    {
        private static string saveFileName = "LevelHighscoreData.txt";

        /// <summary>
        /// This is sort of a wrapper over the LevelHighscoreData so that we can locate the Level assets by name
        /// and then proceed to set the highscore data
        /// If we didn't do that, we wouldn't know what belongs to what unless we used a multiple file approach
        /// But that can get very bothersome very quickly
        /// </summary>
        [Serializable]
        public struct OwnedHighscoreData
        {
            public string levelName;
            public LevelHighscoreData data;
        }

        /// <summary>
        /// Called from PlayerEnteredVictoryZone and other events that want to save high score data after modifying a level asset
        /// </summary>
        /// <param name="levels"></param>
        public static void SaveLevelHighscores()
        {
            var json = GetLevelsAsJson();

            var path = Path.Combine(Application.persistentDataPath, saveFileName);
            if (!File.Exists(path))
            {
                var fStream = File.Create(path);
                fStream.Close();
            }

            File.WriteAllText(path, json);
        }

        /// <summary>
        /// Loads highscore data from given json file, or creates the default file and saves current level asset state in it
        /// For a possible bug, due to how scriptable objects behave in the editor, check LevelManager->LoadHighscores
        /// </summary>
        /// <returns></returns>
        public static OwnedHighscoreData[] LoadLevelHighscores()
        {
            string json = "";

            var path = Path.Combine(Application.persistentDataPath, saveFileName);

            // Makes sure the default file exists if not created
            bool fileExists;
            if ((fileExists = File.Exists(path)) == false || string.IsNullOrEmpty(File.ReadAllText(path)))
            {
                if (!fileExists)
                {
                    var fStream = File.Create(path);
                    fStream.Close();
                }
                
                SaveLevelHighscores();

                return null;
            }

            json = File.ReadAllText(path);

            return ConvertJsonToOwnedHighscoreStructs(json);
        }

        /// <summary>
        /// Converts levels to json by "projecting" them into the wrapper class
        /// </summary>
        /// <param name="levels"></param>
        /// <returns></returns>
        private static string GetLevelsAsJson()
        {
            var highscores = LevelManager.Levels
                .Select(x => new OwnedHighscoreData() {levelName = x.Name, data = x.HighscoreData}).ToArray();

            return JsonHelper.ToJson(highscores, true);
        }

        /// <summary>
        /// Converts the JSON (unless the string is empty, e.g the save file is empty) to the wrapped structs so that we can
        /// map them to their respective levels
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        private static OwnedHighscoreData[] ConvertJsonToOwnedHighscoreStructs(string json)
        {
            if (string.IsNullOrEmpty(json)) return null;

            OwnedHighscoreData[] datas = JsonHelper.FromJson<OwnedHighscoreData>(json);

            return datas;
        }
    }
}