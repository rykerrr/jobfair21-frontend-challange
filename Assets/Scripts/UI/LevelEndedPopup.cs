using Platformer.BlurredScreenshot;
using Platformer.Model;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer.UI
{
    public class LevelEndedPopup : MonoBehaviour
    {
        #region Fields and Properties
        
        [SerializeField] private BlurredBackground blurredBackground;
        [SerializeField] private TMP_Text lblTokens = default;
        [SerializeField] private TMP_Text lblEnemiesKilled = default;
        [SerializeField] private TMP_Text lblUsername;
        [SerializeField] private TMP_Text lblScore = default;
        [SerializeField] private TMP_Text lblEndText;

        [SerializeField] private Color titleLostColor = default;
        [SerializeField] private Color titleWonColor = default;
        
        #endregion Fields and Properties

        private void Show()
        {
            blurredBackground.Show();
            gameObject.SetActive(true);
            
            lblTokens.text = GameDatabase.Instance.CurrentUser.Tokens.ToString();
            lblEnemiesKilled.text = GameDatabase.Instance.CurrentUser.EnemiesKilled.ToString();
            lblUsername.text = GameDatabase.Instance.CurrentUser.Username;
            lblScore.text = GameDatabase.Instance.CurrentUser.Score.ToString();
        }
        
        private void UpdateEndTextLabel(bool won)
        {
            switch (won)
            {
                case true:
                {
                    lblEndText.text = "LEVEL WON";
                    lblEndText.color = titleWonColor;

                    break;
                }
                case false:
                {
                    lblEndText.text = "LEVEL LOST";
                    lblEndText.color = titleLostColor;

                    break;
                }
            }
        }

        public void Show(bool won)
        {
            UpdateEndTextLabel(won);

            Show();
        }

        #region Event Handlers
        
        public void BtnMainMenuClicked()
        {
            SceneManager.LoadScene("MainMenu Scene", LoadSceneMode.Single);
        }

        public void BtnReplayClicked()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        #endregion Event Handlers
    }
}