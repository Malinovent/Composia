using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SerializeReferenceTest : MonoBehaviour
{
    [SerializeReference, SubclassSelector(typeof(ICollectible))]
    public ICollectible collectible;

    [SerializeReference, SubclassSelector(typeof(IAnimal))]
    public IAnimal animal;

    [RequireInterface(typeof(IActivatable))]
    public Object objectReference;

    [RequireInterface(typeof(IActivatable))]
    [SerializeField] private Object complexReference;

    [RequireInterface(typeof(IActivatable))]
    public Object[] objectReferenceArray;

    [RequireInterface(typeof(IActivatable))]
    public List<Object> objectReferenceList;
    
    public IActivatable activatable => complexReference as IActivatable;

}
