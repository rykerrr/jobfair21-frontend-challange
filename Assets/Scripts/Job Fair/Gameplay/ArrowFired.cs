﻿using Platformer.Core;
using Platformer.JobFair.Mechanics;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;

namespace Platformer.JobFair.Gameplay
{
    public class ArrowFired : Simulation.Event<ArrowFired>
    {
        public ArrowTurret turret;

        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            var hasWhoosh = turret.AudioContainer.TryGetClip("crossbow_arrow_whoosh", out var whooshClip);
            if (turret.AudioSource && hasWhoosh) turret.AudioSource.PlayOneShot(whooshClip);

            var arrowClone = GameController.Instance.creationHandler.CreateProjectile(
                turret.ArrowPrefab.gameObject, turret.FirePoint.position, Quaternion.identity);

            arrowClone.GetComponent<ArrowProjectile>().Init(turret.FirePoint.right);
        }
    }
}