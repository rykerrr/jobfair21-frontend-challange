using UnityEngine;

namespace Platformer.JobFair.InputProcessing
{
    /// <summary>
    /// May be replaced in favor of new input system, currently just acts as a delegation responsible
    /// solely for input
    /// </summary>
    public class ButtonInputProcessor : MonoBehaviour
    {
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