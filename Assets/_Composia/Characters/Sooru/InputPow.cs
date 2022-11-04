using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Composia
{
    public class InputPow : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private float powForce = 15;

        // Start is called before the first frame update
        void Start()
        {
            if (!rb) { rb = GetComponent<Rigidbody>(); }
        }

        public void GetInput()
        {
            Pow();
        }

        private void Pow()
        {
            if (!SooruEvents.IsGrounded)
            {
                rb.velocity = new Vector3(rb.velocity.x, -powForce, rb.velocity.z);
            }
        }
    }
}