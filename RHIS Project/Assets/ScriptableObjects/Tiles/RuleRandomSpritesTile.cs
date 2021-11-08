using UnityEngine;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class RuleRandomSpritesTile : Tile
{
    [SerializeField] RuleTile[] ruleTile;
    [SerializeField] private bool isSpriteLocked = false;
    [SerializeField] [Range(0, 100)] int chanceToSpawn = 100;

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        base.GetTileData(position, tilemap, ref tileData);
        if (!isSpriteLocked)
        {
            if (chanceToSpawn >= Random.Range(0, 101))
            {
                if (ruleTile.Length > 0)
                {
                    //tileData.sprite = ruleTile[Random.Range(0, rule.Length)].;
                }
            }
            else
            {
                tileData.sprite = null;
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

#if UNITY_EDITOR
    [MenuItem("Assets/Create/RuleRandomSpritesTile")]
    public static void CreateRuleRandomSpritesTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Rule Random Sprites Tile", "New Rule Random Sprites Tile", "Asset", "Save Rule Random Sprites Tile", "Assets");
        if (path == "")
            return;
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<RuleRandomSpritesTile>(), path);
    }
#endif
}
