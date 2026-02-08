using UnityEditor;
using UnityEngine;

namespace Tiles.Editor
{
    public static class WorldTextureArrayAssetBuilder
    {
        [MenuItem("Tools/Tilemap/Build Texture2DArray From Library")]
        public static void Build()
        {
            var lib = Selection.activeObject as WorldTextureList;
            if (!lib || lib.entries == null || lib.entries.Length == 0)
            {
                Debug.LogError("Select a WorldTextureList asset first.");
                return;
            }

            var textures = lib.GetTextures();
            if (textures == null || textures.Length == 0 || textures[0] == null)
            {
                Debug.LogError("Library has null texture at index 0.");
                return;
            }

            Texture2D t0 = textures[0];

            for (var i = 0; i < textures.Length; i++)
            {
                Texture2D t = textures[i];
                if (!t)
                {
                    Debug.LogError($"Null texture at index {i}.");
                    return;
                }

                if (t.width != t0.width || t.height != t0.height)
                {
                    Debug.LogError($"Size mismatch at index {i}: {t.name} is {t.width}x{t.height}, expected {t0.width}x{t0.height}.");
                    return;
                }

                if (t.graphicsFormat != t0.graphicsFormat)
                {
                    Debug.LogError($"Format mismatch at index {i}: {t.name} is {t.graphicsFormat}, expected {t0.graphicsFormat}.");
                    return;
                }

                if (t.mipmapCount != t0.mipmapCount)
                {
                    Debug.LogError($"Mipmap mismatch at index {i}: {t.name} mipCount={t.mipmapCount}, expected {t0.mipmapCount}.");
                    return;
                }
            }

            string path = EditorUtility.SaveFilePanelInProject(
                "Save Texture2DArray",
                $"{lib.name}_TexArray",
                "asset",
                "Choose where to save the Texture2DArray asset.");

            if (string.IsNullOrEmpty(path)) return;

            var array = new Texture2DArray(t0.width, t0.height, textures.Length, t0.format, false, false)
            {
                wrapMode = TextureWrapMode.Repeat,
                filterMode = t0.filterMode,
                name = $"{lib.name}_TexArray"
            };

            for (var i = 0; i < textures.Length; i++)
                if (textures[i])
                    Graphics.CopyTexture(textures[i], 0, 0, array, i, 0);

            AssetDatabase.CreateAsset(array, path);
            AssetDatabase.SaveAssets();
            EditorGUIUtility.PingObject(array);

            Debug.Log($"Created Texture2DArray: {path}");
        }
    }
}