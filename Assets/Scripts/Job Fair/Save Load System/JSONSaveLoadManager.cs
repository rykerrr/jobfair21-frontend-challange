using System;
using System.IO;
using System.Linq;
using Platformer.JobFair.MainMenu;
using UnityEngine;

namespace Platformer.JobFair.SaveLoad
{
    public class JSONSaveLoadManager : MonoBehaviour
    {
        [SerializeField] private string saveFileName = "LevelHighscoreData.txt";

        private LevelSelectionPanelUI panelUi;

        public struct OwnedHighscoreData
        {
            public string levelName;
            public LevelHighscoreData data;
        }

        private void Awake()
        {
            panelUi = GetComponent<LevelSelectionPanelUI>();
        }

        public void SaveLevelHighscores(params Level[] levels)
        {
            var json = GetLevelsAsJson(levels);
            
            var path = Path.Combine(Application.persistentDataPath, saveFileName);
            if (File.Exists(path)) File.Create(path);
            
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

        private string GetLevelsAsJson(Level[] levels)
        {
            var highscores = levels.Select(x => new OwnedHighscoreData() {levelName = x.Name, data = x.HighscoreData}).ToArray();
            
            return JsonHelper.ToJson(highscores, true);
        }

        private OwnedHighscoreData[] ConvertJsonToOwnedHighscoreStructs(string json)
        {
            OwnedHighscoreData[] datas = JsonHelper.FromJson<OwnedHighscoreData>(json);

            return datas;
        }

        #region editor methods
#if UNITY_EDITOR
        [ContextMenu("Try save high scroes thing")]
        public void TrySaveHighscoresThing() => SaveLevelHighscores(panelUi.Levels);

        [ContextMenu("Try Save Load high scores thing")]
        public void TrySaveLoadHighscores()
        {
            LogOwnedHighscoreDataArr(ConvertJsonToOwnedHighscoreStructs(GetLevelsAsJson(panelUi.Levels)));
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