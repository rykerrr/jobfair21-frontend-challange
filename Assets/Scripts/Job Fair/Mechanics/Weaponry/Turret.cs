using Platformer.Core;
using Platformer.JobFair.Utility.Timers;
using Platformer.JobFair.Gameplay;
using Platformer.JobFair.Gameplay.Args;
using UnityEngine;

namespace Platformer.JobFair.Mechanics.Weaponry
{
    /// <summary>
    /// Entity with the responsibility of firing a projectile
    /// Mostly based on the Idea of minecraft's dispensers
    /// Created from previous ArrowTurret by generalizing it and the ArrowFired event into a ProjectileFired event
    /// </summary>
    public class Turret : MonoBehaviour, IFireProjectile, IHasAudio
    {
        #region fields
        [Header("Preferences")]
        [SerializeField] private float delayPerFire;

        [Header("References")] 
        [SerializeField] private Transform firePoint = default;
        [SerializeField] private Projectile projectilePrefab = default;
        
        // Audio
        [SerializeField] private AudioSource audioSource = default;
        [SerializeField] private AudioContainer audioContainer = default;
        #endregion
        
        public Transform FirePoint => firePoint;
        public Projectile ProjectilePrefab => projectilePrefab;
        public AudioSource AudioSource => audioSource;
        public AudioContainer AudioContainer => audioContainer;
        
        private ITimer firingTimer;
        private ArgsProjectileFireEvent args;

        /// <summary>
        /// Returns whether the turret can fire or not, this is set by a timer
        /// </summary>
        public bool CanFire { get; private set; } = true;


        private void Awake()
        {
            InitTimer();
            TryInjectDefaultReferences();
            
            // Caching it, got the idea from caching WaitForXTime in coroutine, we're passing a reference of whose fields are only references that need to be set once
            // no need to re-instance it each time
            args = new ArgsProjectileFireEvent()
            {
                projectileFirer = this, firerAudio = this, owner = gameObject
            };
        }

        private void TryInjectDefaultReferences()
        {
            audioSource ??= GetComponent<AudioSource>();
            audioContainer ??= GetComponent<AudioContainer>();
        }

        private void InitTimer()
        {
            firingTimer = new DownTimer(delayPerFire);
            
            firingTimer.OnTimerEnd += () =>
            {
                // Debug.Log("Can fire set to true!");

                CanFire = true;
            };
        }

        private void Update()
        {
            // Debug.Log($"Ticked through: {firingTimer.TryTick(Time.deltaTime)}, CanFire: {CanFire}");

            firingTimer.TryTick(Time.deltaTime);
        }
        
        public void Fire()
        {
            if (!CanFire) return;

            CanFire = false;
            CreateAndInitProjectile();
        }

        /// <summary>
        /// Set tool handle rotation to local to see the firePoint's right vector (red arrow)
        /// </summary>
        private void CreateAndInitProjectile()
        {
            var ev = Simulation.Schedule<ProjectileFired>();

            ev.args = args;
            
            firingTimer.Reset();
        }
    }
}
