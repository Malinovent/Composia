using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEngine.UI;

[CustomEditor(typeof(EventResponder), true)]
public class EventResponderInspector : Editor
{
    protected EventResponder myTarget;

    protected string[] types;

    protected string[] events;

    protected string[] preferences;

    protected Type type;
    protected System.Reflection.EventInfo eventInfo;
    protected Type parameterType;

    protected bool isShowingDefault = false;


    public override void OnInspectorGUI()
    {

        myTarget = (EventResponder)target;

        GameObject sender = (GameObject)EditorGUILayout.ObjectField(new GUIContent("Sender", "Add the Game Object that holds the target component."), myTarget.sender, typeof(GameObject), true);

        myTarget.sender = sender;

        if (myTarget.sender != null) types = EventUtils.GetComponentsOnGameObject(myTarget.sender);
        if (myTarget.sender == null) myTarget.sender = myTarget.gameObject;

        if (myTarget.sender != null)
        {
            string typeName = null;
            if (types == null) { types = EventUtils.GetComponentsOnGameObject(myTarget.sender); }
            int typeIndex = Array.IndexOf(types, myTarget.typeName);
            if (typeIndex == -1 || typeIndex >= types.Length) typeIndex = 0;

            if (types != null && types.Length > 0)
            {
                typeName = types[EditorGUILayout.Popup("Component", typeIndex, types)];
            }
            else
            {
                EditorGUILayout.HelpBox("No components found on this GameObject.", MessageType.Info);
            }

            myTarget.typeName = typeName;

            if (myTarget.typeName != null && myTarget.typeName.Length > 0)
            {
                events = EventUtils.GetEventNamesForType(myTarget.typeName);

                if (events != null && events.Length > 0)
                {
                    int eventIndex = Array.IndexOf(events, myTarget.eventName);
                    if (eventIndex == -1 || eventIndex >= events.Length) eventIndex = 0;
                    string name = events[EditorGUILayout.Popup("Event", eventIndex, events)];
                    myTarget.eventName = name;
                }
                else
                {
                    EditorGUILayout.HelpBox("No events found on this component.", MessageType.Info);
                }



            }
        }

        if (myTarget.actions != null)
        {
            for (int i = 0; i < myTarget.actions.Length; i++)
            {

                EditorGUILayout.BeginVertical("HelpBox");

                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (i == 0) GUI.enabled = false;
                if (GUILayout.Button("Move Up", EditorStyles.miniButtonLeft))
                {
                    EventResponse tmp = myTarget.actions[i - 1];
                    myTarget.actions[i - 1] = myTarget.actions[i];
                    myTarget.actions[i] = tmp;
                    break;
                }
                GUI.enabled = true;
                if (i == myTarget.actions.Length - 1) GUI.enabled = false;
                if (GUILayout.Button("Move Down", EditorStyles.miniButtonRight))
                {
                    EventResponse tmp = myTarget.actions[i + 1];
                    myTarget.actions[i + 1] = myTarget.actions[i];
                    myTarget.actions[i] = tmp;
                    break;
                }
                GUI.enabled = true;
                // Remove
                GUILayout.Space(4);
                bool removed = false;
                if (GUILayout.Button("Remove", EditorStyles.miniButton))
                {
                    myTarget.actions = myTarget.actions.Where(a => a != myTarget.actions[i]).ToArray();
                    removed = true;
                }
                GUILayout.EndHorizontal();
                if (!removed) DrawAction(myTarget, myTarget, myTarget.actions[i]);
                EditorGUILayout.EndVertical();
            }
        }

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        // Add new actions
        if (GUILayout.Button("Add Action"))
        {
            if (myTarget.actions == null)
            {
                myTarget.actions = new EventResponse[1];
            }
            else
            {
                // Copy and grow array
                EventResponse[] tmpActions = myTarget.actions;
                myTarget.actions = new EventResponse[tmpActions.Length + 1];
                Array.Copy(tmpActions, myTarget.actions, tmpActions.Length);
            }
        }
        EditorGUILayout.EndHorizontal();

        
        if (GUILayout.Button("Show Default Inspector"))
        {
            isShowingDefault = !isShowingDefault;
        }

        if (isShowingDefault)
        {
            DrawDefaultInspector();
        }
        
        //base.OnInspectorGUI();
    }

