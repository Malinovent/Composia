using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Composia
{
    [RequireComponent(typeof(Collider))]
    public class ActivatableObject_Trigger : ActivatableObject
    {
        public Collider triggerObject;

        private void OnTriggerEnter(Collider other)
        {
            if (other == triggerObject)
            {
                TryActivate();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other == triggerObject)
            {
                DeActivate();
            }
        }

    }
}