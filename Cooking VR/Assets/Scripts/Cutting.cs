using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Cutable"))
        {
            int temp = collision.gameObject.transform.childCount;
            collision.gameObject.transform.GetChild(temp-1).GetComponent<Rigidbody>().isKinematic = false;
            collision.gameObject.transform.GetChild(temp - 1).transform.parent = null;
        }
    }

}
