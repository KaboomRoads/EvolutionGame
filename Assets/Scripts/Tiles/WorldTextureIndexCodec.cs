using UnityEngine;

namespace Tiles
{
    public static class WorldTextureIndexCodec
    {
        public static Color32 Encode(int index)
        {
            return new Color32((byte)((index >> 24) & 0xFF), (byte)((index >> 16) & 0xFF), (byte)((index >> 8) & 0xFF), (byte)(index & 0xFF));
        }

        public static int Decode(Color c)
        {
            Color32 c32 = c;
            return (c32.r << 24) | (c32.g << 16) | (c32.b << 8) | c32.a;
        }
    }
}