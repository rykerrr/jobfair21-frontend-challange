using Platformer.JobFair.Gameplay.Args;
using Platformer.JobFair.Mechanics;
using Platformer.Mechanics;
using UnityEngine;

namespace Platformer.Job_Fair.Mechanics.Items.Item_Equipping
{
    /// <summary>
    /// Pretty much an instance-based data container, same as PhysicalSpeedBoost and PhysicalGun, main usage beyond that is
    /// the GetUseData() interface method
    /// </summary>
    public class PhysicalExtraJump : MonoBehaviour, IHasAudio, IPhysicalItem
    {
        [Header("References, default will be loaded from GameObject if null")] 
        [SerializeField] private AudioSource audioSource = default;
        [SerializeField] private AudioContainer audioContainer = default;
        
        public AudioSource AudioSource { get; }
        public AudioContainer AudioContainer { get; }

        private void Awake()
        {
            TryInjectDefaultReferences();
        }

        private void TryInjectDefaultReferences()
        {
            audioContainer ??= GetComponent<AudioContainer>();
            audioSource ??= GetComponent<AudioSource>();
        }
        
        public SimulationEventArgs GetUseData()
        {
            return new ItemArgs() {PlrController = GameController.Instance.model.player};
        }
    }
}