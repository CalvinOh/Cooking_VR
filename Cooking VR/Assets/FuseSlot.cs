using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseSlot : MonoBehaviour
{
    [SerializeField]
    private Fuse AttachedFuse;

    private Collider MyCollider;
    private FuseBox MyFuseBox;

    private void Awake()
    {
        MyCollider = GetComponent<BoxCollider>();
        MyFuseBox = GetComponentInParent<FuseBox>();
    }



    public void RecieveFuse(Fuse a)
    {
        AttachedFuse = a;
        MyCollider.enabled = false;
        AttachedFuse.MySlot = this;
    }

    public void RemoveFuse()
    {
        AttachedFuse = null;
        MyCollider.enabled = true;
        //AttachedFuse.MySlot = null;
        if (!MyFuseBox.MyEventScript.CurrentlyActive)
            MyFuseBox.MyEventScript.ManualTrigger();
    }

    public bool CheckFuse()
    {
        if(AttachedFuse!=null)
        return !AttachedFuse.Broken;
        return false;
    }

    public void BreakFuse(bool Broken)
    {
        
        AttachedFuse.Break(Broken);
    }
}
