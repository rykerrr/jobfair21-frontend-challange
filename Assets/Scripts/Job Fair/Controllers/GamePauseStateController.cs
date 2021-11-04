using System;
using UnityEngine;

namespace Platformer.JobFair.Controllers
{
    /// <summary>
    /// This component is used to control the game pause state
    /// </summary>
    public class GamePauseStateController : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour playerInput = default;

        public event Action onResume = delegate { };
        public event Action onPause = delegate { };
        
        public void Pause()
        {
            onPause?.Invoke();

            Time.timeScale = 0f;
            playerInput.enabled = false;
        }

        public void Resume()
        {
            onResume?.Invoke();

            Time.timeScale = 1f;
            playerInput.enabled = true;
        }
    }
}
