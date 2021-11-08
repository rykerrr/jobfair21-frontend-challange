using System.Collections.Generic;
using UnityEngine;

namespace Platformer.JobFair.Mechanics.Weaponry.ProjectileCreation
{
    /// <summary>
    /// Simple Implementation of a GameObject based pool (Prefab-Instance pairs)
    /// Choice for this is due to the possibility of multiple prefabs existing, but having the same script
    /// or the same script properties, but different scale, collider, effects, etc
    /// You can probably swap this out for ObjectPool<T> if you decided to change to
    /// unity version 2021.1
    /// </summary>
    public class GameObjectPoolManager : MonoBehaviour, ICreationManager
    {
        private readonly Dictionary<GameObject, Queue<GameObject>> pool =
            new Dictionary<GameObject, Queue<GameObject>>();

        /// <summary>
        /// Returns object to a pool of a given key (the key is the prefab)
        /// Would have preferred to use something like the PrefabUtility to find the prefab,
        /// but that wouldn't work as it's part of the UnityEditor namespace, it wouldn't work
        /// outside the Editor. 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="returningObj"></param>
        public void ReturnToPool(GameObject key, GameObject returningObj)
        {
            if (pool.ContainsKey(key))
            {
                var foundQ = pool[key];

                foundQ.Enqueue(returningObj);
            }
            else
            {
                var newQ = new Queue<GameObject>();
                newQ.Enqueue(returningObj);

                pool.Add(key, newQ);
            }
        }

        /// <summary>
        /// Sets transform properties after GetFromPool does its magic
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <returns></returns>
        public GameObject CreateObj(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            var obj = GetFromPool(prefab);

            obj.transform.position = position;
            obj.transform.rotation = rotation;
            
            obj.SetActive(true);

            return obj;
        }

        public GameObject CreateObj(GameObject prefab, Transform parent)
        {
            var clone = CreateObj(prefab, Vector3.zero, Quaternion.identity).transform;

            clone.parent = parent;
            clone.localPosition = Vector3.zero;

            return clone.gameObject;
        }

        /// <summary>
        /// Return pooled object or instantiate new one
        /// </summary>
        /// <param name="prefab"></param>
        /// <returns></returns>
        private GameObject GetFromPool(GameObject prefab)
        {
            if (pool.ContainsKey(prefab))
            {
                if (pool[prefab].Count > 0)
                {
                    var obj = pool[prefab].Dequeue();
                
                    return obj;
                }
            }

            return Instantiate(prefab);
        }
        
        #region editor methods
        #if UNITY_EDITOR
        [ContextMenu("Log pool dictionary")]
        public void LogPoolDictionary()
        {
            foreach (var kvp in pool)
            {
                Debug.Log($"{kvp.Key} | ");

                foreach (var item in kvp.Value)
                {
                    Debug.Log($"{item} | ");
                }
            }
        }
        #endif
        #endregion
    }
}