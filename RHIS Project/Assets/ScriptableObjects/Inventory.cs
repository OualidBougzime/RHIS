using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : ScriptableObject
{
    public List<ItemObject> Objects = new List<ItemObject>();

    public void Add (ItemObject objectToAdd) { Objects.Add(objectToAdd); }
}
