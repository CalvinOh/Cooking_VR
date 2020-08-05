using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FryerScript : Cooker
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    public override void BroadcastCook()
    {
        foreach (var v in itemsInTrigger)
        {
            if (IsHeated)
            {
                if (v.currentStage == 1)
                    v.StartCook();
                else if(v.currentStage == 0)
                    v.GetComponent<CookableOnionRing>()
            }
            else
                v.stopCook();
        }
    }
}
