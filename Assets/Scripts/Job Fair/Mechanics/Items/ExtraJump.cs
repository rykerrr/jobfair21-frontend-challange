using Platformer.Core;
using Platformer.JobFair.Gameplay;
using Platformer.JobFair.Gameplay.Args;
using UnityEngine;

namespace Platformer.JobFair.Mechanics.Items
{
    /// <summary>
    /// Item that allows the player to jump multiple times by calling Bounce in UseExtraJump
    /// May implement multiple usage (e.g not just once extra jump)
    /// Could be useful for puzzle creation
    /// </summary>
    [CreateAssetMenu(menuName = "Items/Extra Jump", fileName = "New Extra Jump")]
    public class ExtraJump : Item
    {
        [Header("Extra Jump Preferences")]
        [SerializeField] private float jumpVelocity = 7f;

        public float JumpVelocity => jumpVelocity;

        public override GameObject PhysicalItemPrefab { get; }

        public override Simulation.Event Use(SimulationEventArgs args)
        {
            var ev = Simulation.Schedule<UseExtraJump>();
            ev.data = this;
            
            // todo: remove ev.data set above, remove data entirely actually and make it follow the interface pattern
            // todo: similarly to gun and ProjectileFired
            ev.args = args;

            return ev;
        }

        public override PhysicalItemEquipEvent Equip()
        {
            return null;
        }
    }
}
