using Platformer.JobFair.Mechanics.Items;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer.JobFair.UI
{
    /// <summary>
    /// Handles UI logic for movement and item use buttons
    /// Could definitely be named better and refactored
    /// </summary>
    public class ItemUseButtonUI : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private ItemContainer plrItemContainer = default;
        [SerializeField] private GameObject itemUseButton = default;
        [SerializeField] private Image itemUseIcon = default;

        private void ActivateItemButton(Item x)
        {
            itemUseButton.SetActive(true);
            itemUseIcon.sprite = x.ItemIcon;
        }

        private void DeactivateItemButton()
        {
            itemUseButton.SetActive(false);
        }

        private void OnEnable()
        {
            plrItemContainer.onItemEquipped += ActivateItemButton;
            plrItemContainer.onItemUnequipped += DeactivateItemButton;
        }

        private void OnDisable()
        {
            plrItemContainer.onItemEquipped -= ActivateItemButton;
            plrItemContainer.onItemUnequipped -= DeactivateItemButton;
        }
        
        #region event handlers

        public void OnClick_TryUseItem()
        {
            plrItemContainer.UseItem();
        }

        #endregion
    }
}