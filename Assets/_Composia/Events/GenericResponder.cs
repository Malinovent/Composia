using UnityEngine;
using System.Collections;

public abstract class GenericResponder : MonoBehaviour
{

    virtual protected void DoAction(EventResponse action, System.EventArgs args)
    {
        if (action.delay == 0.0f) DoImmediateAction(action, args);
        else StartCoroutine(DoDelayedAction(action, args));
    }

    virtual protected IEnumerator DoDelayedAction(EventResponse action, System.EventArgs args)
    {
        float delayTimer = action.delay;
        while (delayTimer > 0.0f)
        {
            delayTimer -= Time.deltaTime;
            //delayTimer -= TimeManager.FrameTime;
            yield return true;
        }
        DoImmediateAction(action, args);
    }

    virtual protected void DoImmediateAction(EventResponse action, System.EventArgs args)
	{
		switch(action.responseType)
		{
		    case EventResponseType.ACTIVATE_GAMEOBJECT:
			    action.targetGameObject.SetActive(true);	
			    break;
            case EventResponseType.ACTIVATE_GAMEOBJECT_AT_POSITION:
                action.targetGameObject.transform.position = transform.position;
                action.targetGameObject.SetActive(true);
                break;
		    case EventResponseType.DEACTIVATE_GAMEOBJECT:
			    action.targetGameObject.SetActive(false);	
			    break;
            case EventResponseType.TELEPORT:
                break;
            case EventResponseType.SWITCH_CAMERA_TARGET:        
                break;
            case EventResponseType.INTERPOLATE_CAMERA:          
                break;
            case EventResponseType.EVENT_FEEDBACK:
                action.targetFeedback.Activate();
                break;
            case EventResponseType.COLLECT_ITEM:
                (action.targetObject as ItemObject).Collect(action.intValue);
                break;
            case EventResponseType.COLLECT_CURRENCY:
                Events_Global.OnGetCurrency(action.intValue);
                break;
            case EventResponseType.COLLECT_CUSTOM:
                (action.targetComponent as Collectible).Collect();
                break;
            case EventResponseType.PLAY_ANIMATION:
                action.targetAnimator.Play(action.stringValue);
                break;
            case EventResponseType.TRIGGER_ANIMATION:
                //Debug.Log("Triggering Animation! " + action.animParameter.name);
                string parameterName = action.stringValue;
                switch (action.animParameterType)
                {
                    case (AnimatorControllerParameterType.Bool):
                        action.targetAnimator.SetBool(action.stringValue, action.boolValue);
                        break;
                    case (AnimatorControllerParameterType.Trigger):
                        action.targetAnimator.SetTrigger(parameterName);
                        break;
                    case (AnimatorControllerParameterType.Int):
                        action.targetAnimator.SetInteger(parameterName, action.intValue2);
                        break;
                    case (AnimatorControllerParameterType.Float):
                        action.targetAnimator.SetFloat(parameterName, action.floatValue);
                        break;
                }
            break;

            case EventResponseType.UI_UPDATETEXT:
                (action.targetComponent as TMPro.TextMeshPro).SetText(action.stringValue);
                break;
            case EventResponseType.UI_UPDATENUMBER:
                
                if (args is GenericEventArgs)
                {
                    GenericEventArgs noteArgs = (GenericEventArgs)args;
                    if (noteArgs == null) { return; }

                    TMPro.TextMeshProUGUI text = action.targetComponent as TMPro.TextMeshProUGUI;

                    if (action.boolValue)
                    {
                        int intText = int.Parse(text.text);
                        text.SetText((intText + action.intValue).ToString());
                    }
                    else
                    {
                        text.SetText(noteArgs.intValue.ToString());   
                    }
                }
                
                break;

        }
    }
}
