using System;
using System.IO;
using System.Linq;
using Platformer.JobFair.UI.MainMenu;
using UnityEngine;

namespace Platformer.JobFair.SaveLoad
{
    /// <summary>
    /// Main class for saving/loading highscore data with JSON
    /// </summary>
    public class JSONSaveLoadManager : MonoBehaviour
    {
        private static string saveFileName = "LevelHighscoreData.txt";

        private LevelSelectionPanelUI panelUi;

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

        private void Awake()
        {
            TryInjectDefaultReferences();
        }

        private void TryInjectDefaultReferences()
        {
            panelUi = GetComponent<LevelSelectionPanelUI>();
        }

        /// <summary>
        /// Static in order to allow for it to be called from PlayerEnteredVictoryZone and other events
        /// This can definitely be much better written, the Level collection itself is static for the same reason above
        /// This class itself isn't very SOLID compliant either, but it does the job and it was written in a hurry
        /// </summary>
        /// <param name="levels"></param>
        public static void SaveLevelHighscores(params Level[] levels)
        {
            var json = GetLevelsAsJson(levels);
            
            var path = Path.Combine(Application.persistentDataPath, saveFileName);
            if (!File.Exists(path))
            {
                var fStream = File.Create(path);
                fStream.Close();
            }
            
            File.WriteAllText(path, json);
        }

        /// <summary>
        /// Turned static so that this may be initialized within the Levels getter
        /// Levels won't be initialized if the game wasn't started from the main menu scene (this should never happen beyond the editor
        /// but even if it happens from the editor we want to avoid it crashing)
        /// </summary>
        /// <returns></returns>
        public static OwnedHighscoreData[] LoadLevelHighscores()
        {
            string json = "";

            var path = Path.Combine(Application.persistentDataPath, saveFileName);

            if (!File.Exists(path)) return null;

            json = File.ReadAllText(path);
            
            return ConvertJsonToOwnedHighscoreStructs(json);
        }

        /// <summary>
        /// Converts levels to json by "projecting" them into the wrapper class
        /// </summary>
        /// <param name="levels"></param>
        /// <returns></returns>
        private static string GetLevelsAsJson(Level[] levels)
        {
            var highscores = levels.Select(x => new OwnedHighscoreData() {levelName = x.Name, data = x.HighscoreData}).ToArray();
            
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

        #region editor methods
#if UNITY_EDITOR
        [ContextMenu("Try save high scroes thing")]
        public void TrySaveHighscoresThing() => SaveLevelHighscores(LevelSelectionPanelUI.Levels);

        [ContextMenu("Try Save Load high scores thing")]
        public void TrySaveLoadHighscores()
        {
            LogOwnedHighscoreDataArr(ConvertJsonToOwnedHighscoreStructs(GetLevelsAsJson(LevelSelectionPanelUI.Levels)));
        }

        private void LogOwnedHighscoreDataArr(params OwnedHighscoreData[] datas)
        {
            foreach (var data in datas)
            {
                Debug.Log($"Owner: {data.levelName} Data: {data.data}");
            }
        }
#endif
        #endregion
    }
}