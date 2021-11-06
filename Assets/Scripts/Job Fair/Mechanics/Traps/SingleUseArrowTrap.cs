using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.JobFair.Mechanics.Traps
{
    /// <summary>
    /// Fires an arrow from a given ArrowTurret ONCE 
    /// Minecraft's dispenser can be used as a reference point for this Idea
    /// </summary>
    public class SingleUseArrowTrap : Trap
    {
        [SerializeField] private ArrowTurret arrowTurret;

        private bool alreadyFired = false;

        /// <summary>
        /// Due to if statement short-circuit (if the first or clause is true it doesn't check the rest) the
        /// .CanFire doesn't add any more overhead as a just-in-case check
        /// </summary>
        private void TryFireArrow()
        {
            // if (alreadyFired || !arrowTurret.CanFire) return;
            // alreadyFired = true;
            
            arrowTurret.Fire();
        }
        
        public override void Tick()
        {
            TryFireArrow();
        }
    }
}
