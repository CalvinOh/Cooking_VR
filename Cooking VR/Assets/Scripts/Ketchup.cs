using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ketchup : MonoBehaviour
{

    Rigidbody rb;

    Collider collider;

    ParticleSystem particle;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        particle = GetComponent<ParticleSystem>();
        rb.AddForce(Vector3.forward * 300);
        rb.AddForce(Vector3.up * 50);
    }

    private void OnCollisionEnter(Collision collision)
    {
        particle.Stop();
        this.transform.GetChild(0).gameObject.SetActive(true);
        rb.isKinematic = true;
    }
}
