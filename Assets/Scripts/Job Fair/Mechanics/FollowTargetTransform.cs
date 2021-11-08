using UnityEngine;

namespace Platformer.JobFair.Mechanics
{
    /// <summary>
    /// Used for the Physical Item Container, the container itself "smoothly" follows the player
    /// Wall detection and other movement using raycasting could be added here too, in fact you could make a relatively
    /// interesting system here, but this is an extremely simple implementation
    /// </summary>
    public class FollowTargetTransform : MonoBehaviour
    {
        [Header("Preferences")]
        [SerializeField] private Transform targetTransform = default;
        [SerializeField] private float smoothingMultiplier = 2f;
        [SerializeField] private float zOffset = -1f;
        
        private void Update()
        {
            TryUpdatePosition();
        }
        
        /// <summary>
        /// Attempts to update the position based off of the target transform given that there is a target transform
        /// </summary>
        private void TryUpdatePosition()
        {
            if (targetTransform == null) return;

            var thisPos = transform.position;
            
            var relative = targetTransform.position - thisPos;

            // You can swap this out for any smoothing function, I just thought It'd be interesting as i've always done
            // it with something like SmoothDamp, and never Implemented it like this myself
            var newPos = thisPos + relative * (Time.deltaTime * smoothingMultiplier);
            transform.position = new Vector3(newPos.x, newPos.y, zOffset);

            // transform.position = new Vector3(targPos.x, targPos.y, zOffset);
            // position + relative vector between these 2 * timedeltatime * multiplier = simple smoothing?
        }
    }
}
