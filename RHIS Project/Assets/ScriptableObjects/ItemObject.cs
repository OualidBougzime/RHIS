using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType 
{
    Ammo,
    Food,
    Default
}

public abstract class ItemObject : ScriptableObject
{
    public GameObject prefabObject;
    public ItemType typeObject;
}
