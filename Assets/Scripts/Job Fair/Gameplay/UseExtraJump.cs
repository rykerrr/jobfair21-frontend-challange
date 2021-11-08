using Platformer.JobFair.Gameplay.Args;
using Platformer.JobFair.Mechanics.Items;

namespace Platformer.JobFair.Gameplay
{
    public class UseExtraJump : ArgsSimulationEvent<UseExtraJump>
    {
        /// <summary>
        /// Would be extremely easy to Implement audio, but the bare functionality just uses the Bounce method
        /// to propell the player "further"
        /// Kind of more as a jetpack/rocket boots-type-of-thing as opposed to an extra jump, but it does the job
        /// </summary>
        public override void Execute()
        {
            var itemArgs = (ArgsItem) args;
            
            itemArgs.PlrController.Bounce(((ExtraJump)(itemArgs.Item)).JumpVelocity);
        }
    }
}