using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialVO : MonoBehaviour
{
    [SerializeField]
    ManualStack FirstBurgerPlate;

    bool pattyOn = false;
    bool cheeseOn = false;
    bool topBunOn = false;

    int currentIngredients;

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach(GameObject i in FirstBurgerPlate.ChildrenGameObjects)
        {
            string ingredientName = i.GetComponent<ManualStack>().ingredientName.ToString();
            if(!pattyOn && ingredientName == "Medium Patty")
            {
                //insert invoke to Voice Line Here
                pattyOn = true;
            }
            else if(!cheeseOn && ingredientName == "Cheese")
            {
                //insert invoke to Voice Line Here
                cheeseOn = true;
            }
            else if(!topBunOn && ingredientName == "Top Bun")
            {
                //insert invoke to Voice Line Here
                topBunOn = true;
            }
        }
    }
}
