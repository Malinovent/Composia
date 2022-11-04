using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Composia
{
    public class InputJump : MonoBehaviour
    {
        [SerializeField] Rigidbody rb;
        [SerializeField] float jumpBurst = 750;
        [SerializeField] int maxJumps = 1;
        [SerializeField] float fallMultiplier = 2f;
        [SerializeField] float lowJumpMultiplier = 2.5f;

        [ReadOnly][SerializeField] private int _currentJumps = 0;
        [ReadOnly][SerializeField] private bool m_isInputHeld = false;
        [ReadOnly][SerializeField] private bool m_isGrounded = false;
        [ReadOnly][SerializeField] private bool m_isJumping = false;

        [Header("Floor Detection Parameters")]
        [ReadOnly][SerializeField]private float distToGround = 0;
        [SerializeField] private float distToGroundOffset = 0.1f;

        public bool IsInputHeld
        {
            get => m_isInputHeld;
            set
            {
                m_isInputHeld = value;
                if (m_isInputHeld && _currentJumps < maxJumps)
                {
                    JumpBurst();
                }
            }
        }
        
        public bool IsJumping 
        { 
            get => m_isJumping;
            set 
            {
                m_isJumping = value;
                //SooruEvents.IsJumping = m_isJumping;
            }
        }

        public bool IsGrounded 
        { 
            get => m_isGrounded;
            set 
            {
                m_isGrounded = value;

                SooruEvents.IsGrounded = m_isGrounded;
            }
        }

        //When appropriate, translate this into an abstract class to represent exploration movement vs platforming movement


        // Start is called before the first frame update
        void Start()
        {
            if (rb == null) { rb = GetComponent<Rigidbody>(); }
            distToGround = rb.GetComponent<Collider>().bounds.extents.y;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            JumpFall();
        }

        private bool DetectGrounded()
        {
            //Check if jumping
            if(rb.velocity.y > 1.1 || IsJumping)
            {
                Debug.Log("yVelocity is" + rb.velocity.y);
                return false;
            }



            //Check if falling 


            //Else return IsGrounded as true
            return true;

            //return Physics.Raycast(transform.position, -Vector3.up, distToGround + distToGroundOffset);
        }

        private void OnCollisionExit(Collision collision)
        {
            //Check if really exiting
            IsGrounded = DetectGrounded();          

            if(!IsGrounded)
            {
                if (_currentJumps == 0)
                {
                    _currentJumps++;
                }
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.contacts[0].normal.y > 0.8f && !IsGrounded)
            {
                Land();              
            }
        }

        private void Land()
        {
            Debug.Log("Landed!");
            _currentJumps = 0;
            IsGrounded = true;
            IsJumping = false;
            SooruEvents.OnStateChange(SooruState.Land);
        }

        public void GetInput(bool isHeld)
        {
            IsInputHeld = isHeld;
        }

        public void JumpFall()
        {
            if (!IsGrounded)
            {
                if (rb.velocity.y < 0)
                {
                    rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
                }
                else if (rb.velocity.y > 0 && !IsInputHeld)
                {
                    rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
                }
            }
        }

        public void JumpBurst()
        {
            _currentJumps++;
            rb.velocity = new Vector3(rb.velocity.x, jumpBurst, rb.velocity.z);
            IsJumping = true;
            //SooruEvents.OnJump();
            SooruEvents.OnStateChange(SooruState.Jump);
        }

        public Rigidbody GetRigidbody()
        {
            return rb;
        }

        private void OnDrawGizmos()
        {
            Color original = Gizmos.color;

            Gizmos.color = Color.red;

            Vector3 toPosition = this.transform.position;
            toPosition.y = distToGround - distToGroundOffset;

            Gizmos.DrawLine(transform.position, toPosition);

            Gizmos.color = original;
        }

        private void OnValidate()
        {
            distToGround = rb.GetComponent<Collider>().bounds.extents.y;
        }
    }
}