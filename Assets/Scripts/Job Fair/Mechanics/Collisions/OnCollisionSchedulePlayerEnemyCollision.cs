using Platformer.Gameplay;
using Platformer.Mechanics;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.JobFair.Mechanics.Collisions
{
    public class OnCollisionSchedulePlayerEnemyCollision : MonoBehaviour, ICollisionProcessor
    {
        [SerializeField] private EnemyController enemyController = default;

        public void SetEnemyController(EnemyController enemyController)
        {
            this.enemyController = enemyController;
        }
        
        public void ProcessCollision(GameObject other)
        {
            var player = other.GetComponent<PlayerController>();

            if (player != null)
            {
                var ev = Schedule<PlayerEnemyCollision>();
                ev.player = player;
                ev.enemy = enemyController;
            }
        }
    }
}