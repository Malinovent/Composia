using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Some Note", menuName = "Item Menu/Note Item")]
public class DefaultObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.Default;
    }

    public override void Collect(int amount = 1)
    {
        Debug.LogWarning("Did not collect anything, make sure to that this object can be collected");
    }
}
