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
    private enum E
    {
        exitRoom = -1,
        bossRoom = -2,
        startRoom = -3,
        treasureRoom = -4,
        battleRoom = -5
    }

    [Header("Rule Sprite")]
    [SerializeField] private IsometricRuleTile water;
    [SerializeField] private IsometricRuleTile path;

    [Header("Stage")]
    [SerializeField] private GameObject stage;
    private GameObject myStage;
    private Tilemap groundStageTilemap;
    private Tilemap obstaclesSatgeTilemap;




    void Start()
    {
        nbrRoom = treasureRoom + bossRoom + exitRoom + battleRoom + startRoom;
        Vector3Int sizeMax = GetSizeMax(rooms);
        SetListOfPos(sizeMax);
        CreateStage(sizeMax);
        InstanciateRooms();

        for (int i = 0; i < nbrRoom; ++i)
        {
            GameObject room = CreateRoom(sizeMax);
            RandomDestroyTilesFromRoom(room);
        }
    }

    private void CreateStage(Vector3Int size)
    {
        myStage = Instantiate(stage);
        Tilemap[] tilemaps = myStage.GetComponentsInChildren<Tilemap>();
        foreach (Tilemap tilemap in tilemaps)
        {
            if (tilemap.tag == "Ground")
            {
                groundStageTilemap = tilemap;
                groundStageTilemap.size = size * nbrRoom;
                groundStageTilemap.origin = new Vector3Int(-size.x * nbrRoom / 4, - size.x * nbrRoom / 2);
                fillTilemap(groundStageTilemap);
            }
            else if (tilemap.tag == "Obstacles")
            {
                obstaclesSatgeTilemap = tilemap;
                obstaclesSatgeTilemap.size = size * nbrRoom;
                obstaclesSatgeTilemap.origin = new Vector3Int(-size.x * nbrRoom / 4, -size.x * nbrRoom / 2);
                fillTilemap(obstaclesSatgeTilemap);
            }
        }
    }

    private void fillTilemap(Tilemap tilemap)
    {
        for (int i = tilemap.origin.x; i < tilemap.origin.x + tilemap.size.x; ++i)
        {
            for (int j = tilemap.origin.y; j < tilemap.origin.y + tilemap.size.y; ++j)
            {
                tilemap.SetTile(new Vector3Int(i, j), tilemap.GetComponent<SetTiles>().getRandomScriptableTile());
            }
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

    private Vector3Int GetSizeMax(GameObject[] rooms)
    {
        Vector3Int sizeMax = new();
        Vector3Int size = new();
        foreach (GameObject room in rooms)
        {
            size = GetSizeMax(room);
            if (sizeMax.x < size.x)
            {
                sizeMax.x = size.x;
            }
            if (sizeMax.y < size.y)
            {
                sizeMax.y = size.y;
            }
        }
        return sizeMax;
    }

    private Vector3Int GetSizeMax(GameObject room)
    {
        Vector3Int sizeMax = new();
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
        return sizeMax;
    }

    private GameObject CreateRoom(Vector3Int sizeMax)
    {
        GameObject room = chooseRoom();

        Vector2Int pos =  GetFreePos();
        //TODO: ValidateNewPos(pos,Room)
        
        deletPos(pos, room);

        return Instantiate(room, listOfPos[pos.x,pos.y] + GetRandPos(sizeMax,room), Quaternion.identity);
    }

    private Vector3Int GetRandPos(Vector3Int sizeMax, GameObject room)
    {
        Vector3Int size = GetSizeMax(room);
        int diffX = sizeMax.x - size.x;
        int diffY = sizeMax.y - size.y;
        return new Vector3Int(Random.Range(-diffX / 2, diffX / 2), Random.Range(-diffY / 2, diffY / 2));
    }

    private void deletPos(Vector2Int pos, GameObject room)
    {
        int z;
        switch(room.tag)
        {
            case "ExitRoom":
                z = ((int)E.exitRoom);
                break;
            case "BossRoom":
                z = ((int)E.bossRoom);
                break;
            case "StartRoom":
                z = ((int)E.startRoom);
                break;
            case "TreasureRoom":
                z = ((int)E.treasureRoom);
                break;
            default:
                z = ((int)E.battleRoom);
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
            DestroyTilesFromTilemap(tilemap, room);
            if (tilemap.tag == "Water")
            {
                placeTile(tilemap,water);
            }
            else if (tilemap.tag == "Path")
            {
                placeTile(tilemap,path);
            }
        }
    }

    private void placeTile(Tilemap tilemap, IsometricRuleTile rule)
    {
        for (int x = tilemap.origin.x; x < tilemap.origin.x + tilemap.size.x; ++x)
        {
            for (int y = tilemap.origin.y; y < tilemap.origin.y + tilemap.size.y; ++y)
            {
                Vector3Int pos = new(x, y);
                if (tilemap.HasTile(pos))
                {
                    tilemap.SetTile(pos, rule);
                }
            }
        }
    }

    private void DestroyTilesFromTilemap(Tilemap tilemap, GameObject room)
    {
        Vector3Int stagePos;
        Vector3 roomPos = room.GetComponent<Transform>().position;
        for (int x = tilemap.origin.x; x < tilemap.origin.x + tilemap.size.x; ++x)
        {
            for (int y = tilemap.origin.y; y < tilemap.origin.y + tilemap.size.y; ++y)
            {
                Vector3Int pos = new(x, y);
                RandomDestroyTile(tilemap, pos);
                stagePos = PosToStagePos(pos, roomPos);
                DestroyStage(tilemap, pos, stagePos);
            }
        }
    }

    private Vector3Int PosToStagePos(Vector3Int pos, Vector3 roomPos)
    {
        int roomPosX = (int)roomPos.x;
        int roomPosY = 2*(int)roomPos.y;
        int stagePosX = roomPosX + roomPosY;
        int stagePosY = -roomPosX + roomPosY;
        return new Vector3Int(stagePosX, stagePosY)+pos;
    }

    private void DestroyStage(Tilemap tilemap, Vector3Int pos, Vector3Int stagePos)
    {
        if (tilemap.HasTile(pos))
        {
            groundStageTilemap.SetTile(stagePos, null);
            obstaclesSatgeTilemap.SetTile(stagePos, null);
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

    private GameObject chooseRoom()
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
