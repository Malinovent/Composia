using UnityEngine;

[System.Serializable]
public class InventoryItem_Note
{

    [SerializeField] private NoteObject noteObject;
    [SerializeField] private int m_amount;

    public NoteObject GetNoteObject()
    {
        return noteObject;
    }
    public int GetAmount()
    {
        return m_amount;
    }
    public void SetAmount(int num)
    {
        m_amount += num;
        if (m_amount < 0) { m_amount = 0; }
        Debug.Log("Adding " + num + " to " + noteObject.type + " note. Amount is now: " + m_amount);
    }
    
}
