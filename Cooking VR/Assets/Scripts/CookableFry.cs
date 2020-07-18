using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookableFry : CookableFood
{
    byte stage1 = 0;
    byte stage2 = 10;
    byte stage3 = 20;

    // Start is called before the first frame update
    void Start()
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
        this.stageRefs = new ushort[] { stage1, stage2, stage3 };
    }
}
