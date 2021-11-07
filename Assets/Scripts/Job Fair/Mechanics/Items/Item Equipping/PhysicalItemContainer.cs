using Platformer.JobFair.Destruction;
using Platformer.JobFair.Gameplay.Args;
using UnityEngine;

namespace Platformer
{
    public class PhysicalItemContainer : MonoBehaviour
    {
        [Header("Set by self or injected, shown for debug")]
        [SerializeField] private GameObject equippedItem;

        public void Equip(GameObject physicalItem)
        {
            TryUnEquip();
            
            equippedItem = physicalItem;
        }

        public void TryUnEquip()
        {
            if (equippedItem == null) return;

            equippedItem.GetComponent<IDestructionProcessor>().Destroy();
        }

        /// <summary>
        /// This pretty much acts as an abstraction layer to the data
        /// If you were to expose the equipped item in a property, this would be near to useless
        /// </summary>
        /// <returns></returns>
        public SimulationEventArgs GetItemUseEventData()
        {
            if (equippedItem == null || 
                !equippedItem.TryGetComponent<IPhysicalItem>(out var physItem)) return null;

            return physItem.GetUseData();
        }
    }
}
