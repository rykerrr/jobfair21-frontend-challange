using System.Linq;
using Platformer.JobFair.SaveLoad;
using Platformer.JobFair.UI.MainMenu;
using UnityEngine;

namespace Platformer.JobFair.Utility
{
    public static class LevelManager
    {
        private static string levelsLocationInResources = "Game Data/Levels";

        private static Level[] levels;

        public static Level[] Levels
        {
            get
            {
                if (levels == null)
                {
                    InitLevels();
                }

                return levels;
            }
            private set => levels = value;
        }
        
        private static void InitLevels()
        {
            Levels = Resources.LoadAll<Level>(levelsLocationInResources);

            LoadHighscores();
        }

        /// <summary>
        /// The actual mapping of the wrapped HighscoreData class (OwnedHighscoreData) happens here
        /// If the JSON file is empty, it will create it and fill it with the default data, the default data being the current data of the levels
        /// Due to ScriptableObject's sort of...weird...behaviour, that their memory state persists in the editor but not in game builds,
        /// if you're in the editor it will save the asset's current data which may mean that a level may have been cleared and have a highscore
        /// If you delete the JSON file after setting a highscore, the highscore will be kept
        /// (The above also only counts if you're in the editor due to the SO behaviour)
        /// </summary>
        private static void LoadHighscores()
        {
            var ownedHighscores = JSONSaveLoadManager.LoadLevelHighscores();
            if (ownedHighscores == null) return;

            foreach (var ownedHighscore in ownedHighscores)
            {
                Debug.Log(ownedHighscore.levelName + "|" + ownedHighscore.data.highscore);
                // (General case at least)
                // This will throw an InvalidOperationException if the JSON file is not properly written
                // E.g there's more levels in the JSON form than there is level assets
                // You can fix this by clearing the JSON file
                var level = Levels.First(x => x.Name == ownedHighscore.levelName);

                level.SetLevelScoreData(ownedHighscore.data);
            }
        }
    }
}
