using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseBox : MonoBehaviour
{
    [SerializeField]
    private List<FuseSlot> AttachedFuseSlots;

    [SerializeField]
    private GameObject Switch;
    [SerializeField]
    private GameObject FusePrefab;

    private LightsOuts LightsOutEventScript;


    public LightsOuts MyEventScript;
    private PowerBoxSlider MySlider;

    private void Start()
    {
        LightsOutEventScript = FindObjectOfType<LightsOuts>();
        foreach (FuseSlot a in GetComponentsInChildren<FuseSlot>())
        {
            AttachedFuseSlots.Add(a);
        }

        SetUpBox();
        MySlider = GetComponentInChildren<PowerBoxSlider>();
        MyEventScript = FindObjectOfType<LightsOuts>();
    }

    private void SetUpBox()
    {
        foreach (FuseSlot a in AttachedFuseSlots)
        {
            GameObject NewFuse = Instantiate(FusePrefab, a.transform.position, a.transform.rotation);
            NewFuse.GetComponent<Fuse>().SlotIn(a);
        }
    }
         


    public void TriggerEvent(int BrokenFuseNum)
    {
        Debug.Log("Breaking "+ BrokenFuseNum + " Fuses");

        if (BrokenFuseNum == 0)
        {
            return;
        }
        else if (BrokenFuseNum == 1)
        {
            
            AttachedFuseSlots[Random.Range(0, 3)].BreakFuse(true);
        }
        else if (BrokenFuseNum == 2)
        {
           
            foreach (FuseSlot a in AttachedFuseSlots)
            {
                a.BreakFuse(true);
            }
            AttachedFuseSlots[Random.Range(0, 3)].BreakFuse(false);
        }
        else
        {
            
            foreach (FuseSlot a in AttachedFuseSlots)
            {
                a.BreakFuse(true);
            }
        }

        //move switch to off position and prepare other things for evennt start
        MySlider.ResetPosition();
    }

    public bool AttemptFix()
    {
        if (CheckFuses())
        {
            LightsOutEventScript.EndEvent();

            return true;
        }

        // sound
        return false;
    }

    private bool CheckFuses()
    {
        foreach (FuseSlot a in AttachedFuseSlots)
        {
            if (!a.CheckFuse())
                return false;
        }

        // sound come back on
        return true;
    }
}