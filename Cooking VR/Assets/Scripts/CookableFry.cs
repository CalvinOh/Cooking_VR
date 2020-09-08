using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookableFry : CookableFood
{
    [SerializeField]
    Material[] mats;

    [SerializeField]
    MeshRenderer meshRenderer;

    byte stage1 = 0;
    byte stage2 = 10;
    byte stage3 = 20;
    byte stage4 = 30;

    // Start is called before the first frame update
    void Start()
    {
        if (this.meshRenderer == null)
        {
            this.meshRenderer = this.GetComponent<MeshRenderer>();
        }
        AssignStageRefs();
        this.currentStage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    //protected override void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Fryer")
    //    {
    //        if (this.meshRenderer.material == mats[0])
    //        {

    //        }
    //    }
    //}

    public override void StartCook()
    {
        base.StartCook();
        // Call the sound effect for the fryer
    }

    protected override void AssignStageRefs()
    {
        this.stageRefs = new ushort[] { stage1, stage2, stage3, stage4};
        this.stages = (byte)mats.Length;
    }

    protected override void SwitchVisualObject()
    {
        this.meshRenderer.material = mats[currentStage];
    }
}
