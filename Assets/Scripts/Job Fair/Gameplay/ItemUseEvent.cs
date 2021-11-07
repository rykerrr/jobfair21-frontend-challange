using Platformer.Core;
using Platformer.Mechanics;

namespace Platformer.JobFair.Gameplay
{
    /// <summary>
    /// Class's main purpose is to allow these events to be returned from ScriptableObject methods
    /// Thus essentially keeping bare-bones logic from scriptable objects and keeping them as data
    /// containers but with a bit more purpose
    /// </summary>
    public abstract class ItemUseEvent : Simulation.Event<ItemUseEvent>
    {
        public PlayerController playerController;
    }
}