using Platformer.JobFair.Mechanics.Collisions;
using UnityEngine;

namespace Platformer.JobFair.Mechanics
{
    public class ArrowProjectile : MonoBehaviour
    {
        [Header("Preferences")] 
        [SerializeField] private float speed = 20f;

        [Header("References")] 
        [SerializeField] private Rigidbody2D thisRb = default;
        [SerializeField] private SpriteRenderer spriteRenderer = default;
        
        private ICollisionProcessor playerHitCollisionProcessor;

        private void Awake()
        {
            playerHitCollisionProcessor = GetComponent<ICollisionProcessor>();
        }

        public void Init(Vector2 forwardVector)
        {
            transform.right = forwardVector;
            
            thisRb.velocity = forwardVector.normalized * speed * Time.fixedDeltaTime;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            playerHitCollisionProcessor.ProcessCollision(other.gameObject);
        }
    }
}