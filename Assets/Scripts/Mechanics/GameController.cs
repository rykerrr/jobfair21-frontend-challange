using System.Linq;
using Platformer.Core;
using Platformer.JobFair.Mechanics.Weaponry.ProjectileCreation;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This class exposes the the game model in the inspector, and ticks the
    /// simulation.
    /// </summary> 
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }

        //This model field is public and can be therefore be modified in the 
        //inspector.
        //The reference actually comes from the InstanceRegister, and is shared
        //through the simulation and events. Unity will deserialize over this
        //shared reference when the scene loads, allowing the model to be
        //conveniently configured inside the inspector.
        public PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public ICreationManager CreationManager { get; private set; }

        private void Awake()
        {
            TryInjectDefaultReferences();
        }

        private void TryInjectDefaultReferences()
        {
            CreationManager = GetComponent<ICreationManager>();

            if (GameDatabase.Instance.CurrentUser.CurrentLevel == null)
            {
                TrySetLevelAsCurrentScene();
            }
        }

        /// <summary>
        /// This is specifically needed when you don't start the game from the main menu
        /// Since it's supposed to be started from there, so that a level can be selected and set here for high score setting
        /// </summary>
        private static void TrySetLevelAsCurrentScene()
        {
            var curSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
            
            var tryFindLevel =
                JobFair.Utility.LevelManager.Levels.FirstOrDefault(x =>
                    x.name == curSceneName);

            if (tryFindLevel != null)
            {
                GameDatabase.Instance.CurrentUser.CurrentLevel = tryFindLevel;
            }
        }

        private void OnEnable()
        {
            Instance = this;
        }

        private void OnDisable()
        {
            if (Instance == this) Instance = null;
        }

        private void OnDestroy()
        {
            Simulation.Clear();
        }

        private void Update()
        {
            if (Instance == this) Simulation.Tick();
        }
    }
}