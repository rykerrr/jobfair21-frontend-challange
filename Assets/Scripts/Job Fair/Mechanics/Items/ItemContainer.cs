using System;
using Platformer.JobFair.Gameplay;
using Platformer.JobFair.Gameplay.Args;
using Platformer.Mechanics;
using UnityEngine;

namespace Platformer.JobFair.Mechanics.Items
{
    /// <summary>
    /// Class for containing an item, sort of acts as intermediary between input and usage
    /// </summary>
    public class ItemContainer : MonoBehaviour
    {
        [SerializeField] private PhysicalItemContainer plrPhysicalItemContainer = default;
        
        private Item equippedItem = default;

        public event Action<Item> onItemEquipped = delegate { };
        public event Action onItemUnequipped = delegate { };

        public Item EquippedItem => equippedItem;
        
        private void Awake()
        {
            TryInjectDefaultReferences();
        }
        
        private void TryInjectDefaultReferences()
        {
            if(plrPhysicalItemContainer == null) Debug.LogError("Player Physical Item Container is null", this);
        }

        public void UseItem()
        {
            if (equippedItem == null) return;

            // Gets the args for the equipped item's use event
            // This could have just been placed in the SO, but I wanted to keep that responsibility separate
            // The SO would have had problems creating the args as well as it requires instances within the scene
            var args = plrPhysicalItemContainer.GetItemUseEventData();
            
            equippedItem.Use(args);
            
            UnEquip();
        }

        public void Equip(ItemPickup itemPickup)
        {
            Item prevItem = null;
            
            if (equippedItem != null)
            {
                prevItem = equippedItem;
                
                UnEquip();
            }
            
            equippedItem = itemPickup.Item;
            
            var ev = equippedItem.Equip();
            onItemEquipped?.Invoke(itemPickup.Item);

            itemPickup.PickUp(prevItem);

            if (ev == null) return;
            ev.physicalItemContainer = plrPhysicalItemContainer;
        }

        public void UnEquip()
        {
            equippedItem = null;
            onItemUnequipped?.Invoke();

            plrPhysicalItemContainer.TryUnEquip();
        }

        #region editor methods
#if UNITY_EDITOR
        [ContextMenu("Log equipped item")]
        public void LogEquippedItem()
        {
            Debug.Log(equippedItem);
        }
#endif        
        #endregion
    }
}