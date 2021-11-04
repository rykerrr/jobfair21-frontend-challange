using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;
using Platformer.Job_Fair.Mechanics;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This is the main class used to implement control of the player.
    /// It is a superset of the AnimationController class, but is inlined to allow for any kind of customisation.
    /// </summary>
    public class PlayerController : KinematicObject
    {
        [Header("References, default will be loaded from GameObject if null")]
        [SerializeField] private AudioContainer audioContainer = default;
        [SerializeField] private Health health;
        [SerializeField] private Collider2D collider2d;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private Animator animator;

        public AudioContainer AudioContainer => audioContainer;
        public Health Health => health;
        public AudioSource AudioSource => audioSource;
        public Collider2D Collider2d => collider2d;
        public Animator Animator => animator;


        /// <summary>
        /// Max horizontal speed of the player.
        /// </summary>
        [Header("Preferences")]
        public float maxSpeed = 7;
        /// <summary>
        /// Initial jump velocity at the start of a jump.
        /// </summary>
        public float jumpTakeOffSpeed = 7;

        public bool controlEnabled = true;

        private readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        private static readonly int Grounded = Animator.StringToHash("grounded");
        private static readonly int VelocityX = Animator.StringToHash("velocityX");

        private SpriteRenderer spriteRenderer;
        public JumpState jumpState = JumpState.Grounded;
        private Vector2 move;
        private bool stopJump;
        private bool jump;

        private void Awake()
        {
            TryInjectDefaultReferences();
        }

        private void TryInjectDefaultReferences()
        {
            health ??= GetComponent<Health>();
            audioSource ??= GetComponent<AudioSource>();
            collider2d ??= GetComponent<Collider2D>();
            spriteRenderer ??= GetComponent<SpriteRenderer>();
            animator ??= GetComponent<Animator>();
            audioContainer ??= GetComponent<AudioContainer>();
        }

        protected override void Update()
        {
            if (controlEnabled)
            {
                // Input
                move.x = Input.GetAxis("Horizontal");
                if (jumpState == JumpState.Grounded && Input.GetButtonDown("Jump"))
                    jumpState = JumpState.PrepareToJump;
                else if (Input.GetButtonUp("Jump"))
                {
                    stopJump = true;
                    Schedule<PlayerStopJump>().player = this;
                }
            }
            else
            {
                move.x = 0;
            }
            
            // State control 
            UpdateJumpState();
            
            base.Update();
        }

        private void UpdateJumpState()
        {
            // State control
            jump = false;
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    jumpState = JumpState.Jumping;
                    jump = true;
                    stopJump = false;
                    break;
                case JumpState.Jumping:
                    if (!IsGrounded)
                    {
                        Schedule<PlayerJumped>().player = this;
                        jumpState = JumpState.InFlight;
                    }
                    break;
                case JumpState.InFlight:
                    if (IsGrounded)
                    {
                        Schedule<PlayerLanded>().player = this;
                        jumpState = JumpState.Landed;
                    }
                    break;
                case JumpState.Landed:
                    jumpState = JumpState.Grounded;
                    break;
            }
        }

        protected override void ComputeVelocity()
        {
            // State control
            if (jump && IsGrounded)
            {
                velocity.y = jumpTakeOffSpeed * model.jumpModifier;
                jump = false;
            }
            else if (stopJump)
            {
                stopJump = false;
                if (velocity.y > 0)
                {
                    velocity.y *= model.jumpDeceleration;
                }
            }

            // Graphics
            if (move.x > 0.01f)
                spriteRenderer.flipX = false;
            else if (move.x < -0.01f)
                spriteRenderer.flipX = true;

            animator.SetBool(Grounded, IsGrounded);
            animator.SetFloat(VelocityX, Mathf.Abs(velocity.x) / maxSpeed);

            // State control
            targetVelocity = move * maxSpeed;
        }
        
        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed
        }
    }
}