using Platformer.JobFair.Mechanics;
using Platformer.JobFair.Mechanics.Collisions;
using UnityEngine;

namespace Platformer.Mechanics
{
    /// <summary>
    /// A simple controller for enemies. Provides movement control over a patrol path.
    /// </summary>
    [RequireComponent(typeof(AnimationController), typeof(Collider2D))]
    public class EnemyController : MonoBehaviour
    {
        [Header("References, default will be loaded from GameObject if null")] [SerializeField]
        private SpriteRenderer spriteRenderer = default;
        [SerializeField] private AnimationController animController = default;
        [SerializeField] private Collider2D collider2d = default;
        [SerializeField] private AudioSource audioSource = default;
        [SerializeField] private AudioContainer audioContainer = default;
        [SerializeField] private PatrolPath path = default;

        public AudioContainer AudioContainer => audioContainer;
        public AnimationController AnimController => animController;
        public Collider2D Collider2d => collider2d;
        public AudioSource AudioSource => audioSource;

        private ICollisionProcessor collisionProcessor;
        
        [Header("Displayed for debugging")]
        [SerializeReference] private PatrolPath.Mover mover;

        private void Awake()
        {
            TryInjectDefaultReferences();
        }

        private void TryInjectDefaultReferences()
        {
            collisionProcessor = GetComponent<ICollisionProcessor>();

            animController ??= GetComponent<AnimationController>();
            collider2d ??= GetComponent<Collider2D>();
            audioSource ??= GetComponent<AudioSource>();
            spriteRenderer ??= GetComponent<SpriteRenderer>();
            audioContainer ??= GetComponent<AudioContainer>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            collisionProcessor.ProcessCollision(collision.gameObject);
        }

        private void Update()
        {
            if (path != null)
            {
                mover ??= path.CreateMover(animController.maxSpeed * 0.5f);

                UpdateMoveVectorViaMover();
            }
        }

        private void UpdateMoveVectorViaMover()
        {
            var newMoveHorizontal = Mathf.Clamp(mover.Position.x - transform.position.x
                , -1, 1);
            
            animController.move.x = newMoveHorizontal;
        }
        
        #region editor methods
        #if UNITY_EDITOR
        [ContextMenu("Reset mover to state it would be if it were instantiated normally")]
        public void ResetMover()
        {
            if (!path) return;
            
            mover = path.CreateMover(animController.maxSpeed * 0.5f);
        }
        #endif
        #endregion
    }
}