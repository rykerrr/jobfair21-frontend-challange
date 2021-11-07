using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Platformer.JobFair.MainMenu
{
    [CreateAssetMenu(menuName = "Levels/New Level Listing", fileName = "New Level Listing")]
    public class Level : ScriptableObject
    {
        [SerializeField] private new string name;
        
        /// <summary>
        /// Opted for string scene management instead of index-based due to the fact that the indices may change
        /// due to the build order, etc, strings seem like they'll be slightly less of a pain to change here
        /// </summary>
        [SerializeField] private string sceneName;

        [SerializeField] private Sprite sceneSprite;

        private bool levelFinished = default;
        private int highscore = default;
        private string highscoreSetter = default;
        private DateTime highscoreTime = default;

        public string Name => name;
        public string SceneName => sceneName;
        public Sprite SceneSprite => sceneSprite;
        public bool LevelFinished => levelFinished;
        public int Highscore => highscore;
        public string HighscoreSetter => highscoreSetter;
        public DateTime HighscoreTime => highscoreTime;

        public void LoadLevelScoreData()
        {
            
        }

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
