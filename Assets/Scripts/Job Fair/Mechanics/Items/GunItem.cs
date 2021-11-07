using Platformer.Core;
using Platformer.JobFair.Gameplay;
using UnityEngine;

namespace Platformer.JobFair.Mechanics.Items
{
    /// <summary>
    /// Practically an item that can "fire" a projectile which would kill an enemy
    /// </summary>
    [CreateAssetMenu(menuName = "Items/Gun", fileName = "New Gun")]
    public class GunItem : Item
    {
        [Header("Gun Preferences")]
        [SerializeField] private int bulletCount = 1;
        [SerializeField] protected GameObject physicalItemPrefab = default;
        public int BulletCount => bulletCount;

        public override GameObject PhysicalItemPrefab => physicalItemPrefab;

        public override ItemUseEvent Use()
        {
            var ev = Simulation.Schedule<UseGun>();
            ev.data = this;
            
            return ev;
        }

        public override PhysicalItemEquipEvent Equip()
        {
            var ev = Simulation.Schedule<PhysicalItemEquipEvent>();
            ev.itemData = this;

            return ev;
        }
    }
}
