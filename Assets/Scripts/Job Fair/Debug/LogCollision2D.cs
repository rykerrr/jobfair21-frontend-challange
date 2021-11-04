using UnityEngine;

namespace Platformer.JobFair.Debugging
{
    public class LogCollision2D : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log("Collision enter");
        }
    }
}
