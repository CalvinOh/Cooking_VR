using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Fuse : MonoBehaviour
{
    
    public bool Broken;

    private bool Attached;

    [SerializeField]
    private GameObject BrokenCoil;

    [SerializeField]
    private GameObject IntactCoil;

    private List<GameObject> SlotsInRange;

    private Valve.VR.InteractionSystem.Interactable interactable;
    private bool WasHeld;
    private Rigidbody MyRigidbody;

    public FuseSlot MySlot;


    private void Awake()
    {
        MySlot = null;
        MyRigidbody = GetComponent<Rigidbody>();
        SlotsInRange = new List<GameObject>();
        interactable = GetComponent<Valve.VR.InteractionSystem.Interactable>();
        Break(false);
    }

    public void Break(bool broken)
    {
        
        Broken = broken;
        BrokenCoil.SetActive(broken);
        IntactCoil.SetActive(!broken);
    }

    private void Update()
    {
        if (interactable.attachedToHand != WasHeld)
        {
            if (WasHeld)//released
            {
                
                if (SlotsInRange.Count > 0)//check for slot in
                {
                    SlotIn(SlotsInRange[0].GetComponent<FuseSlot>());
                }
                else//no slots, released elsewhere
                {
                    MyRigidbody.isKinematic = false;
                    // turn rigidbody back to falling if not installed
                }

                

            }
            else//grabbed
            {
                Attached = false;
                if(MySlot!=null)
                MySlot.RemoveFuse();
                MySlot = null;
            }

            WasHeld = interactable.attachedToHand;
        }
    }

    public void SlotIn(FuseSlot SlotToSlotInto)
    {
        Attached = true;
        SlotToSlotInto.RecieveFuse(this);
        transform.parent = SlotToSlotInto.transform;
        transform.position = SlotToSlotInto.transform.position;
        transform.rotation = SlotToSlotInto.transform.rotation;
        MyRigidbody.isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FuseSlot"))
        {
            if (!SlotsInRange.Contains(other.gameObject))
            {
                SlotsInRange.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (SlotsInRange.Contains(other.gameObject))
        {
            SlotsInRange.Remove(other.gameObject);
        }
    }

}
