using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseBox : MonoBehaviour
{

    private List<Fuse> AttachedFuses;

    [SerializeField]
    private GameObject Switch;
    [SerializeField]
    private GameObject FusePrefab;

    public List<FuseSlot> Slots = new List<FuseSlot>(); //pulic for debugging
    private LightsOuts LightsOutEventScript;

    

    private void Start()
    {
        LightsOutEventScript = FindObjectOfType<LightsOuts>();
        foreach (FuseSlot a in GetComponentsInChildren<FuseSlot>())
        {
            Slots.Add(a);
        }

        SetUpBox();
    }

    private void SetUpBox()
    {
        foreach (FuseSlot a in Slots)
        {
            GameObject NewFuse = Instantiate(FusePrefab, a.transform.position, a.transform.rotation);
            NewFuse.GetComponent<Fuse>().SlotIn(a);
        }
    }
         


    public void TriggerEvent(int BrokenFuseNum)
    {
        if (BrokenFuseNum == 0)
        {
        }
        if (BrokenFuseNum == 1)
        {
            AttachedFuses[Random.Range(0, 2)].Break(true);
        }
        else if (BrokenFuseNum == 2)
        {
            foreach (Fuse a in AttachedFuses)
            {
                a.Break(true);
            }
            AttachedFuses[Random.Range(0, 2)].Break(false);
        }
        else
        {
            foreach (Fuse a in AttachedFuses)
            {
                a.Break(true);
            }
        }

        //move switch to off position and prepare other things for evennt start



    }

    public void AttemptFix()
    {
        if (CheckFuses())
        {
            LightsOutEventScript.EndEvent();
        }
    }

    private bool CheckFuses()
    {
        foreach (FuseSlot a in Slots)
        {
            if (!a.CheckFuse())
                return false;
        }
        return true;
    }

}
