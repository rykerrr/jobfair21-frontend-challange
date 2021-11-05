using System;
using UnityEngine;

namespace Platformer.Mechanics
{
    public partial class PatrolPath
    {
        /// <summary>
        /// The Mover class oscillates between start and end points of a path at a defined speed.
        /// </summary>
        [Serializable]
        public class Mover
        {
            [SerializeReference] PatrolPath path;
            [SerializeField] private float p = 0;
            [SerializeField] private float duration;
            [SerializeField] private float startTime;
            
            [SerializeField] private Vector2 nonTransformed;
            [SerializeField] private Vector2 transformed;
            
            public Mover(PatrolPath path, float speed)
            {
                this.path = path;
                
                // Bill travels from A to B with a constant speed of speed
                // The time it takes for bill to travel is (distance(A, B)/speed)
                this.duration = (path.endPosition - path.startPosition).magnitude / speed;
                
                this.startTime = Time.time;
            }

            /// <summary>
            /// Get the position of the mover for the current frame.
            /// </summary>
            /// <value></value>
            public Vector2 Position => ReturnPositionOnPathAsGlobal();

            private Vector2 ReturnPositionOnPathAsGlobal()
            {
                // PingPong is a function that returns a y value on a given "graph" for a given t (in this case, Time.time - startTime)
                // the length (duration in this case) is the height/max value of y in the "graph", the parameter we pass is the duration
                // it takes for an enemy to go from point a to b in it's given path, the function in of itself is also repeating
                var pingPongValue = Mathf.PingPong(Time.time - startTime, duration);
                // pingPongValue will be between 0 and duration

                // Lerp returns a value between A and B based on T, InverseLerp returns T based on a value between A and B
                p = Mathf.InverseLerp(0, duration, pingPongValue);

                var lerpPosition = Vector2.Lerp(path.startPosition, path.endPosition, p);

                // Transforms it to a global position since Path's Start and End positions are local
                // to the path (the path's transform is the origin to them)
                return path.transform.TransformPoint(lerpPosition);
            }
        }
    }
}