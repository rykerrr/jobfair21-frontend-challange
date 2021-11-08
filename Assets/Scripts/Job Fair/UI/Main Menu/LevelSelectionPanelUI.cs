using System.Collections.Generic;
using System.IO;
using System.Linq;
using Platformer.JobFair.SaveLoad;
using UnityEngine;

namespace Platformer.JobFair.UI.MainMenu
{
    /// <summary>
    /// Class responsible for displaying the Level assets
    /// </summary>
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

        /// <summary>
        /// The actual mapping of the wrapped HighscoreData class (OwnedHighscoreData) happens here
        /// Should definitely be delegated as there's no reason the level selection panel should be responsible
        /// for loading the levels, if it's already responsible for displaying them
        /// </summary>
        private void LoadHighscores()
        {
            var ownedHighscores = jsonSaveLoad.LoadLevelHighscores();
            if (ownedHighscores == null) return;
            
            foreach (var ownedHighscore in ownedHighscores)
            {
                var level = Levels.First(x => x.Name == ownedHighscore.levelName);
                
                level.SetLevelScoreData(ownedHighscore.data);
            }
        }

        private void CreateLevelListings()
        {
            foreach (var level in Levels)
            {
                listings.Add(CreateListing(level));
            }
        }

        /// <summary>
        /// Creates a level listing and initializes it with the given level and popup reference
        /// The popup reference injection is needed as, when you click on the listing, it shows the popup
        /// But due to that it also has to initialize the popup with it's level reference
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
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
