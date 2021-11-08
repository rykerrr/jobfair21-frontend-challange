using Platformer.JobFair.Destruction;
using UnityEngine;

namespace Platformer.JobFair.Mechanics.Items
{
    /// <summary>
    /// Component class for a "physical" depiction of an item, it's animations and other stuff
    /// regarding visuals
    /// </summary>
    public class ItemPickup : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private SpriteRenderer spriteRenderer = default;

        [Header("Preferences")]
        [SerializeField] private Item item = default;
        
        private IDestructionProcessor destructionProcessor;

        public Item Item => item;

        private void Awake()
        {
            destructionProcessor = GetComponent<IDestructionProcessor>();
            
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            spriteRenderer.sprite = item.ItemIcon;
        }
        
        public void PickUp(Item item)
        {
            this.item = item;
            
            // if there's a new item, meaning we swapped, update the display
            if (this.item != null)
            {
                UpdateDisplay();
            }
            else destructionProcessor.Destroy();
        }
    }
}
