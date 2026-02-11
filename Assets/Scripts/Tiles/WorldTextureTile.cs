using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tiles
{
    [CreateAssetMenu(menuName = "Tiles/World Texture Tile")]
    public class WorldTextureTile : Tile
    {
        public int index;

        private void OnValidate()
        {
            color = WorldTextureIndexCodec.Encode(index);
        }

        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            base.GetTileData(position, tilemap, ref tileData);

            tileData.color = WorldTextureIndexCodec.Encode(index);
        }
    }
}