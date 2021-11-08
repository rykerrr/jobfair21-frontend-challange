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
        [SerializeField] protected GameObject physicalItemPrefab = default;

        public float JumpVelocity => jumpVelocity;

        public override GameObject PhysicalItemPrefab => physicalItemPrefab;

        public override Simulation.Event Use(SimulationEventArgs args)
        {
            var ev = Simulation.Schedule<UseExtraJump>();
            ((ArgsItem)args).Item = this;

            ev.args = args;

            return ev;
        }

        /// <summary>
        /// Could be moved to the Item base class as it's pretty much the same now everywhere
        /// </summary>
        /// <returns></returns>
        public override PhysicalItemEquipEvent Equip()
        {
            var ev = Simulation.Schedule<PhysicalItemEquipEvent>();
            ev.itemData = this;

            return ev;
        }
    }
}
