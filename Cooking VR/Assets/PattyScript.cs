using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Stackable))]
public class PattyScript : MonoBehaviour
{
    //Originally prototyped and created by P3DR0, later perfected by Jiening

    public float currentCookAmount; // The progress of how much the item is cooked
    public const float maxCookAmount = 60; // The max value for currentCookAmount

    private float[] donenessRefs = { 10, 20, 30, 40 };

    private Stackable MyStackScript;
    [SerializeField]
    private bool currentlyCooking; // Is the object inside a cookBox? If true, will continue cooking.

    // Start is called before the first frame update
    void Start()
    {
        currentCookAmount = 0;
        MyStackScript.ingredientName = OrderManager.Ingridents.RawPatty;
        MyStackScript = this.GetComponent<Stackable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentlyCooking && currentCookAmount <= maxCookAmount)
        {
            currentCookAmount += Time.deltaTime;

            for (int i = 0; i < donenessRefs.Length; i++)
            {
                if (currentCookAmount > donenessRefs[i])
                {
                    switch (i)
                    {
                        case 0: // Rare
                            {
                                MyStackScript.ingredientName = OrderManager.Ingridents.RarePatty;
                                break;
                            }
                        case 1: // Medium
                            {
                                MyStackScript.ingredientName = OrderManager.Ingridents.MediumPatty;
                                break;
                            }
                        case 2: // Well Done
                            {
                                MyStackScript.ingredientName = OrderManager.Ingridents.WellDonePatty;
                                break;
                            }
                        case 3: // Burnt
                            {
                                MyStackScript.ingredientName = OrderManager.Ingridents.BurntPatty;
                                break;
                            }
                    }
                }
            }
        }
    }


    /*
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
    */

    private void OnCollisionEnter(Collision collision)
    {
        // Ground Beef joke
        if (collision.gameObject.CompareTag("Ground") && MyStackScript.ingredientName == OrderManager.Ingridents.RawPatty)
        {
            this.currentCookAmount = 30;
            MyStackScript.ingredientName = OrderManager.Ingridents.WellDonePatty;
        }
    }

    public void StartCooking()
    {
        currentlyCooking = true;
    }

    public void StopCooking()
    {
        currentlyCooking = false;
    }
}
