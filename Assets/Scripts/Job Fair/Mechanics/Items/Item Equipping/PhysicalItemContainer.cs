using System.Collections;
using System.Collections.Generic;
using Platformer.JobFair.Destruction;
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
    }
}
