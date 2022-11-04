using UnityEngine;
using UnityEditor;
using System.Linq;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(RequireInterfaceAttribute))]
public class RequireInterfaceDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //Check if this is reference type property
        if (property.propertyType == SerializedPropertyType.ObjectReference)
        {          
            //Debug.Log(arraySizeProp);

            Rect propertyPosition = position;
            if (property.objectReferenceValue != null) 
            { 
                propertyPosition.width -= 20;
            }

            //Get attribute parameters
            var requiredAttribute = this.attribute as RequireInterfaceAttribute;

            //Begin drawing property field
            EditorGUI.BeginProperty(propertyPosition, label, property);

            //Draw property field
            var reference = EditorGUI.ObjectField(propertyPosition, label, property.objectReferenceValue, requiredAttribute.requiredType, true);

            var obj = EditorGUI.ObjectField(propertyPosition, label, property.objectReferenceValue, typeof(Object), true);

            if (obj is GameObject g)
            {
                reference = g.GetComponent(requiredAttribute.requiredType);
            }
            
            property.objectReferenceValue = reference;
            
            //Finish drawing property field
            EditorGUI.EndProperty();

            if (property.objectReferenceValue != null)
            {
                Rect buttonRect = new Rect(position.xMax - position.height, position.y, position.height, position.height);
                //position.xMax
                if (GUI.Button(buttonRect, new GUIContent("X")))
                {
                    property.objectReferenceValue = null;
                }
            }
        }
        else
        {
            //If field is not reference, show error message.
            //Save previous color and change GUI to red
            var previousColor = GUI.color;
            GUI.color = Color.red;

            EditorGUI.LabelField(position, label, new GUIContent("Property is not a reference type!!"));

            //Revert color change.
            GUI.color = previousColor;
        }
    }
}
