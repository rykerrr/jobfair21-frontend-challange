using System.Collections.Generic;
using System.IO;
using System.Linq;
using Platformer.JobFair.SaveLoad;
using Platformer.JobFair.Utility;
using UnityEngine;

namespace Platformer.JobFair.UI.MainMenu
{
    /// <summary>
    /// Class responsible for displaying the Level assets
    /// </summary>
    public class LevelSelectionPanelUI : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private Transform selectionPanelContent = default;
        [SerializeField] private LevelPopupUI popup = default;

        [Header("Preferences")] [SerializeField]
        private LevelListingUI listingPrefab = default;

        private readonly List<LevelListingUI> listings = new List<LevelListingUI>();

        private void Awake()
        {
            CreateLevelListings();
        }

        private void CreateLevelListings()
        {
            foreach (var level in LevelManager.Levels)
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
        public void SaveLoadedLevelHighscores() => JSONSaveLoadManager.SaveLevelHighscores();
#endif

        #endregion
    }
}