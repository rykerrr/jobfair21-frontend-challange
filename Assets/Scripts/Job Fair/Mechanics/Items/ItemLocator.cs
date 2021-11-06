using System;
using System.Linq;
using UnityEngine;

namespace Platformer.JobFair.Mechanics.Items
{
    /// <summary>
    /// Component that can be used to locate items in a given radius
    /// Making this a generic (or just making the class itself more generic) could prove useful in
    /// different areas, beyond simply finding items, such as explosions, etc
    /// </summary>
    public class ItemLocator : MonoBehaviour
    {
        [Header("Preferences")]
        [SerializeField] private LayerMask whatIsItem = default;
        [SerializeField] private int itemFindRadius = 3;

        private Collider2D[] itemsFound;

        private void Awake()
        {
            itemsFound = new Collider2D[3];
        }

        public ItemPickup FindFirstItem()
        {
            Physics2D.OverlapCircleNonAlloc(transform.position, itemFindRadius, itemsFound, whatIsItem);
            var items = itemsFound.Select(x =>
                x != null && x.TryGetComponent(typeof(ItemPickup), out var item) ? (ItemPickup) item : null).ToList();

            var itemToEquip = items.FirstOrDefault();

            if (itemToEquip == null) return null;

            Array.Clear(itemsFound, 0, itemsFound.Length);
            return itemToEquip;
        }

        #region editor methods

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, itemFindRadius);
        }

        #endregion
    }
}