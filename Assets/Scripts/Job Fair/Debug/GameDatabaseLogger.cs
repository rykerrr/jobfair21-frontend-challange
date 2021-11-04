using Platformer.Model;
using UnityEngine;

namespace Platformer.JobFair.Debug
{
    public class GameDatabaseLogger : MonoBehaviour
    {
        #if UNITY_EDITOR
        
        /// <summary>
        /// Use this to log user data for debugging as serializing it for this purpose isn't needed
        /// </summary>
        [ContextMenu("Log Data")]
        public void LogData()
        {
            var db = GameDatabase.Instance;
            var user = db.CurrentUser;
            
            UnityEngine.Debug.Log($"Name: {user.Username} Enemies Killed: {user.EnemiesKilled} Tokens: {user.Tokens} Score: {user.Score}");
        }
        
        #endif
    }
}
