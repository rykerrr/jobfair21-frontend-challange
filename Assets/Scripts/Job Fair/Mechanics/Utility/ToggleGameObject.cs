using UnityEngine;

namespace Platformer.JobFair.Utility
{
    public class ToggleGameObject : MonoBehaviour
    {
        [SerializeField] private GameObject objToToggle = default;
        
        public void Toggle()
        {
            objToToggle.SetActive(!objToToggle.activeSelf);
        }
    }
}
