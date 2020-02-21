using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldStacking : MonoBehaviour
{
    Collider collider;
    Transform location;


    bool stackable;
    [SerializeField]
    bool isStacking;

    [SerializeField]
    GameObject currentParent; //{ get { return this.transform.parent.gameObject; } }

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
        location = this.gameObject.transform;
        stackable = true;
        isStacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isStacking == true)
        {
            //this.transform.position = this.gameObject.transform.parent.transform.position;
            //this.transform.rotation = this.gameObject.transform.parent.transform.rotation;

            //this.transform.position = MasterParentPosition();
            //this.transform.rotation = MasterParentRotation();

            //this.gameObject.GetComponent<Rigidbody>().freezeRotation = true;
        }
  
  
    }

    private void OnCollisionEnter(Collision collision)
    {
        this.gameObject.GetComponent<Rigidbody>().velocity = collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        if(this.gameObject.transform.parent == null)
        {
            // If the other object is higher than the current object
            if (collision.gameObject.GetComponent<OldStacking>().stackable == true &&
                collision.gameObject.transform.position.y >= this.gameObject.transform.position.y)
            {
                if (collision.gameObject.transform.parent.gameObject != this.gameObject)
                {
                    collision.gameObject.transform.parent = this.MasterParentTransform(collision);
                    collision.gameObject.GetComponent<OldStacking>().currentParent = collision.gameObject.transform.parent.gameObject;
                    collision.gameObject.transform.rotation = this.MasterParentRotation(collision);//new Quaternion(0, collision.gameObject.transform.rotation.y, 0, collision.gameObject.transform.rotation.w);
                                                                                          //isStacking = true;
                }
            }
            else if (this.gameObject.GetComponent<OldStacking>().stackable == true &&
                collision.gameObject.transform.position.y < this.gameObject.transform.position.y)
                {
                    // If the current object is higher than the other object
                    if (this.gameObject.transform.parent != collision.gameObject.transform)
                    {
                        this.gameObject.transform.parent = collision.gameObject.transform;
                        currentParent = this.gameObject.transform.gameObject;
                        this.gameObject.transform.rotation = new Quaternion(0, this.gameObject.transform.rotation.y, 0, this.gameObject.transform.rotation.w);
                        //isStacking = true;
                    }
                }

        }

        //if(collision.gameObject.GetComponent<Stacking>().stackable == true)
        //{
        //    currentParent = collision.gameObject;
        //    this.gameObject.transform.parent = collision.transform;
        //    this.transform.position = collision.transform.position;
        //    this.transform.rotation = collision.transform.rotation;

        //    isStacking = true;
        //}
    }
    private void OnCollisionExit(Collision collision)
    {
        isStacking = false;
        if(false)
        {

        }

    }

    public Transform MasterParentTransform(Collision collision)
    {
        if (collision.transform.parent == null)
        {
            return this.gameObject.transform;
        }
        else
        {
            return this.gameObject.GetComponent<OldStacking>().MasterParentTransform(collision);
        }
    }

    public Vector3 MasterParentPosition(Collision collision)
    {
        if(this.transform.parent == null)
        {
            return this.gameObject.transform.position;
        }
        else
        {
            return this.gameObject.GetComponent<OldStacking>().MasterParentPosition(collision);
        }
    }

    public Quaternion MasterParentRotation(Collision collision)
    {
        if (this.transform.parent == null)
        {
            return this.gameObject.transform.rotation;
        }
        else
        {
            return this.gameObject.GetComponent<OldStacking>().MasterParentRotation(collision);
        }
    }

}
