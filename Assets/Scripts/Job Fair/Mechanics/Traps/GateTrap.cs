using UnityEngine;

namespace Platformer.JobFair.Mechanics.Traps
{
    /// <summary>
    /// Trap that utilizes a gate for it's purpose, e.g to release a slime, drop a boulder
    /// </summary>
    public class GateTrap : Trap
    {
        [SerializeField] private Gate gate = default;
        
        private bool alreadyEntered = false;

        private void OpenGate()
        {
            gate.OpenGate();
        }
        
        public override void Tick()
        {
            if (alreadyEntered) return;

            alreadyEntered = true;
            OpenGate();
        }
    }
}
