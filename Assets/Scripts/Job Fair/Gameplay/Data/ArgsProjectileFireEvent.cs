using UnityEngine;

namespace Platformer.JobFair.Gameplay.Args
{
    public class ArgsProjectileFireEvent : SimulationEventArgs
    {
        public IFireProjectile projectileFirer;
        public IHasAudio firerAudio;
        public GameObject owner { get; set; }
    }
}