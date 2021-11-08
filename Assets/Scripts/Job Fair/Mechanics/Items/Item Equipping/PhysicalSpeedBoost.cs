using System;
using Platformer.JobFair.Gameplay.Args;
using Platformer.JobFair.Mechanics;
using Platformer.Mechanics;
using UnityEngine;

namespace Platformer.Job_Fair.Mechanics.Items.Item_Equipping
{
    /// <summary>
    /// Pretty much an instance-based data container, same as PhysicalExtraJump and PhysicalGun, main usage beyond that is
    /// the GetUseData() interface method
    /// </summary>
    public class PhysicalSpeedBoost : MonoBehaviour, IHasAudio, IPhysicalItem
    {
        [Header("References, default will be loaded from GameObject if null")] 
        [SerializeField] private AudioSource audioSource = default;
        [SerializeField] private AudioContainer audioContainer = default;

        public AudioSource AudioSource => audioSource;
        public AudioContainer AudioContainer => audioContainer;

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