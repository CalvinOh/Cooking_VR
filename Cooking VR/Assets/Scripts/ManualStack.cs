using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class ManualStack : MonoBehaviour
{
    /// <summary>
    /// The object's parent GameObject
    /// </summary>
    public GameObject StackParent;

    private Rigidbody Rb;

    Interactable interactable;

    static Hand leftHand, rightHand;
    private Hand[] tempHands;
    /// <summary>
    /// The object's list of children GameObjects
    /// </summary>
    public List<GameObject> ChildrenGameObjects;

    [SerializeField]
    public OrderManager.Ingridents ingredientName;

    /// <summary>
    /// if true, this object will not have a parent and will be the parent for other game objects.
    /// </summary>
    public bool IsStacked;

    public int isHeld = 0; //0 = notHeld, 1 = rightHandHolding, 2 = leftHandHolding
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
        this.IsStacked = false;
        ChildrenGameObjects = new List<GameObject>();
        this.interactable = this.GetComponent<Interactable>();
        lastHeld = -1;
        stackWindowLength = 0.3f;


        if(leftHand == null || rightHand == null)
        {
            tempHands = FindObjectsOfType<Hand>();
            foreach (Hand h in tempHands)
            {
                if (h.handType == SteamVR_Input_Sources.LeftHand)
                {
                    leftHand = h;
                    rightHand = h.otherHand;
                }
            }
        }

        scaleRatio = 1;// 5;//(100 / this.transform.localScale.y);
                       // Gets thickness and rounds it to the nearest thousandth (3 decimal places)
        SetThickness();

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
        CheckHeld();

        CheckLetGo();
        if (Time.time >= lastHeld + stackWindowLength)
        {
            canStack = false;
        }

        //if (this.interactable.attachedToHand.currentAttachedObject.Equals(this.gameObject))
        //{
        //    // being grabbed
        //    this.Parent.GetComponent<ManualStack>().RemoveChild(this.gameObject);
        //    this.UnassignParent();
        //}
    }

    private void CheckLetGo()
    {
        if (this.gameObject != leftHand.currentAttachedObject && this.gameObject != rightHand.currentAttachedObject)//.transform.parent != leftHand.transform && this.transform.parent != rightHand.transform)
        {
            if (isHeld != 0 && lastHeld < Time.time)
            {
                if (isHeld == 1)
                {
                    rightHand.DetachObject(this.gameObject);
                    this.Rb.isKinematic = false;
                }
                else if (isHeld == 2)
                {
                    leftHand.DetachObject(this.gameObject);
                    this.Rb.isKinematic = false;
                }

                isHeld = 0;
                this.Rb.isKinematic = false;
                SceneManager.MoveGameObjectToScene(this.gameObject, SceneManager.GetActiveScene());
                lastHeld = Time.time;
            }

        }
    }

    private void CheckHeld()
    {
        if (this.gameObject == leftHand.currentAttachedObject)//this.transform.parent == rightHand.transform)//if (this.StackParent != null && this.transform.parent.gameObject != this.StackParent)
        {
            isHeld = 2;

            if (this.StackParent != null)
                UnassignParent();
        }
        else if (this.gameObject == rightHand.currentAttachedObject)
        {
            isHeld = 1;

            if (this.StackParent != null)
                UnassignParent();
        }
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
            //Hand hand;
            //if (this.transform.parent == leftHand.transform)
            //{
            //    hand = leftHand;
            //}
            //else if (this.transform.parent = rightHand.transform)
            //{
            //    hand = rightHand;
            //}



            //foreach (Hand h in this.interactable.hoveringHands)
            //{
            //    if (h.currentAttachedObject.Equals(this.gameObject))
            //    {
            //        this.AssignParent(other.gameObject);
            //    }
            //}

            if ((TestDrop && falling))
            {
                if(ms.StackParent != this.gameObject) // || (canStack && (Time.time <= lastHeld + stackWindowLength)) || 
                {
                    this.AssignParent(other.gameObject);
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent(out ManualStack ms) && canStack && (Time.time <= lastHeld + stackWindowLength)) // and let go
        {
            if(this.StackParent != other.gameObject && this.StackParent != this.gameObject)
                this.AssignParent(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out ManualStack ms) && !other.gameObject.Equals(this.gameObject))
        {
            canStack = false;
        }
    }

    public void AssignParent(GameObject parent)
    {
        if (this.StackParent == null && !this.gameObject.name.Equals(parent.name))
        {
            //Check if the parent has a parent.
            if (parent.GetComponent<ManualStack>().StackParent == null)
            {
                GlueToParent(parent);
            }
            else
            {
                GlueToParent(parent.GetComponent<ManualStack>().StackParent);
            }
        }
       

    }

    public void UnassignParent()
    {
        this.StackParent.GetComponent<ManualStack>().RemoveChild(this.gameObject);
        
        this.StackParent = null;
        //this.Rb.isKinematic = false;
        this.GetComponent<SphereCollider>().enabled = true;
        this.GetComponent<Collider>().enabled = true;
        //this.gameObject.transform.parent = null;
    }

    private void GlueToParent(GameObject parent)
    {
        this.StackParent = parent;

        Debug.Log($"this {this.gameObject.name}'s StackParent is {this.StackParent.gameObject.name}");
        this.Rb.velocity = Vector3.zero;
        falling = false;

        Quaternion ogRot = this.StackParent.transform.rotation;
        this.StackParent.gameObject.transform.rotation = Quaternion.identity;//.Set(0, 0, 0, ogRot.w);
        //Quaternion thisOgRot = this.transform.rotation; //this.gameObject.transform.rotation.Set(0, 0, 0, this.transform.rotation.w);
        this.gameObject.transform.rotation = Quaternion.identity;//thisOgRot.x = thisOgRot.y = thisOgRot.z = 0;


        this.Rb.isKinematic = true;

        //this.verDistanceOffset += parent.GetComponent<ManualStack>().verDistanceOffset;
        //parent.GetComponent<ManualStack>().verDistanceOffset += this.thickness;
        //Vector3 vec3offset = new Vector3(0, parent.GetComponent<ManualStack>().verDistanceOffset, 0);
        if(this.StackParent.GetComponent<ManualStack>().ChildrenGameObjects.Count > 0)
        {
            vec3offset = new Vector3(0, this.StackParent.GetComponent<ManualStack>().ChildrenGameObjects.Last().GetComponent<ManualStack>().thickness + this.thickness, 0);
            Debug.Log($"{this.gameObject.name} is stacking to {this.StackParent.GetComponent<ManualStack>().ChildrenGameObjects.Last().gameObject.name}");
        }
        else
        {
            vec3offset = new Vector3(0, this.StackParent.GetComponent<ManualStack>().thickness + this.thickness, 0);
            Debug.Log($"{this.gameObject.name} is stacking to {this.StackParent.gameObject.name}");
        }


        if (this.StackParent.GetComponent<ManualStack>().ChildrenGameObjects.Count > 0)
            this.gameObject.transform.SetPositionAndRotation((this.StackParent.GetComponent<ManualStack>().ChildrenGameObjects.Last().transform.position + vec3offset), Quaternion.identity);
        else
            this.gameObject.transform.SetPositionAndRotation((this.StackParent.transform.position + vec3offset), Quaternion.identity);

        //this.gameObject.transform.position = this.Parent.transform.position + offset;

        this.StackParent.transform.rotation = ogRot;
        
        Vector3 newRot = new Vector3(0, UnityEngine.Random.Range(0f,360f), 0);
        this.transform.Rotate(newRot, Space.Self);//Quaternion.Euler(0, r.Next(360), 0);
        
        if (this.ingredientName == OrderManager.Ingridents.Cheese)
            this.gameObject.transform.Rotate(Vector3.forward, 90);

        this.gameObject.transform.parent = this.StackParent.transform;
        StackParent.GetComponent<SphereCollider>().enabled = false;
        this.StackParent.GetComponent<ManualStack>().AddChild(this.gameObject);
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
            //this.ChildrenGameObjects.Last().GetComponent<Collider>().enabled = newValue;
            this.ChildrenGameObjects.Last().GetComponent<SphereCollider>().enabled = newValue;
        }
    }
}
