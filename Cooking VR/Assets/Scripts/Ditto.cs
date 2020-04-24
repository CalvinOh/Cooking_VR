using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ditto : MonoBehaviour
{

    [SerializeField]
    [Tooltip("What tags this item can copy, pleas enter exactly as tag is named")]
    private List<string> TagWhiteList = new List<string>();

    private void OnCollisionEnter(Collision collision)
    {
        if(TagWhiteList.Contains(collision.gameObject.tag))
        {
           Instantiate(collision.gameObject, this.transform.position, transform.rotation, null);
           Destroy(this.gameObject);
        }

    }


}
