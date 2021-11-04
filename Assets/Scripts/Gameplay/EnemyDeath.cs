using Platformer.Core;
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
            enemy._collider.enabled = false;
            enemy.control.enabled = false;

            var hasDeathAudio = enemy.AudioContainer.TryGetClip("LandOnEnemy", out var deathAudio);
            
            if (enemy._audio && hasDeathAudio)
            {
                enemy._audio.PlayOneShot(deathAudio);
            }
        }
    }
}