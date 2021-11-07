using UnityEngine;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class RandomSpritesTile : Tile
{
    [SerializeField] Sprite[] sprites;

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        if (sprites.Length > 0)
        {
            base.GetTileData(position, tilemap, ref tileData);
            tileData.sprite = sprites[Random.Range(0, sprites.Length)];
        }
        else { Debug.Log("Initilisation random sprites"); }
    }

#if UNITY_EDITOR
    [MenuItem("Assets/Create/RandomSpritesTile")]
    public static void CreateRoadTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Random Sprites Tile", "New Random Sprites Tile", "Asset", "Save Random Sprites Tile", "Assets");
        if (path == "")
            return;
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<RandomSpritesTile>(), path);
    }
#endif
}
