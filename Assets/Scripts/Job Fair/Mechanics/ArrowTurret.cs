using Platformer.Core;
using Platformer.JobFair.Utility.Timers;
using Platformer.JobFair.Gameplay;
using UnityEngine;

namespace Platformer.JobFair.Mechanics
{
    /// <summary>
    /// Entity with the responsibility of firing an arrow
    /// Mostly based on the Idea of minecraft's dispensers
    /// </summary>
    public class ArrowTurret : MonoBehaviour
    {
        #region fields
        [Header("Preferences")]
        [SerializeField] private float delayPerFire;

        [Header("References")] 
        [SerializeField] private Transform firePoint = default;
        [SerializeField] private ArrowProjectile arrowPrefab = default;
        
        // Audio
        [SerializeField] private AudioSource audioSource = default;
        [SerializeField] private AudioContainer audioContainer = default;
        #endregion
        
        public Transform FirePoint => firePoint;
        public ArrowProjectile ArrowPrefab => arrowPrefab;
        public AudioSource AudioSource => audioSource;
        public AudioContainer AudioContainer => audioContainer;
        
        private ITimer firingTimer;

        /// <summary>
        /// Returns whether the turret can fire or not, this is set by a timer
        /// </summary>
        public bool CanFire { get; private set; } = true;


        private void Awake()
        {
            InitTimer();
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
            CreateAndInitArrow();
        }

        /// <summary>
        /// Set tool handle rotation to local to see the firePoint's right vector (red arrow)
        /// </summary>
        private void CreateAndInitArrow()
        {
            var ev = Simulation.Schedule<ArrowFired>();
            ev.turret = this;
            
            firingTimer.Reset();
        }
    }
}
