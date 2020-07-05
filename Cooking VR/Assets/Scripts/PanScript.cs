using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class PanScript : MonoBehaviour, IGrabbable
{
    [SerializeField]
    private bool _IsHeated;
    public bool IsHeated
    {
        get
        { 
            return _IsHeated; 
        } 
        private set 
        { 
            _IsHeated = value; 
            BroadcastBurner();
        } 
    }

    public byte testHeated;

    [SerializeField]
    private bool isSnapped = false;
    
    [SerializeField]
    Transform bottomPanPositon;

    [SerializeField]
    private List<CookableFood> itemsInTrigger;

    public Interactable interactable { get; set; }

    public bool wasGrabbed { get; set; }

    private void Start()
    {
        InitGrabbable();

        if (bottomPanPositon == null)
            bottomPanPositon = this.GetComponentInChildren<Transform>();// this.GetComponent<SphereCollider>().transform.position;

        Vector3 position = bottomPanPositon.TransformVector(bottomPanPositon.position);

        itemsInTrigger = new List<CookableFood>();
    }

    public void InitGrabbable()
    {
        this.interactable = this.GetComponent<Interactable>();
        wasGrabbed = false;
    }

    private void Update()
    {
        CheckGrabbed();
        //if(testHeated == 1)
        //{
        //    //"just turned it on"
        //    StartCooking();
        //    testHeated++;

        //}
        //else if (testHeated == 2)
        //{
        //    // stayed on
        //}
        //else if(testHeated == 0)
        //{
        //    StopCooking();
        //}
    }

    public void CheckGrabbed()
    {
        if (this.interactable.attachedToHand)
        {
            wasGrabbed = true;
        }

        if (wasGrabbed && !this.interactable.attachedToHand)
        {
            UnSnap();
            this.wasGrabbed = false;
        }
    }

    public void AddItemInTrigger(CookableFood item)
    {
        if(!itemsInTrigger.Contains(item))
        {
            itemsInTrigger.Add(item);
            Debug.Log($"{item.gameObject.name} was added to the panScript's list of items in trigger");
            if (this._IsHeated)
            {
                item.StartCook();
                Debug.Log($"{item.gameObject.name} was told to start cooking");
            }
        }
    }

    public void RemoveItemInTrigger(CookableFood item)
    {
        if (itemsInTrigger.Contains(item))
        {
            itemsInTrigger.Remove(item);
            item.stopCook();
        }
        
    }

    public void BroadcastBurner()
    {
        foreach(var v in itemsInTrigger)
        {
            if (IsHeated)
                v.StartCook();
            else
                v.stopCook();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Burner") && !isSnapped)
        {
            isSnapped = true;
            SnapToBurner(other);
        }
        else if(other.TryGetComponent<CookableFood>(out CookableFood cf))
        {
            this.AddItemInTrigger(cf);
        }
    }

    private void SnapToBurner(Collider other)
    {
        Quaternion rot = this.gameObject.transform.rotation;
        this.gameObject.transform.rotation = new Quaternion(0, rot.y, 0, rot.w);
        this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.gameObject.GetComponent<Rigidbody>().isKinematic = true;

        float distanceToOffsetX = other.transform.position.x - bottomPanPositon.position.x; //bottomPanPositon.position.x - other.transform.position.x;
        float distanceToOffsetZ = other.transform.position.z - bottomPanPositon.position.z;//bottomPanPositon.position.z - other.transform.position.z;
        float distanceToOffsetY = other.transform.position.y - bottomPanPositon.position.y;
        //Vector3.Distance(this.gameObject.transform.position, bottomPanPositon);
        //Debug.Log($"Distance is {distanceToOffsetZ}");

        this.transform.position = new Vector3(this.transform.position.x + distanceToOffsetX, this.gameObject.transform.position.y + distanceToOffsetY, this.transform.position.z + distanceToOffsetZ);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Burner"))
        {
            UnSnap();
        }
        else if (other.TryGetComponent<CookableFood>(out CookableFood cf))
        {
            this.RemoveItemInTrigger(cf);
        }
    }

    private void UnSnap()
    {
        isSnapped = false;
        this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }

    public void StartCooking()
    {
        IsHeated = true;
    }

    public void StopCooking()
    {
        IsHeated = false;
    }

}
