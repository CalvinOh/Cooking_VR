using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stackable : MonoBehaviour
{
    [SerializeField]
    public GameObject ParentGameObject;

    [SerializeField]
    public List<GameObject> ChildrenGameObjects;

    /// <summary>
    /// if true, this item will not have a parent and will be the parent for other game objects.
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

    private void AssignParent(GameObject NewParent)
    {
        this.ParentGameObject = NewParent;
        this.gameObject.transform.parent = NewParent.transform;
        NewParent.GetComponent<Stackable>().IsMasterParent = true;

        if(!NewParent.GetComponent<Stackable>().ChildrenGameObjects.Contains(this.gameObject))
            NewParent.GetComponent<Stackable>().ChildrenGameObjects.Add(this.gameObject);

        if (this.ChildrenGameObjects.Count != 0)
        {
            this.ReassignChildrenToNewParent(this.ParentGameObject);
        }

    }

    public void ReassignChildrenToNewParent(GameObject NewParent)
    {
        foreach (GameObject child in ChildrenGameObjects)
        {
            child.GetComponent<Stackable>().AssignParent(NewParent);

            //child.GetComponent<Stackable>().ParentGameObject = NewParent;
            //child.gameObject.transform.parent = child.GetComponent<Stackable>().ParentGameObject.transform;
            //child.GetComponent<Stackable>().ParentGameObject.GetComponent<Stackable>().ChildrenGameObjects.Add(child);
        }
        this.ChildrenGameObjects.Clear();
    }
}
