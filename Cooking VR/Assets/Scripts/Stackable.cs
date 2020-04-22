using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Stackable : MonoBehaviour
{
    /// <summary>
    /// The object's parent GameObject
    /// </summary>
    public GameObject ParentGameObject;

    Interactable interactable;

    /// <summary>
    /// The object's list of children GameObjects
    /// </summary>
    public List<GameObject> ChildrenGameObjects;

    [SerializeField]
    public OrderManager.Ingridents ingredientName;

    /// <summary>
    /// if true, this object will not have a parent and will be the parent for other game objects.
    /// </summary>
    public bool IsMasterParent;

    public bool wasGrabbed = false;

    // Start is called before the first frame update
    void Start()
    {
        this.IsMasterParent = false;
        ChildrenGameObjects = new List<GameObject>();
        this.interactable = this.GetComponent<Interactable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.interactable.attachedToHand)
        {
            wasGrabbed = true;
        }

        if (wasGrabbed && !this.interactable.attachedToHand)
        {
            if (this.ParentGameObject != null)
            {
                UnassignParent();
            }
            this.wasGrabbed = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if parenting has already occured between the two objects
        bool alreadyDone = false;

        if (collision.gameObject.GetComponent<Stackable>().ParentGameObject.Equals(this.gameObject) || this.ParentGameObject.Equals(collision.gameObject))
        {
            alreadyDone = true;

            //audio
            PlaySoundStack();
        }

        if (!alreadyDone)
        {
            CheckPositions(collision);

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
    /// Unassigns parent of the child object
    /// </summary>
    private void UnassignParent()
    {
        //if(ParentGameObject.GetComponent<Stackable>().ChildrenGameObjects.Contains(this.gameObject))
        this.ParentGameObject.GetComponent<Stackable>().ChildrenGameObjects.Remove(this.gameObject);

        this.ParentGameObject = null;
        this.GetComponent<Rigidbody>().freezeRotation = false;
        this.GetComponent<Rigidbody>().isKinematic = false;
    }

    /// <summary>
    /// Assigns a new parent to the object
    /// </summary>
    /// <param name="NewParent"></param>
    private void AssignParent(GameObject NewParent)
    {
        this.ParentGameObject = NewParent;
        this.gameObject.transform.parent = NewParent.transform;
        Quaternion OGRotation = NewParent.transform.rotation;
        NewParent.transform.rotation = new Quaternion(0, 0, 0, OGRotation.w);

        // Set distance offset to half the meshCollider's height.
        //float distanceOffset = (this.GetComponent<Collider>().bounds.size.y * this.transform.localScale.y) / 2;
        this.transform.position = new Vector3(this.ParentGameObject.transform.position.x, this.transform.position.y, this.ParentGameObject.transform.position.z);

        NewParent.transform.rotation = OGRotation;


        // If it's a vertical object like a cheese, pickle, or tomato.
        if (this.ingredientName == OrderManager.Ingridents.Cheese || this.ingredientName == OrderManager.Ingridents.Pickle || this.ingredientName == OrderManager.Ingridents.Tomato)
            this.transform.rotation = new Quaternion(0f, transform.rotation.y, 90f, transform.rotation.w);
        else // Normal stack
            this.transform.rotation = new Quaternion(0f, transform.rotation.y, 0f, transform.rotation.w);
        //Debug.Break();

        this.GlueGameObjectToParent(this.GetComponent<Rigidbody>());
        //this.gameObject.transform.parent = NewParent.transform;

        this.IsMasterParent = false;
        NewParent.GetComponent<Stackable>().IsMasterParent = true;

        if (!NewParent.GetComponent<Stackable>().ChildrenGameObjects.Contains(this.gameObject))
            NewParent.GetComponent<Stackable>().ChildrenGameObjects.Add(this.gameObject);

        if (this.ChildrenGameObjects.Count != 0)
        {
            this.ReassignChildrenToNewParent(this.ParentGameObject);
        }

        int i = this.ParentGameObject.GetComponent<Stackable>().ChildrenGameObjects.Count - 1;
        this.ParentGameObject.GetComponent<Stackable>().ChildrenGameObjects[(i - 1)].GetComponent<MeshCollider>().enabled = false;


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

    //audio
    private void PlaySoundStack()
    {
        AkSoundEngine.PostEvent("Stack", gameObject);
    }
}
