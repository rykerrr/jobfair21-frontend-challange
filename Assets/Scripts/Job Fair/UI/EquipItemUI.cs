using Platformer.JobFair.Mechanics.Items;
using TMPro;
using UnityEngine;

namespace Platformer.JobFair.UI
{
    /// <summary>
    /// Class responsible for handling the UI code for the Equip item, showing and hiding it depending on
    /// game state
    /// </summary>
    public class EquipItemUI : MonoBehaviour
    {
        [SerializeField] private ItemContainer plrItemContainer = default;
        [SerializeField] private ItemLocator itemLocator = default;
      
        [SerializeField] private GameObject itemEquipButton = default;
        [SerializeField] private TextMeshProUGUI itemEquipText = default;

        private void Update()
        {
            UpdateEquipButton();
        }

        private void UpdateEquipButton()
        {
            var item = itemLocator.FindFirstItem();

            // the .activeSelf check's purposes is simply there due to the fact that I don't know how SetActive is Implemented in unity
            // specifically, whether constantly calling SetActive would eventually have performance drawbacks
            
            if (item == null)
            {
                if(itemEquipButton.gameObject.activeSelf) itemEquipButton.gameObject.SetActive(false);
            }
            else
            {
                if(!itemEquipButton.gameObject.activeSelf) itemEquipButton.SetActive(true);

                if (plrItemContainer.EquippedItem != null)
                {
                    itemEquipText.text = $"Press to swap: {plrItemContainer.EquippedItem.name} with {item.Item.name}";
                }
                else itemEquipText.text = $"Press to equip: {item.Item.name}";
            }
        }

        #region event handling
        public void OnClick_TryEquipItem()
        {
            var itemPickup = itemLocator.FindFirstItem();

            if (itemPickup == null) return;

            // need iteminrangeevent in itemlocator or smth
            // itemEquipText.text = $"Equip item: {itemPickup.Item.name}";
            plrItemContainer.Equip(itemPickup);
        }
        #endregion
    }
}