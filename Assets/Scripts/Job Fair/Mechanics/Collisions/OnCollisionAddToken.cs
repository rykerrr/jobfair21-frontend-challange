using System;
using Platformer.Mechanics;
using UnityEngine;
using Platformer.Gameplay;
using Platformer.JobFair.Destruction;
using static Platformer.Core.Simulation;

namespace Platformer.JobFair.Mechanics.Collisions
{
    /// <summary>
    /// Token removal logic delegated to an ICollisionProcessor from TokenInstance
    /// </summary>
    public class OnCollisionAddToken : MonoBehaviour, ICollisionProcessor
    {
        private TokenInstance tokenInstance;

        public event Action onCollisionSuccess;

        private void Awake()
        {
            tokenInstance = GetComponent<TokenInstance>();
        }

        public void ProcessCollision(GameObject other)
        {
            var player = other.gameObject.GetComponent<PlayerController>();
            if (player == null) return;
            if (tokenInstance.Collected) return;
            
            // Delegated logic for:
            //disable the gameObject and remove it from the controller update list.
            tokenInstance.GetComponent<IDestructionProcessor>().Destroy();

            if (tokenInstance.TokenController != null)
                tokenInstance.Collected = true;
            
            //send an event into the gameplay system to perform some behaviour.
            var ev = Schedule<PlayerTokenCollision>();
            ev.token = tokenInstance;
            ev.player = player;
            
            onCollisionSuccess?.Invoke();
        }
    }
}