using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stackable : MonoBehaviour
{
    [SerializeField]
    public GameObject ThisThingsParentGameobject;

    /// <summary>
    /// if true, this item will not have a parent and will be the parent for other game objects.
    /// </summary>
    public bool IsMasterParent;

    // Start is called before the first frame update
    void Start()
    {
        this.IsMasterParent = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if parenting has already occured between the two objects
        bool alreadyDone = false;

        if (collision.gameObject.GetComponent<Stackable>().ThisThingsParentGameobject.Equals(this.gameObject) || this.ThisThingsParentGameobject.Equals(collision.gameObject))
        {
            alreadyDone = true;
        }

        if (!alreadyDone)
        {
            AssignParenthood(collision);
        }

    }

    private void AssignParenthood(Collision collision)
    {
        // If other object is above this.gameObject = this.gameObject is the parent.
        if (collision.gameObject.transform.position.y >= this.gameObject.transform.position.y)
        {
            if(this.ThisThingsParentGameobject == null) // No other parent
            {
                collision.gameObject.GetComponent<Stackable>().ThisThingsParentGameobject = this.gameObject;
                collision.gameObject.transform.parent = this.gameObject.transform;
                this.IsMasterParent = true;
            }
            // There's a Master Parent that isn't this.
            else
            {
                collision.gameObject.GetComponent<Stackable>().ThisThingsParentGameobject = this.gameObject.GetComponent<Stackable>().ThisThingsParentGameobject;
                collision.gameObject.transform.parent = collision.gameObject.GetComponent<Stackable>().ThisThingsParentGameobject.transform;
            }
        }
        // If current object is above other object.
        else // if(collision.gameObject.transform.position.y < this.gameObject.transform.position.y)
        {
            if(collision.gameObject.GetComponent<Stackable>().ThisThingsParentGameobject == null)
            {
                this.ThisThingsParentGameobject = collision.gameObject;
                this.gameObject.transform.parent = this.ThisThingsParentGameobject.transform;
                collision.gameObject.GetComponent<Stackable>().IsMasterParent = true;
            }
            // There's a Master Parent that isn't the collision game object.
            else
            {
                this.ThisThingsParentGameobject = collision.gameObject.GetComponent<Stackable>().ThisThingsParentGameobject;
                this.gameObject.transform.parent = collision.gameObject.GetComponent<Stackable>().ThisThingsParentGameobject.transform;
            }
            
        }
    }
}
