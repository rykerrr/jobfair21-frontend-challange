using System;

namespace Platformer.JobFair.UI.MainMenu
{
    [Serializable]
    public class LevelHighscoreData
    {
        public bool levelFinished = default;
        public int highscore = default;
        public string highscoreSetter = default;
        public DateTime highscoreTime = default;
    }
}