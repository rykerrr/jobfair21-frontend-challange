using Platformer.JobFair.Mechanics.Items;
using Platformer.Mechanics;

namespace Platformer.JobFair.Gameplay.Args
{
    /// <summary>
    /// Args for items, used in their respective ItemUse events
    /// </summary>
    public class ArgsItem : SimulationEventArgs
    {
        public PlayerController PlrController { get; set; }
        public Item Item { get; set; }
    }
}