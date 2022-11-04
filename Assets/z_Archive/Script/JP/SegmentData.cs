using System.Collections.Generic;

[System.Serializable]
public struct SegmentData
{
    public string myName;
    public int timeSignature;
    public int originalBPM;
    public List<NoteData> notesInOrder;
    //public List<NoteData> localNotes;

    public SegmentData(string myName, int timeSignature, int originalBPM, List<Composia.NotePiece> notes/*, List<Composition.NotationPiece> localNotes*/)
    {
        this.myName = myName;
        this.timeSignature = timeSignature;
        this.originalBPM = originalBPM;
        this.notesInOrder = new List<NoteData>();
        //this.localNotes = new List<NoteData>();

        foreach (Composia.NotePiece p in notes)
        {
            NoteData note = new NoteData(p.x, p.y, p.type);
            this.notesInOrder.Add(note);

            /*//Copy note into local
            if (localNotes.Contains(p))
            {
                this.localNotes.Add(note);
                UnityEngine.Debug.Log("Added " + note + " to local list");
            }*/
        }
    }

    public SegmentData(string myName, int timeSignature, int originalBPM, List<Note> notes)
    {
        this.myName = myName;
        this.timeSignature = timeSignature;
        this.originalBPM = originalBPM;
        this.notesInOrder = new List<NoteData>();

        int j = 0;
        for (int i = 0; i < notes.Count; i++)
        {
            int size = notes[i].length;
            int x = size + j;
            j += size;
            int y = NoteUtils.ConvertNoteToY(notes[i].note);
            NoteTypeEnum type = NoteUtils.ConvertSizeToNote(size, notes[i].note);
            NoteData note = new NoteData(x, y, type);
            this.notesInOrder.Add(note);
        }
    }
}

[System.Serializable]
public struct NoteData
{
    public int x;
    public int y;
    public NoteTypeEnum type;

    public NoteData(int x, int y, NoteTypeEnum type)
    {
        this.x = x;
        this.y = y;
        this.type = type;
    }
}
