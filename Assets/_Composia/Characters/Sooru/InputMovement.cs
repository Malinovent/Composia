using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Composia
{
    public class InputMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private float movementSpeed = 5f;
        [SerializeField] private float lookSpeed = 5f;
        private Vector2 m_moveVector = Vector2.zero;


        public Transform lookCamera;

        //When appropriate, translate this into an abstract class to represent exploration movement vs platforming movement

        // Start is called before the first frame update
        void Start()
        {
            if (rb == null) { rb = GetComponent<Rigidbody>(); }
        }

        private void FixedUpdate()
        {
            SetMovementDirection();
        }

        public void GetInput(Vector2 moveVector)
        {
            m_moveVector = moveVector;
        }

        public void SetMovementDirection()
        {
            Vector3 movementDirection = new Vector3(m_moveVector.x, 0, m_moveVector.y);
            

            if (lookCamera)
            {
                //this is the direction in the world space we want to move:
                movementDirection = (lookCamera.forward * m_moveVector.y * movementSpeed * Time.deltaTime) + (lookCamera.right * m_moveVector.x * movementSpeed * Time.deltaTime);
                rb.velocity = new Vector3(movementDirection.x, rb.velocity.y, movementDirection.z);
            }
            else 
            {
                rb.velocity = new Vector3(m_moveVector.x * movementSpeed * Time.deltaTime, rb.velocity.y, m_moveVector.y * movementSpeed * Time.deltaTime);
            }



            if (m_moveVector == Vector2.zero)
            {
                //SooruEvents.OnIdle();
                SooruEvents.OnStateChange(SooruState.Idle);
            }
            else
            {
                /* LOOK ROTATION */
                if (rb.velocity != Vector3.zero)
                {
                    movementDirection.Normalize();
                    Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                    toRotation.x = 0;
                    toRotation.z = 0;
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, lookSpeed * Time.deltaTime);
                }

                //SooruEvents.OnWalk(rb.velocity.magnitude / 2);
                SooruEvents.OnStateChange(SooruState.Walk);
            }
        }
    }
}
