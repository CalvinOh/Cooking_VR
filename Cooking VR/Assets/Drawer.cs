using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : MonoBehaviour
{
 
    private void OnCollisionEnter(Collision collision)
    {
        collision.transform.parent = this.gameObject.transform;
    }

   
}
