using System.Collections;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Serialization;
using Platformer.JobFair.Mechanics.Items;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Platformer.JobFair.Gameplay
{
    public class UseSpeedPowerup : ItemUseEvent
    {
        public SpeedPowerup data;

        public override void Execute()
        {
            playerController.StartCoroutine(SpeedUpCoroutine());
        }

        /// <summary>
        /// The way this functions is that, the player uses the speed up, this speeds up the player
        /// returns him to normal speed after a given duration
        /// Using multiple should effectively extend the duration but currently will bug out
        /// Player uses one speed up, uses another speed up, first speed up finishes and returns him to
        /// normal speed but the second one is still "up", or well, the coroutine is running
        /// TODO: Fix that above, timer will most likely work
        /// </summary>
        /// <returns></returns>
        private IEnumerator SpeedUpCoroutine()
        {
            var prevSpeed = playerController.MaxSpeed;
            playerController.curMaxSpeed = prevSpeed * data.SpeedMultiplier;

            yield return new WaitForSeconds(data.Duration);

            playerController.curMaxSpeed = prevSpeed;
        }
    }
}