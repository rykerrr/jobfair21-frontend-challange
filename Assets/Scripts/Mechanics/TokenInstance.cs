using Platformer.Gameplay;
using Platformer.JobFair.Mechanics;
using Platformer.JobFair.Mechanics.Collisions;
using UnityEngine;
using static Platformer.Core.Simulation;


namespace Platformer.Mechanics
{
    /// <summary>
    /// This class contains the data required for implementing token collection mechanics.
    /// It does not perform animation of the token, this is handled in a batch by the 
    /// TokenController in the scene.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class TokenInstance : MonoBehaviour
    {
        [Header("References, default will be loaded from GameObject if null")]
        [SerializeField] private AudioContainer audioContainer = default;
        [SerializeField] private SpriteRenderer spriteRenderer;

        [Header("Preferences")]
        [Tooltip("If true, animation will start at a random position in the sequence.")]
        [SerializeField] private bool randomAnimationStartTime = false;
        [Tooltip("List of frames that make up the animation.")]
        public Sprite[] idleAnimation, collectedAnimation;

        [Header("Updated by the controller")]
        //active frame in animation, updated by the controller.
        public int frame = 0;
        
        //unique index which is assigned by the TokenController in a scene.
        private TokenController tokenController = default;
        private ICollisionProcessor collisionProcessor = default;
        private int tokenIndex = -1;

        internal Sprite[] sprites = new Sprite[0];
        private bool collected = false;

        public TokenController TokenController => tokenController;
        public AudioContainer AudioContainer => audioContainer;
        public SpriteRenderer SpriteRenderer => spriteRenderer;
        
        public bool Collected
        {
            get => collected;
            set => collected = value;
        }

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            collisionProcessor = GetComponent<ICollisionProcessor>();
            
            if (randomAnimationStartTime)
                frame = Random.Range(0, sprites.Length);
            
            sprites = idleAnimation;
        }

        public void InitToken(TokenController tokenController, int tokenIndex)
        {
            this.tokenController = tokenController;
            this.tokenIndex = tokenIndex;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            collisionProcessor.ProcessCollision(other.gameObject);
        }

#if UNITY_EDITOR
        /// <summary>
        /// Token instance wasn't a prefab and there's a 100 tokens meaning either a 100 reference sets or this
        /// </summary>
        [ContextMenu("Grab audio container reference")]
        private void Grab()
        {
            audioContainer = GetComponent<AudioContainer>();
        }
#endif
    }
}