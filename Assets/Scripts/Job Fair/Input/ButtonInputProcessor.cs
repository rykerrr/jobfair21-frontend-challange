using Platformer.JobFair.Mechanics.Items;
using UnityEngine;
using UnityEngine.Events;

namespace Platformer.JobFair.InputProcessing
{
    /// <summary>
    /// May be replaced in favor of new input system, currently just acts as a delegation responsible
    /// solely for input
    /// </summary>
    public class ButtonInputProcessor : MonoBehaviour
    {
        [SerializeField] private ItemLocator itemLocator;
        
        [SerializeField] private UnityEvent<ItemPickup> itemEquipEvent = default;
        [SerializeField] private UnityEvent<Vector2> mousePosChanged = default;
        [SerializeField] private UnityEvent itemUseEvent = default;

        private Camera mainCam;
        
        private void Awake()
        {
            TryInjectDefaultReferences();
        }

        private void TryInjectDefaultReferences()
        {
            itemLocator ??= GetComponent<ItemLocator>();

            mainCam = Camera.main;
        }

        private void Update()
        {
            CheckForItemUseInput();
            CheckForItemEquipInput();
            CheckForMousePosChange();
        }

        #region Temporary, "mimics" new input system
        /// <summary>
        /// Will be removed once new input system is imported if I have time
        /// </summary>
        private void CheckForItemUseInput()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Input_UseItem();
            }
        }

        private void CheckForMousePosChange()
        {
            Vector2 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            
            mousePosChanged?.Invoke(mousePos);
        }
        
        private void CheckForItemEquipInput()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Input_EquipItem();
            }
        }

        private void Input_EquipItem()
        {
            var item = itemLocator.FindFirstItem();

            if (item == null) return;

            itemEquipEvent?.Invoke(item);
        }

        /// <summary>
        /// Will also be removed if new input system is imported as this just mimics part of its functionality
        /// </summary>
        public void Input_UseItem()
        {
            itemUseEvent?.Invoke();
        }
        #endregion
        
        public ButtonState GetButtonState(string button)
        {
            if (Input.GetButtonDown(button))
            {
                return ButtonState.PressedThisFrame;
            }
            else if (Input.GetButtonUp(button))
            {
                return ButtonState.ReleasedThisFrame;
            }
            else return ButtonState.Pressed;
        }

        public float GetAxis(string axis)
        {
            return Input.GetAxis(axis);
        }
        
        public enum ButtonState
        {
            PressedThisFrame,
            Pressed,
            ReleasedThisFrame
        }
    }
}