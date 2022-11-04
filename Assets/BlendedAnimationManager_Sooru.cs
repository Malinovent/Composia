using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Composia;

public class BlendedAnimationManager_Sooru : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody _playerRigidbody;

    private const string WALK = "isWalking";
    private const string IDLE = "isIdle";
    private const string INTERACT = "isInteract";
    private const string POW = "isPow";
    private const string GROUNDED = "isGrounded";
    private const string VELOCITY_Y = "yVelocity";
    private const string VELOCITY_XZ = "xzVelocity";

    [Tooltip("This is a multiplication factor. The smaller the number the slower the animation speed for the walk")]
    [SerializeField] private float walkAnimSpeedFactor = 1;

    public void OnEnable()
    {
        SooruEvents.StateChange += PlayAnimation;
    }

    public void OnDisable()
    {
        SooruEvents.StateChange -= PlayAnimation;
    }

    public void Awake()
    {
        if (!_animator) { GetComponent<Animator>(); }
        if (!_playerRigidbody) { _playerRigidbody = FindObjectOfType<InputJump>().GetRigidbody() ; }
        if (!_playerRigidbody) { Debug.LogWarning("There is no rigidbody attached to this blended animation component"); }
    }

    public void FixedUpdate()
    {
        GetYVelocity();
        GetGroundedVelocity();
    }

    public void DisableOtherAnimations(string animation)
    {
        foreach (AnimatorControllerParameter parameter in _animator.parameters)
        {
            if (parameter.name != animation && parameter.name != GROUNDED)
            {
                if (parameter.type == AnimatorControllerParameterType.Bool)
                {
                    _animator.SetBool(parameter.name, false);
                }              
            }
        }
    }

    public void DisableAllAnimations()
    {
        foreach (AnimatorControllerParameter parameter in _animator.parameters)
        {          
            if (parameter.type == AnimatorControllerParameterType.Bool)
            {
                _animator.SetBool(parameter.name, false);
            }          
        }
    }


    public void GetYVelocity()
    {
        if (!_playerRigidbody) { return; }

        if (!_animator.GetBool(GROUNDED))
        {
            _animator.SetFloat(VELOCITY_Y, _playerRigidbody.velocity.y * walkAnimSpeedFactor);
        }
    }

    public void GetGroundedVelocity()
    {
        if (!_playerRigidbody) { return; }

        if (_animator.GetBool(WALK))
        {
            _animator.SetFloat(VELOCITY_XZ, _playerRigidbody.velocity.magnitude * walkAnimSpeedFactor);
        }
    }

    public void PlayAnimation(SooruState state)
    {
        switch (SooruEvents.CurrentState)
        {
            case SooruState.Jump:
                AnimateJump();
                break;
            case SooruState.Idle:
                AnimateIdle();
                break;
            case SooruState.Interact:
                AnimateInteract();
                break;
            case SooruState.Pow:
                AnimatePow();
                break;
            case SooruState.Walk:
                AnimateWalk();
                break;
            case SooruState.Land:
                AnimateLand();
                break;
        }
    }


    #region ANIMATIONS
    private void Animate(string boolName)
    {
        DisableOtherAnimations(boolName);

        _animator.SetBool(boolName, true);
    }

    public void AnimateInteract()
    {
        Animate(INTERACT);
    }

    public void AnimateIdle()
    {
        Animate(IDLE);
    }

    public void AnimateWalk()
    {
        Animate(WALK);
    }

    public void AnimatePow()
    {
        Animate(POW);
    }

    public void AnimateJump()
    {
        DisableAllAnimations();
    }

    public void AnimateLand()
    {
        Animate(GROUNDED);
    }

    #endregion
}

