using Platformer.Core;

namespace Platformer.JobFair.Gameplay.Args
{
    /// <summary>
    /// Simulation event that contains arguments, inherit from this if you wish to set arguments
    /// Mostly used due to the inheritance, it allows for polymorphism for something like item use events
    /// It also allowed me to generalize UseGun and TurretFired into ProjectileFired
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ArgsSimulationEvent<T> : Simulation.Event<T> where T:Simulation.Event<T>
    {
        public SimulationEventArgs args;
    }
}