using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{ 
    Note,
    Default,
}

public abstract class ItemObject : ScriptableObject, ICollectible
{
    public GameObject prefab;
    public ItemType type;
    public abstract void Collect(int amount = 1);
}
