using Platformer.JobFair.Gameplay.Args;

namespace Platformer
{
    /// <summary>
    /// Created to be used with SimulationEventArgs, the implementation simply needs to return the event args
    /// that would be used for the given event. The given event (for using the item) casts the args to what it can use
    /// </summary>
    public interface IPhysicalItem
    {
        SimulationEventArgs GetUseData();
    }
}