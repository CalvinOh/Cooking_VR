using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfZoneRespawn : MonoBehaviour
{
    [SerializeField]
    Transform respawnZone;
    [SerializeField]
    bool enteredKillZone;
    [SerializeField]
    bool grounded;

    private Rigidbody rb;

    [SerializeField]
    private float timeSpentOnGround;
    [SerializeField]
    private float timerOnGround;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        enteredKillZone = grounded = false;
        timeSpentOnGround = 0;
        timerOnGround = 3;
        respawnZone = GameObject.FindGameObjectWithTag("RespawnZone").gameObject.transform;

    }

    private void Update()
    {
        if (enteredKillZone)
        {
            this.gameObject.transform.position = respawnZone.transform.position;
            this.rb.velocity = this.rb.angularVelocity = Vector3.zero;

            enteredKillZone = false;

            // audio
            AkSoundEngine.PostEvent("Teleport_item", this.gameObject);
        }

        if(grounded && Time.time >= timeSpentOnGround)
        {
            this.gameObject.transform.position = respawnZone.transform.position;
            this.rb.velocity = this.rb.angularVelocity = Vector3.zero;
            grounded = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KillZone"))
        {
            Debug.Log("Entering Kill Box");
            enteredKillZone = true;
            Debug.Log("Respawning");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log($"On The Ground - {this.gameObject.name}");
            grounded = true;
            timeSpentOnGround = Time.time + timerOnGround;
            //RespawnOnGround(collision.gameObject, true);
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log($"{this.gameObject.name} was picked up From Ground!");
            grounded = false;
        }
    }

    /// <summary>
    /// Item Spent too much time on the ground and respawned
    /// </summary>
    private void RespawnOnGround(GameObject item, bool onGround = false)
    {
        //Bugged, respawning instantly
        while (onGround)
        {
            timeSpentOnGround += 0.01f * Time.deltaTime;
            if (timeSpentOnGround > timerOnGround)
            {
                enteredKillZone = true;
                Debug.Log($"{this.gameObject.name} is respawning from ground");
                timeSpentOnGround = 0.0f;
                onGround = false;

            }
        } 
    }
}
