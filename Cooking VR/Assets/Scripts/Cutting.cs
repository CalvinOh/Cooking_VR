using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutting : MonoBehaviour
{
    
    private void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.CompareTag("Cutable"))
        {
            Debug.Log("Cutting");
           
            int temp = collision.gameObject.transform.childCount;

            Debug.Log(collision.gameObject.transform.childCount + " & " /*+ collision.gameObject.transform.GetChild(temp - 1).name*/);

            collision.gameObject.transform.GetChild(temp-1).GetComponent<Rigidbody>().isKinematic = false;
            collision.gameObject.transform.GetChild(temp - 1).transform.parent = null;
        }
    }

}
