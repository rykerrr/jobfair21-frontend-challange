using System;
using Platformer.JobFair.SaveLoad;

namespace Platformer.JobFair.UI.MainMenu
{
    /// <summary>
    /// Data that contains specific highscore stuff
    /// DateTime could be removed, the main purpose was to implement the time the highscore was set but the struct isn't serializable
    /// This can definitely be fixed but I'm not sure if I'll have time to do that
    /// </summary>
    [Serializable]
    public class LevelHighscoreData
    {
        public bool levelFinished = default;
        public int highscore = default;
        public string highscoreSetter = default;
        public JsonDateTime highscoreTime = default;
    }
}