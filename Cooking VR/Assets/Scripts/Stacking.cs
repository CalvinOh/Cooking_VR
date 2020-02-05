using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stacking : MonoBehaviour
{
    Collider collider;
    Transform location;


    bool stackable;
    [SerializeField]
    bool isStacking;

    [SerializeField]
    GameObject currentParent;

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
            this.transform.position = this.gameObject.transform.parent.transform.position;
            this.transform.rotation = this.gameObject.transform.parent.transform.rotation;
        }
  
  
    }

    private void OnCollisionEnter(Collision collision)
    {
        this.gameObject.GetComponent<Rigidbody>().velocity = collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        // If the other object is higher than the current object
        if(collision.gameObject.GetComponent<Stacking>().stackable == true &&
            collision.gameObject.transform.position.y >= this.gameObject.transform.position.y)
        {
            if(collision.gameObject.transform.parent.gameObject != this.gameObject)
            {
                collision.gameObject.transform.parent = this.gameObject.transform;
                collision.gameObject.transform.rotation = new Quaternion(0, collision.gameObject.transform.rotation.y, 0, collision.gameObject.transform.rotation.w);
                isStacking = true;
            }
        }
        else if(this.gameObject.GetComponent<Stacking>().stackable == true &&
            collision.gameObject.transform.position.y < this.gameObject.transform.position.y)
            {
                if(this.gameObject.transform.parent != collision.gameObject.transform)
                {
                    this.gameObject.transform.transform.parent = collision.gameObject.transform;
                    this.gameObject.transform.rotation = new Quaternion(0, this.gameObject.transform.rotation.y, 0, this.gameObject.transform.rotation.w);
                    isStacking = true;
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

 
}
