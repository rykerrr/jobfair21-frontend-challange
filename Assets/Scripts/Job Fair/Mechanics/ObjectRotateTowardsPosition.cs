using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class ObjectRotateTowardsPosition : MonoBehaviour
    {
        public Vector2 TargetVector { get; private set; }

        public void ChangeTargetVector(Vector2 newTarget)
        {
            TargetVector = newTarget;
        }
        
        private void FixedUpdate()
        {
            var relative = TargetVector - (Vector2)transform.position;

            transform.right = relative;
        }
        
        
        #region editor methods        
#if UNITY_EDITOR

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            
            Gizmos.DrawLine(transform.position, TargetVector);
            Gizmos.DrawWireSphere(TargetVector, 1f);
        }
#endif
        #endregion
    }
}
