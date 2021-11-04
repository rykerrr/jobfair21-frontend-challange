using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when the player is spawned after dying.
    /// </summary>
    public class PlayerSpawn : Simulation.Event<PlayerSpawn>
    {
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        private static readonly int Dead = Animator.StringToHash("dead");

        public override void Execute()
        {
            var player = model.player;
            player.Collider2d.enabled = true;
            player.controlEnabled = false;

            var hasRespawnAudio = player.AudioContainer.TryGetClip("Death", out var respawnAudio);

            if (player.AudioSource && hasRespawnAudio)
            {
                player.AudioSource.PlayOneShot(respawnAudio);
            }
            
            player.Health.Increment();
            player.Teleport(model.spawnPoint.transform.position);
            player.jumpState = PlayerController.JumpState.Grounded;
            player.Animator.SetBool(Dead, false);
            model.virtualCamera.m_Follow = player.transform;
            model.virtualCamera.m_LookAt = player.transform;
            Simulation.Schedule<EnablePlayerInput>(2f);
        }
    }
}