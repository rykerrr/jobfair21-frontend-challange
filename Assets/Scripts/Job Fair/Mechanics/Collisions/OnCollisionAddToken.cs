using Platformer.Mechanics;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;

namespace Platformer.JobFair.Mechanics.Collisions
{
    public class OnCollisionAddToken : MonoBehaviour, ICollisionProcessor
    {
        [SerializeField] private TokenInstance tokenInstance;

        public void SetTokenInstance(TokenInstance tokenInstance)
        {
            this.tokenInstance = tokenInstance;
        }
        
        public void ProcessCollision(GameObject other)
        {
            var player = other.gameObject.GetComponent<PlayerController>();
            if (player == null) return;
            if (tokenInstance.Collected) return;
            
            //disable the gameObject and remove it from the controller update list.
            tokenInstance.frame = 0;
            tokenInstance.sprites = tokenInstance.collectedAnimation;
            
            if (tokenInstance.TokenController != null)
                tokenInstance.Collected = true;
            
            //send an event into the gameplay system to perform some behaviour.
            var ev = Schedule<PlayerTokenCollision>();
            ev.token = tokenInstance;
            ev.player = player;
        }
        
#if UNITY_EDITOR
        /// <summary>
        /// Same problem as with token instance...token instance wasn't a prefab and there's a 100 tokens
        /// meaning either a 100 reference sets or this
        /// </summary>
        [ContextMenu("Grab token instance reference")]
        private void Grab()
        {
            tokenInstance = GetComponent<TokenInstance>();
        }
#endif
    }
}