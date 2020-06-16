using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class PanScript : MonoBehaviour, IGrabbable
{
    [SerializeField]
    public bool IsHeated { get { return IsHeated; } private set { IsHeated = value; BroadcastBurner();} }

    [SerializeField]
    private bool isSnapped = false;
    
    [SerializeField]
    Transform bottomPanPositon;

    private List<CookableFood> itemsInTrigger;

    public Interactable interactable { get; set; }

    public bool wasGrabbed { get; set; }

    private void Start()
    {
        InitGrabbable();

        if (bottomPanPositon == null)
            bottomPanPositon = this.GetComponentInChildren<Transform>();// this.GetComponent<SphereCollider>().transform.position;

        Vector3 position = bottomPanPositon.TransformVector(bottomPanPositon.position);

        Debug.Log($"bottom pan position: {position.ToString()}");
    }

    public void InitGrabbable()
    {
        this.interactable = this.GetComponent<Interactable>();
        wasGrabbed = false;
    }

    private void Update()
    {
        CheckGrabbed();
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
            itemsInTrigger.Add(item);
    }

    public void RemoveItemInTrigger(CookableFood item)
    {
        if (itemsInTrigger.Contains(item))
            itemsInTrigger.Remove(item);
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

        Debug.Log($"Collider position: {other.transform.position.ToString()}");
        Debug.Log($"bottom pan position: {bottomPanPositon.position.ToString()}");

        this.transform.position = new Vector3(this.transform.position.x + distanceToOffsetX, this.gameObject.transform.position.y + distanceToOffsetY, this.transform.position.z + distanceToOffsetZ);

        Debug.Log($"frying pan position: {this.transform.position.ToString()}");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Burner"))
        {
            UnSnap();
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
