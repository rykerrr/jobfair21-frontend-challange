using System;
using System.Collections;
using System.Collections.Generic;
using Platformer.JobFair.Destruction;
using Platformer.Mechanics;
using UnityEngine;

namespace Platformer
{
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
