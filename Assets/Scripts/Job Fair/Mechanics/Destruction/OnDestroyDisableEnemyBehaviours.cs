using System.Collections;
using System.Collections.Generic;
using Platformer.JobFair.Destruction;
using UnityEngine;

namespace Platformer
{
    /// <summary>
    /// Delegation of enemy's removal logic
    /// </summary>
    public class OnDestroyDisableEnemyBehaviours : MonoBehaviour, IDestructionProcessor
    {
        [SerializeField] private List<Behaviour> behavioursToDisable = default;
        
        public void Destroy()
        {
            DisableComponents();
        }

        public void DisableComponents()
        {
            foreach (var behaviour in behavioursToDisable)
            {
                behaviour.enabled = false;
            }
        }
    }
}
