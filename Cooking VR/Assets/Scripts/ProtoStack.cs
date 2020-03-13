using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public struct StackPoint
{
    public Vector3 position { get; set; }
    public float extent { get; set; }
    public float thickness { get { return extent * 2; } }

    public StackPoint(Vector3 vec3, float ex)
    {
        position = vec3;
        extent = ex;
    }
}

public class ProtoStack : MonoBehaviour
{
    /// <summary>
    /// The object's parent GameObject
    /// </summary>
    [SerializeField]
    public GameObject ParentGameObject;

    Interactable interactable;

    /// <summary>
    /// The object's list of children GameObjects
    /// </summary>
    public List<GameObject> ChildrenGameObjects;

    public List<StackPoint> stackPoints;

    public float SlotDistance;

    [SerializeField]
    public OrderManager.Ingridents ingredientName;

    // Start is called before the first frame update
    void Start()
    {
        stackPoints = new List<StackPoint>();
        ChildrenGameObjects = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // check if collision has the stacking script.
        //if(collision.gameObject.GetComponent<ProtoStack>())
        {
            // Check if either object has already done the stacking process to skip it for the other
            if (this.ParentGameObject == collision.gameObject || collision.gameObject.GetComponent<ProtoStack>().ParentGameObject == this.gameObject)
            {
                // skip parenting
            }
            else
            {
                // This is the parent
                if (this.transform.position.y <= collision.transform.position.y)
                {
                    this.AssignParent(collision.gameObject);
                }
                else // Collision is the parent
                {
                    collision.gameObject.GetComponent<ProtoStack>().AssignParent(this.gameObject);
                }
            }
        }
       
    }

    public void AssignParent(GameObject child)
    {
        Quaternion OGRotation = this.transform.rotation;
        this.transform.rotation = new Quaternion(0, 0, 0, OGRotation.w);
        child.transform.rotation = new Quaternion(0, 0, 0, child.transform.rotation.w);

        this.ChildrenGameObjects.Add(child);
        child.transform.parent = this.transform;
        Rigidbody rb = child.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;

        float thisThickness = (this.gameObject.GetComponent<MeshCollider>().bounds.size.y / 2); //this.gameObject.GetComponent<MeshRenderer>().bounds.extents.y;
        float thatThickness = (child.gameObject.GetComponent<MeshCollider>().bounds.size.y / 2); //child.GetComponent<MeshRenderer>().bounds.extents.y;

        float offset = thisThickness;

        for(int i = 0; i < stackPoints.Count; i++)
        {
            offset += stackPoints[i].thickness;
        }

        Vector3 pos = new Vector3(this.transform.position.x, this.transform.position.y + (offset + thatThickness), this.transform.position.z);
        stackPoints.Add(new StackPoint(pos, thatThickness));

        child.transform.position = stackPoints[(stackPoints.Count - 1)].position;

        this.transform.rotation = OGRotation;
    }

    public void UnAssignParent(GameObject formerParent)
    {
        this.ParentGameObject = null;
        formerParent.GetComponent<ProtoStack>().ChildrenGameObjects.Remove(this.gameObject);
    }

    public void ReParentChildren(GameObject newParent)
    {
        for(int i = 0; i < ChildrenGameObjects.Count; i++)
        {
            ChildrenGameObjects[i].GetComponent<ProtoStack>().AssignParent(newParent);
            //newParentProtoStackScript.ChildrenGameObjects.Add(ChildrenGameObjects[i]);
        }
        ChildrenGameObjects = new List<GameObject>();

    }


}
