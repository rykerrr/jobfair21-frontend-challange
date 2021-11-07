using System;
using Platformer.JobFair.Gameplay.Args;
using UnityEngine;

namespace Platformer.JobFair.Mechanics.Weaponry
{
    public class Gun : MonoBehaviour, IPhysicalItem, IFireProjectile, IHasAudio
    {
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
