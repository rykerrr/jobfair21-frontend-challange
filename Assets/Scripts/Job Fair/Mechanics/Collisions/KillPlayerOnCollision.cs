﻿using Platformer.Core;
using Platformer.Gameplay;
using Platformer.Mechanics;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.JobFair.Mechanics.Collisions
{
    public class KillPlayerOnCollision : MonoBehaviour, ICollisionProcessor
    {
        public void ProcessCollision(GameObject other)
        {
            if (other.GetComponent<PlayerController>())
            {
                var plrDeath = Simulation.Schedule<PlayerDeath>();
            }
        }
    }
}