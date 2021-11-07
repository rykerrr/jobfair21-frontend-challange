using Platformer.Core;
using Platformer.JobFair.Mechanics.Items;
using Platformer.Mechanics;
using UnityEngine;

namespace Platformer.JobFair.Gameplay
{
    public class UseGun : ItemUseEvent
    {
        public GunItem data;

        public override void Execute()
        {
            Debug.Log("Pew pew pew!");
            
            throw new System.NotImplementedException();
        }
    }
}