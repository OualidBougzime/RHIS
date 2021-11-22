using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : ScriptableObject
{
    private List<Room> rooms = new();

    public List<Vector3Int> createAllPaths(List<List<Vector3Int>> allDoors)
    {
        SetPathfinder(allDoors);
        List<(Vector3Int, Vector3Int)> tuples = CreateTuples();
        List<Vector3Int> pathPos = new();
        
        foreach ((Vector3Int,Vector3Int) tuple in tuples)
        {
            pathPos.AddRange(FindPath(tuple.Item1, tuple.Item2));
        }

        return pathPos;
    }

    private List<Vector3Int> FindPath(Vector3Int start, Vector3Int stop)
    {
        int diffX = stop.x - start.x;
        int diffY = stop.y - start.y;
        return FindPath(start, stop, diffX, diffY);
        throw new NotImplementedException();
    }

    private List<Vector3Int> FindPath(Vector3Int start, Vector3Int stop, int diffX, int diffY) //TODO: Good path
    {
        int x;
        int y;
        List<Vector3Int> path = new();
        path.Add(start);
        Vector3Int progress = start;
        x = ChooseStep(diffX);
        y = ChooseStep(diffY);
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
        return path;
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
