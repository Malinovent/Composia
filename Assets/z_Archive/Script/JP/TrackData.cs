using System.Collections.Generic;

[System.Serializable]
public struct TrackData
{
    public string myName;
    public SegmentData[] segments;

    public TrackData(SegmentData[] segments, string myName = "Default Track")
    {
        this.myName = myName;
        this.segments = segments;
    }
}

