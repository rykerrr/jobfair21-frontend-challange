using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Platformer.JobFair.InputProcessing
{
    /// <summary>
    /// Pretty much the touch-based approach to the MousePositionInput class for mobile devices
    /// Could generalize it using an abstract class as they share a lot of the same behaviour
    /// </summary>
    public class LastTouchPositionInput : MonoBehaviour
    {
        [Header("Event Handling")]
        [SerializeField] private UnityEvent<Vector2> lastTouchPosUpdated = default;

        private Camera mainCam = default;
        private Vector2 lastTouchPosition = default;

        private void Awake()
        {
            DisableIfNotAndroid();
            
            mainCam = Camera.main;
        }

        /// <summary>
        /// Pretty much the same as it's counterpart for pc, disables itself if it's on pc to avoid
        /// unnecessary update calls if the platform isn't android and it's not in the editor
        /// If it's in the editor it's left as on due to Unity remote working directly with the editor
        /// </summary>
        private void DisableIfNotAndroid()
        {
            // Debug.Log(Application.isEditor);
            // Debug.Log(Application.platform);
            
            if (!Application.isEditor && Application.platform != RuntimePlatform.Android) enabled = false;
        }

        private void Update()
        {
            TryUpdateTouchPos();
        }

        /// <summary>
        /// This works fine due to if statement short circuit
        /// if noTouches is true, we don't care about anything that comes after due to the logical or operator
        /// </summary>
        private void TryUpdateTouchPos()
        {
            var noTouches = Input.touchCount == 0;
            if (noTouches || EventSystem.current.IsPointerOverGameObject(0)) return;

            UpdateTouchPos();
        }

        private void UpdateTouchPos()
        {
            var touch = Input.GetTouch(0);

            lastTouchPosition = mainCam.ScreenToWorldPoint(touch.position);
            lastTouchPosUpdated?.Invoke(lastTouchPosition);
        }
    }
}