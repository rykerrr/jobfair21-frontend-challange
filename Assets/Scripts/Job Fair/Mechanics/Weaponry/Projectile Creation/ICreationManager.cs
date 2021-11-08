using UnityEngine;

namespace Platformer.JobFair.Mechanics.Weaponry.ProjectileCreation
{
    public interface ICreationManager
    {
        /// <summary>
        /// Basically like a normal instantiation call, but whether it's actually an instantiation,
        /// a "reactivation", or pooling, or something else, we leave up to the implementation
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <returns></returns>
        public GameObject CreateObj(GameObject prefab, Vector3 position, Quaternion rotation);
        
        /// <summary>
        /// Again, just like a normal instantiation call, but this time with parent 
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public GameObject CreateObj(GameObject prefab, Transform parent);
    }
}