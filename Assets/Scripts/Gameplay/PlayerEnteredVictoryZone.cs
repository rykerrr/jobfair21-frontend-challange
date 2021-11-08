using System;
using System.Linq;
using Platformer.Core;
using Platformer.JobFair.UI.MainMenu;
using Platformer.JobFair.SaveLoad;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Gameplay
{
    /// <summary>
    /// This event is triggered when the player character enters a trigger with a VictoryZone component.
    /// </summary>
    /// <typeparam name="PlayerEnteredVictoryZone"></typeparam>
    public class PlayerEnteredVictoryZone : Simulation.Event<PlayerEnteredVictoryZone>
    {
        public VictoryZone victoryZone;

        PlatformerModel model = Simulation.GetModel<PlatformerModel>();
        
        private static readonly int Victory = Animator.StringToHash("victory");

        public override void Execute()
        {
            model.player.Animator.SetTrigger(Victory);
            model.player.controlEnabled = false;

            var curUser = GameDatabase.Instance.CurrentUser;

            var curLevelAsset = LevelSelectionPanelUI.Levels.First(x => x.Name == curUser.CurrentLevelName);
            curLevelAsset.SetLevelScoreData(
                new LevelHighscoreData() {highscore = curUser.Score, highscoreSetter = curUser.Username, highscoreTime = DateTime.Now, levelFinished = true});
            
            JSONSaveLoadManager.SaveLevelHighscores(LevelSelectionPanelUI.Levels);
            
        }
    }
}