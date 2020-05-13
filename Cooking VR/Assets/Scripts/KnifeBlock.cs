using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeBlock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("OnCollisionExit");
        if(other.gameObject.transform.parent.gameObject.GetComponent<SillyKnife>() == true)
        {
            other.gameObject.transform.parent.gameObject.GetComponent<SillyKnife>().ChangeSize();
        }
    }
}
