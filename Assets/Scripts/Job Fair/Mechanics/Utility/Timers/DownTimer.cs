using System;
using UnityEngine;

namespace Platformer.JobFair.Utility.Timers
{
    public class DownTimer : ITimer
    {
        public event Action OnTimerEnd = delegate { };

        private float time;
        private float defaultTime;

        public float DefaultTime => defaultTime;
        public float Time => time;

        public DownTimer(float defaultTime = 1f)
        {
            SetDefaultTime(defaultTime, true);
        }
        
        public void SetDefaultTime(float newTime, bool reset = false)
        {
            defaultTime = newTime;

            if (reset) Reset();
        }
        
        public void Reset()
        {
            time = defaultTime;
        }
        
        /// <summary>
        /// True for ticked through, false if not able to tick
        /// </summary>
        /// <param name="deltaTime"></param>
        /// <returns></returns>
        public bool TryTick(float deltaTime)
        {
            if (time <= 0) return false;
            
            time = Mathf.Clamp(time - deltaTime, 0f, defaultTime);

            if (time - Mathf.Epsilon <= 0) OnTimerEnd?.Invoke();

            return true;
        }
    }
}