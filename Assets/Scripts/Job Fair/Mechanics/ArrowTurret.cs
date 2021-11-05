using Platformer.JobFair.Utility.Timers;
using UnityEngine;

namespace Platformer.JobFair.Mechanics
{
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
        
        private ITimer timer;

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
            timer = new DownTimer(delayPerFire);
            timer.OnTimerEnd += () => CanFire = true;
        }

        private void Update()
        {
            timer.TryTick(Time.deltaTime);
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
            var hasWhoosh = audioContainer.TryGetClip("crossbow_arrow_whoosh", out var whooshClip);
            if(hasWhoosh) audioSource.PlayOneShot(whooshClip);
            
            var arrowClone = Instantiate(arrowPrefab, firePoint.position, Quaternion.identity);

            arrowClone.Init(firePoint.right);
        }
    }
}
