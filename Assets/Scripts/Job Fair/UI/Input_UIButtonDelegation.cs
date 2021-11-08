using System;
using Platformer.JobFair.Mechanics.Items;
using Platformer.Mechanics;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer.JobFair.UI
{
    public class Input_UIButtonDelegation : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private PlayerController plrController = default;
        [SerializeField] private ItemContainer plrItemContainer = default;
        [SerializeField] private GameObject itemUseButton = default;
        [SerializeField] private Image itemUseIcon = default;
        
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