using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MyTile : ScriptableObject, IComparable<MyTile>
{
    private List<MyTile> neighbours = new();
    private Vector3Int position;
    private int cout = int.MaxValue;
    private int heuristique = int.MaxValue;
    private bool traversable = true;
    private bool isPath = false;

    public void AddNeighbours(MyTile tile)
    {
        neighbours.Add(tile);
    }
    public List<MyTile> GetNeihgbours()
    {
        return neighbours;
    }
    public void SetPosition(Vector3Int pos)
    {
        position = pos;
    }
    public Vector3Int Getposition()
    {
        return position;
    }
    public void SetCout(int cout)
    {
        this.cout = cout;
    }
    public int getCout()
    {
        return cout;
    }
    public void SetHeuristique(int heuristique)
    {
        this.heuristique = heuristique;
    }
    public int GetHeuristique()
    {
        return heuristique;
    }
    public bool IsTraversable()
    {
        return traversable;
    }
    public void SetTraversable(bool t)
    {
        traversable = t;
    }

    public void SetIsPath(bool p)
    {
        isPath = p;
    }
    public bool IsPath()
    {
        return isPath;
    }

    public int CompareTo(MyTile other)
    {
        if (GetHeuristique() < other.GetHeuristique())
        {
            return -1;
        }
        else if (GetHeuristique() == other.GetHeuristique())
        {
            return 0;
        }
        return 1;
    }
}
