using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ketchup : MonoBehaviour
{
    //Rigidbody of the ketchup object
    Rigidbody rb;

    //Particle system that creates the midair spray
    ParticleSystem particle;

    bool hitActive = false;

    bool hitNotStackable = false;

    [SerializeField]
    float timeStored;

    [SerializeField]
    float timeToDelete;

    // Start is called before the first frame update
    void Start()
    {
        //Grabs the ketchup's components
        rb = GetComponent<Rigidbody>();
        particle = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if(timeStored >= timeToDelete)
        {
            Destroy(gameObject.transform.GetChild(0).gameObject);
            Destroy(gameObject);
        }
        else
        {
            if(this.transform.parent == null)
            {
                timeStored += Time.deltaTime;
            }
            else
            {
                timeStored = 0;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hitActive)
        {
            //When the ketchup hits an object it deactivates the particle system, activates the splat and adjusts the splat to stick to the location it hits.
            particle.Stop();
            this.transform.GetChild(0).gameObject.SetActive(true);
            rb.isKinematic = true;
            this.transform.SetPositionAndRotation(this.transform.position, rb.rotation);
            rb.freezeRotation = true;
            if (!collision.gameObject.GetComponent<ManualStack>())
            {
                hitNotStackable = true;
                timeStored = Time.time;
            }
            hitActive = true;
        }
    }
}
