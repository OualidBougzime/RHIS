using UnityEngine;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class RandomEnemies : Tile
{
    [SerializeField] Object[] enemies;
    [SerializeField] [Range(0, 100)] private int chanceToSpawn = 100;

    public int GetChanceToSpawn()
    {
        return chanceToSpawn;
    }

    public Object GetEnemy()
    {
        return enemies[Random.Range(0, enemies.Length)];
    }

#if UNITY_EDITOR
    [MenuItem("Assets/Create/RandomEnemies")]
    public static void CreateRandomSpritesTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Random Enemies", "New Random Enemies", "Asset", "Save Random Enemies", "Assets");
        if (path == "")
            return;
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<RandomEnemies>(), path);
    }
#endif
}
