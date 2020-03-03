using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ketchup : MonoBehaviour
{
    //Rigidbody of the ketchup object
    Rigidbody rb;

    //Particle system that creates the midair spray
    ParticleSystem particle;

    // Start is called before the first frame update
    void Start()
    {
        //Grabs the ketchup's components
        rb = GetComponent<Rigidbody>();
        particle = GetComponent<ParticleSystem>();

        //Test force on ketchup
        //rb.AddForce(Vector3.forward * 300);
        //rb.AddForce(Vector3.up * 50);
        //rb.AddForce(Vector3.right * 50);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //When the ketchup hits an object it deactivates the particle system, activates the splat and adjusts the splat to stick to the location it hits.
        particle.Stop();
        this.transform.GetChild(0).gameObject.SetActive(true);
        rb.isKinematic = true;
        this.transform.SetPositionAndRotation(this.transform.position, collision.transform.rotation);
    }
}
