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

        public bool TryTick(float deltaTime);
        
        public void Reset();
    }
}