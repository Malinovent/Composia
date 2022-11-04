using UnityEngine;
using System.Collections;

public class GenericEventArgs : System.EventArgs
{
	public string stringValue
    {
        get;
        protected set;
    }

    public int intValue
    {
        get;
        protected set;
    }
    public GenericEventArgs(string stringValue)
    {
        this.stringValue = stringValue;
    }

    public GenericEventArgs(int intValue)
    {
        this.intValue = intValue;
    }

    public GenericEventArgs(string stringValue, int intValue)
    {
        this.stringValue = stringValue;
        this.intValue = intValue;
    }

    public void Update(string stringValue, int intValue)
    {
        this.intValue = intValue;
        this.stringValue = stringValue;
    }
}

