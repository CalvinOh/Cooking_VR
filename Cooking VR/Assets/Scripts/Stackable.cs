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
            /*
             * 
             * PLAY SOUND HERE!!!
             *
             */
            Debug.Log($"Beep! From {this.gameObject.name}");
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

    }

    /// <summary>
    /// Assigns a new parent to the object
    /// </summary>
    /// <param name="NewParent"></param>
    private void AssignParent(GameObject NewParent)
    {
        this.ParentGameObject = NewParent;
        this.gameObject.transform.parent = NewParent.transform;
        //this.transform.position = new Vector3(this.ParentGameObject.transform.position.x, this.transform.position.y, this.ParentGameObject.transform.position.z);
        Quaternion OGRotation = NewParent.transform.rotation;
        NewParent.transform.rotation = new Quaternion(0, 0, 0, OGRotation.w);

        float distanceOffset = (this.GetComponent<MeshCollider>().bounds.size.y * this.transform.localScale.y) / 2;// * (3 / 2);
        float highestChild = 0;
        foreach (GameObject child in ParentGameObject.GetComponent<Stackable>().ChildrenGameObjects)
        {
            if (child.transform.position.y >= highestChild)
                highestChild = child.transform.position.y;
        }
        if (ParentGameObject.GetComponent<Stackable>().ChildrenGameObjects.Count == 0)
            distanceOffset += ((NewParent.GetComponent<MeshCollider>().bounds.size.y * NewParent.transform.localScale.y) / 2);//distanceOffset *= 0.5f;
        else
            distanceOffset += ((NewParent.GetComponent<MeshCollider>().bounds.size.y * NewParent.transform.localScale.y) / 2);

        //else
        //    distanceOffset *= 1.5f;


        //distanceOffset += highestChild;
        this.transform.position = new Vector3(this.ParentGameObject.transform.position.x, this.ParentGameObject.transform.position.y + distanceOffset, this.ParentGameObject.transform.position.z);

        NewParent.transform.rotation = OGRotation;

        this.GlueGameObjectToParent(this.GetComponent<Rigidbody>());

        this.transform.localRotation = new Quaternion(0f, transform.rotation.y, 0f, transform.rotation.w);
        //Debug.Break();

        this.IsMasterParent = false;
        NewParent.GetComponent<Stackable>().IsMasterParent = true;
        //NewParent.GetComponent<Rigidbody>().isKinematic = true;

        //CheckChildrenList(NewParent.GetComponent<Stackable>(), this.gameObject);
        if (!NewParent.GetComponent<Stackable>().ChildrenGameObjects.Contains(this.gameObject))
            NewParent.GetComponent<Stackable>().ChildrenGameObjects.Add(this.gameObject);

        if (this.ChildrenGameObjects.Count != 0)
        {
            this.ReassignChildrenToNewParent(this.ParentGameObject);
        }

    }

    /// <summary>
    /// Makes the GameObject kinematic and freezes its' rotation so that appears to be "Glued" to the parent.
    /// </summary>
    /// <param name="rb"></param>
    private void GlueGameObjectToParent(Rigidbody rb)
    {
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        rb.freezeRotation = true;
    }

    private void CheckChildrenList(Stackable ParentStackScript, GameObject Child)
    {
        if (!ParentStackScript.ChildrenGameObjects.Contains(Child))
            ChildrenGameObjects.Add(Child);
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
