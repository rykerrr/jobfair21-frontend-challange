using Platformer.Mechanics;
using UnityEngine;

namespace Platformer.JobFair.Mechanics.Items
{
    /// <summary>
    /// Class for containing an item, sort of acts as intermediary between input and usage
    /// </summary>
    public class ItemContainer : MonoBehaviour
    {
        [SerializeField] private PlayerController plrController = default;
        [SerializeField] private PhysicalItemContainer plrPhysicalItemContainer = default;
        
        private Item equippedItem = default;

        private void Awake()
        {
            TryInjectDefaultReferences();
        }
        
        private void TryInjectDefaultReferences()
        {
            plrController ??= GetComponent<PlayerController>();
            
            if(plrPhysicalItemContainer == null) Debug.LogError("Player Physical Item Container is null", this);
        }

        public void UseItem()
        {
            if (equippedItem == null) return;

            var itemEv = equippedItem.Use();
            itemEv.playerController = plrController;
            
            // Currently item's are single use, prone to change in the future
            UnEquip();
        }
        
        public void Equip(ItemPickup itemPickup)
        {
            if(equippedItem != null) UnEquip();
            
            equippedItem = itemPickup.Item;
            
            var ev = equippedItem.Equip();

            if (ev == null) return;
            ev.physicalItemContainer = plrPhysicalItemContainer;
        }

        public void UnEquip()
        {
            equippedItem = null;

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