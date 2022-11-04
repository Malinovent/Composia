using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Composia
{
    public class WholeNotePiece : NotePiece
    {
        public float chargeValue = 4;



        public float CalculateChargeTime()
        {
            float chargeTime = TimeManager.Instance.BPS * chargeValue;

            return chargeTime;
        }
    }
}