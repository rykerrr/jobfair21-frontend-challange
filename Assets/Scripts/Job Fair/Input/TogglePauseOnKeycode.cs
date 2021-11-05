using Platformer.JobFair.Controllers;
using UnityEngine;

namespace Platformer.JobFair.InputProcessing
{
    /// <summary>
    /// Toggles the pause menu to prevent requirement of pressing a button (on PC)
    /// </summary>
    public class TogglePauseOnKeycode : MonoBehaviour
    {
        #region fields
        [SerializeField] private KeyCode toggleKeycode = KeyCode.Escape;
        [SerializeField] private GamePauseStateController pauseStateController = default;
        #endregion
        
        private bool pause;
        
        private void Update()
        {
            var pressedThisFrame = Input.GetKeyDown(toggleKeycode);

            if (pressedThisFrame)
            {
                pause = !pause;

                TogglePause();
            }
        }

        private void TogglePause()
        {
            switch (pause)
            {
                case true:
                    pauseStateController.Pause();
                    break;
                case false:
                    pauseStateController.Resume();
                    break;
            }
        }
    }
}
