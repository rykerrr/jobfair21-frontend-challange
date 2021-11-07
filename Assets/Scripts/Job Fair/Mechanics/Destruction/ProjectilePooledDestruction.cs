using Platformer.JobFair.Destruction;
using Platformer.JobFair.Mechanics;
using Platformer.JobFair.Mechanics.Weaponry;
using Platformer.JobFair.Mechanics.Weaponry.ProjectileCreation;
using Platformer.Mechanics;
using UnityEngine;

namespace Platformer
{
    /// <summary>
    /// Works with GameObjectPoolManager to achieve an object pooling effect
    /// You can probably swap this out for ObjectPool<T> if you decided to change to
    /// unity version 2021.1
    /// </summary>
    public class ProjectilePooledDestruction : MonoBehaviour, IDestructionProcessor
    {
        [SerializeField] private Projectile owner = default;
        
        [Header("Set by self or injected, shown for debug")]
        [SerializeField] private GameObject prefabKey = default;

        private GameObjectPoolManager pool;

        private void Start()
        {
            prefabKey = owner.ProjectileSpawner.ProjectilePrefab.gameObject;
            
            if (GameController.Instance.gameObject.TryGetComponent(typeof(GameObjectPoolManager),
                out var pool))
            {
                this.pool = pool as GameObjectPoolManager;
            }
        }

        public void Destroy()
        {
            pool.ReturnToPool(prefabKey, gameObject);
            
            gameObject.SetActive(false);
        }
    }
}
