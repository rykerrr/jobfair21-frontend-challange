using Platformer.Core;
using Platformer.JobFair.Mechanics;
using Platformer.JobFair.Mechanics.Items;
using Platformer.Mechanics;

namespace Platformer.JobFair.Gameplay
{
    public class PhysicalItemEquipEvent : Simulation.Event<PhysicalItemEquipEvent>
    {
        public PhysicalItemContainer physicalItemContainer;
        public Item itemData;

        public override void Execute()
        {
            var clone = GameController.Instance.ProjectileManager.CreateProjectile(itemData.PhysicalItemPrefab
                , physicalItemContainer.transform);

            clone.transform.parent = physicalItemContainer.transform;
            
            // Wanted to put this line in ItemContainer as ItemContainer already calls UnEquip
            // but we do need the reference to the instantiated clone...
            physicalItemContainer.Equip(clone);
        }
    }
}