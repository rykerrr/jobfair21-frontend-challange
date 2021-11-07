using Platformer.JobFair.Mechanics;
using UnityEngine;

namespace Platformer
{
    /// <summary>
    /// Interface for usage of the generalized ProjectileFired event 
    /// </summary>
    public interface IFireProjectile
    {
        public Projectile ProjectilePrefab { get; }
        public Transform FirePoint { get; }
    }
}
