using Platformer.Core;
using Platformer.JobFair.Mechanics;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Job_Fair.Gameplay
{
    public class ArrowFired : Simulation.Event<ArrowFired>
    {
        public ArrowTurret turret;
        
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();
        
        public override void Execute()
        {
            var hasWhoosh = turret.AudioContainer.TryGetClip("crossbow_arrow_whoosh", out var whooshClip);
            if(turret.AudioSource && hasWhoosh) turret.AudioSource.PlayOneShot(whooshClip);
            
            var arrowClone = Object.Instantiate(turret.ArrowPrefab, turret.FirePoint.position, Quaternion.identity);

            arrowClone.Init(turret.FirePoint.right);
        }
    }
}