using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Platformer.JobFair.InputProcessing
{
    public class LastTouchPositionInput : MonoBehaviour
    {
        [SerializeField] private UnityEvent<Vector2> lastTouchPosUpdated = default;

        private Camera mainCam = default;
        private Vector2 lastTouchPosition = default;

        private void Awake()
        {
            DisableIfNotAndroid();
            
            mainCam = Camera.main;
        }

        private void DisableIfNotAndroid()
        {
            Debug.Log(Application.isEditor);
            Debug.Log(Application.platform);
            
            if (!Application.isEditor && Application.platform != RuntimePlatform.Android) gameObject.SetActive(false);
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