using Platformer.JobFair.Gameplay.Args;
using Platformer.JobFair.Mechanics.Items;

namespace Platformer.JobFair.Gameplay
{
    public class UseExtraJump : ArgsSimulationEvent<UseExtraJump>
    {
        public ExtraJump data;
        
        public override void Execute()
        {
            var itemArgs = (ArgsItem) args;
            
            itemArgs.PlrController.Bounce(((ExtraJump)(itemArgs.Item)).JumpVelocity);
        }
    }
}