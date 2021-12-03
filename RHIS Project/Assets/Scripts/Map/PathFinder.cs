using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathFinder : ScriptableObject
{
    private List<Room> rooms = new();
    private List<List<MyTile>> tiles = new();
    
    private int diffPosX;
    private int diffPosY;


    private void CreateTiles(Tilemap tilemap, List<Vector3Int> ground)
    {
        int k = 0;
        diffPosX = -tilemap.origin.x;
        diffPosY = -tilemap.origin.y;
        for (int i = tilemap.origin.x; i < tilemap.origin.x + tilemap.size.x; ++i)
        {
            tiles.Add(new List<MyTile>());
            for (int j = tilemap.origin.y; j < tilemap.origin.y + tilemap.size.y; ++j)
            {
                Vector3Int v = new(i, j);
                MyTile tile = ScriptableObject.CreateInstance<MyTile>();
                tile.SetPosition(v);
                if (ground.Contains(v))
                {
                    tile.SetTraversable(false);
                }
                tiles[k].Add(tile);
            }
            k++;
        }
        for (int i = 0; i < tiles.Count; ++i)
        {
            for (int j = 0; j < tiles[i].Count; ++j)
            {
                if (i!=0)
                {
                    tiles[i][j].AddNeighbours(tiles[i - 1][j]);
                }
                if(i != tiles.Count - 1)
                {
                    tiles[i][j].AddNeighbours(tiles[i+1][j]);
                }
                if (j != 0)
                {
                    tiles[i][j].AddNeighbours(tiles[i][j - 1]);
                }
                if (j != tiles[i].Count - 1)
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
            pathPos.AddRange(FindPath(FindTile(tuple.Item1), FindTile(tuple.Item2)));
        }
        //pathPos = FindPath(FindTile(tuples[0].Item1), FindTile(tuples[0].Item2)); //Debug purpose
        return pathPos;
    }

    private MyTile FindTile(Vector3Int pos)
    {
        return (tiles[pos.x + diffPosX][pos.y + diffPosY]);
    }

    private int CompareMyTiles(MyTile n1, MyTile n2)
    {
        if (n1.GetHeuristique() < n2.GetHeuristique())
        {
            return 1;
        }
        else if (n1.GetHeuristique() == n2.GetHeuristique())
        {
            return 0;
        }
        return -1;
    }

    private List<Vector3Int> FindPath(MyTile start, MyTile stop)
    {
        List<MyTile> closedList = new();
        PriorityQueue<MyTile> openList = new();

        start.SetCout(0);
        start.SetHeuristique(start.getCout() + (int)Vector3Int.Distance(start.Getposition(), stop.Getposition()));
        openList.Enqueue(start);

        //Debug.Log("Start : " + start.Getposition() + " stop : " + stop.Getposition());
        int aaa = 0;

        while(openList.Count != 0)
        {
            aaa++;
            if(aaa==1000000)
            {
                List<Vector3Int> p = new();
                /*foreach (MyTile m in closedList)
                {
                    p.Add(m.Getposition());
                }*/
                Debug.Log("arret du pathfinding");
                return p;
            }
            MyTile u = openList.Dequeue();
            if(u.Getposition() == stop.Getposition())
            {
                return ReconstituatePath(u, openList.ToList());
            }
            foreach (MyTile v in u.GetNeihgbours())
            {
                if (!v.IsTraversable() && v.Getposition() != stop.Getposition())
                {
                    continue;
                }
                if (closedList.Contains(v))
                {
                    continue;
                }
                if (v.getCout() <= u.getCout() + 1)
                {
                    continue;
                }
                v.SetCout(u.getCout() + 1);
                v.SetHeuristique(v.getCout() + (int)Vector3Int.Distance(v.Getposition(), stop.Getposition()));
                openList.Enqueue(v);
            }
            closedList.Add(u);
        }
        throw new Exception("Aucun chemin trouvé" );

        /*int diffX = stop.x - start.x;
        int diffY = stop.y - start.y;
        List<Vector3Int> path = new();
        path.Add(start);
        Vector3Int progress = start;
        int x = ChooseStep(diffX);
        int y = ChooseStep(diffY);

        RecursivePath(ground, start, stop, ref path, diffX, diffY, x, y);*/

        

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
        }
        return path;*/
    }
    private void ResetTiles(List<List<MyTile>> myList)
    {
        foreach(List<MyTile> l in myList)
        {
            ResetTiles(l);
        }
    }

    private void ResetTiles(List<MyTile> myList)
    {
        foreach(MyTile m in myList)
        {
            m.SetCout (int.MaxValue);
            m.SetHeuristique(int.MaxValue);
        }
    }

    private List<Vector3Int> ReconstituatePath(MyTile u, List<MyTile> openList)
    {
        List<Vector3Int> path = new();
        MyTile next = u;
        path.Add(next.Getposition());
        
        while (next.getCout() != 0)
        {
            foreach(MyTile t in next.GetNeihgbours())
            {
                if (t.getCout() == next.getCout() - 1)
                {
                    next = t;
                    break;
                }
            }
            path.Add(next.Getposition());
        }
        ResetTiles(tiles);
        return path;
    }

    /*private void RecursivePath(List<Vector3Int> ground, Vector3Int progress, Vector3Int stop, ref List<Vector3Int> path, int diffX, int diffY, int x, int y)
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
        if (diffX != 0 || diffY != 0)
        {
            RecursivePath(ground, progress, stop, ref path, diffX, diffY, x, y);
        }
    }*/

    /*private void RemovePath(int startIterator, ref List<Vector3Int> path, ref int diff, int direction)
    {
        int nbr = (path.Count - startIterator) / 4;
        if (nbr > 3)
        {
            nbr = 3;
        }
        diff -= nbr * direction;
        path.RemoveRange(path.Count - nbr, nbr);
    }*/

    /*private int ChooseStep(int diff)
    {
        if (diff == 0)
        {
            return 0;
        }
        return diff / Mathf.Abs(diff);
    }*/

    private List<(Vector3Int, Vector3Int)> CreateTuples()
    {
        List<(Vector3Int, Vector3Int)> tuples = new();
        Room exit = rooms[0];
        Room boss = rooms[1];
        tuples.Add(exit.CreateNeighbour(boss));
        while (!NeighboursCreated())
        {
            foreach (Room room in rooms)
            {
                if (!room.NeighboursCreated())
                {
                    tuples.Add(room.CreateNeighbour(GetNextRoom(room)));
                }
            }
        }

        CreateLinks(tuples);

        return tuples;
    }

    private Room GetNextRoom (Room room)
    {
        return GetNextRoom(room, rooms);
    }

    private Room GetNextRoom(Room room, List<Room> listRooms)
    {
        Room returnRoom = null;
        float maxDistance = float.MaxValue;
        foreach (Room r in listRooms)
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

    private void CreateLinks(List<(Vector3Int, Vector3Int)> tuples) //TODO: IsRelated
    {
        List<Room> notLinked = new();
        List<Room> linked = new();
        Room room, nextRoom;

        while (!IsLinked(ref linked, ref notLinked))
        {
            room = ChooseRoom(linked);
            if (room == null)
            {
                Debug.Log("Impossible de lier toutes les salles : room");
                return;
            }
            nextRoom = GetNextRoom(room, notLinked);
            if (nextRoom == null)
            {
                Debug.Log("Impossible de lier toutes les salles : nextRoom");
                return;
            }
            tuples.Add(room.CreateNeighbour(nextRoom));
        } 
    }

    private Room ChooseRoom(List<Room> linked)
    {
        Room room;
        
        do
        {
            if (linked.Count == 0)
            {
                return null;
            }
            room = linked[UnityEngine.Random.Range(0, linked.Count)];
            linked.Remove(room);
        }
        while (!room.HasOpenDoor());
        return room;
    }

    private bool IsLinked(ref List<Room> linked, ref List<Room> notLinked)
    {
        List<Room> queue = new();
        Room room;

        notLinked = new(rooms);
        linked.Clear();
        queue.Clear();
        queue.Add(rooms[0]);

        while (queue.Count != 0)
        {
            room = queue[0];
            queue.RemoveAt(0);
            linked.Add(room);
            notLinked.Remove(room);
            foreach (Room r in room.GetNeighbours())
            {
                if (!linked.Contains(r) && !queue.Contains(r))
                {
                    queue.Add(r);
                    notLinked.Remove(r);
                }
            }
        }
        if (notLinked.Count == 0)
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
