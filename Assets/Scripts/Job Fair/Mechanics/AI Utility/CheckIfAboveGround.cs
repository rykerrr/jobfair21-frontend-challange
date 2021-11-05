using UnityEngine;

namespace Platformer.JobFair.Mechanics.AiUtility
{
    /// <summary>
    /// Implements a method that only checks if the object is above an object of it's layermask
    /// </summary>
    public class CheckIfAboveGround : MonoBehaviour
    {
        [SerializeField] private LayerMask whatIsGround = default;
        [SerializeField] private float allowedDistance = 1f;

        private void Update()
        {
            var aboveGround = IsAboveGround();

            Debug.Log($"Above Ground: {aboveGround}");

            Debug.DrawRay(transform.position, Vector2.down, 
                aboveGround ? Color.green : Color.red);
        }

        public bool IsAboveGround()
        {
            var hit = Physics2D.Raycast(transform.position, Vector2.down,
                allowedDistance, whatIsGround);

            return hit.collider;
        }
    }
}