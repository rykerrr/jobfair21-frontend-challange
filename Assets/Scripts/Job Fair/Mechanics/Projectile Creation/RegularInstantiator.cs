using System;
using UnityEngine;

namespace Platformer.JobFair.Mechanics
{
    public class RegularInstantiator : MonoBehaviour, IProjectileCreationHandler
    {
        public GameObject CreateProjectile(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            var clone = Instantiate(prefab, position, rotation);

            return clone;
        }
    }
}