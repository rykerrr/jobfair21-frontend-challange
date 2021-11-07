using Platformer.JobFair.Mechanics.Collisions;
using UnityEngine;

namespace Platformer.JobFair.Mechanics
{
    /// <summary>
    /// Simple Implementation of a velocity-based arrow projectile
    /// </summary>
    public class Projectile : MonoBehaviour
    {
        #region fields
        [Header("Preferences")] 
        [SerializeField] private float speed = 20f;

        [Header("References")] 
        [SerializeField] private Rigidbody2D thisRb = default;
        [SerializeField] private SpriteRenderer spriteRenderer = default;
        
        [Header("Set by self or injected, shown for debug")]
        [SerializeField] private IFireProjectile projectileSpawner = default;
        #endregion

        public IFireProjectile ProjectileSpawner => projectileSpawner;
        
        private ICollisionProcessor playerHitCollisionProcessor;

        private void Awake()
        {
            playerHitCollisionProcessor = GetComponent<ICollisionProcessor>();
        }
        
        /// <summary>
        /// Projectile spawner mostly used by the pooling system, but can be useful for other purposes too
        /// </summary>
        /// <param name="forwardVector"></param>
        /// <param name="projectileSpawner"></param>
        public void Init(Vector2 forwardVector, IFireProjectile projectileSpawner)
        {
            this.projectileSpawner = projectileSpawner;
            
            transform.right = forwardVector;
            
            thisRb.velocity = forwardVector.normalized * speed * Time.fixedDeltaTime;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            playerHitCollisionProcessor.ProcessCollision(other.gameObject);
        }
    }
}