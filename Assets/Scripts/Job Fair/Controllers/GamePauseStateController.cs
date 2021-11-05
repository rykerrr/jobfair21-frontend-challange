using System;
using UnityEngine;

namespace Platformer.JobFair.Controllers
{
    /// <summary>
    /// This component is used to control the game pause state
    /// No need to turn it into a singleton although only one should exist per scene
    /// As this is part of the UI, multi-scene workflow may be a perfect fit
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

        /// <summary>
        /// Prevent possible runtime errors occurring due to timeScale being static by resetting it
        /// </summary>
        private void OnDestroy()
        {
            Time.timeScale = 1f;
        }
    }
}
