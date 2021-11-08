using System;
using Platformer.JobFair.Mechanics.Weaponry;
using UnityEngine;

namespace Platformer.JobFair.Mechanics.Traps
{
    /// <summary>
    /// Fires an arrow from a given ArrowTurret ONCE 
    /// Minecraft's dispenser can be used as a reference point for this Idea
    /// </summary>
    public class ArrowTrap : Trap
    {
        [Header("References")]
        [SerializeField] private Turret turret;
        [Header("Preferences")] 
        [SerializeField] private int fireCount = 1;

        private int localFireCount;

        private void Awake()
        {
            localFireCount = fireCount;
        }

        /// <summary>
        /// Due to if statement short-circuit (if the first or clause is true it doesn't check the rest) the
        /// .CanFire doesn't add any more overhead as a just-in-case check
        /// </summary>
        private void TryFireArrow()
        {
            if (localFireCount <= 0 || !turret.CanFire) return;
            localFireCount--;
            
            turret.Fire();
        }
        
        public override void Tick()
        {
            TryFireArrow();
        }
    }
}
