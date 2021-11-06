using System;
using Platformer.JobFair.Destruction;
using UnityEngine;

namespace Platformer.JobFair.Mechanics.Collisions
{
    public class OnCollisionKillPlayerAndDestroySelf : OnCollisionKillPlayer
    {
        private IDestructionProcessor destructionProcessor;

        private void Awake()
        {
            destructionProcessor = GetComponent<IDestructionProcessor>();

            onCollisionSuccess += destructionProcessor.Destroy;
        }
    }
}