using UnityEngine;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class RandomSpritesTile : Tile
{
    [SerializeField] Sprite[] sprites;
    [SerializeField] private bool isSpriteLocked = false;
    [SerializeField] [Range(0, 100)] private int chanceToSpawn = 100;

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        base.GetTileData(position, tilemap, ref tileData);
        if (!isSpriteLocked)
        {
            if (sprites.Length > 0)
            {
                tileData.sprite = sprites[Random.Range(0, sprites.Length)];
            }
        }
    }

    public void lockSprite()
    {
        isSpriteLocked = true;
    }

    public void unlockSprite()
    {
        isSpriteLocked = false;
    }

    public int getChanceToSpawn()
    {
        return chanceToSpawn;
    }

#if UNITY_EDITOR
    [MenuItem("Assets/Create/RandomSpritesTile")]
    public static void CreateRandomSpritesTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Random Sprites Tile", "New Random Sprites Tile", "Asset", "Save Random Sprites Tile", "Assets");
        if (path == "")
            return;
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<RandomSpritesTile>(), path);
    }
#endif
}
