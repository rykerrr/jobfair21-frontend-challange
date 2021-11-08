using Platformer.Mechanics;
using UnityEngine;

namespace Platformer.JobFair.UI
{
    /// <summary>
    /// UI Code for the movement-related buttons such as Jump, Move left/right
    /// </summary>
    public class MovementButtonsUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private PlayerController plrController;
        
        private float horizontalMoveValue;
        private bool isMoving;
        
        private void Update()
        {
            if (!isMoving) return;

            plrController.UpdateMoveState(horizontalMoveValue);
        }
        
        #region event handling
        public void OnClick_TryJump()
        {
            plrController.TryJump();
        }

        /// <summary>
        /// Works in tandem with stop move, on press it starts the movement, used by an event trigger component
        /// You can look at the way it works similarly to how dragging a UI element would generally work, on press the
        /// dragging starts, on release it would stop
        /// </summary>
        /// <param name="normalizedHorizontalMovement"></param>
        public void OnClick_StartMove(float normalizedHorizontalMovement)
        {
            isMoving = true;
            horizontalMoveValue = normalizedHorizontalMovement;
        }

        /// <summary>
        /// Works in tandem with start move, on release it stops the movement, used by an event trigger component
        /// You can look at the way it works similarly to how dragging a UI element would generally work, on press the
        /// dragging starts, on release it would stop
        /// </summary>
        public void OnRelease_StopMove()
        {
            isMoving = false;

            plrController.StopMovingByButton();
        }
        #endregion
    }
}
