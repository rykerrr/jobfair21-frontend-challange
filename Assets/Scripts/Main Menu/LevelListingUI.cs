using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer.JobFair.MainMenu
{
    public class LevelListingUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TextMeshProUGUI levelNameText = default;
        [SerializeField] private Image levelFinishedImage = default;
        [SerializeField] private Button thisButton = default;

        [Header("Preferences")]
        [SerializeField] protected Level listing = default;

        private LevelPopupUI popup;

        private void Start()
        {
            // listing.LoadLevelScoreData();
            
            UpdateUI();
        }

        public void SetPopup(LevelPopupUI popup)
        {
            this.popup = popup;
        }
        
        public void SetLevel(Level listing)
        {
            this.listing = listing;

            void LoadlLevelAction()
            {
                popup.SetLevel(listing);
                popup.gameObject.SetActive(true);
            }

            thisButton.onClick.RemoveAllListeners();
            thisButton.onClick.AddListener(LoadlLevelAction);

            UpdateUI();
        }
        
        private void UpdateUI()
        {
            levelNameText.text = $"{listing.Name}";

            levelFinishedImage.color = listing.HighscoreData.levelFinished ? Color.green : Color.red;
        }
    }
}
