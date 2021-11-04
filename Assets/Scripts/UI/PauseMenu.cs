using System;
using Platformer.BlurredScreenshot;
using Platformer.JobFair.Controllers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer.UI
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private BlurredBackground blurredBackground;
        [SerializeField] private GamePauseStateController gamePauseStateController;
        
        public void Show()
        {
            blurredBackground.Show();
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            blurredBackground.Hide();
            gameObject.SetActive(false);
        }

        #region Event Handlers

        public void BtnResumeClicked()
        {
            gamePauseStateController.Resume();
        }

        public void BtnMainMenuClicked()
        {
            SceneManager.LoadScene("Assets/Scenes/MainScene.unity", LoadSceneMode.Single);
        }

        public void BtnRestartClicked()
        {
            SceneManager.LoadScene("Assets/Scenes/LevelScene.unity", LoadSceneMode.Single);
        }

        #endregion Event Handlers
    }
}