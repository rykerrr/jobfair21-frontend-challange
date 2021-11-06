using System;
using System.Linq;
using Platformer.Mechanics;
using UnityEngine;

namespace Platformer.JobFair.Mechanics.Items
{
    /// <summary>
    /// Class for containing an item, sort of acts as intermediary between input and usage
    /// </summary>
    public class ItemContainer : MonoBehaviour
    {
        #region interactables finder stuff
        [SerializeField] private LayerMask whatIsItem = default;
        [SerializeField] private float radius = 3f;

        private int colliderAmn = 3;
        private Collider2D[] itemsFound = default;
        #endregion
        
        [SerializeField] private PlayerController plrController = default;
        
        private Item equippedItem = default;

        private void Awake()
        {
            itemsFound = new Collider2D[colliderAmn];
            
            TryInjectDefaultReferences();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                FindAndTryEquipItem();
            }
        }

        private void FindAndTryEquipItem()
        {
            Debug.Log("Calling overlap sphere");
            
            Physics2D.OverlapCircleNonAlloc(transform.position, radius, itemsFound, whatIsItem);
            var items = itemsFound.Select(x => x != null && x.TryGetComponent(typeof(ItemPickup), out var item) ? (ItemPickup)item : null).ToList();

            Debug.Log("Select statement finished");
            
            var itemToEquip = items.FirstOrDefault();
            
            Debug.Log("item found");
            
            if (itemToEquip == null) return;

            Debug.Log("attempting to equip found item");
            
            Equip(itemToEquip);
        }

        private void TryInjectDefaultReferences()
        {
            plrController ??= GetComponent<PlayerController>();
        }

        public void UseItem()
        {
            if (equippedItem == null) return;

            var itemEv = equippedItem.Use();
            itemEv.playerController = plrController;
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
        
        #region editor methods

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, radius);
        }

        #endregion
    }
}