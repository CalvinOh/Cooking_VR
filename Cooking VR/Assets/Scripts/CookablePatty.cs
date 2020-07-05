using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class CookablePatty : CookableFood
{
    byte stage1 = 0;
    byte stage2 = 10;
    byte stage3 = 20;
    byte stage4 = 30;
    byte stage5 = 40;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        AssignStageRefs();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    protected override void AssignStageRefs()
    {
        base.AssignStageRefs();
        this.stageRefs = new ushort[] { stage1, stage2, stage3, stage4, stage5};
    }
}
