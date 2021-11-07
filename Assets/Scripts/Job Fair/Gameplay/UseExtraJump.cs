using Platformer.JobFair.Mechanics.Items;

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