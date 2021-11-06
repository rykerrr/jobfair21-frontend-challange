using System.IO;
using Platformer.Core;
using Platformer.JobFair.Gameplay;
using UnityEditor;
using UnityEngine;

namespace Platformer.JobFair.Mechanics.Items
{
    /// <summary>
    /// Base class foundation for an asset depicting item data
    /// </summary>
    public abstract class Item : ScriptableObject
    {
        [Header("Item Preferences")]
        [SerializeField] protected new string name;
        [SerializeField] protected string description;
        
        [SerializeField] protected Sprite itemIcon;
        
        public string ItemName => name;
        public string Description => description;

        public abstract ItemUseEvent Use();

        #region editor convenience code
        protected virtual void OnValidate()
        {
            SetNameAsAssetFileName();
        }
        
        private void OnEnable()
        {
            EditorApplication.projectChanged += SetNameAsAssetFileName;
        }

        private void OnDisable()
        {
            EditorApplication.projectChanged -= SetNameAsAssetFileName;
        }

        /// <summary>
        /// Sets the name field as the filename of the asset when renamed and when created
        /// Just for convenience as renaming then setting the name field gets annoying very quickly
        /// </summary>
        private void SetNameAsAssetFileName()
        {
            var assetPath = AssetDatabase.GetAssetPath(this.GetInstanceID());
            
            name = Path.GetFileNameWithoutExtension(assetPath);
        }
        #endregion
    }
}
