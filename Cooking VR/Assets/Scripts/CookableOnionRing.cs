using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookableOnionRing : CookableFood
{
    [SerializeField]
    Material[] mats;
    
    [SerializeField]
    MeshRenderer meshRenderer;

    byte stage1 = byte.MaxValue;
    byte stage2 = 0;
    byte stage3 = 10;
    byte stage4 = 20;
    

    // Start is called before the first frame update
    void Start()
    {
        if(this.meshRenderer == null)
        {
            this.meshRenderer = this.GetComponent<MeshRenderer>();
        }
        AssignStageRefs();
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

    // audio
    protected void OnTriggerExit(Collider other)
    {
        if (other.tag == "Fryer")
        {
            AkSoundEngine.PostEvent("Oil_Fry_Stop", gameObject);
        }
    }

    public override void StartCook()
    {
        base.StartCook();

        //audio
        AkSoundEngine.PostEvent("Oil_Fry_Start", gameObject);
    }

    protected override void AssignStageRefs()
    {
        this.stageRefs = new ushort[] { stage1, stage2, stage3, stage4, stage4 };
        this.stages = (byte)mats.Length;
    }

    protected override void SwitchVisualObject()
    {
        this.meshRenderer.material = mats[currentStage];
    }
}
