using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTile : ScriptableObject
{
    private List<MyTile> neighbours = new();
    private Vector3Int position;
    private int cout = 0;
    private int heuristique = 0;
    private bool traversable = true;

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

}
