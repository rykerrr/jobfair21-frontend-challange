using System.Collections;
using Platformer.JobFair.Gameplay.Args;
using Platformer.JobFair.Mechanics.Items;
using UnityEngine;

namespace Platformer.JobFair.Gameplay
{
    public class UseSpeedPowerup : ArgsSimulationEvent<UseSpeedPowerup>
    {
        private ItemArgs itemArgs;
        
        public override void Execute()
        {
            itemArgs = (ItemArgs) args;
            
            itemArgs.PlrController.StartCoroutine(SpeedUpCoroutine());
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
            var item = (SpeedPowerup)itemArgs.Item;
            var playerController = itemArgs.PlrController;
         
            Debug.Log(item);
            Debug.Log(args);
            Debug.Log(itemArgs);
            Debug.Log(playerController);
            
            var prevSpeed = playerController.MaxSpeed;
            playerController.curMaxSpeed = prevSpeed * item.SpeedMultiplier;

            yield return new WaitForSeconds(item.Duration);

            playerController.curMaxSpeed = prevSpeed;
        }
    }
}