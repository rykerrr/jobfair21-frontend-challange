using System;

namespace Platformer.JobFair.Utility.Timers
{
    /// <summary>
    /// Allows for future addition of other timers such as a Stopwatch/Up timer, other custom timers
    /// without forcing behaviour
    /// </summary>
    public interface ITimer
    {
        public event Action OnTimerEnd;

        public float Time { get; }

        /// <summary>
        /// The bool return type is whether it ticked through successfully or not
        /// E.g: It might not be successful if the timer is disabled or "target" time was reached
        /// </summary>
        /// <param name="deltaTime"></param>
        /// <returns></returns>
        public bool TryTick(float deltaTime);
        
        public void Reset();
    }
}