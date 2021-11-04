using Platformer.Gameplay;
using Platformer.JobFair.Mechanics;
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

        [Header("Preferences")]
        [Tooltip("If true, animation will start at a random position in the sequence.")]
        [SerializeField] private bool randomAnimationStartTime = false;
        [Tooltip("List of frames that make up the animation.")]
        [SerializeField] private Sprite[] idleAnimation, collectedAnimation;

        //active frame in animation, updated by the controller.
        public int frame = 0;
        
        //unique index which is assigned by the TokenController in a scene.
        public int tokenIndex = -1;
        public TokenController controller;
        
        internal Sprite[] sprites = new Sprite[0];
        private SpriteRenderer spriteRenderer;
        private bool collected = false;

        public AudioContainer AudioContainer => audioContainer;
        public SpriteRenderer SpriteRenderer => spriteRenderer;
        public bool Collected => collected;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            if (randomAnimationStartTime)
                frame = Random.Range(0, sprites.Length);
            sprites = idleAnimation;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            //only exectue OnPlayerEnter if the player collides with this token.
            var player = other.gameObject.GetComponent<PlayerController>();
            if (player != null) OnPlayerEnter(player);
        }

        private void OnPlayerEnter(PlayerController player)
        {
            if (collected) return;
            //disable the gameObject and remove it from the controller update list.
            frame = 0;
            sprites = collectedAnimation;
            if (controller != null)
                collected = true;
            //send an event into the gameplay system to perform some behaviour.
            var ev = Schedule<PlayerTokenCollision>();
            ev.token = this;
            ev.player = player;
        }
        
        #if UNITY_EDITOR
        [ContextMenu("Grab reference")]
        private void Grab()
        {
            audioContainer = GetComponent<AudioContainer>();
        }
        #endif
    }
}