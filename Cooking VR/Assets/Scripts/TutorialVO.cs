using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TutorialVO : MonoBehaviour
{
    [SerializeField]
    ManualStack FirstBurgerPlate;

    bool pattyOn = false;
    bool cheeseOn = false;
    bool topBunOn = false;

    int currentIngredients;

    public static event Action<string, float> VOTrigger;

    private void Start()
    {
        VOTrigger.Invoke("Play_vx_a_1", 2);
        VOTrigger.Invoke("Play_vx_a_2", 40);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        foreach(GameObject i in FirstBurgerPlate.ChildrenGameObjects)
        {
            string ingredientName = i.GetComponent<ManualStack>().ingredientName.ToString();
            if(!pattyOn && ingredientName == "Medium Patty")
            {
                VOTrigger.Invoke("Play_vx_a_3", 0);
                pattyOn = true;
            }
            else if(!cheeseOn && ingredientName == "Cheese")
            {
                VOTrigger.Invoke("Play_vx_a_4", 0);
                cheeseOn = true;
            }
            else if(!topBunOn && ingredientName == "Top Bun")
            {
               VOTrigger.Invoke("Play_vx_a_5", 0);
                topBunOn = true;
            }
        }
    }
}
