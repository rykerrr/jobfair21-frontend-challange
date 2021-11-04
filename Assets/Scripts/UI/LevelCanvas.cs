using Platformer.Gameplay;
using Platformer.JobFair.Controllers;
using Platformer.Model;
using TMPro;
using UnityEngine;

namespace Platformer.UI
{
    public class LevelCanvas : MonoBehaviour
    {
        #region Fields and Properties

        [SerializeField] private GamePauseStateController gamePauseStateController = default;
        [SerializeField] private PauseMenu pauseMenu = default;
        [SerializeField] private LevelEndedPopup levelEndedPopup = default;
        [SerializeField] private TMP_Text lblTokens = default;
        [SerializeField] private TMP_Text lblEnemiesKilled = default;
        [SerializeField] private TMP_Text lblUsername = default;

        #endregion Fields and Properties

        private void Awake()
        {
            PlayerDeath.OnExecute += PlayerDiedCallback;
            PlayerEnteredVictoryZone.OnExecute += PlayerWonCallback;

            var gameDb = GameDatabase.Instance;
            gameDb.ResetScore();

            lblUsername.text = gameDb.CurrentUser.Username;
        }

        private void OnDestroy()
        {
            PlayerDeath.OnExecute -= PlayerDiedCallback;
            PlayerEnteredVictoryZone.OnExecute -= PlayerWonCallback;
        }

        private void Update()
        {
            lblTokens.text = GameDatabase.Instance.CurrentUser.Tokens.ToString();
            lblEnemiesKilled.text = GameDatabase.Instance.CurrentUser.EnemiesKilled.ToString();
        }
        
        private void OnEnable()
        {
            gamePauseStateController.onPause += pauseMenu.Show;
            gamePauseStateController.onResume += pauseMenu.Hide;
        }

        private void OnDisable()
        {
            gamePauseStateController.onPause -= pauseMenu.Show;
            gamePauseStateController.onResume -= pauseMenu.Hide;
        }

        #region Event Handlers

        private void PlayerDiedCallback(PlayerDeath playerDeath)
        {
            levelEndedPopup.Show(false);
        }

        private void PlayerWonCallback(PlayerEnteredVictoryZone playerEnteredVictoryZone)
        {
            levelEndedPopup.Show(true);
        }

        public void BtnPauseClicked()
        {
            gamePauseStateController.Pause();
        }

        #endregion Event Handlers
    }
}