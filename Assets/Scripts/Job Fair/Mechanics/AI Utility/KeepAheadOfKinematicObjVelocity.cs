using Platformer.Mechanics;
using UnityEngine;

namespace Platformer.JobFair.Mechanics.AiUtility
{
    /// <summary>
    /// Extremely simple component to keep a GameObject in front of a Rigidbody using the the
    /// rb's velocity
    /// </summary>
    public class KeepAheadOfKinematicObjVelocity : MonoBehaviour
    {
        [SerializeField] private float forwardMultiplier = 1f;
        
        /// <summary>
        /// Velocity gets multiplied by this, if you want to keep it forwards only in x,
        /// or y, or both (1,0) ; (0,1) ; (1,1)
        /// </summary>
        [SerializeField] private Vector2 multiplierMask = new Vector2(1, 0);
        
        [SerializeField] private KinematicObject kinematic = default;

        private void Update()
        {
            SetPositionWithGivenVelocity();
        }

        private void SetPositionWithGivenVelocity()
        {
            transform.position =
                kinematic.transform.position + (Vector3) (kinematic.velocity * multiplierMask) * forwardMultiplier;
        }
    }
}
