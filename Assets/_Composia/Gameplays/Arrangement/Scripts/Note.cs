[System.Serializable]
public class Note 
{ 
    public NotesEnum note;
    public int length;

    public Note(NotesEnum note, int length)
    {
        this.note = note;
        this.length = length;
    }
}
