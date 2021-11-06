using Platformer.Core;
using Platformer.JobFair.Destruction;
using Platformer.Mechanics;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when the health component on an enemy has a hitpoint value of  0.
    /// </summary>
    /// <typeparam name="EnemyDeath"></typeparam>
    public class EnemyDeath : Simulation.Event<EnemyDeath>
    {
        public EnemyController enemy;

        public override void Execute()
        {
            enemy.GetComponent<IDestructionProcessor>().Destroy();

            var hasDeathAudio = enemy.AudioContainer.TryGetClip("LandOnEnemy", out var deathAudio);
            
            if (enemy.AudioSource && hasDeathAudio)
            {
                enemy.AudioSource.PlayOneShot(deathAudio);
            }
        }
    }
}