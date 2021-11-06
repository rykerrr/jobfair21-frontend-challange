using Platformer.JobFair.Destruction;
using Platformer.Mechanics;
using UnityEngine;

namespace Platformer
{
    /// <summary>
    /// Delegation of token's removal logic
    /// </summary>
    public class OnDestroyDisableToken : MonoBehaviour, IDestructionProcessor
    {
        private TokenInstance tokenInstance;

        private void Awake()
        {
            tokenInstance = GetComponent<TokenInstance>();
        }

        public void Destroy()
        {
            tokenInstance.frame = 0;
            tokenInstance.sprites = tokenInstance.collectedAnimation;
        }
    }
}
