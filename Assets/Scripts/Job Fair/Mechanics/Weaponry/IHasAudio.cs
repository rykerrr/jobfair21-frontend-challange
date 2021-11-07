using System.Collections;
using System.Collections.Generic;
using Platformer.JobFair.Mechanics;
using UnityEngine;

namespace Platformer
{
    /// <summary>
    /// Interface for usage of the generalized ProjectileFired event
    /// Can be used for other purposes too, especially for generalizing other events that use audio
    /// </summary>
    public interface IHasAudio 
    {
        AudioSource AudioSource { get; }
        AudioContainer AudioContainer { get; }
    }
}
