using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.JobFair.Mechanics.Traps
{
    public class PlayerEnteredTrapTrigger : MonoBehaviour
    {
        [SerializeField] private List<Trap> traps = default;

        private void TickTraps()
        {
            foreach (var trap in traps)
            {
                trap.Tick();
            }
        }
        
        private void OnTriggerStay2D(Collider2D other)
        {
            TickTraps();
        }
    }
}
