using Platformer.Model;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Platformer.UI
{
    public class MainMenuCanvas : MonoBehaviour
    {
        [SerializeField] private TMP_InputField inputUsername;
        [SerializeField] private Button btnPlay = default;

        private void Awake()
        {
            inputUsername.onValueChanged.AddListener(OnUsernameInputChanged);

            SetUsernameIfNotDefault();

            var usernameWasDefault = string.IsNullOrEmpty(inputUsername.text);
            if (!usernameWasDefault) EnablePlay();
        }

        private void EnablePlay()
        {
            btnPlay.interactable = true;
        }

        private void SetUsernameIfNotDefault()
        {
            var user = GameDatabase.Instance.CurrentUser;
            if (user.UsernameWasSet)
            {
                inputUsername.text = user.Username;
            }
        }

        private void OnDestroy()
        {
            inputUsername.onValueChanged.RemoveListener(OnUsernameInputChanged);
        }

        #region Event Handlers

        private void OnUsernameInputChanged(string newName)
        {
            GameDatabase.Instance.SetUsername(newName);

            EnablePlay();
        }

        public void BtnPlayClicked()
        {
            SceneManager.LoadScene("Assets/Scenes/LevelScene.unity", LoadSceneMode.Single);
        }
        
        #endregion Event Handlers
    }
}