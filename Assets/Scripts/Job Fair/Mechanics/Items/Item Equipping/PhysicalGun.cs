using Platformer.JobFair.Gameplay.Args;
using UnityEngine;

namespace Platformer.JobFair.Mechanics.Items
{
    /// <summary>
    /// Mostly an isntanced data container beyond GetUseData() from IPhysicalItem,
    /// firing happens with the ProjectileFired event
    /// </summary>
    public class PhysicalGun : MonoBehaviour, IPhysicalItem, IFireProjectile, IHasAudio
    {
        [Header("References, default will be loaded from GameObject if null")] 
        [SerializeField] private Projectile projectilePrefab = default;
        [SerializeField] private Transform firePoint = default;
        [SerializeField] private AudioSource audioSource = default;
        [SerializeField] private AudioContainer audioContainer = default;
        
        public Projectile ProjectilePrefab => projectilePrefab;
        public Transform FirePoint => firePoint;
        public AudioSource AudioSource => audioSource;
        public AudioContainer AudioContainer => audioContainer;

        private void Awake()
        {
            TryInjectDefaultReferences();
        }
        
        private void TryInjectDefaultReferences()
        {
            audioSource ??= GetComponent<AudioSource>();
            audioContainer ??= GetComponent<AudioContainer>();
        }

        public SimulationEventArgs GetUseData()
        {
            return new ArgsProjectileFireEvent() {firerAudio = this, projectileFirer = this, owner = gameObject};
        }
    }
}
