using UnityEngine;

namespace Platformer.JobFair.Mechanics
{
    public interface IProjectileCreationHandler
    {
        public GameObject CreateProjectile(GameObject prefab, Vector3 position, Quaternion rotation);
    }
}