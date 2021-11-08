using System.Collections.Generic;
using System.IO;
using System.Linq;
using Platformer.JobFair.SaveLoad;
using UnityEngine;

namespace Platformer.JobFair.UI.MainMenu
{
    public class LevelSelectionPanelUI : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private Transform selectionPanelContent = default;
        [SerializeField] private LevelPopupUI popup = default;
        
        [Header("Preferences")]
        [SerializeField] private LevelListingUI listingPrefab = default;
        
        private static string levelsLocationInResources = "Game Data/Levels";
        private readonly List<LevelListingUI> listings = new List<LevelListingUI>();

        private JSONSaveLoadManager jsonSaveLoad = default;
        
        public static Level[] Levels { get; set; }

        private void Awake()
        {
            jsonSaveLoad = GetComponent<JSONSaveLoadManager>();
            
            InitLevels();
            CreateLevelListings();
        }

        private void InitLevels()
        {
            if (Levels == null || Levels.Length == 0)
            {
                Levels = Resources.LoadAll<Level>(levelsLocationInResources);
            }
            
            LoadHighscores();   
        }

        private void LoadHighscores()
        {
            var ownedHighscores = jsonSaveLoad.LoadLevelHighscores();
            if (ownedHighscores == null) return;
            
            Debug.Log("Highscore data loading imminent");
            
            foreach (var ownedHighscore in ownedHighscores)
            {
                var level = Levels.First(x => x.Name == ownedHighscore.levelName);
                
                level.SetLevelScoreData(ownedHighscore.data);
            }
        }

        private void CreateLevelListings()
        {
            var path = Path.Combine(Application.dataPath, levelsLocationInResources);
            
            foreach (var level in Levels)
            {
                listings.Add(CreateListing(level));
            }
        }

        private LevelListingUI CreateListing(Level level)
        {
            var listingClone = Instantiate(listingPrefab, selectionPanelContent);
            
            listingClone.SetLevel(level);
            listingClone.SetPopup(popup);

            return listingClone;
        }
        
        #region editor methods
#if UNITY_EDITOR
        [ContextMenu("Save loaded level high scores")]
        public void SaveLoadedLevelHighscores() => JSONSaveLoadManager.SaveLevelHighscores(Levels);
#endif

        #endregion
    }
}
