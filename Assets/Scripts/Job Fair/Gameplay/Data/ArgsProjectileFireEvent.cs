using UnityEngine;

namespace Platformer.JobFair.Gameplay.Args
{
    /// <summary>
    /// Args for a ProjectileFired event, the "firer" is effectively the object that began the projectile spawn
    /// So a turret, a gun, etc
    /// </summary>
    public class ArgsProjectileFireEvent : SimulationEventArgs
    {
        public IFireProjectile projectileFirer;
        public IHasAudio firerAudio;
        public GameObject owner { get; set; }
    }
}