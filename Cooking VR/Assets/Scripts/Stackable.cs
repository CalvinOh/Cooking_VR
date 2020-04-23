using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Valve.VR.InteractionSystem;

public class Stackable : MonoBehaviour
{
    /// <summary>
    /// The object's parent GameObject
    /// </summary>
    public GameObject ParentGameObject;

    Interactable interactable;

    public Vector3 topStackPoint, bottomStackPoint;

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

    public float thickness;
    public float scaleRatio;
    public float distanceOffset;
    private float StackRotationAngle;


    // Start is called before the first frame update
    void Start()
    {
        this.IsMasterParent = false;
        ChildrenGameObjects = new List<GameObject>();
        this.interactable = this.GetComponent<Interactable>();

        topStackPoint = bottomStackPoint = Vector3.zero;

        scaleRatio = 5;//(100 / this.transform.localScale.y);
        thickness = (this.GetComponent<Collider>().bounds.size.y * scaleRatio) / 2;

        // Accounting for "vertically sliced" ingredients. 
        switch(this.ingredientName)
        {
            case OrderManager.Ingridents.Cheese:
            case OrderManager.Ingridents.Pickle:
            case OrderManager.Ingridents.Tomato:
                {
                    this.StackRotationAngle = 90;
                    break;
                }
            default:
                {
                    this.StackRotationAngle = 0;
                    break;
                }
        }
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

        //Debug.Log($"Top Stack Point: {topStackPoint.ToString()}");
        //Debug.Log($"Top Stack Point: {bottomStackPoint.ToString()}");
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if parenting has already occured between the two objects
        bool alreadyDone = false;
        //if(collision.gameObject.GetComponent<Stackable>())
        //{

        //}
        if (collision.gameObject.GetComponent<Stackable>() && collision.gameObject.GetComponent<Stackable>().ParentGameObject.Equals(this.gameObject) || this.ParentGameObject.Equals(collision.gameObject))
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
            //Debug.Log($"Beep! From {this.gameObject.name}");
        }

    }

    public void CheckStackPoints()
    {
        Collider temp = this.GetComponent<Collider>();
        this.topStackPoint = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + ((temp.bounds.size.y * (100 /this.gameObject.transform.localScale.y)) / 2), this.gameObject.transform.position.z);
        this.bottomStackPoint = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - ((temp.bounds.size.y * (100 / this.gameObject.transform.localScale.y)) / 2), this.gameObject.transform.position.z);
    }

    public float CheckHighestChildStackPoint()
    {
        float yPos = this.topStackPoint.y;
        if(ChildrenGameObjects.Count > 0)
        {
            foreach (var c in ChildrenGameObjects)
            {
                c.GetComponent<Stackable>().CheckStackPoints();
                if (c.GetComponent<Stackable>().topStackPoint.y > yPos)
                {
                    yPos = c.GetComponent<Stackable>().topStackPoint.y;
                }
            }
        }
        

        return yPos;
    }

    /// <summary>
    /// Check the vertical position of the objects that collided
    /// </summary>
    /// <param name="collision"></param>
    private void CheckPositions(Collision collision)
    {
        if(collision.gameObject.GetComponent<Stackable>())
        {
            // If other object is above this.gameObject = this.gameObject is the parent.
            if (collision.gameObject.transform.position.y >= this.gameObject.transform.position.y)
            {

                if (this.ParentGameObject == null) // No other parent
                {
                    collision.gameObject.GetComponent<Stackable>().AssignParent(this.gameObject);
                }
                // There's a Master Parent that isn't this.
                else
                {
                    collision.gameObject.GetComponent<Stackable>().AssignParent(this.gameObject.GetComponent<Stackable>().ParentGameObject); //(this.gameObject);
                }
            }
            // If current object is above other object.
            else // if(collision.gameObject.transform.position.y < this.gameObject.transform.position.y)
            {
                if (collision.gameObject.GetComponent<Stackable>().ParentGameObject == null)
                {
                    this.AssignParent(collision.gameObject);
                }
                // There's a Master Parent that isn't the collision game object.
                else
                {
                    this.AssignParent(collision.gameObject.GetComponent<Stackable>().ParentGameObject); //(collision.gameObject);
                }

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
        this.GetComponent<MeshCollider>().enabled = true;
    }

    /// <summary>
    /// Assigns a new parent to the object
    /// </summary>
    /// <param name="NewParent"></param>
    private void AssignParent(GameObject NewParent)
    {
        this.ParentGameObject = NewParent;
        //Debug.Log($"{this.gameObject.name} is colliding with the parent {this.ParentGameObject.name}");

        //CheckStackPoints();
        //this.ParentGameObject.GetComponent<Stackable>().CheckStackPoints();

        //float y = this.bottomStackPoint.y - this.ParentGameObject.GetComponent<Stackable>().CheckHighestChildStackPoint();

        this.ParentGameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.gameObject.transform.parent = NewParent.transform;

        Quaternion OGRotation = NewParent.transform.rotation;
        NewParent.transform.rotation = new Quaternion(0, 0, 0, OGRotation.w);

        // Set distance offset to half the meshCollider's height.

        //float scaleRatio = (100 / this.transform.localScale.y);
        //float distanceOffset = (this.GetComponent<Collider>().bounds.size.y * scaleRatio) / 2;
        //float parentScaleRatio = (100 / NewParent.transform.localScale.y);
        //float parentThickness = (NewParent.GetComponent<Collider>().bounds.size.y * parentScaleRatio) / 2;
        Stackable ParentLocalReference = NewParent.GetComponent<Stackable>();
        distanceOffset = this.thickness + ParentLocalReference.thickness;

        if(ParentLocalReference.ChildrenGameObjects.Count > 0)
            distanceOffset += ParentLocalReference.ChildrenGameObjects[ParentLocalReference.ChildrenGameObjects.Count-1].GetComponent<Stackable>().distanceOffset;


        //for (int j = 0; j < ParentLocalReference.ChildrenGameObjects.Count; j++)
        //{
        //    distanceOffset += ((ParentLocalReference.ChildrenGameObjects[j].GetComponent<Stackable>().thickness * 2)); //* ParentLocalReference.ChildrenGameObjects[j].transform.lossyScale.y);
        //}

        Debug.Log($"{this.gameObject.name} Thickness: {thickness}");
        Debug.Log($"{this.gameObject.name} distance offset: {distanceOffset}");
        //Debug.Log($"Parent: {this.ParentGameObject.name} / Child: {this.gameObject.name} / MostRecentChild: {ParentLocalReference.ChildrenGameObjects[ParentLocalReference.ChildrenGameObjects.Count - 1].gameObject.name}");

        //if (ParentLocalReference.ChildrenGameObjects.Count > 0)
        //    this.transform.localPosition = new Vector3(this.ParentGameObject.transform.position.x, ParentLocalReference.ChildrenGameObjects[ParentLocalReference.ChildrenGameObjects.Count - 1].transform.position.y + distanceOffset, this.ParentGameObject.transform.position.z);
        //else
        //    this.transform.localPosition = new Vector3(this.ParentGameObject.transform.position.x, this.ParentGameObject.transform.position.y + distanceOffset, this.ParentGameObject.transform.position.z);

        //this.transform.localPosition = new Vector3(this.ParentGameObject.transform.position.x, this.transform.position.y, this.ParentGameObject.transform.position.z);

        this.gameObject.transform.rotation = new Quaternion(0, 0, this.StackRotationAngle, OGRotation.w);
        NewParent.transform.rotation = OGRotation;


        // If it's a vertical object like a cheese, pickle, or tomato.
        //if (this.ingredientName == OrderManager.Ingridents.Cheese || this.ingredientName == OrderManager.Ingridents.Pickle || this.ingredientName == OrderManager.Ingridents.Tomato)
        //    this.transform.rotation = new Quaternion(0f, transform.rotation.y, 90f, transform.rotation.w);
        //else // Normal stack
        //    this.transform.rotation = new Quaternion(0f, transform.rotation.y, 0f, transform.rotation.w);

        //Debug.Break();

        this.GlueGameObjectToParent(this.GetComponent<Rigidbody>());
        //this.gameObject.transform.parent = NewParent.transform;

        this.IsMasterParent = false;
        //NewParent.GetComponent<Stackable>().IsMasterParent = true;
        ParentLocalReference.IsMasterParent = true;
        
        //if (!NewParent.GetComponent<Stackable>().ChildrenGameObjects.Contains(this.gameObject))
        //    NewParent.GetComponent<Stackable>().ChildrenGameObjects.Add(this.gameObject);
        if (!ParentLocalReference.ChildrenGameObjects.Contains(this.gameObject))
            ParentLocalReference.ChildrenGameObjects.Add(this.gameObject);


        if (this.ChildrenGameObjects.Count != 0)
        {
            this.ReassignChildrenToNewParent(this.ParentGameObject);
        }

        //int i = this.ParentGameObject.GetComponent<Stackable>().ChildrenGameObjects.Count - 1;
        //if (ParentGameObject.GetComponent<Stackable>().ChildrenGameObjects.Count > 1)
        //    this.ParentGameObject.GetComponent<Stackable>().ChildrenGameObjects[(i - 1)].GetComponent<MeshCollider>().enabled = false;

        int i = ParentLocalReference.ChildrenGameObjects.Count - 1;
        if (ParentLocalReference.ChildrenGameObjects.Count > 1)
            ParentLocalReference.ChildrenGameObjects[(i - 1)].GetComponent<MeshCollider>().enabled = false;


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
        if(ChildrenGameObjects.Count > 0)
        {
            foreach (GameObject child in ChildrenGameObjects)
            {
                child.GetComponent<Stackable>().AssignParent(NewParent);
            }
            this.ChildrenGameObjects.Clear();
        }
        
    }
}
