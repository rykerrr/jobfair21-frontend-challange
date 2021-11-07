using System;
using Platformer.Core;
using Platformer.Gameplay;
using Platformer.JobFair.Mechanics.Collisions;
using Platformer.Mechanics;
using UnityEngine;

namespace Platformer
{
    public class OnCollisionKillEntityWithHealthAndDestroySelf : MonoBehaviour, ICollisionProcessor
    {
        public event Action onCollisionSuccess;

        public void ProcessCollision(GameObject other)
        {
            if (other.GetComponent<PlayerController>())
            {
                onCollisionSuccess?.Invoke();
                
                Simulation.Schedule<PlayerDeath>();
            }
            else if (other.TryGetComponent<EnemyController>(out var en))
            {
                onCollisionSuccess?.Invoke();

                var ev = Simulation.Schedule<EnemyDeath>();
                ev.enemy = en;
            }
        }
    }
}