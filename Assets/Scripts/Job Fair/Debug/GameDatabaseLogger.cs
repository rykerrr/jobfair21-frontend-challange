using Platformer.Model;
using UnityEngine;

namespace Platformer.JobFair.Debugging
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
            
            Debug.Log($"Name: {user.Username} Enemies Killed: {user.EnemiesKilled} Tokens: {user.Tokens} Score: {user.Score}");
        }
        
        #endif
    }
}
