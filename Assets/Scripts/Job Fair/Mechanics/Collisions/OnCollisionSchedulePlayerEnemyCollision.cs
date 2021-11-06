using System;
using Platformer.Gameplay;
using Platformer.Mechanics;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.JobFair.Mechanics.Collisions
{
    /// <summary>
    /// Delegation of enemy collision logic to an ICollisionProcessor
    /// </summary>
    public class OnCollisionSchedulePlayerEnemyCollision : MonoBehaviour, ICollisionProcessor
    {
        [SerializeField] private EnemyController enemyController = default;

        public event Action onCollisionSuccess;

        public void ProcessCollision(GameObject other)
        {
            var player = other.GetComponent<PlayerController>();

            if (player != null)
            {
                var ev = Schedule<PlayerEnemyCollision>();
                ev.player = player;
                ev.enemy = enemyController;
                
                onCollisionSuccess?.Invoke();
            }
        }
    }
}