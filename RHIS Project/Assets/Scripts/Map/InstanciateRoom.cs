using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InstanciateRoom : MonoBehaviour
{
    [Header("Room")]
    [SerializeField] private GameObject[] rooms;
    [SerializeField] [Range(0.5f, 5)] private float xSpacing = 1;
    [SerializeField] [Range(0.5f, 5)] private float ySpacing = 1;
    private int nbrRoom;
    private Vector3Int[,] listOfPos;
    private bool firstRoom = false;
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
    [SerializeField] [Range(0, 1)] private float fill;
    [SerializeField] [Range(0, 1)] private float ground;
    private GameObject myStage;
    private Tilemap groundStageTilemap;
    private Tilemap obstaclesStageTilemap;
    private Tilemap pathStageTilemap;
    private List<List<Vector3Int>> doors = new();
    private List<Vector3Int> groundRoom = new();

    private PathFinder pathfinder;




    void Start()
    {
        pathfinder = ScriptableObject.CreateInstance<PathFinder>();
        nbrRoom = treasureRoom + bossRoom + exitRoom + battleRoom + startRoom;
        Vector3Int sizeMax = GetSizeMax(rooms);
        SetListOfPos(sizeMax);
        CreateStage(sizeMax);
        InstanciateRooms();

        for (int i = 0; i < nbrRoom; ++i)
        {
            GameObject room = CreateRoom(sizeMax);
            //room.transform.parent = stage.transform; Disabled to prevent data corruption
            AffectTilemapFromRoom(room);
        }

        InstanciatePath(pathfinder.CreateAllPaths(doors, groundRoom, groundStageTilemap));
    }

    /*private List<Vector3Int> GetGround()
    {
        List<Vector3Int> ground = new();

        foreach (Tilemap tilemap in GetComponentsInChildren<Tilemap>())
        {
            print("a");
            if (tilemap.CompareTag("Ground"))
            {
                for (int i = tilemap.origin.x; i < tilemap.origin.x + tilemap.size.x; ++i)
                {
                    for (int j = tilemap.origin.y; j < tilemap.origin.y + tilemap.size.y; ++j)
                    {
                        Vector3Int v = new(i, j);
                        if (tilemap.HasTile(v))
                        {
                            ground.Add(PosToStagePos(v, tilemap.GetComponent<Transform>().position));
                        }
                    }
                }
            }
        }
        return ground;
    }*/

    private void InstanciatePath(List<Vector3Int> path)
    {
        if (path == null)
        {
            return;
        }
        foreach(Vector3Int pos in path)
        {
            pathStageTilemap.SetTile(pos, this.path);
            obstaclesStageTilemap.SetTile(pos, null);
        }
    }

    private void CreateStage(Vector3Int size)
    {
        myStage = Instantiate(stage);
        myStage.transform.parent = GetComponentInParent<Transform>();
        Tilemap[] tilemaps = myStage.GetComponentsInChildren<Tilemap>();
        foreach (Tilemap tilemap in tilemaps)
        {
            if (tilemap.CompareTag("Ground"))
            {
                groundStageTilemap = tilemap;
                groundStageTilemap.size = SetSize(size);
                groundStageTilemap.origin = SetOrigin(groundStageTilemap.size);
                FillTilemap(groundStageTilemap, ground);
            }
            else if (tilemap.CompareTag("Obstacles"))
            {
                obstaclesStageTilemap = tilemap;
                obstaclesStageTilemap.size = SetSize(size);
                obstaclesStageTilemap.origin = SetOrigin(obstaclesStageTilemap.size);
                FillTilemap(obstaclesStageTilemap, fill);
            }
            else if (tilemap.CompareTag("Path"))
            {
                pathStageTilemap = tilemap;
                pathStageTilemap.size = SetSize(size);
                pathStageTilemap.origin = SetOrigin(pathStageTilemap.size);
            }
        }
    }

    private Vector3Int SetSize(Vector3Int size)
    {
        return new Vector3Int((int)((size.y + size.x) * nbrRoom *2),(int)((size.y + size.x) * nbrRoom *2));
    }

    private Vector3Int SetOrigin(Vector3Int size)
    {
        int x = size.x;
        int y = size.y;
        return new Vector3Int(-x/4,-y/2);
    }

    private Vector3Int posToStagePos(Vector3Int pos)
    {
        int x = pos.x + pos.y;
        int y = pos.y - pos.x;
        return new Vector3Int(x, y);
    }

    private void FillTilemap(Tilemap tilemap, float chance)
    {
        for (int i = tilemap.origin.x; i < tilemap.origin.x + tilemap.size.x; ++i)
        {
            for (int j = tilemap.origin.y; j < tilemap.origin.y + tilemap.size.y; ++j)
            {
                if (! (Random.Range(0,100)>=chance*100))
                {
                    tilemap.SetTile(new Vector3Int(i, j), tilemap.GetComponent<SetTiles>().getRandomScriptableTile());
                }
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
                listOfPos[i, j] = new Vector3Int((int)(sizeMax.x * i / xSpacing),(int)(sizeMax.y * j / ySpacing));
            }
        }
    }

    private Vector3Int GetSizeMax(GameObject[] rooms)
    {
        Vector3Int sizeMax = new();
        Vector3Int size;
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
        GameObject room = ChooseRoom();
        Vector2Int pos;
        do
        {
             pos = GetFreePos();
        } while (!ValidateNewPos(pos, room));
        
        DeletPos(pos, room);

        return Instantiate(room, new Vector3Int(listOfPos[pos.x,pos.y].x, listOfPos[pos.x, pos.y].y) + GetRandPos(sizeMax,room), Quaternion.identity);
    }

    private bool ValidateNewPos(Vector2Int pos, GameObject room)
    {
        List<Vector2Int> neighbours = GetNeighbours(pos);
        switch (room.tag)
        {
            case "ExitRoom":
                firstRoom = true;
                return true;
            case "BossRoom":
                if (!firstRoom)
                {
                    firstRoom = true;
                    return true;
                }
                return Contains(neighbours, E.exitRoom);
            case "StartRoom":
                if (!firstRoom)
                {
                    firstRoom = true;
                    return true;
                }
                return !Contains(neighbours, E.bossRoom);
            case "TreasureRoom":
                if (!firstRoom)
                {
                    firstRoom = true;
                    return true;
                }
                return !Contains(neighbours, E.bossRoom) && !Contains(neighbours, E.startRoom);
            default:
                if (!firstRoom)
                {
                    Debug.Log("true");
                    firstRoom = true;
                    return true;
                }
                return Contains(neighbours);
        }
    }

    private bool Contains(List<Vector2Int> neighbours)
    {
        foreach (Vector2Int n in neighbours)
        {
            if (listOfPos[n.x, n.y].z != 0)
            {
                return true;
            }
        }
        return false;
    }

    private bool Contains(List<Vector2Int> neighbours, E tag)
    {
        foreach(Vector2Int n in neighbours)
        {
            if(listOfPos[n.x,n.y].z == (int)tag)
            {
                return true;
            }
        }
        return false;
    }

    private List<Vector2Int> GetNeighbours(Vector2Int pos)
    {
        List<Vector2Int> neighbours = new();

        for (int x = pos.x - 1; x <= pos.x + 1; ++x)
        {
            if (x < 0 || x > nbrRoom / 2 - 1)
            {
                continue;
            }
            for (int y = pos.y - 1; y <= pos.y + 1; ++y)
            {
                if (y < 0 || y > nbrRoom / 2 - 1)
                {
                    continue;
                }
                if(x == pos.x && y == pos.y)
                {
                    continue;
                }
                neighbours.Add(new Vector2Int(x, y));
            }
        }
        string n = "";
        foreach (Vector2Int v in neighbours)
        {
            n +=v + ", ";
        }
        Debug.Log("room : " + pos + ", voisins : " + n);
        return neighbours;
    }

    private Vector3Int GetRandPos(Vector3Int sizeMax, GameObject room)
    {
        Vector3Int size = GetSizeMax(room);
        int diffX = sizeMax.x - size.x;
        int diffY = sizeMax.y - size.y;
        return new Vector3Int(Random.Range(-diffX / 2, diffX / 2), Random.Range(-diffY / 2, diffY / 2));
    }

    private void DeletPos(Vector2Int pos, GameObject room)
    {
        var z = room.tag switch
        {
            "ExitRoom" => ((int)E.exitRoom),
            "BossRoom" => ((int)E.bossRoom),
            "StartRoom" => ((int)E.startRoom),
            "TreasureRoom" => ((int)E.treasureRoom),
            _ => ((int)E.battleRoom),
        };
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

    private void AffectTilemapFromRoom(GameObject room)
    {
        Tilemap[] tilemaps = room.GetComponentsInChildren<Tilemap>();
        foreach (Tilemap tilemap in tilemaps)
        {
            DestroyTilesFromTilemap(tilemap, room);
            if (tilemap.CompareTag("Water"))
            {
                PlaceTile(tilemap,water);
            }
            else if (tilemap.CompareTag("Path"))
            {
                PlaceTile(tilemap,path);
            }
            else if (tilemap.CompareTag("Door"))
            {
                AddDoors(tilemap);
            }
            else if (tilemap.CompareTag("Ground"))
            {
                AddGround(tilemap);
            }
        }
    }

    private void AddGround(Tilemap tilemap)
    {
        for (int i = tilemap.origin.x; i < tilemap.origin.x + tilemap.size.x; ++i)
        {
            for (int j = tilemap.origin.y; j < tilemap.origin.y + tilemap.size.y; ++j)
            {
                Vector3Int v = new(i, j);
                if (tilemap.HasTile(v))
                {
                    groundRoom.Add(PosToStagePos(v, tilemap.GetComponent<Transform>().position));
                }
            }
        }
    }

    private void PlaceTile(Tilemap tilemap, IsometricRuleTile rule)
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

    private void AddDoors(Tilemap tilemap)
    {
        List<Vector3Int> listDoors = new();
        for (int i = tilemap.origin.x; i < tilemap.origin.x + tilemap.size.x; ++i)
        {
            for (int j = tilemap.origin.y; j < tilemap.origin.y + tilemap.size.y; ++j)
            {
                if (tilemap.HasTile(new Vector3Int(i,j)))
                {
                    listDoors.Add(PosToStagePos(new Vector3Int(i, j),tilemap.GetComponent<Transform>().position));
                }
            }
        }
        doors.Add(listDoors);
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
            obstaclesStageTilemap.SetTile(stagePos, null);
        }
    }

    private void RandomDestroyTile(Tilemap tilemap, Vector3Int pos)
    {
        if (tilemap.HasTile(pos))
        {
            RandomSpritesTile t = tilemap.GetTile<RandomSpritesTile>(pos);
            if (t != null)
            {
                if (t.getChanceToSpawn() <= Random.Range(0, 100))
                {
                    tilemap.SetTile(pos, null);
                }
            }
            else
            {
                RandomEnemies r = tilemap.GetTile<RandomEnemies>(pos);
                if (r.GetChanceToSpawn() >= Random.Range(0, 100))
                {
                    Object enemy = r.GetEnemy();
                    Instantiate(enemy, RoomPosToFlatPos(pos) + tilemap.GetComponentInParent<Transform>().position, Quaternion.identity);
                    if (enemy.name == "Rhis Variant")
                    {
                        Debug.Log("pos " + pos + " flatPos " + RoomPosToFlatPos(pos)+ " position " + (pos + tilemap.GetComponentInParent<Transform>().position));
                    }
                    //Instantiate(enemy, pos, Quaternion.identity);
                }
                tilemap.SetTile(pos, null);
            }
        }
    }

    private Vector3 RoomPosToFlatPos(Vector3Int v)
    {
        float fX = v.x - v.y;
        float fY = v.x + v.y + 1f;
        return new Vector3(fX/2f,fY/4f);
    }

    private GameObject ChooseRoom()
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
