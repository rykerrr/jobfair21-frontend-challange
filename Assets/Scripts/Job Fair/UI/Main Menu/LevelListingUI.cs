using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer.JobFair.UI.MainMenu
{
    /// <summary>
    /// Pretty much the UI instance of a Level asset
    /// </summary>
    public class LevelListingUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TextMeshProUGUI levelNameText = default;
        [SerializeField] private Image levelFinishedImage = default;
        [SerializeField] private Button thisButton = default;

        [Header("Preferences")]
        [SerializeField] protected Level level = default;

        private LevelPopupUI popup;

        private void Start()
        {
            UpdateUI();
        }

        public void SetPopup(LevelPopupUI popup)
        {
            this.popup = popup;
        }
        
        /// <summary>
        /// Updates this listing's onClick function with the level it's given
        /// The purpose is the fact that the listing are created during runtime and need to be initialized
        /// </summary>
        /// <param name="level"></param>
        public void SetLevel(Level level)
        {
            this.level = level;
            
            void LoadLevelAction()
            {
                popup.SetLevel(level);
                popup.gameObject.SetActive(true);
            }

            thisButton.onClick.RemoveAllListeners();
            thisButton.onClick.AddListener(LoadLevelAction);

            UpdateUI();
        }
        
        private void UpdateUI()
        {
            levelNameText.text = $"{level.Name}";

            levelFinishedImage.color = level.HighscoreData.levelFinished ? Color.green : Color.red;
        }
    }
}
