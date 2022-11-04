using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Rough script to control the player during creation phase
    //Manage input
    //Manage player action

    private Drone_Deplacement _drone { get { return GameObject.FindObjectOfType<Drone_Deplacement>(); } }

    public Rigidbody _rigidbody;

    public float _speed;
    public bool _canWalk;

    public float _jumpForce;
    public float _lowJumpMultiplier;
    public float _fallMultiplier;

    public float _spaceIncreased;
    public float _spaceIncreaseLimit;

    private void Update()
    {
        //Move
        if (Input.GetKey(KeyCode.Space))
            _canWalk = true;

        if (_canWalk)
            _rigidbody.velocity = Vector3.right * (_drone._speed);

        //Jump
        if (Input.GetKey(KeyCode.Space))
        {
            _rigidbody.velocity = Vector3.up * _jumpForce;
        }

        //More manage jump
        if (_rigidbody.velocity.y < 0)
        {
            _rigidbody.velocity += Vector3.up * Physics.gravity.y * (_fallMultiplier - 1) * Time.deltaTime;
        }
        else if (!Input.GetKey(KeyCode.Space) && _rigidbody.velocity.y > 0)
        {
            _rigidbody.velocity += Vector3.up * Physics.gravity.y * (_lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private void CalculateSpeed(float distance, float nbrBeats, float _bpm)
    {
        float beatDistance = distance / nbrBeats;

    }

}
