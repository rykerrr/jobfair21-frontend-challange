using UnityEngine;

namespace Platformer.JobFair.InputProcessing
{
    public class ButtonInputProcessor : MonoBehaviour
    {
        public enum ButtonState
        {
            PressedThisFrame,
            Pressed,
            ReleasedThisFrame
        }

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
    }
}