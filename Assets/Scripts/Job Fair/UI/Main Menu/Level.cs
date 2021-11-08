using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Platformer.JobFair.UI.MainMenu
{
    /// <summary>
    /// Asset that holds level-related data
    /// Sort of like a scene wrapper
    /// </summary>
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

        [SerializeField] private LevelHighscoreData highscoreData;

        public string Name => name;
        public string SceneName => sceneName;
        public Sprite SceneSprite => sceneSprite;
        public LevelHighscoreData HighscoreData => highscoreData;

        /// <summary>
        /// Updates the highscore data, such as when loaded from a file or when a level was won
        /// </summary>
        /// <param name="data"></param>
        public void SetLevelScoreData(LevelHighscoreData data)
        {
            if (!data.levelFinished) return;
            
            highscoreData.levelFinished = true;
            highscoreData.highscore = data.highscore;
            highscoreData.highscoreSetter = data.highscoreSetter;
            highscoreData.highscoreTime = data.highscoreTime;
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
