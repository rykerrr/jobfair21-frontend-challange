using System;
using Platformer.Core;
using Platformer.Gameplay;
using Platformer.Mechanics;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.JobFair.Mechanics.Collisions
{
    /// <summary>
    /// Arrow collision logic delegated to an ICollisionProcessor
    /// </summary>
    public class OnCollisionKillPlayer : MonoBehaviour, ICollisionProcessor
    {
        public event Action onCollisionSuccess;

        public void ProcessCollision(GameObject other)
        {
            if (other.GetComponent<PlayerController>())
            {
                var plrDeath = Simulation.Schedule<PlayerDeath>();
                
                onCollisionSuccess?.Invoke();
            }
        }
    }
}