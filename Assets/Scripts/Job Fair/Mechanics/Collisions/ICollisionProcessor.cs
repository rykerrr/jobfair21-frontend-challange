using System;
using UnityEngine;

namespace Platformer.JobFair.Mechanics.Collisions
{
    /// <summary>
    /// Allows for usage of SOLID's Open-Closed principle with entities that use OnTrigger/OnCollision unity callbacks
    /// Also allows for a centralized place for these callbacks instead of copying code from OnTrigger into OnCollision
    /// each time
    /// </summary>
    public interface ICollisionProcessor
    {
        event Action onCollisionSuccess;
        
        void ProcessCollision(GameObject other);
    }
}