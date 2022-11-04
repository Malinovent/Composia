using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Some Note", menuName = "Item Menu/Note Item")]
public class NoteObject : ItemObject
{
    public NoteTypeEnum noteType;
    public void Awake()
    {
        type = ItemType.Note;
    }

    public override void Collect(int amount)
    {
        Events_Global.OnGetNote(this, amount);
    }
}
