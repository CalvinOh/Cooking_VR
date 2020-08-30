using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookableOnionRing : CookableFood
{
    [SerializeField]
    Material[] mats;
    
    [SerializeField]
    MeshRenderer meshRenderer;

    byte stage1 = 0;
    byte stage2 = 10;
    byte stage3 = 20;
    

    // Start is called before the first frame update
    void Start()
    {
        if(this.meshRenderer == null)
        {
            this.meshRenderer = this.GetComponent<MeshRenderer>();
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Batter")
        {
            this.currentStage = 1;
            this.meshRenderer.material = mats[currentStage];
        }

        if (other.tag == "Fryer")
        {
            if(this.meshRenderer.material == mats[1])
            {

            }
        }
    }

    protected override void CheckIfSwitchVisual()
    {

    }

    protected override void AssignStageRefs()
    {
        base.AssignStageRefs();
        this.stageRefs = new ushort[] { stage1, stage2, stage3 };
    }
}
