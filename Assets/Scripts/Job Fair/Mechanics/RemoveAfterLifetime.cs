using Platformer.JobFair.Destruction;
using Platformer.JobFair.Utility.Timers;
using UnityEngine;

namespace Platformer.JobFair.Mechanics
{
    public class RemoveAfterLifetime : MonoBehaviour
    {
        [SerializeField] private float lifetime = 3f;
        
        private IDestructionProcessor destructionProcessor;

        private ITimer lifeTimer;

        private void Awake()
        {
            destructionProcessor = GetComponent<IDestructionProcessor>();
        }

        private void OnEnable()
        {
            lifeTimer ??= new DownTimer(lifetime);
            
            lifeTimer.Reset();

            lifeTimer.OnTimerEnd += destructionProcessor.Destroy;
        }

        private void Update()
        {
            lifeTimer.TryTick(Time.deltaTime);
        }

        private void OnDisable()
        {
            if (lifeTimer != null)
            {
                lifeTimer.OnTimerEnd -= destructionProcessor.Destroy;
            }
        }
    }
}
