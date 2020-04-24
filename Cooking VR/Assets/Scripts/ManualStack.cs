using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Valve.VR.InteractionSystem;

public class ManualStack : MonoBehaviour
{
    /// <summary>
    /// The object's parent GameObject
    /// </summary>
    public GameObject Parent;

    private Rigidbody Rb;

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

    public bool isHeld = false;
    private float lastHeld; // the last time the object was held (measured as seconds since play)
    private float stackWindowLength; // How long the object should check since it was let go to stack. Measusred in seconds.

    [SerializeField]
    public Vector3 vec3offset;

    [SerializeField]
    public float thickness;
    public float scaleRatio;
    public bool TestDrop = false; // = true if you'd like to test without going into VR to test.
    public bool falling = false; // This is controlled by the ingredient falling with gravity.

    [SerializeField]
    private bool canStack = false;


    // Start is called before the first frame update
    void Start()
    {
        this.Rb = this.gameObject.GetComponent<Rigidbody>();
        this.IsMasterParent = false;
        ChildrenGameObjects = new List<GameObject>();
        this.interactable = this.GetComponent<Interactable>();
        lastHeld = 0;
        stackWindowLength = 0.3f;


        scaleRatio = 1;// 5;//(100 / this.transform.localScale.y);
                       // Gets thickness and rounds it to the nearest thousandth (3 decimal places)
        SetThickness();

        // Accounting for "vertically sliced" ingredients. 
        //switch (this.ingredientName)
        //{
        //    case OrderManager.Ingridents.Cheese:
        //    case OrderManager.Ingridents.Pickle:
        //    case OrderManager.Ingridents.Tomato:
        //        {
        //            this.StackRotationAngle = 90;
        //            break;
        //        }
        //    default:
        //        {
        //            this.StackRotationAngle = 0;
        //            break;
        //        }
        //}

    }

    private void preventSlicesStacking(ManualStack other)
    {
        if(this.ingredientName == OrderManager.Ingridents.Cheese ||
            this.ingredientName == OrderManager.Ingridents.Pickle || 
            this.ingredientName == OrderManager.Ingridents.Tomato)
        {
            if(this.ingredientName == other.ingredientName)
            {
                canStack = false;
            }
        }
        
    }

