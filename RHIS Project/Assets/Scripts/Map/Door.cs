using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : ScriptableObject
{
    private List<Door> connectedTo;
    private Vector3Int position;


    internal void Create(Vector3Int doorPos)
    {
        position = doorPos;
        connectedTo = new();
    }
    internal Vector3Int GetPosition()
    {
        return position;
    }

    internal bool IsUsed()
    {
        if (connectedTo.Count == 0)
        {
            return false;
        }
        return true;
    }

    internal void ConnectTo(Door doorStop)
    {
        if (connectedTo.Contains(doorStop))
        {
            throw new Exception("connectedTo contains already doorStop");
        }
        connectedTo.Add(doorStop);
    }
}
