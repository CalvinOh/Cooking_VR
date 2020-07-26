using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseSlot : MonoBehaviour
{
    private Fuse AttachedFuse;

    private Collider MyCollider;

    private void Awake()
    {
        MyCollider = GetComponent<BoxCollider>();
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
    }

    public bool CheckFuse()
    {
        if(AttachedFuse!=null)
        return !AttachedFuse.Broken;
        return false;
    }
}
