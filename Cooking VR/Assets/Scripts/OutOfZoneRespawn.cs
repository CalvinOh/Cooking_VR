using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfZoneRespawn : MonoBehaviour
{
    [SerializeField]
    Transform respawnZone;
    [SerializeField]
    bool enteredKillZone;
    private void Update()
    {
        if(enteredKillZone)
        {
            this.gameObject.transform.position = respawnZone.transform.position;
            this.gameObject.GetComponent<Rigidbody>().velocity = this.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            
            enteredKillZone = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.CompareTag("KillZone"))
        {
            Debug.Log("Entering Kill Box");
            enteredKillZone = true;
            Debug.Log("Respawning");
        }
    }

    
}
