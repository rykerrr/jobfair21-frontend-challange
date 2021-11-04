using Platformer.Core;
using Platformer.Mechanics;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when the player performs a Jump.
    /// </summary>
    /// <typeparam name="PlayerJumped"></typeparam>
    public class PlayerJumped : Simulation.Event<PlayerJumped>
    {
        public PlayerController player;

        public override void Execute()
        {
            var jumpExists = player.AudioContainer.TryGetClip("jump", out var jumpAudio);

            if (player.AudioSource && jumpExists)
            {
                player.AudioSource.PlayOneShot(jumpAudio);
            }
        }
    }
}