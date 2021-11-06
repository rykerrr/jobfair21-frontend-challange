using System;
using UnityEngine;

namespace Platformer.JobFair.Mechanics
{
    /// <summary>
    /// Delegation of runtime instantiation responsibility to a single object, this acts as sort of a factory
    /// No need for it to be a Singleton as it'll always be under GameController, which already is a singleton
    /// </summary>
    public class RegularInstantiator : MonoBehaviour, IProjectileCreationHandler
    {
        public GameObject CreateProjectile(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            var clone = Instantiate(prefab, position, rotation);

            return clone;
        }
    }
}