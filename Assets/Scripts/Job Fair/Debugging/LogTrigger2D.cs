using UnityEngine;

namespace Platformer.JobFair.Debugging
{
    public class LogTrigger2D : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Trigger enter");
        }
    }
}
