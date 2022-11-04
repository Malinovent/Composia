using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable 
{
    public void Interacting(float timeHeld);

    public void StopInteracting();

}
