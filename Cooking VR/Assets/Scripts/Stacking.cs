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
        if(collision.gameObject.GetComponent<Stacking>().stackable == true)
        {
            currentParent = collision.gameObject;
            this.gameObject.transform.parent = collision.transform;
            this.transform.position = collision.transform.position;
            this.transform.rotation = collision.transform.rotation;

            isStacking = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        isStacking = false;

    }

 
}
