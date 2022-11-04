using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : Collectible
{
    public int amount;

    private void TriggerCurrencyEvent()
    {
        Events_Global.OnGetCurrency(amount);
    }
    public override void Collect(int amount = 1)
    {
        TriggerCurrencyEvent();
    }
}
