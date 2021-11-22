using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathFinder : ScriptableObject
{
    private List<Room> rooms = new();
    private List<List<MyTile>> tiles = new();

    private void CreateTiles(Tilemap tilemap, List<Vector3Int> ground)
    {
        for (int i = tilemap.origin.x; i < tilemap.origin.x + tilemap.size.x; ++i)
        {
            for (int j = tilemap.origin.y; j < tilemap.origin.y + tilemap.size.y; ++j)
            {
                Vector3Int v = new(i, j);
                MyTile tile = ScriptableObject.CreateInstance<MyTile>();
                tile.SetPosition(v);
                if (!ground.Contains(v))
                {
                    tile.SetTraversable(false);
                }
                tiles[i].Add(tile);
            }
        }
        for (int i = tilemap.origin.x; i < tilemap.origin.x + tilemap.size.x; ++i)
        {
            for (int j = tilemap.origin.y; j < tilemap.origin.y + tilemap.size.y; ++j)
            {
                if (i!=0)
                {
                    tiles[i][j].AddNeighbours(tiles[i - 1][j]);
                }
                if(i != tilemap.origin.x + tilemap.size.x - 1)
                {
                    tiles[i][j].AddNeighbours(tiles[i+i][j]);
                }
                if (j != 0)
                {
                    tiles[i][j].AddNeighbours(tiles[i][j - 1]);
                }
                if (j != tilemap.origin.y + tilemap.size.y - 1)
                {
                    tiles[i][j].AddNeighbours(tiles[i][j + 1]);
                }
            }
        }

    }

    public List<Vector3Int> CreateAllPaths(List<List<Vector3Int>> allDoors, List<Vector3Int> ground, Tilemap tilemap)
    {
        SetPathfinder(allDoors);
        List<(Vector3Int, Vector3Int)> tuples = CreateTuples();
        List<Vector3Int> pathPos = new();

        CreateTiles(tilemap, ground);

        foreach ((Vector3Int,Vector3Int) tuple in tuples)
        {
            pathPos.AddRange(FindPath(tuple.Item1, tuple.Item2, ground));
        }

        return pathPos;
    }

    private List<Vector3Int> FindPath(Vector3Int start, Vector3Int stop, List<Vector3Int> ground) //TODO: Good path
    {
        int diffX = stop.x - start.x;
        int diffY = stop.y - start.y;
        List<Vector3Int> path = new();
        path.Add(start);
        Vector3Int progress = start;
        int x = ChooseStep(diffX);
        int y = ChooseStep(diffY);

        RecursivePath(ground, start, stop, ref path, diffX, diffY, x, y);

        

        /*
        while (diffX != 0)
        {
            progress.x += x;
            diffX -= x;
            path.Add(progress);
        }
        while (diffY != 0)
        {
            progress.y += y;
            diffY -= y;
            path.Add(progress);
        }*/
        return path;
    }

    private void RecursivePath(List<Vector3Int> ground, Vector3Int progress, Vector3Int stop, ref List<Vector3Int> path, int diffX, int diffY, int x, int y)
    {
        int startIterator = path.Count;
        while (diffX != 0)
        {
            progress.x += x;
            diffX -= x;
            if(path.Contains(progress))
            {
                return;
            }
            path.Add(progress);
            if (ground.Contains(progress) && progress != stop)
            {
                RemovePath(startIterator, ref path, ref diffX, x);
                break;
            }
        }
        while (diffY != 0)
        {
            progress.y += y;
            diffY -= y;
            if (path.Contains(progress))
            {
                return;
            }
            path.Add(progress);
            if (ground.Contains(progress) && progress != stop)
            {
                RemovePath(startIterator, ref path, ref diffY, y);
                break;
            }
        }
        /*if (diffX != 0 || diffY != 0)
        {
            RecursivePath(ground, progress, stop, ref path, diffX, diffY, x, y);
        }*/
    }

    private void RemovePath(int startIterator, ref List<Vector3Int> path, ref int diff, int direction)
    {
        int nbr = (path.Count - startIterator) / 4;
        if (nbr > 3)
        {
            nbr = 3;
        }
        diff -= nbr * direction;
        path.RemoveRange(path.Count - nbr, nbr);
    }

    private int ChooseStep(int diff)
    {
        if (diff == 0)
        {
            return 0;
        }
        return diff / Mathf.Abs(diff);
    }

    private List<(Vector3Int, Vector3Int)> CreateTuples()
    {
        List<(Vector3Int, Vector3Int)> tuples = new();
        while (/*!IsRelated() ||*/ !NeighboursCreated())
        {
            foreach (Room room in rooms)
            {
                if (!room.NeighboursCreated())
                {
                    tuples.Add(room.CreateNeighbour(GetNextRoom(room)));
                }
            }
        }

        return tuples;
    }

    private Room GetNextRoom(Room room)
    {
        Room returnRoom = null;
        float maxDistance = float.MaxValue;
        foreach (Room r in rooms)
        {
            if (r == room)
            {
                continue;
            }
            if (room.GetNeighbours().Contains(r))
            {
                continue;
            }
            if (!r.HasOpenDoor())
            {
                continue;
            }
            float distance = room.GetDistance(r);
            if (maxDistance > room.GetDistance(r))
            {
                returnRoom = r;
                maxDistance = distance;
            }
        }
        return returnRoom;
    }

    private bool NeighboursCreated()
    {
        foreach (Room room in rooms)
        {
            if (room.NeighboursCreated())
            {
                continue;
            }
            return false;
        }
        return true;
    }

    private bool IsRelated() //TODO: IsRelated
    {
        List<Room> passed = new();
        List<Room> queue = new();
        Room room;

        queue.Add(rooms[0]);
        while (queue.Count != 0)
        {
            room = queue[0];
            queue.RemoveAt(0);
            passed.Add(room);
            foreach (Room r in room.GetNeighbours())
            {
                if (!passed.Contains(r) && !queue.Contains(r))
                {
                    queue.Add(r);
                }
            }
        }

        if (passed.Count == rooms.Count)
        {
            return true;
        }
        return false;
    }

    private void SetPathfinder(List<List<Vector3Int>> allDoors)
    {
        foreach (List<Vector3Int> roomDoorPos in allDoors)
        {
            Room room = ScriptableObject.CreateInstance<Room>();
            room.Create(roomDoorPos);
            rooms.Add(room);
        }
    }
}
