using System;
using System.Collections;
using System.Collections.Generic;
using Platformer.Mechanics;
using UnityEngine;

namespace Platformer
{
    /// <summary>
    /// Implements a method that only checks if the object is next to an object of it's layermask
    /// </summary>
    public class CheckIfHuggingWall : MonoBehaviour
    {
        [SerializeField] private LayerMask whatIsWall = default;
        [SerializeField] private float maxDistanceToWall = 0.2f;

        [SerializeField] private KinematicObject kinematicObject = default;
        
        private void Update()
        {
            var forwardVector = kinematicObject.velocity.normalized;
            var huggingWall = IsHuggingWall(forwardVector);
            
            Debug.Log($"Hugging wall: {huggingWall}");
            
            Debug.DrawRay(transform.position, forwardVector, 
                huggingWall ? Color.red : Color.green);
        }

        public bool IsHuggingWall(Vector2 forwardVector)
        {
            var hit = Physics2D.Raycast(transform.position, forwardVector,
                maxDistanceToWall, whatIsWall);
            
            return hit.collider;
        }
    }
}
