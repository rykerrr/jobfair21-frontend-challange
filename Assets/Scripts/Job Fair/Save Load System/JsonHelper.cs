using System;
using UnityEngine;

namespace Platformer.JobFair.SaveLoad
{
    /// <summary>
    /// Taken from https://stackoverflow.com/questions/36239705/serialize-and-deserialize-json-and-json-array-in-unity#:~:text=Unity's%20JsonUtility%20does%20not%20support,get%20array%20working%20with%20JsonUtility%20.
    /// The problem with JsonUtility is that it doesn't permit serialization of collections, despite being an extremely quick JSON serializer/deserializer
    /// This class acts as it's name provides, as a helper class, to serialize/deserialize arrays
    /// </summary>
    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }
}