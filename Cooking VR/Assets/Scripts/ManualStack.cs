using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
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

    public bool isHeld = false;

    public float thickness;
    public float scaleRatio;
    public float distanceOffset;
    private float StackRotationAngle;
    public bool TestDrop = false;
    public bool falling = false;


    // Start is called before the first frame update
    void Start()
    {
        this.Rb = this.gameObject.GetComponent<Rigidbody>();
        this.IsMasterParent = false;
        ChildrenGameObjects = new List<GameObject>();
        this.interactable = this.GetComponent<Interactable>();

        topStackPoint = bottomStackPoint = Vector3.zero;

        scaleRatio = 1;// 5;//(100 / this.transform.localScale.y);
        // Gets thickness and rounds it to the nearest thousandth (3 decimal places)
        thickness = (this.GetComponent<Collider>().bounds.size.y * scaleRatio) / 2;
        thickness = (float)(Math.Round(thickness * 1000f) / 1000f);

        // Accounting for "vertically sliced" ingredients. 
        switch (this.ingredientName)
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
        if (this.Rb.velocity.y > 0 || this.Rb.velocity.y < 0)
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
        if (other.gameObject.TryGetComponent(out ManualStack ms))
        {
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
                this.AssignParent(other.gameObject);
            }
        }
    }

    public void AssignParent(GameObject parent)
    {
        this.Parent = parent;
        this.Rb.velocity = Vector3.zero;
        this.Rb.isKinematic = true;

        Vector3 offset = new Vector3(0, this.thickness + parent.GetComponent<ManualStack>().thickness, 0);
        Quaternion ogRot = this.Parent.transform.rotation;
        this.Parent.transform.rotation.Set(0, 0, 0, ogRot.w);
        Quaternion disOgRot = this.transform.rotation; //this.gameObject.transform.rotation.Set(0, 0, 0, this.transform.rotation.w);
        disOgRot.x = disOgRot.y = disOgRot.z = 0;
        this.gameObject.transform.SetPositionAndRotation((this.Parent.transform.position + offset), disOgRot);

        //this.gameObject.transform.position = this.Parent.transform.position + offset;

        this.Parent.transform.rotation = ogRot;

        this.gameObject.transform.parent = parent.transform;
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
            this.ChildrenGameObjects.Add(child);
        }
    }
}
