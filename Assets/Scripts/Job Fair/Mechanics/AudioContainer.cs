using System.Collections.Generic;
using UnityEngine;
using StringBuilder = System.Text.StringBuilder;

namespace Platformer.Job_Fair.Mechanics
{
    public class AudioContainer : MonoBehaviour
    {
        [SerializeField] private List<AudioClip> clips = new List<AudioClip>();

        private readonly Dictionary<string, AudioClip> lookup = new Dictionary<string, AudioClip>();

        private void Awake()
        {
            PopulateLookup();
        }

        private void PopulateLookup()
        {
            foreach (var clip in clips)
            {
                lookup.Add(clip.name, clip);
            }
        }

        public bool TryGetClip(string clipName, out AudioClip clip)
        {
            Debug.Log("e");
            
            clip = null;
            
            if (lookup.ContainsKey(clipName))
            {
                Debug.Log("wee");
                
                clip = lookup[clipName];

                Debug.Log(clip);
                
                return true;
            }

            return false;
        }
        
        #if UNITY_EDITOR
        [ContextMenu("Log Dictionary")]
        public void LogDictionaryKVPs()
        {
            var sb = new StringBuilder();
            
            foreach (var kvp in lookup)
            {
                sb.Append($"Key/Name: {kvp.Key} Value/Clip Reference: {kvp.Value}").AppendLine();
            }
            
            Debug.Log(sb.ToString());
        }
        #endif
    }
}