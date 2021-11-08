using UnityEngine;
using UnityEngine.Events;

namespace Platformer.JobFair.InputProcessing
{
    /// <summary>
    /// Extracted from ButtonInputProcessor so that newly instantiated components can use this
    /// Such as the Gun
    /// </summary>
    public class MousePositionInput : MonoBehaviour
    {
        [SerializeField] private UnityEvent<Vector2> mousePosChanged = default;

        private Camera mainCam;
        
        private void Awake()
        {
            DisableIfNotPC();
            
            mainCam = Camera.main;
        }

        /// <summary>
        /// Pretty much the same as it's counterpart for android, disables itself if it's on android to avoid
        /// unnecessary update calls if the platform isn't android and it's not in the editor
        /// If it's in the editor it's left as on due to Unity remote working directly with the editor
        /// </summary>
        private void DisableIfNotPC()
        {
            // Debug.Log(Application.isEditor);
            // Debug.Log(Application.platform);
            
            if(!Application.isEditor && Application.platform != RuntimePlatform.WindowsPlayer) gameObject.SetActive(false);
        }

        private void Update()
        {
            CheckForMousePosChange();
        }
        
        private void CheckForMousePosChange()
        {
            Vector2 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            
            mousePosChanged?.Invoke(mousePos);
        }
    }
}
