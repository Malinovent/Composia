using System;
using System.Linq;
using UnityEngine;
public static class EventUtils
{
    /// <summary>
    /// Returns the names of the events
    /// </summary>
    public static string[] GetEventNamesForType(string typeName)
    {
        Type type = typeof(EventManager).Assembly.GetType(typeName);

        if (type == null) type = typeof(EventManager).Assembly.GetTypes().Where(t => t.Name == typeName).FirstOrDefault();
        //Debug.Log("Found Type: " + type);
        if (type == null) return new string[0];

        System.Reflection.EventInfo[] events = type.GetEvents(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

        if (events == null) return new string[0];

        return type.GetEvents(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).Select(e => e.Name).OrderBy(s => s).ToArray();
    }

    /// <summary>
    /// Gets the type names of all components on a game object.
    /// </summary>
    /// <returns>The Components on game object.</returns>
    /// <param name="go">Go.</param>
    public static string[] GetComponentsOnGameObject(GameObject go)
    {
        return go.GetComponents(typeof(Component)).Select(c => c.GetType().Name).OrderBy(s => s).ToArray();
    }
}
