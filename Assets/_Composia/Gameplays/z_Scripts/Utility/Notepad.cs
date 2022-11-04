using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notepad : MonoBehaviour
{
    [TextArea]
    public List<string> notes = new List<string>();

}
