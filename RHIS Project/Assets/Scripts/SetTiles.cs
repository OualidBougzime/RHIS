using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SetTiles : MonoBehaviour
{
    [SerializeField] RandomSpritesTile[] scriptableTiles;
    private Tilemap tilemap;

    void Start()
    {
        tilemap = GetComponent<Tilemap>();

        unlockAllSprites();
        tilemap.RefreshAllTiles();
        lockAllSprites();
    }

    private void lockAllSprites()
    {
        foreach (RandomSpritesTile tile in scriptableTiles)
        {
            tile.lockSprite();
        }
    }

    private void unlockAllSprites()
    {
        foreach (RandomSpritesTile tile in scriptableTiles)
        {
            tile.unlockSprite();
        }
    }
}
