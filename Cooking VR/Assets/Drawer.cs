using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : MonoBehaviour
{



    void ParentObject(Rigidbody item)
    {
        item.isKinematic = true;
        item.transform.parent = this.gameObject.transform;
    }

    void UnparentObject(Rigidbody item)
    {
        item.isKinematic = false;
        item.transform.parent = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        ParentObject(collision.rigidbody);
    }

    private void OnCollisionExit(Collision collision)
    {
        UnparentObject(collision.rigidbody);
    }

}
