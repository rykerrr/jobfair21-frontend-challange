using System.Collections;
using System.Collections.Generic;
using Platformer.JobFair.Mechanics;
using UnityEngine;

namespace Platformer
{
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
