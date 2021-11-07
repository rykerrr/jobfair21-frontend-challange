﻿using System;
using UnityEngine;

namespace Platformer.JobFair.Mechanics
{
    /// <summary>
    /// Delegation of runtime instantiation responsibility to a single object, this acts as sort of a factory
    /// No need for it to be a Singleton as it'll always be under GameController, which already is a singleton
    /// </summary>
    public class RegularInstantiator : MonoBehaviour, IProjectileManager
    {
        public GameObject CreateProjectile(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            var clone = Instantiate(prefab, position, rotation);

            return clone;
        }

        public GameObject CreateProjectile(GameObject prefab, Transform parent)
        {
            var clone = CreateProjectile(prefab, Vector3.zero, Quaternion.identity).transform;

            clone.parent = parent;
            clone.localPosition = Vector3.zero;

            return clone.gameObject;
        }
    }
}