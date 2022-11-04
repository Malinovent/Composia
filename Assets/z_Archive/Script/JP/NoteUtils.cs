using UnityEngine;

public static class NoteUtils
{

    public static int ConvertNoteToSize(NoteTypeEnum type)
    {
        switch (type)
        {
            case NoteTypeEnum.QuarterNote:
                return 4;
            case NoteTypeEnum.QuarterRest:
                return 4;
            case NoteTypeEnum.WholeNote:
                return 16;
            case NoteTypeEnum.HalfNote:
                return 8;
            case NoteTypeEnum.HalfRest:
                return 8;
            case NoteTypeEnum.EighthNote:
                return 2;
            case NoteTypeEnum.EigthRest:
                return 2;
            case NoteTypeEnum.SixteenthNote:
                return 1;
        }

        Debug.Log("No such type");
        return 0;
    }

    public static NoteTypeEnum ConvertSizeToNote(int size, NotesEnum note)
    {
        switch (size)
        {
            case 1:
                return NoteTypeEnum.SixteenthNote;
            case 2:
                if (note == NotesEnum.Rest)
                {
                    return NoteTypeEnum.EigthRest;
                }
                else return NoteTypeEnum.EighthNote;
            case 4:
                if (note == NotesEnum.Rest)
                {
                    return NoteTypeEnum.QuarterRest;
                }
                else return NoteTypeEnum.QuarterNote;
            case 8:
                if (note == NotesEnum.Rest)
                {
                    return NoteTypeEnum.HalfRest;
                }
                else return NoteTypeEnum.HalfNote;
            case 16:
                return NoteTypeEnum.WholeNote;
        }

        Debug.Log("No such type");
        return NoteTypeEnum.QuarterNote;
    }

    public static string ConvertYToNoteString(int y)
    {
        switch (y)
        {
            case 0:
                return "B3";
            case 1:
                return "C4";
            case 2:
                return "D4";
            case 3:
                return "E4";
            case 4:
                return "F4";
            case 5:
                return "G4";
            case 6:
                return "A4";
            case 7:
                return "B4";
            case 8:
                return "C5";
            case 9:
                return "D5";
            case 10:
                return "E5";
            case 11:
                return "F5";
        }

        return "B3";
    }

    public static NotesEnum ConvertYToNote(int y)
    {
        switch (y)
        {
            case 0:
                return NotesEnum.B3;
            case 1:
                return NotesEnum.C4;
            case 2:
                return NotesEnum.D4;
            case 3:
                return NotesEnum.E4;
            case 4:
                return NotesEnum.F4;
            case 5:
                return NotesEnum.G4;
            case 6:
                return NotesEnum.A4;
            case 7:
                return NotesEnum.B4;
            case 8:
                return NotesEnum.C5;
            case 9:
                return NotesEnum.D5;
            case 10:
                return NotesEnum.E5;
            case 11:
                return NotesEnum.F5;
        }

        return NotesEnum.B3;
    }

    public static int ConvertNoteToY(NotesEnum note)
    {
        switch (note)
        {
            case NotesEnum.B3:
                return 0;
            case NotesEnum.C4:
                return 1;
            case NotesEnum.D4:
                return 2;
            case NotesEnum.E4:
                return 3;
            case NotesEnum.F4:
                return 4;
            case NotesEnum.G4:
                return 5;
            case NotesEnum.A4:
                return 6;
            case NotesEnum.B4:
                return 7;
            case NotesEnum.C5:
                return 8;
            case NotesEnum.D5:
                return 9;
            case NotesEnum.E5:
                return 10;
            case NotesEnum.F5:
                return 11;
        }

        return 0;
    }

}
