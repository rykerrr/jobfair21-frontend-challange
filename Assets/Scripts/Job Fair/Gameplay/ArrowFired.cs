using Platformer.Core;
using Platformer.JobFair.Mechanics;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;

namespace Platformer.JobFair.Gameplay
{
    /// <summary>
    /// DES usage for an arrow being fired, could actually be much more generic if an interface or abstract
    /// class was created for ArrowTurret, and ArrowTurret/other projectile firing entities inherited from it
    /// </summary>
    public class ArrowFired : Simulation.Event<ArrowFired>
    {
        public ArrowTurret turret;

        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            var hasWhoosh = turret.AudioContainer.TryGetClip("crossbow_arrow_whoosh", out var whooshClip);
            if (turret.AudioSource && hasWhoosh) turret.AudioSource.PlayOneShot(whooshClip);

            // this delegation projectile creation logic is what would allow to make this event generalized
            // for ProjectileFired instead of simply ArrowFired
            var arrowClone = GameController.Instance.creationHandler.CreateProjectile(
                turret.ArrowPrefab.gameObject, turret.FirePoint.position, Quaternion.identity);

            arrowClone.GetComponent<ArrowProjectile>().Init(turret.FirePoint.right);
        }
    }
}