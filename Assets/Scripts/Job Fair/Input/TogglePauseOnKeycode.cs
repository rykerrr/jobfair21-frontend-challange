using Platformer.JobFair.Controllers;
using UnityEngine;

namespace Platformer
{
    public class TogglePauseOnKeycode : MonoBehaviour
    {
        [SerializeField] private KeyCode toggleKeycode = KeyCode.Escape;
        [SerializeField] private GamePauseStateController pauseStateController = default;
        
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
