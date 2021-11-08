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

        private void DisableIfNotPC()
        {
            Debug.Log(Application.isEditor);
            Debug.Log(Application.platform);
            
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