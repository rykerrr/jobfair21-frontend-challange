using Platformer.JobFair.Mechanics.Weaponry.ProjectileCreation;
using UnityEngine;

namespace Platformer
{
    /// <summary>
    /// Safe to delete, just used it to quickly tested the pooling system as it was quite buggy in the beginning
    /// When an instance of a prefab is created, ALL the references pointing to that prefab point to the now new instance
    /// What this means is that you can't have any references to the prefab unless you set them during runtime, e.g once you create the object
    /// The implication of this means that a prefab-based pooling system would be interesting to implement simply due to the fact
    /// you can't have a reference to the prefab unless you set it
    /// </summary>
    public class TestScript : MonoBehaviour
    {
        [SerializeField] private GameObjectPoolManager poolManager = default;
        [SerializeField] private GameObject prefab = default;
        [SerializeField] private GameObject objToPool = default;
        
        [ContextMenu("Try Pool Object")]
        public void TryPoolObject()
        {
            poolManager.ReturnToPool(prefab, objToPool);
        }
    }
}
