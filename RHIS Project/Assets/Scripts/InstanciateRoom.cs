using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InstanciateRoom : MonoBehaviour
{
    [Header("Room")]
    [SerializeField] private GameObject[] rooms;
    private int nbrRoom;
    private Vector3Int[,] listOfPos;
    [SerializeField] private int exitRoom = 1;
    private List<GameObject> exitRooms = new();
    [SerializeField] private int bossRoom = 1;
    private List<GameObject> bossRooms = new();
    [SerializeField] private int startRoom = 1;
    private List<GameObject> startRooms = new();
    [SerializeField] private int treasureRoom = 1;
    private List<GameObject> treasureRooms = new();
    [SerializeField] private int battleRoom = 10;
    private List<GameObject> battleRooms = new();
    private enum e
    {
        exitRoom = -1,
        bossRoom = -2,
        startRoom = -3,
        treasureRoom = -4,
        battleRoom = -5
    }

    [Header("Rule Sprite")]
    [SerializeField] private RuleTile water;
    [SerializeField] private RuleTile path;



    void Start()
    {
        nbrRoom = treasureRoom + bossRoom + exitRoom + battleRoom + startRoom;
        SetListOfPos(GetSizeMax());
        InstanciateRooms();

        for (int i = 0; i < nbrRoom; ++i)
        {
            GameObject room = CreateRoom();
            RandomDestroyTilesFromRoom(room);
        }
    }

    private void InstanciateRooms()
    {
        foreach(GameObject room in rooms)
        {
            switch (room.tag)
            {
                case "ExitRoom":
                    exitRooms.Add(room);
                    break;
                case "BossRoom":
                    bossRooms.Add(room);
                    break;
                case "StartRoom":
                    startRooms.Add(room);
                    break;
                case "TreasureRoom":
                    treasureRooms.Add(room);
                    break;
                default:
                    battleRooms.Add(room);
                    break;
            }
        }
    }

    private void SetListOfPos(Vector3Int sizeMax)
    {
        listOfPos = new Vector3Int[nbrRoom/2, nbrRoom/2];
        for (int i = 0; i < nbrRoom / 2; ++i)
        {
            for (int j = 0; j < nbrRoom / 2; ++j)
            {
                listOfPos[i, j] = new Vector3Int((int)((sizeMax.x * i)/1.5),(int)((sizeMax.y * j)/2));
            }
        }
    }

    private Vector3Int GetSizeMax()
    {
        Vector3Int sizeMax = new();
        foreach (GameObject room in rooms)
        {
            Tilemap[] tilemaps = room.GetComponentsInChildren<Tilemap>();
            foreach (Tilemap tilemap in tilemaps)
            {
                tilemap.CompressBounds();
                if (sizeMax.x < tilemap.size.x)
                {
                    sizeMax.x = tilemap.size.x;
                }
                if (sizeMax.y < tilemap.size.y)
                {
                    sizeMax.y = tilemap.size.y;
                }
            }
        }
        return sizeMax;
    }

    private GameObject CreateRoom()
    {
        GameObject room = chooseRoom();

        Vector2Int pos =  GetFreePos();
        //TODO: ValidateNewPos(pos,Room)
        
        deletPos(pos, room);

        return Instantiate(room, listOfPos[pos.x,pos.y], Quaternion.identity);
    }

    private void deletPos(Vector2Int pos, GameObject room)
    {
        int z;
        switch(room.tag)
        {
            case "ExitRoom":
                z = ((int)e.exitRoom);
                break;
            case "BossRoom":
                z = ((int)e.bossRoom);
                break;
            case "StartRoom":
                z = ((int)e.startRoom);
                break;
            case "TreasureRoom":
                z = ((int)e.treasureRoom);
                break;
            default:
                z = ((int)e.battleRoom);
                break;
        }
        listOfPos[pos.x, pos.y].z = z;
    }

    private Vector2Int GetFreePos()
    {
        int x, y;
        do
        {
            x = Random.Range(0, nbrRoom / 2);
            y = Random.Range(0, nbrRoom / 2);
        } while (listOfPos[x, y].z != 0);
        return new Vector2Int(x, y);
    }

    private void RandomDestroyTilesFromRoom(GameObject room)
    {
        Tilemap[] tilemaps = room.GetComponentsInChildren<Tilemap>();
        foreach (Tilemap tilemap in tilemaps)
        {
            print(tilemap.tag);
            if (tilemap.tag != "Water")
            {
                RandomDestroyTilesFromTilemap(tilemap);
            }
        }
    }

    private static void RandomDestroyTilesFromTilemap(Tilemap tilemap)
    {
        for (int x = tilemap.origin.x; x < tilemap.origin.x + tilemap.size.x; ++x)
        {
            for (int y = tilemap.origin.y; y < tilemap.origin.y + tilemap.size.y; ++y)
            {
                Vector3Int pos = new(x, y);
                RandomDestroyTile(tilemap, pos);
            }
        }
    }

    private static void RandomDestroyTile(Tilemap tilemap, Vector3Int pos)
    {
        if (tilemap.HasTile(pos))
        {
            if (tilemap.GetTile<RandomSpritesTile>(pos).getChanceToSpawn() <= Random.Range(0, 100))
            {
                tilemap.SetTile(pos, null);
            }
        }
    }

    GameObject chooseRoom()
    {
        if (exitRoom > 0)
        {
            exitRoom--;
            return exitRooms[Random.Range(0,exitRooms.Count)];
        }
        else if (bossRoom > 0)
        {
            bossRoom--;
            return bossRooms[Random.Range(0, bossRooms.Count)];
        }
        else if (startRoom > 0)
        {
            startRoom--;
            return startRooms[Random.Range(0, startRooms.Count)];
        }
        else if (treasureRoom > 0)
        {
            treasureRoom--;
            return treasureRooms[Random.Range(0, treasureRooms.Count)];
        }
        else if (battleRoom > 0)
        {
            battleRoom--;
            return battleRooms[Random.Range(0, battleRooms.Count)];
        }
        return null;
    }
}
