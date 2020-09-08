using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PattyScript : MonoBehaviour
{
    //Originally prototyped and created by P3DR0, later perfected by Jiening

    public float currentCookAmount; // The progress of how much the item is cooked
    public const float maxCookAmount = 60; // The max value for currentCookAmount

    private float[] donenessRefs = {0, 10, 20, 30, 40 };
    private int currentStage;
    private ManualStack MyStackScript;
    [SerializeField]// serialized for debugging
    private bool currentlyCooking; // Is the object inside a cookBox? If true, will continue cooking.

    [SerializeField]
    private List<GameObject> VisualObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        MyStackScript = this.GetComponent<ManualStack>();
        currentCookAmount = 0;
        MyStackScript.ingredientName = OrderManager.Ingridents.RawPatty;
        currentStage = 0;
        //MyStackScript.ingredientName = OrderManager.Ingridents.RarePatty;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentStage <= donenessRefs.Length)
        {
            if (currentlyCooking && currentCookAmount <= maxCookAmount)
            {
                currentCookAmount += Time.deltaTime;

                if (currentCookAmount > donenessRefs[currentStage + 1])
                {
                    switch (currentStage)
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
                    currentStage++;
                    SwitchVisualObject();
                }

            }
        }
        
    }

    private void SwitchVisualObject()
    {
        foreach (GameObject VO in VisualObjects)
        {
            VO.SetActive(false);
        }
        VisualObjects[currentStage].SetActive(true);
    }

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
        //audio
        PlaySoundBurgerCook();
    }

    public void StopCooking()
    {
        currentlyCooking = false;
        //audio
        StopSoundBurgerCook();
    }

    //audio
    private void PlaySoundBurgerCook()
    {
        AkSoundEngine.PostEvent("Burger_Cook_Start", gameObject);
    }

    private void StopSoundBurgerCook()
    {
        AkSoundEngine.PostEvent("Burger_Cook_Stop", gameObject);
    }

    public void BurnPatty()
    {
        currentCookAmount = maxCookAmount;
        MyStackScript.ingredientName = OrderManager.Ingridents.BurntPatty;
        currentStage = 4;
        SwitchVisualObject();
    }
}
