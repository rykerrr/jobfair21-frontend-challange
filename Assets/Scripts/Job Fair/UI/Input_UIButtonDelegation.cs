using System;
using Platformer.JobFair.Mechanics.Items;
using Platformer.Mechanics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer.JobFair.UI
{
    public class Input_UIButtonDelegation : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private PlayerController plrController = default;
        [SerializeField] private ItemContainer plrItemContainer = default;
        [SerializeField] private GameObject itemUseButton = default;
        [SerializeField] private Image itemUseIcon = default;
        [SerializeField] private GameObject itemEquipButton = default;
        [SerializeField] private TextMeshProUGUI itemEquipText = default;
        [SerializeField] private ItemLocator itemLocator = default;
        
        private bool isMoving;
        private float move;

        private Action<Item> itemUseButtonEnable;
        private Action itemUseButtonDisable;

        private void Awake()
        {
            SetActions();
        }

        private void SetActions()
        {
            itemUseButtonEnable = x =>
            {
                itemUseButton.SetActive(true);
                itemUseIcon.sprite = x.ItemIcon;
            };

            itemUseButtonDisable = () => itemUseButton.SetActive(false);
        }

        private void Update()
        {
            if (!isMoving) return;

            plrController.UpdateMoveState(move);
        }

        #region event handlers

        public void OnClick_TryUseItem()
        {
            plrItemContainer.UseItem();
        }

        public void OnClick_TryEquipItem()
        {
            var itemPickup = itemLocator.FindFirstItem();

            if (itemPickup == null) return;
            
            // need iteminrangeevent in itemlocator or smth
            // itemEquipText.text = $"Equip item: {itemPickup.Item.name}";
            plrItemContainer.Equip(itemPickup);
        }

        public void OnClick_TryJump()
        {
            plrController.TryJump();
        }

        public void OnClick_StartMove(float normalizedHorizontalMovement)
        {
            isMoving = true;
            move = normalizedHorizontalMovement;
        }

        public void OnRelease_StopMove()
        {
            isMoving = false;

            plrController.StopMovingByButton();
        }

        #endregion

        private void OnEnable()
        {
            plrItemContainer.onItemEquipped += itemUseButtonEnable;
            plrItemContainer.onItemUnequipped += itemUseButtonDisable;
        }

        private void OnDisable()
        {
            plrItemContainer.onItemEquipped -= itemUseButtonEnable;
            plrItemContainer.onItemUnequipped -= itemUseButtonDisable;
        }
    }
}