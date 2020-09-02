using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class PanScript : Cooker, IGrabbable
{
    [SerializeField]
    private string tagToSnapTo;

    [SerializeField]
    private bool isSnapped = false;
    
    [SerializeField]
    Transform bottomPanPositon;
    
    public Interactable interactable { get; set; }

    public bool wasGrabbed { get; set; }

    protected override void Start()
    {
        base.Start();
        InitGrabbable();

        if (bottomPanPositon == null)
            bottomPanPositon = this.GetComponentInChildren<Transform>();// this.GetComponent<SphereCollider>().transform.position;

        Vector3 position = bottomPanPositon.TransformVector(bottomPanPositon.position);

        if (tagToSnapTo == null)
            tagToSnapTo = "Burner";
    }

    public void InitGrabbable()
    {
        this.interactable = this.GetComponent<Interactable>();
        wasGrabbed = false;
    }

    protected override void Update()
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
    
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if(other.CompareTag(tagToSnapTo) && !isSnapped)
        {
            isSnapped = true;
            SnapToBurner(other);

            if (tagToSnapTo == "Fryer")
                this.StartCooking();
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

        // audio
        AkSoundEngine.PostEvent("Impact_Pan", gameObject);
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        if (other.CompareTag(tagToSnapTo))
        {
            UnSnap();
        }
    }

    private void UnSnap()
    {
        isSnapped = false;
        this.gameObject.GetComponent<Rigidbody>().isKinematic = false;

        if (tagToSnapTo == "Fryer")
            this.StopCooking();
    }
}
