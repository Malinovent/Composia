using UnityEngine;
using TMPro;

/// <summary>
/// Defines a repsonse to an event.
/// </summary>
[System.Serializable]
public class EventResponse
{
	/// <summary>
	/// Type of response.
	/// </summary>
	public EventResponseType responseType;

	public float delay;

    public GameObject targetGameObject;

    public Animator targetAnimator;

	public EventFeedback targetFeedback;

	public Camera targetCamera;

    public Component targetComponent;

	public ScriptableObject targetObject;

	public string message;

	public string overrideState;

	public Sprite newSprite;

	public AnimationState animationState;

	public int intValue;

    public int intValue2;

	public Vector2 vectorValue;

	public bool boolValue;

	public float floatValue;

    public float cameraTime;

    public float toCameraLerp;

    public float toNormalLerp;

    public string stringValue;

    public string stringValue2;

	[SerializeField]
    public AnimatorControllerParameterType animParameterType;

	[SerializeField]
	public AnimatorControllerParameterType[] animParameters = new AnimatorControllerParameterType[1];

    /// <summary>
    /// Initializes a new instance of the <see cref="PlatformerPro.EventResponse"/> class.
    /// </summary>
    public EventResponse()
	{

	}

	/// <summary>
	/// Initializes a new instance of the <see cref="PlatformerPro.EventResponse"/> class by cloning another instance.
	/// </summary>
	/// <param name="original">Original.</param>
	public EventResponse(EventResponse original)
	{
		this.responseType = original.responseType;	
		this.delay = original.delay;
		this.targetGameObject = original.targetGameObject;
        this.targetCamera = original.targetCamera;
		this.targetComponent = original.targetComponent;
        this.cameraTime = original.cameraTime;
        this.toCameraLerp = original.toCameraLerp;
        this.toNormalLerp = original.toNormalLerp;
		this.animParameterType = original.animParameterType;
		this.animParameters = original.animParameters;
	}

	/// <summary>
	/// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="PlatformerPro.EventResponse"/>.
	/// </summary>
	/// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="PlatformerPro.EventResponse"/>.</param>
	/// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
	/// <see cref="PlatformerPro.EventResponse"/>; otherwise, <c>false</c>.</returns>
	public override bool Equals (object obj)
	{
		if (obj == null)
			return false;
		if (ReferenceEquals (this, obj))
			return true;
		if (obj.GetType () != typeof(EventResponse))
			return false;
		EventResponse other = (EventResponse)obj;
		return responseType == other.responseType && delay == other.delay && targetGameObject == other.targetGameObject && targetComponent == other.targetComponent && message == other.message && overrideState == other.overrideState && newSprite == other.newSprite && animationState == other.animationState && intValue == other.intValue && vectorValue == other.vectorValue && boolValue == other.boolValue && floatValue == other.floatValue;
	}
		
	/// <summary>
	/// Serves as a hash function for a <see cref="PlatformerPro.EventResponse"/> object.
	/// </summary>
	/// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
	public override int GetHashCode ()
	{
		unchecked {
			return responseType.GetHashCode () ^ delay.GetHashCode () ^ (targetGameObject != null ? targetGameObject.GetHashCode () : 0) ^ (targetComponent != null ? targetComponent.GetHashCode () : 0) ^ (message != null ? message.GetHashCode () : 0) ^ (overrideState != null ? overrideState.GetHashCode () : 0) ^ (newSprite != null ? newSprite.GetHashCode () : 0) ^ animationState.GetHashCode () ^ intValue.GetHashCode () ^ vectorValue.GetHashCode () ^ boolValue.GetHashCode () ^ floatValue.GetHashCode ();
		}

    }
		

}
