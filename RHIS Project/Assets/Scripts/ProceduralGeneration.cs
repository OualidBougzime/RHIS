using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ProceduralGeneration : MonoBehaviour
{
    Tilemap tilemap;

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        StartCoroutine(myWhile());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator myWhile()
    {
        while (true)
        {
            tilemap.RefreshAllTiles();
            yield return new WaitForSeconds(1f);
        }
    }
}
