using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stackable : MonoBehaviour
{
    /// <summary>
    /// The object's parent GameObject
    /// </summary>
    public GameObject ParentGameObject;

    /// <summary>
    /// The object's list of children GameObjects
    /// </summary>
    public List<GameObject> ChildrenGameObjects;

    /// <summary>
    /// if true, this object will not have a parent and will be the parent for other game objects.
    /// </summary>
    public bool IsMasterParent;

    // Start is called before the first frame update
    void Start()
    {
        this.IsMasterParent = false;
        ChildrenGameObjects = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if parenting has already occured between the two objects
        bool alreadyDone = false;

        if (collision.gameObject.GetComponent<Stackable>().ParentGameObject.Equals(this.gameObject) || this.ParentGameObject.Equals(collision.gameObject))
        {
            alreadyDone = true;
        }

        if (!alreadyDone)
        {
            CheckPositions(collision);
        }

    }

    /// <summary>
    /// Check the vertical position of the objects that collided
    /// </summary>
    /// <param name="collision"></param>
    private void CheckPositions(Collision collision)
    {
        // If other object is above this.gameObject = this.gameObject is the parent.
        if (collision.gameObject.transform.position.y >= this.gameObject.transform.position.y)
        {
            if(this.ParentGameObject == null) // No other parent
            {
                collision.gameObject.GetComponent<Stackable>().AssignParent(this.gameObject);
            }
            // There's a Master Parent that isn't this.
            else
            {
                collision.gameObject.GetComponent<Stackable>().AssignParent(this.gameObject.GetComponent<Stackable>().ParentGameObject);
            }
        }
        // If current object is above other object.
        else // if(collision.gameObject.transform.position.y < this.gameObject.transform.position.y)
        {
            if(collision.gameObject.GetComponent<Stackable>().ParentGameObject == null)
            {
                this.AssignParent(collision.gameObject);
            }
            // There's a Master Parent that isn't the collision game object.
            else
            {
                this.AssignParent(collision.gameObject.GetComponent<Stackable>().ParentGameObject);
            }
            
        }

        
        /*
         * 
         * PLAY SOUND HERE!!!
         *
         */
    }

    /// <summary>
    /// Assigns a new parent to the object
    /// </summary>
    /// <param name="NewParent"></param>
    private void AssignParent(GameObject NewParent)
    {

        this.ParentGameObject = NewParent;
        this.gameObject.transform.parent = NewParent.transform;

        this.gameObject.GetComponent<Rigidbody>().freezeRotation = true;
        this.transform.localRotation = new Quaternion(0f, transform.rotation.y, 0f, transform.rotation.w);
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Debug.Break();

        this.IsMasterParent = false;
        NewParent.GetComponent<Stackable>().IsMasterParent = true;

        if(!NewParent.GetComponent<Stackable>().ChildrenGameObjects.Contains(this.gameObject))
            NewParent.GetComponent<Stackable>().ChildrenGameObjects.Add(this.gameObject);

        if (this.ChildrenGameObjects.Count != 0)
        {
            this.ReassignChildrenToNewParent(this.ParentGameObject);
        }

    }

    /// <summary>
    /// Reassigns the parent of the object's children to the parameter sent
    /// </summary>
    /// <param name="NewParent"></param>
    public void ReassignChildrenToNewParent(GameObject NewParent)
    {
        foreach (GameObject child in ChildrenGameObjects)
        {
            child.GetComponent<Stackable>().AssignParent(NewParent);
        }
        this.ChildrenGameObjects.Clear();
    }
}
