using UnityEngine;

namespace Platformer.JobFair.Destruction
{
    public class SimpleDestroy : MonoBehaviour, IDestructionProcessor
    {
        public void Destroy()
        {
            gameObject.Destroy();
        }
    }
}
