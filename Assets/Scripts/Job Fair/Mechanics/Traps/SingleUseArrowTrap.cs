using Platformer.JobFair.Mechanics.Weaponry;
using UnityEngine;

namespace Platformer.JobFair.Mechanics.Traps
{
    /// <summary>
    /// Fires an arrow from a given ArrowTurret ONCE 
    /// Minecraft's dispenser can be used as a reference point for this Idea
    /// </summary>
    public class SingleUseArrowTrap : Trap
    {
        [SerializeField] private Turret turret;

        private bool alreadyFired = false;

        /// <summary>
        /// Due to if statement short-circuit (if the first or clause is true it doesn't check the rest) the
        /// .CanFire doesn't add any more overhead as a just-in-case check
        /// </summary>
        private void TryFireArrow()
        {
            // if (alreadyFired || !arrowTurret.CanFire) return;
            // alreadyFired = true;
            
            turret.Fire();
        }
        
        public override void Tick()
        {
            TryFireArrow();
        }
    }
}
