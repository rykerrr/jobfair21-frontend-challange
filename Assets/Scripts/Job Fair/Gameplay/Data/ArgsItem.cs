using Platformer.JobFair.Mechanics.Items;
using Platformer.Mechanics;

namespace Platformer.JobFair.Gameplay.Args
{
    public class ArgsItem : SimulationEventArgs
    {
        public PlayerController PlrController { get; set; }
        public Item Item { get; set; }
    }
}