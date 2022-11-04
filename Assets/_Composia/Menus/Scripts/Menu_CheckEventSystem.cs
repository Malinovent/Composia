using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Menu_CheckEventSystem : MonoBehaviour
{
    //Check if event system exist
    private void Awake()
    {
        if (GameObject.FindObjectOfType<EventSystem>() != null)
        {
            Debug.Log("Canvas event system already exist, destroy this one " + this.transform.name );
            Destroy(this.gameObject);
        }
    }
}
