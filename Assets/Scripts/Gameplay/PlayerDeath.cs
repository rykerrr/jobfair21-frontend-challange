using Platformer.Core;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when the player has died.
    /// </summary>
    /// <typeparam name="PlayerDeath"></typeparam>
    public class PlayerDeath : Simulation.Event<PlayerDeath>
    {
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();
        
        private static readonly int Hurt = Animator.StringToHash("hurt");
        private static readonly int Dead = Animator.StringToHash("dead");

        public override void Execute()
        {
            var player = model.player;
            
            if (player.health.IsAlive)
            {
                player.health.Die();
                model.virtualCamera.m_Follow = null;
                model.virtualCamera.m_LookAt = null;
                player.controlEnabled = false;
                // player.collider.enabled = false;

                var deathAudioExists = player.AudioContainer.TryGetClip("Hurt", out var deathAudio);

                if (player.audioSource && deathAudioExists)
                    player.audioSource.PlayOneShot(deathAudio);
                
                player.animator.SetTrigger(Hurt);
                player.animator.SetBool(Dead, true);
                Simulation.Schedule<PlayerSpawn>(2);
            }
        }
    }
}