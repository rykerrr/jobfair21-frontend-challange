using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Platformer.JobFair.MainMenu
{
    public class LevelPopupUI : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private TextMeshProUGUI highscoreText = default;
        [SerializeField] private TextMeshProUGUI highscoreSetterText = default;
        [SerializeField] private TextMeshProUGUI highscoreDateSetText = default;
        [SerializeField] private TextMeshProUGUI levelNameText = default;
        [SerializeField] private Image levelFinishedImage = default;

        [SerializeField] private GameObject highscoreLabel = default;
        [SerializeField] private GameObject highscoreSetterLabel = default;
        [SerializeField] private GameObject highscoreDateSetLabel = default;

        
        private Level listing;
        
        private void UpdateUI()
        {
            levelNameText.text = $"{listing.Name}";

            if (listing.LevelFinished)
            {
                highscoreLabel.SetActive(true);
                highscoreSetterLabel.SetActive(true);
                highscoreDateSetLabel.SetActive(true);
                highscoreText.gameObject.SetActive(true);
                highscoreSetterText.gameObject.SetActive(true);
                highscoreDateSetText.gameObject.SetActive(true);
                
                highscoreText.text = $"{listing.Highscore}";
                highscoreSetterText.text = $"{listing.HighscoreSetter}";
                highscoreDateSetText.text = $"{listing.HighscoreTime}";
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
            
            levelFinishedImage.color = listing.LevelFinished ? Color.green : Color.red;
        }

        public void SetLevel(Level listing)
        {
            this.listing = listing;

            UpdateUI();
        }
        
        #region event handlers
        public void OnClick_LoadLevel()
        {
            SceneManager.LoadScene(listing.SceneIndex);
        }

        public void OnClick_Cancel()
        {
            gameObject.SetActive(false);
        }
        #endregion
    }
}
