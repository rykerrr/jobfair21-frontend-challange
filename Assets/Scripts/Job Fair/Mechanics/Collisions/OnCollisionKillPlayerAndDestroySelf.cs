using System;
using Platformer.JobFair.Destruction;
using UnityEngine;

namespace Platformer.JobFair.Mechanics.Collisions
{
    /// <summary>
    /// Further implementation of ArrowProjectile collision logic, event's need is due to
    /// interface methods not being able to be virtual
    /// </summary>
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