using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Doneness
{
    Raw,
    Rare,
    Medium,
    WellDone,
    Burnt
};

public class CookScript : MonoBehaviour
{
    public float currentCookAmount; // The progress of how much the item is cooked
    public const float maxCookAmount = 60; // The max value for currentCookAmount
    public Doneness doneness;

    private float[] donenessRefs = { 10, 20, 30, 40 };

    private bool currentlyCooking; // Is the object inside a cookBox? If true, will continue cooking.

    // Start is called before the first frame update
    void Start()
    {
        currentCookAmount = 0;
        doneness = Doneness.Raw;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentlyCooking && currentCookAmount <= maxCookAmount)
        {
            currentCookAmount += Time.deltaTime;

            for(int i = 0; i < donenessRefs.Length; i++)
            {
                if(currentCookAmount > donenessRefs[i])
                {
                    switch(i)
                    {
                        case 0: // Rare
                            {
                                doneness = Doneness.Rare;
                                break;
                            }
                        case 1: // Medium
                            {
                                doneness = Doneness.Medium;
                                break;
                            }
                        case 2: // Well Done
                            {
                                doneness = Doneness.WellDone;
                                break;
                            }
                        case 3: // Burnt
                            {
                                doneness = Doneness.Burnt;
                                break;
                            }
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if(other.CompareTag("CookBox"))
        {
            currentlyCooking = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if(other.CompareTag("CookBox"))
        {
            currentlyCooking = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Ground Beef joke
        if(collision.gameObject.CompareTag("Ground") && this.doneness == Doneness.Raw)
        {
            this.currentCookAmount = 30;
            this.doneness = Doneness.WellDone;
        }
    }
}
