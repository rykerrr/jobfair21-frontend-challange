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
            
            if (player.Health.IsAlive)
            {
                player.Health.Die();
                model.virtualCamera.m_Follow = null;
                model.virtualCamera.m_LookAt = null;
                player.controlEnabled = false;
                // player.collider.enabled = false;

                var deathAudioExists = player.AudioContainer.TryGetClip("Hurt", out var deathAudio);

                if (player.AudioSource && deathAudioExists)
                    player.AudioSource.PlayOneShot(deathAudio);
                
                player.Animator.SetTrigger(Hurt);
                player.Animator.SetBool(Dead, true);
                Simulation.Schedule<PlayerSpawn>(2);
            }
        }
    }
}