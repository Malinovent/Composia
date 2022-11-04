public enum EventResponseType
{
    ACTIVATE_GAMEOBJECT,
    DEACTIVATE_GAMEOBJECT,
    //PLAY_SFX,
    //PLAY_SONG,
    //STOP_SONG,
    PLAY_ANIMATION,
    TRIGGER_ANIMATION,
    STOP_ANIMATION,
    TELEPORT,
    ACTIVATE_GAMEOBJECT_AT_POSITION,
    SWITCH_CAMERA_TARGET,
    INTERPOLATE_CAMERA,
    EVENT_FEEDBACK,
    COLLECT_ITEM,
    COLLECT_CURRENCY,
    COLLECT_CUSTOM,
    UI_UPDATETEXT,
    UI_UPDATENUMBER,
    UI_DISPLAY,
    UI_HIDE
}


public static class EventResponseExtensions
{
    public static string GetName(this EventResponseType me)
    {
        switch (me)
        {
            case EventResponseType.ACTIVATE_GAMEOBJECT: return "Activation/Activate GameObject";
            case EventResponseType.ACTIVATE_GAMEOBJECT_AT_POSITION: return "Activation/Activate GameObject At Position";
            case EventResponseType.DEACTIVATE_GAMEOBJECT: return "Activation/Deactivate GameObject";

            case EventResponseType.SWITCH_CAMERA_TARGET: return "Camera/Switch Target";
            case EventResponseType.INTERPOLATE_CAMERA: return "Camera/Interpolate To Camera";
            case EventResponseType.PLAY_ANIMATION: return "Animation/Play Animations";
            case EventResponseType.EVENT_FEEDBACK: return "Feedback/GenericFeedback";
            case EventResponseType.COLLECT_ITEM: return "Collectibles/Collect Item";
            case EventResponseType.COLLECT_CURRENCY: return "Collectibles/Collect Currency";
            case EventResponseType.COLLECT_CUSTOM: return "Collectibles/Collect Custom";

            case EventResponseType.TRIGGER_ANIMATION: return "Animation/Trigger Animation";
            case EventResponseType.STOP_ANIMATION: return "Animation/Stop Animation";

            //case EventResponseType.UI_HIDE: return "UI/Hide Element";
            //case EventResponseType.UI_DISPLAY: return "UI/Display Element";
            case EventResponseType.UI_UPDATETEXT: return "UI/Update Text";
            case EventResponseType.UI_UPDATENUMBER: return "UI/Update Number";

                //case EventResponseType.PLAY_SFX: return "Effects/Sound/Play SFX";
                //case EventResponseType.PLAY_SONG: return "Effects/Sound/Play Song";
                //case EventResponseType.STOP_SONG: return "Effects/Sound/Stop Song";

        }
        return "Other/" + me;
    }
    public static string GetDescription(this EventResponseType me)
    {
        return "";
    }
}

