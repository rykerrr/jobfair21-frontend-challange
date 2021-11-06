using Platformer.JobFair.Mechanics.Collisions;
using UnityEngine;

namespace Platformer.JobFair.Mechanics
{
    /// <summary>
    /// Simple Implementation of a velocity-based arrow projectile
    /// </summary>
    public class ArrowProjectile : MonoBehaviour
    {
        #region fields
        [Header("Preferences")] 
        [SerializeField] private float speed = 20f;

        [Header("References")] 
        [SerializeField] private Rigidbody2D thisRb = default;
        [SerializeField] private SpriteRenderer spriteRenderer = default;
        
        [Header("Set by self or injected, shown for debug")]
        [SerializeField] private GameObject owner = default;
        #endregion

        public GameObject Owner => owner;
        
        private ICollisionProcessor playerHitCollisionProcessor;

        private void Awake()
        {
            playerHitCollisionProcessor = GetComponent<ICollisionProcessor>();
        }

        public void Init(Vector2 forwardVector, GameObject owner)
        {
            this.owner = owner;
            
            transform.right = forwardVector;
            
            thisRb.velocity = forwardVector.normalized * speed * Time.fixedDeltaTime;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            playerHitCollisionProcessor.ProcessCollision(other.gameObject);
        }
    }
}