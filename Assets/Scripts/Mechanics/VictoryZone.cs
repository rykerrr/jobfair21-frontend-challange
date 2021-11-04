using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// Marks a trigger as a VictoryZone, usually used to end the current game level.
    /// </summary>
    public class VictoryZone : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collider)
        {
            var plr = collider.gameObject.GetComponent<PlayerController>();
            
            if (plr != null)
            {
                var ev = Schedule<PlayerEnteredVictoryZone>();
                ev.victoryZone = this;
            }
        }
    }
}