using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Platformer.JobFair.MainMenu
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

        private void Awake()
        {
            LoadLevels();
        }

        private void LoadLevels()
        {
            var levels = Resources.LoadAll<Level>(levelsLocationInResources);

            var path = Path.Combine(Application.dataPath, levelsLocationInResources);
            
            foreach (var level in levels)
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
    }
}
