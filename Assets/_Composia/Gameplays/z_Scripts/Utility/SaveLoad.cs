using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Composia;
using System.IO;

public static class SaveLoad
{
    static string filePath = Application.dataPath + "/SaveFiles/";

    public static void Save<T>(T data, string fileName)
    {
        string json = JsonUtility.ToJson(data);
        Debug.Log(json);

        File.WriteAllText(filePath + fileName + ".json", json);
    }

    private static T Load<T>(string fileName)
    {
        string json = File.ReadAllText(filePath + fileName + ".json");
        T loadedData = JsonUtility.FromJson<T>(json);

        return loadedData;
    }
    /*
    private static T ReadJson<T>(TextAsset textFile)
    {
        T loadData = JsonUtility.FromJson<T>(textFile.);
    }*/

    public static void SaveSegment(int timeSignature, int BPM, List<NotePiece> notes, string fileName = "defaultSegment")
    {
        SegmentData data = new SegmentData(fileName, timeSignature, BPM, notes);

        Save(data, fileName);
    }

    public static void SaveTrack(TrackData data, string fileName = "defaultTrack")
    {
        Save(data, fileName);
    }

    public static SegmentData LoadSegment(string fileName)
    {
        return Load<SegmentData>(fileName);
    }

    public static TrackData LoadTrack(string fileName)
    {
        return Load<TrackData>(fileName);
    }
  
}
