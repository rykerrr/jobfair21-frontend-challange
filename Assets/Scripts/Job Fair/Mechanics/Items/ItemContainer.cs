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
        [SerializeField] private ItemLocator itemLocator = default;
        
        private Item equippedItem = default;

        private void Awake()
        {
            TryInjectDefaultReferences();
        }
        
        private void TryInjectDefaultReferences()
        {
            plrController ??= GetComponent<PlayerController>();
            itemLocator ??= GetComponent<ItemLocator>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                var item = itemLocator.FindFirstItem();

                if (item == null) return;
                
                Equip(item);
            }
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
            if(equippedItem) UnEquip();
            
            equippedItem = itemPickup.Item;
        }

        public void UnEquip()
        {
            equippedItem = null;
        }
    }
}