    private void SetThickness()
    {
        if(thickness == 0)
        {
            thickness = (this.GetComponent<Collider>().bounds.extents.y * scaleRatio);// / 2;
            thickness = (float)(Math.Round(thickness * 1000f) / 1000f);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckFalling();
        //if(this.interactable.attachedToHand.currentAttachedObject.Equals(this.gameObject))
        //{
        //    // being grabbed
        //    this.Parent.GetComponent<ManualStack>().RemoveChild(this.gameObject);
        //    this.UnassignParent();
        //}
    }

    private void CheckFalling()
    {
        if (this.Rb.velocity.y > 0.1f || this.Rb.velocity.y < -0.1f)
        {
            falling = true;
        }
        else
        {
            falling = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If other has manual stack, can stack
        if (other.gameObject.TryGetComponent(out ManualStack ms) && !other.gameObject.Equals(this.gameObject))
        {
            canStack = true;

            // Check if this is the held item, should be child.
            foreach (Hand h in this.interactable.hoveringHands)
            {
                if (h.currentAttachedObject.Equals(this.gameObject))
                {
                    this.AssignParent(other.gameObject);
                }
            }

            if ((TestDrop && falling))
            {
                if(ms.Parent != this.gameObject)
                {
                    //other.gameObject.transform.SetPositionAndRotation(other.gameObject.transform.position, Quaternion.identity);
                    //this.gameObject.transform.SetPositionAndRotation(this.gameObject.transform.position, Quaternion.identity);
                    //other.gameObject.transform.rotation = Quaternion.identity;//.Set(0, 0, 0, ogRot.w);
                    ////Quaternion thisOgRot = this.transform.rotation; //this.gameObject.transform.rotation.Set(0, 0, 0, this.transform.rotation.w);
                    //this.gameObject.transform.rotation = Quaternion.identity;//thisOgRot.x = thisOgRot.y = thisOgRot.z = 0;
                    this.AssignParent(other.gameObject);
                }
            }
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if(canStack && (Time.time <= lastHeld + stackWindowLength)) // and let go
    //    {
    //        this.AssignParent(other.gameObject);
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.TryGetComponent(out ManualStack ms) && !other.gameObject.Equals(this.gameObject))
    //    {
    //        canStack = false;
    //    }
    //}

    public void AssignParent(GameObject parent)
    {
        //Check if the parent has a parent.
        if (parent.GetComponent<ManualStack>().Parent == null)
        {
            GlueToParent(parent);
        }
        else 
        {
            GlueToParent(parent.GetComponent<ManualStack>().Parent);
        }

    }

    public void UnassignParent()
    {
        this.Parent = null;
        this.Rb.isKinematic = false;
        //this.gameObject.transform.parent = null;
    }

    private void GlueToParent(GameObject parent)
    {
        this.Parent = parent;
        this.Rb.velocity = Vector3.zero;
        falling = false;

        Quaternion ogRot = this.Parent.transform.rotation;
        this.Parent.gameObject.transform.rotation = Quaternion.identity;//.Set(0, 0, 0, ogRot.w);
        //Quaternion thisOgRot = this.transform.rotation; //this.gameObject.transform.rotation.Set(0, 0, 0, this.transform.rotation.w);
        this.gameObject.transform.rotation = Quaternion.identity;//thisOgRot.x = thisOgRot.y = thisOgRot.z = 0;
        this.Rb.isKinematic = true;

        //this.verDistanceOffset += parent.GetComponent<ManualStack>().verDistanceOffset;
        //parent.GetComponent<ManualStack>().verDistanceOffset += this.thickness;
        //Vector3 vec3offset = new Vector3(0, parent.GetComponent<ManualStack>().verDistanceOffset, 0);
        if(this.Parent.GetComponent<ManualStack>().ChildrenGameObjects.Count > 0)
        {
            vec3offset = new Vector3(0, this.Parent.GetComponent<ManualStack>().ChildrenGameObjects.Last().GetComponent<ManualStack>().thickness + this.thickness, 0);
            Debug.Log($"{this.gameObject.name} is stacking to {this.Parent.GetComponent<ManualStack>().ChildrenGameObjects.Last().gameObject.name}");
        }
        else
        {
            vec3offset = new Vector3(0, this.Parent.GetComponent<ManualStack>().thickness + this.thickness, 0);
            Debug.Log($"{this.gameObject.name} is stacking to {this.Parent.gameObject.name}");
        }


        if (this.Parent.GetComponent<ManualStack>().ChildrenGameObjects.Count > 0)
            this.gameObject.transform.SetPositionAndRotation((this.Parent.GetComponent<ManualStack>().ChildrenGameObjects.Last().transform.position + vec3offset), Quaternion.identity);
        else
            this.gameObject.transform.SetPositionAndRotation((this.Parent.transform.position + vec3offset), Quaternion.identity);

        //this.gameObject.transform.position = this.Parent.transform.position + offset;

        this.Parent.transform.rotation = ogRot;
        
        Vector3 newRot = new Vector3(0, UnityEngine.Random.Range(0f,360f), 0);
        this.transform.Rotate(newRot, Space.Self);//Quaternion.Euler(0, r.Next(360), 0);

        this.gameObject.transform.parent = this.Parent.transform;
        Parent.GetComponent<SphereCollider>().enabled = false;
        this.Parent.GetComponent<ManualStack>().AddChild(this.gameObject);
    }

    /// <summary>
    /// Checks if child has already been added before adding to the children list
    /// </summary>
    /// <param name="child"></param>
    public void AddChild(GameObject child)
    {
        if(!this.ChildrenGameObjects.Contains(child))
        {
            AlterLastChildHoverAndCollider(false);
            this.ChildrenGameObjects.Add(child);
            AlterLastChildHoverAndCollider(true);
        }
    }

    public void RemoveChild(GameObject child)
    {
        if(this.ChildrenGameObjects.Contains(child))
        {
            this.ChildrenGameObjects.Remove(child);
            AlterLastChildHoverAndCollider(true);
        }
    }

    private void AlterLastChildHoverAndCollider(bool newValue)
    {
        if(this.ChildrenGameObjects.Count > 0)
        {
            //this.ChildrenGameObjects.Last().GetComponent<Interactable>().highlightOnHover = newValue;
            this.ChildrenGameObjects.Last().GetComponent<MeshCollider>().enabled = newValue;
        }
    }
}
