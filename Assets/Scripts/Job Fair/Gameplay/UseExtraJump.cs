using Platformer.Core;
using Platformer.JobFair.Mechanics.Items;
using Platformer.Mechanics;

namespace Platformer.JobFair.Gameplay
{
    public class UseExtraJump : ItemUseEvent
    {
        public ExtraJump data;
        
        public override void Execute()
        {
            playerController.Bounce(data.JumpVelocity);
        }
    }
}