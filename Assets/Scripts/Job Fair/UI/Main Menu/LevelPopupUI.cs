using Platformer.Model;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Platformer.JobFair.UI.MainMenu
{
    /// <summary>
    /// Class responsible for displaying data for a "selected" level, once a level is clicked it shows this object
    /// </summary>
    public class LevelPopupUI : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private TextMeshProUGUI highscoreText = default;
        [SerializeField] private TextMeshProUGUI highscoreSetterText = default;
        [SerializeField] private TextMeshProUGUI highscoreDateSetText = default;
        [SerializeField] private TextMeshProUGUI levelNameText = default;
        [SerializeField] private Image levelFinishedImage = default;

        // The labels are just TMP text elements that don't get changed, specifically they're "static"
        // They display a label like "Highscore: ", "Highscore setter: ", etc, so that the UI can be placed more in-line
        // The auto size would make Highscore setter: Name in almost every case smaller than Highscore: Score, so
        // the label was separated from the text display itself
        
        [SerializeField] private GameObject highscoreLabel = default;
        [SerializeField] private GameObject highscoreSetterLabel = default;
        [SerializeField] private GameObject highscoreDateSetLabel = default;

        private Level level;
        
        /// <summary>
        /// Disables highscore-related data if no highscore was set
        /// A high score will be set once a level is won, which means that the level will have been finished
        /// If it's not finished, there can't be a highscore
        /// </summary>
        private void UpdateUI()
        {
            levelNameText.text = $"{level.Name}";

            if (level.HighscoreData.levelFinished)
            {
                highscoreLabel.SetActive(true);
                highscoreSetterLabel.SetActive(true);
                highscoreDateSetLabel.SetActive(true);
                highscoreText.gameObject.SetActive(true);
                highscoreSetterText.gameObject.SetActive(true);
                highscoreDateSetText.gameObject.SetActive(true);
                
                highscoreText.text = $"{level.HighscoreData.highscore}";
                highscoreSetterText.text = $"{level.HighscoreData.highscoreSetter}";
                highscoreDateSetText.text = $"{level.HighscoreData.highscoreTime}";
            }
            else
            {
                highscoreLabel.SetActive(false);
                highscoreSetterLabel.SetActive(false);
                highscoreDateSetLabel.SetActive(false);
                
                highscoreText.gameObject.SetActive(false);
                highscoreSetterText.gameObject.SetActive(false);
                highscoreDateSetText.gameObject.SetActive(false);
            }
            
            levelFinishedImage.color = level.HighscoreData.levelFinished ? Color.green : Color.red;
        }

        /// <summary>
        /// Used to "inject" the level reference by the selected LevelListing
        /// </summary>
        /// <param name="level"></param>
        public void SetLevel(Level level)
        {
            this.level = level;

            UpdateUI();
        }
        
        #region event handlers
        public void OnClick_LoadLevel()
        {
            GameDatabase.Instance.CurrentUser.CurrentLevel = level;
        }
        #endregion
    }
}
