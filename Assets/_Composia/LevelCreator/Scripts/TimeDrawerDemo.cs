using Composia.LevelCreator;
using UnityEngine;

public class TimeDrawerDemo : MonoBehaviour
{
    [Time]
    public int TimeMinutes = 300;
    [Time(true)]
    public int TimeHours = 3600;
    [Time]
    public float TimeError = 3600;
}
