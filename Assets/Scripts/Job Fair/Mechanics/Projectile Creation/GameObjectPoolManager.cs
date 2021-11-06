using System;
using UnityEngine;

namespace Platformer.JobFair.Mechanics
{
    public class GameObjectPoolManager : MonoBehaviour, IProjectileCreationHandler
    {
        public GameObject CreateProjectile(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            throw new NotImplementedException();
        }
    }
}