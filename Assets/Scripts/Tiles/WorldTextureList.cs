using UnityEngine;

namespace Tiles
{
    [CreateAssetMenu(menuName = "Tiles/World Texture List")]
    public class WorldTextureList : ScriptableObject
    {
        [System.Serializable]
        public class Entry
        {
            public Texture2D texture;
        }

        public Entry[] entries;

        public int Count => entries?.Length ?? 0;

        public Texture2D[] GetTextures()
        {
            if (entries is null) return null;
            var arr = new Texture2D[entries.Length];
            for (var i = 0; i < entries.Length; i++) arr[i] = entries[i].texture;
            return arr;
        }
    }
}