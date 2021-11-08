using Platformer.Core;
using Platformer.JobFair.Gameplay.Args;
using Platformer.JobFair.Mechanics;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;

namespace Platformer.JobFair.Gameplay
{
    /// <summary>
    /// This class is effectively the same as the previous UseGun and ArrowFired events
    /// But generalized for all projectiles using the simulation args
    /// </summary>
    public class ProjectileFired : ArgsSimulationEvent<ProjectileFired>
    {
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            var pFiredData = args as ArgsProjectileFireEvent;
            
#if UNITY_EDITOR
            if(pFiredData == null)
            {
                Debug.LogError("Gun data is null!");
                Debug.Break();
            }
#endif
            
            var pFirer = pFiredData.projectileFirer;
            var pFirerAudio = pFiredData.firerAudio;
            
            var hasWhoosh = pFirerAudio.AudioContainer.TryGetClip("crossbow_arrow_whoosh", out var whooshClip);
            if (pFirerAudio.AudioSource && hasWhoosh) pFirerAudio.AudioSource.PlayOneShot(whooshClip);

            // this delegation projectile creation logic is what would allow to make this event generalized
            // for ProjectileFired instead of simply ArrowFired
            var projectileClone = GameController.Instance.CreationManager.CreateObj(
                pFirer.ProjectilePrefab.gameObject, pFirer.FirePoint.position, Quaternion.identity);

            projectileClone.GetComponent<Projectile>().Init(pFirer.FirePoint.right, pFiredData.projectileFirer);
        }
    }
}