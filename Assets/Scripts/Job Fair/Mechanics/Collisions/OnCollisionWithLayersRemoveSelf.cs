using System;
using Platformer.JobFair.Destruction;
using Platformer.JobFair.Mechanics.Collisions;
using UnityEngine;

namespace Platformer
{
    /// <summary>
    /// Used mostly with the arrow, the layermask could be set dynamically during runtime with an easy modification but
    /// I thought this would be more interesting for the current way the game works
    /// </summary>
    public class OnCollisionWithLayersRemoveSelf : MonoBehaviour, ICollisionProcessor
    {
        [Header("Preferences")]
        [SerializeField] private LayerMask whatIsHittable = default;

        private IDestructionProcessor destructionProcessor;
        
        public event Action onCollisionSuccess;

        private void Awake()
        {
            destructionProcessor = GetComponent<IDestructionProcessor>();
        }

        public void ProcessCollision(GameObject other)
        {
            if (whatIsHittable == (whatIsHittable | (1 << other.layer)))
            {
                onCollisionSuccess?.Invoke();

                destructionProcessor.Destroy();
            }
        }
    }
}
