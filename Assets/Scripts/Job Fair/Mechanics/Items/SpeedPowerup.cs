using Platformer.Core;
using Platformer.JobFair.Gameplay;
using Platformer.JobFair.Gameplay.Args;
using UnityEngine;

namespace Platformer.JobFair.Mechanics.Items
{
    /// <summary>
    /// Increases the player's speed by a multiplier for a given duration
    /// </summary>
    [CreateAssetMenu(menuName = "Items/Speed Powerup", fileName = "New Speed Powerup")]
    public class SpeedPowerup : Item
    {
        [Header("Speed Powerup Preferences")]
        [SerializeField] private float speedMultiplier = 1.5f;
        [SerializeField] private float duration = 2f;

        public float SpeedMultiplier => speedMultiplier;
        public float Duration => duration;

        public override GameObject PhysicalItemPrefab { get; }
        
        public override Simulation.Event Use(SimulationEventArgs args)
        {
            var ev = Simulation.Schedule<UseSpeedPowerup>();
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
