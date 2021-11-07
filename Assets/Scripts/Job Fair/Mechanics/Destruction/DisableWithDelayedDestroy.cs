using Platformer.JobFair.Destruction;
using UnityEngine;

namespace Platformer
{
    /// <summary>
    /// The main purpose of this is due to certain objects needing to access what created them
    /// But what created them gets destroyed during the same frame, so we delay it, but disable it
    /// This doesn't matter with pooled objects though
    /// </summary>
    public class DisableWithDelayedDestroy : MonoBehaviour, IDestructionProcessor
    {
        [SerializeField] private float delay;
        
        public void Destroy()
        {
            Destroy(gameObject, delay);
            gameObject.SetActive(false);
        }
    }
}
