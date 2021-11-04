﻿using Platformer.Gameplay;
using Platformer.Job_Fair.Mechanics;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// A simple controller for enemies. Provides movement control over a patrol path.
    /// </summary>
    [RequireComponent(typeof(AnimationController), typeof(Collider2D))]
    public class EnemyController : MonoBehaviour
    {
        [Header("References, default will be loaded from GameObject if null")]
        [SerializeField] private SpriteRenderer spriteRenderer = default;
        [SerializeField] private AnimationController animController = default;
        [SerializeField] private Collider2D collider2d = default;
        [SerializeField] private AudioSource audioSource = default;
        [SerializeField] private AudioContainer audioContainer = default;
        [SerializeField] private PatrolPath path = default;
        
        public AudioContainer AudioContainer => audioContainer;
        public SpriteRenderer SpriteRenderer => spriteRenderer;
        public AnimationController AnimController => animController;
        public Collider2D Collider2d => collider2d;
        public AudioSource AudioSource => audioSource;

        private PatrolPath.Mover mover;
        
        private void Awake()
        {
            TryInjectDefaultReferences();
        }

        private void TryInjectDefaultReferences()
        {
            animController ??= GetComponent<AnimationController>();
            collider2d ??= GetComponent<Collider2D>();
            audioSource ??= GetComponent<AudioSource>();
            spriteRenderer ??= GetComponent<SpriteRenderer>();
            audioContainer ??= GetComponent<AudioContainer>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var player = collision.gameObject.GetComponent<PlayerController>();
            
            if (player != null)
            {
                var ev = Schedule<PlayerEnemyCollision>();
                ev.player = player;
                ev.enemy = this;
            }
        }

        private void Update()
        {
            if (path != null)
            {
                mover ??= path.CreateMover(animController.maxSpeed * 0.5f);

                animController.move.x = Mathf.Clamp(mover.Position.x - transform.position.x, -1, 1);
            }
        }

    }
}