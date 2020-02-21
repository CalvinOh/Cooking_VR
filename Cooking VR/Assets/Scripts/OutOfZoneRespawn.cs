using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfZoneRespawn : MonoBehaviour
{
    [SerializeField]
    Transform respawnZone;
    [SerializeField]
    bool enteredKillZone;

    private Rigidbody rb;

    [SerializeField]
    private float timeSpentOnGround;
    [SerializeField]
    private float timerOnGround;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(enteredKillZone)
        {
            this.gameObject.transform.position = respawnZone.transform.position;
            this.rb.velocity = this.rb.angularVelocity = Vector3.zero;
            
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

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            timeSpentOnGround += 0.1f * Time.deltaTime;
            RespawnOnGround();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        
        
    }

    /// <summary>
    /// Item Spent too much time on the ground and respawned
    /// </summary>
    private void RespawnOnGround()
    {
        if (timeSpentOnGround > timerOnGround)
        {
            enteredKillZone = true;
            Debug.Log("Respawning");
            timeSpentOnGround = 0.0f;

        }
    }
}
