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
        [SerializeField] private GameObject projectilePrefab = default;
        [SerializeField] private int bulletCount = 1;

        public int BulletCount => bulletCount;
        
        public override ItemUseEvent Use()
        {
            var ev = Simulation.Schedule<UseGun>();
            ev.data = this;
            
            return ev;
        }
    }
}