    /// <summary>
    /// Draws an event response action in the inspector.
    /// </summary>
    /// <param name="action">Action.</param>
    public static void DrawAction(EventResponder target, object responder, EventResponse action)
    {

        if (action == null) action = new EventResponse();

        // action.responseType = (EventResponseType) EditorGUILayout.EnumPopup( new GUIContent("Action Type", "The type of action to do when this event occurs."), action.responseType);
        // TODO No need to create this every update
        GUIContent[] popUps = new GUIContent[Enum.GetValues(typeof(EventResponseType)).Length];
        int i = 0;
        foreach (object t in Enum.GetValues(typeof(EventResponseType)))
        {
            popUps[i] = new GUIContent(((EventResponseType)t).GetName(), "");
            i++;
        }
        int actionIndex = (int)action.responseType;
        actionIndex = EditorGUILayout.Popup(new GUIContent("Action Type", "The type of action to do when this event occurs."), actionIndex, popUps);
        action.responseType = (EventResponseType)actionIndex;

        // Delay
        action.delay = EditorGUILayout.FloatField(new GUIContent("Action Delay", "how long to wait before doing the action."), action.delay);
        if (action.delay < 0.0f) action.delay = 0.0f;
        else if (action.delay > 0.0f) EditorGUILayout.HelpBox("If you use many events with delay you may notice some garbage collection issues on mobile devices", MessageType.Info);

        // Game Object
        if (action.responseType == EventResponseType.ACTIVATE_GAMEOBJECT || action.responseType == EventResponseType.DEACTIVATE_GAMEOBJECT)
        {
            action.targetGameObject = (GameObject)EditorGUILayout.ObjectField(new GUIContent("Game Object", "The game object that will be acted on"), action.targetGameObject, typeof(GameObject), true);
        }

        // Teleport
        if (action.responseType == EventResponseType.TELEPORT)
        {
            action.targetGameObject = (GameObject)EditorGUILayout.ObjectField(new GUIContent("Teleport Target", "The Transform position where the player will be teleported to."), action.targetGameObject, typeof(GameObject), true);
        }

        if (action.responseType == EventResponseType.SWITCH_CAMERA_TARGET)
        {
            action.targetGameObject = (GameObject)EditorGUILayout.ObjectField(new GUIContent("Game Object", "The new Camera Target"), action.targetGameObject, typeof(GameObject), true);
            action.cameraTime = EditorGUILayout.FloatField(new GUIContent("Time On Target", "How long the camera will look at the new target"), action.cameraTime);
        }

        if (action.responseType == EventResponseType.INTERPOLATE_CAMERA)
        {
            action.targetCamera = (Camera)EditorGUILayout.ObjectField(new GUIContent("Camera", "The Camera reference"), action.targetCamera, typeof(Camera), true);
            action.toCameraLerp = EditorGUILayout.FloatField(new GUIContent("Time moving to target", "How long the camera will stay on target"), action.toCameraLerp);
            action.toNormalLerp = EditorGUILayout.FloatField(new GUIContent("Time coming back to normal", "How long the camera will take to return to original state"), action.toNormalLerp);
        }

        if (action.responseType == EventResponseType.PLAY_ANIMATION)
        {
            action.targetAnimator = (Animator)EditorGUILayout.ObjectField(new GUIContent("Animator", "Object to Animate"), action.targetAnimator, typeof(Animator), true);

            if (action.targetAnimator == null)
            {
                EditorGUILayout.HelpBox(new GUIContent("There is no animator in this object"));
            }
            else
            {
                List<UnityEditor.Animations.AnimatorState> states = EditorUtils.GetAnimatorStateInfo(action.targetAnimator);
                int numOfAnimations = states.Count;
                          
                string[] animationNames = new string[numOfAnimations];

                for (int j = 0; j < numOfAnimations; j++)
                {
                    animationNames[j] = states[j].name;
                }

                action.intValue = EditorGUILayout.Popup("Animations", action.intValue, animationNames);
                action.stringValue = animationNames[action.intValue];
            }
        }

        if (action.responseType == EventResponseType.EVENT_FEEDBACK)
        {
            action.targetFeedback = (EventFeedback)EditorGUILayout.ObjectField(new GUIContent("Feedback"), action.targetFeedback, typeof(EventFeedback), true);

            if (action.targetFeedback == null)
            {
                EditorGUILayout.HelpBox(new GUIContent("There is no feedback in this object"));

                EditorGUILayout.BeginHorizontal();

                if (GUILayout.Button("FIND Feedback"))
                {
                    action.targetFeedback = target.gameObject.GetComponent<EventFeedback>();
                }

                if (GUILayout.Button("ADD Event Feedback"))
                {
                    action.targetFeedback = target.gameObject.AddComponent<EventFeedback>();
                }

                EditorGUILayout.EndHorizontal();
            }
        }

        if (action.responseType == EventResponseType.COLLECT_ITEM)
        {
            action.intValue = EditorGUILayout.IntSlider("Amount", action.intValue, 1, 50);
            action.targetObject = (ItemObject)EditorGUILayout.ObjectField(new GUIContent("Collectible"), action.targetObject, typeof(ItemObject), true);
        }

        if (action.responseType == EventResponseType.COLLECT_CURRENCY)
        {
            action.intValue = EditorGUILayout.IntSlider("Amount", action.intValue, 1, 150);
        }

        if (action.responseType == EventResponseType.COLLECT_CUSTOM)
        {
            action.intValue = EditorGUILayout.IntField("Amount", action.intValue);
            action.targetComponent = (Collectible)EditorGUILayout.ObjectField(new GUIContent("Collectible"), action.targetComponent, typeof(Collectible), true);
        }

        if (action.responseType == EventResponseType.TRIGGER_ANIMATION)
        {
            action.targetAnimator = (Animator)EditorGUILayout.ObjectField(new GUIContent("Animator", "Object to Animate"), action.targetAnimator, typeof(Animator), true);

            if (action.targetAnimator == null)
            {
                EditorGUILayout.HelpBox(new GUIContent("There is no animator in this object"));
            }
            else
            {
                action.targetAnimator.gameObject.SetActive(true);
                int numOfParameters = action.targetAnimator.parameters.Length;
                if (numOfParameters > 0)
                {
                    if (action.animParameters.Length < numOfParameters)
                    {
                        action.animParameters = new AnimatorControllerParameterType[numOfParameters];
                    }
                    for (int j = 0; j < numOfParameters; j++)
                    {
                        action.animParameters[j] = action.targetAnimator.parameters[j].type;
                    }
                }

                
                string[] triggerNames = new string[action.targetAnimator.parameters.Length];
                for (int j = 0; j < action.targetAnimator.parameters.Length; j++)
                {
                    triggerNames[j] = action.targetAnimator.parameters[j].name;
                }

                GUILayout.BeginHorizontal();
                if (action.animParameters.Length < action.intValue) { action.intValue = 0; }
                action.intValue = EditorGUILayout.Popup("Animation Parameters", action.intValue, triggerNames);
                action.stringValue = triggerNames[action.intValue];
                if (action.animParameters.Length > action.intValue)
                {
                    action.animParameterType = action.animParameters[action.intValue];

                    switch (action.animParameterType)
                    {
                        case (AnimatorControllerParameterType.Bool):
                            action.boolValue = EditorGUILayout.Toggle(action.boolValue, GUILayout.Width(15));
                            break;
                        case (AnimatorControllerParameterType.Trigger):
                            //Do nothing
                            break;
                        case (AnimatorControllerParameterType.Int):
                            action.intValue2 = EditorGUILayout.IntField(action.intValue2, GUILayout.MaxWidth(50));
                            break;
                        case (AnimatorControllerParameterType.Float):
                            action.floatValue = EditorGUILayout.FloatField(action.floatValue, GUILayout.MaxWidth(50));
                            break;
                    }
                }
                GUILayout.EndHorizontal();
            }
        }

        if (action.responseType == EventResponseType.UI_UPDATETEXT)
        {
            action.targetComponent = (Text)EditorGUILayout.ObjectField(new GUIContent("TMP PRO UI"), action.targetComponent, typeof(Text), true);
            action.stringValue = EditorGUILayout.TextField("Custom Text", action.stringValue);
        }

        if (action.responseType == EventResponseType.UI_UPDATENUMBER)
        {
            action.targetComponent = (TMP_Text)EditorGUILayout.ObjectField(new GUIContent("TMP PRO UI"), action.targetComponent, typeof(TMP_Text), true);
            action.boolValue = EditorGUILayout.Toggle("Custom Value", action.boolValue);
            
            if (action.boolValue)
            {
                action.intValue = EditorGUILayout.IntSlider("Amount", action.intValue, 1, 150);
            }
        }

    }

    private void OnValidate()
    {
        //EditorUtility.SetDirty(myTarget.gameObject);
    }

    private void OnDisable()
    {

    }

    private void OnDestroy()
    {
        //RemoveEvent();
    }

}
