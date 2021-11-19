using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : ScriptableObject
{
    private List<Door> doors;
    private List<Room> neighbours;
    private int nbrNeighbours;
    private Vector3Int position;


    internal void Create(List<Vector3Int> roomDoorPos)
    {
        doors = new();
        neighbours = new();
        foreach (Vector3Int doorPos in roomDoorPos)
        {
            Door door = ScriptableObject.CreateInstance<Door>();
            door.Create(doorPos);
            doors.Add(door);
        }
        nbrNeighbours = SetNbrNeighbours(doors.Count);
        position = SetPosition(doors);
    }

    private Vector3Int SetPosition(List<Door> doors)
    {
        int x = 0;
        int y = 0;
        foreach (Door door in doors)
        {
            Vector3Int position = door.GetPosition();
            x += position.x;
            y += position.y;
        }
        return new Vector3Int(x / 4, y / 4);
    }

    private int SetNbrNeighbours(int nbrDoors)
    {
        int random = UnityEngine.Random.Range(0, 100);
        switch (nbrDoors)
        {
            case 2:
                if (random<75)
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            case 3:
                switch(random)
                {
                    case < 25:
                        return 1;
                    case < 75:
                        return 2;
                    default:
                        return 3;
                }
            case 4:
                switch(random)
                {
                    case < 10:
                        return 1;
                    case < 70:
                        return 2;
                    case < 90:
                        return 3;
                    default:
                        return 4;
                }
            case 5:
                switch(random)
                {
                    case < 5:
                        return 1;
                    case < 30:
                        return 2;
                    case < 70:
                        return 3;
                    case < 95:
                        return 4;
                    default:
                        return 5;
                }
            default:
                return 1;
        }
    }

    internal bool HasOpenDoor()
    {
        foreach (Door door in doors)
        {
            if (!door.IsUsed())
            {
                return true;
            }
        }
        return false;
    }

    internal bool NeighboursCreated()
    {
        return nbrNeighbours <= neighbours.Count;
    }

    internal (Vector3Int,Vector3Int) CreateNeighbour(Room room)
    {
        if (neighbours.Contains(room))
        {
            throw new Exception("room is already a neigbour");
        }
        Door doorStart = GetNextDoor(room.GetPostition());
        if (doorStart == null)
        {
            throw new Exception("start null");
        }
        Door doorStop = room.GetNextDoor(doorStart.GetPosition());
        if (doorStop == null)
        {
            throw new Exception("stop null");
        }
        doorStart.ConnectTo(doorStop);
        doorStop.ConnectTo(doorStart);
        AddNeighbour(room);
        room.AddNeighbour(this);

        return (doorStart.GetPosition(), doorStop.GetPosition());
    }

    private void AddNeighbour(Room room)
    {
        if (neighbours.Contains(room))
        {
            throw new Exception("room is already a neigbour");
        }
        neighbours.Add(room);
    }

    internal Door GetNextDoor(Vector3Int position)
    {
        Door door = null;
        float maxDistance = float.MaxValue;
        foreach(Door d in doors)
        {
            if (d.IsUsed())
            {
                continue;
            }
            float distance = Vector3Int.Distance(d.GetPosition(), position);
            if (distance < maxDistance)
            {
                maxDistance = distance;
                door = d;
            }
        }
        return door;
    }

    internal float GetDistance(Room r)
    {
        return Vector3Int.Distance(position, r.GetPostition());
    }

    internal Vector3Int GetPostition()
    {
        return position;
    }
    internal List<Room> GetNeighbours()
    {
        return neighbours;
    }
}
