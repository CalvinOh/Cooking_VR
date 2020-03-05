using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanScript : MonoBehaviour
{
    [SerializeField]
    private bool IsHeated;

    private PattyScript CurrentPattyOnPan;

    [SerializeField]
    private bool isSnapped = false;
    
    [SerializeField]
    Transform bottomPanPositon;

    private void Start()
    {
        if(bottomPanPositon == null)
            bottomPanPositon = this.GetComponentInChildren<Transform>();// this.GetComponent<SphereCollider>().transform.position;

        Vector3 position = bottomPanPositon.TransformVector(bottomPanPositon.position);

        Debug.Log($"bottom pan position: {position.ToString()}");
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Patty"))
        {
            CurrentPattyOnPan = other.GetComponent<PattyScript>();
            if(IsHeated)
            CurrentPattyOnPan.StartCooking();
        }
        else if(other.CompareTag("Burner") && !isSnapped)
        {
            isSnapped = true;
            Debug.Log($"is snapped = {isSnapped.ToString()}");
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
        if (other.CompareTag("Patty"))
        {
            CurrentPattyOnPan.StopCooking();
            CurrentPattyOnPan = null;
        }
        else if (other.CompareTag("Burner"))
        {
            isSnapped = false;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            Debug.Log($"is snapped = {isSnapped.ToString()}");
        }
    }

    public void StartCooking()
    {
        IsHeated = true;
        CurrentPattyOnPan.StartCooking();
    }

    public void StopCooking()
    {
        IsHeated = false;
        CurrentPattyOnPan.StopCooking();
    }

}
