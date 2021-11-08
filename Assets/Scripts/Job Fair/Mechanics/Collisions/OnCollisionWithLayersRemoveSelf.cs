using System;
using Platformer.JobFair.Destruction;
using Platformer.JobFair.Mechanics.Collisions;
using UnityEngine;

namespace Platformer
{
    public class OnCollisionWithLayersRemoveSelf : MonoBehaviour, ICollisionProcessor
    {
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
