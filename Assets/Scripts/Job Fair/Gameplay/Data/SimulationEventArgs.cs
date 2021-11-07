using System;
using Platformer.Core;
using UnityEngine;

namespace Platformer.JobFair.Gameplay.Args
{
    /// <summary>
    /// Pretty much based on the idea of C#'s EventArgs to allow for "polymorphic" args usage in simulation events
    /// By polymorphic I mean, you can have a data container that inherits from this, then use that data container inside of an
    /// simulation args event without explicitly stating it, you only cast it in the behaviour
    /// Meaning if you wanted to create something like weapons, you could have different args for each of them, but besides the creation of those args
    /// and the casting in the Simulation Event's logic, everything else would treat it as SimulationEventArgs
    /// </summary>
    public class SimulationEventArgs
    {
        public static SimulationEventArgs Empty => new SimulationEventArgs();
    }
}