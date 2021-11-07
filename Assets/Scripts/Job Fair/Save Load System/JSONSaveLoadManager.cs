using System;
using System.IO;
using System.Linq;
using Platformer.JobFair.MainMenu;
using UnityEngine;

namespace Platformer.JobFair.SaveLoad
{
    public class JSONSaveLoadManager : MonoBehaviour
    {
        private static string saveFileName = "LevelHighscoreData.txt";

        private LevelSelectionPanelUI panelUi;

        [Serializable]
        public struct OwnedHighscoreData
        {
            public string levelName;
            public LevelHighscoreData data;
        }

        private void Awake()
        {
            panelUi = GetComponent<LevelSelectionPanelUI>();
        }

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

        public OwnedHighscoreData[] LoadLevelHighscores()
        {
            string json = "";

            var path = Path.Combine(Application.persistentDataPath, saveFileName);

            if (!File.Exists(path)) return null;

            json = File.ReadAllText(path);
            
            return ConvertJsonToOwnedHighscoreStructs(json);
        }

        private static string GetLevelsAsJson(Level[] levels)
        {
            var highscores = levels.Select(x => new OwnedHighscoreData() {levelName = x.Name, data = x.HighscoreData}).ToArray();
            
            Debug.Log(highscores.Length);
            
            return JsonHelper.ToJson(highscores, true);
        }

        private OwnedHighscoreData[] ConvertJsonToOwnedHighscoreStructs(string json)
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