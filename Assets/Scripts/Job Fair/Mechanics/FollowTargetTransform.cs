using UnityEngine;

namespace Platformer.JobFair.Mechanics
{
    public class FollowTargetTransform : MonoBehaviour
    {
        [SerializeField] private Transform targetTransform = default;
        [SerializeField] private float smoothingMultiplier = 2f;
        [SerializeField] private float zOffset = -1f;
        
        private void Update()
        {
            TryUpdatePosition();
        }

        private void TryUpdatePosition()
        {
            if (targetTransform == null) return;

            var thisPos = transform.position;
            
            var relative = targetTransform.position - thisPos;

            var newPos = thisPos + relative * (Time.deltaTime * smoothingMultiplier);
            transform.position = new Vector3(newPos.x, newPos.y, zOffset);

            // transform.position = new Vector3(targPos.x, targPos.y, zOffset);
            // position + relative vector between these 2 * timedeltatime * multiplier = simple smoothing?
        }
    }
}
