using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when a player collides with a token.
    /// </summary>
    /// <typeparam name="PlayerCollision"></typeparam>
    public class PlayerTokenCollision : Simulation.Event<PlayerTokenCollision>
    {
        public PlayerController player;
        public TokenInstance token;

        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            var hasTokenCollectAudio = token.AudioContainer
                .TryGetClip("Collectable", out var tokenCollectAudio);

            if (!hasTokenCollectAudio) return;

            // The main camera seems to generally be located at -9f so this moves it closer to the camera
            // to make the sound louder, otherwise it's nearly negligible
            var newPos = token.transform.position + new Vector3(0f, 0f, -9f);
            
            AudioSource.PlayClipAtPoint(tokenCollectAudio, newPos, 1f);
        }
    }
}