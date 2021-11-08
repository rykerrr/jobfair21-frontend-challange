using System;
using UnityEngine;

namespace Platformer.JobFair.SaveLoad
{
    /// <summary>
    /// Wrapper around DateTime to allow it to be serialized by JSON
    /// </summary>
    [Serializable]
    public struct JsonDateTime {
        
        public long value;
        
        public static implicit operator DateTime(JsonDateTime jdt) 
        {
#if UNITY_EDITOR
            Debug.Log("Converted to time");
#endif
            
            return DateTime.FromFileTimeUtc(jdt.value);
        }
        
        public static implicit operator JsonDateTime(DateTime dt) 
        {
#if UNITY_EDITOR
            Debug.Log("Converted to JDT");
#endif

            var jdt = new JsonDateTime {value = dt.ToFileTimeUtc()};
            return jdt;
        }
    }
}
