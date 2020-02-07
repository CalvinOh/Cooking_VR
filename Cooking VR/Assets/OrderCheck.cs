using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderCheck : MonoBehaviour
{



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    OrderManager.FinishedOrder ArchiveOrder(List<string> SubmittedFood,OrderManager.Order OrderToServe)
    {
        //this function returns a completed order to be achived for the end of the game
        OrderManager.FinishedOrder TheOrder = new OrderManager.FinishedOrder();
        TheOrder.TotalAmountOfIngredients = OrderToServe.Ingredents.Count;
        TheOrder.TimeTaken = Time.fixedTime - OrderToServe.TimeIssued;
        TheOrder.Score = CompareFoodToOrder(SubmittedFood, OrderToServe.Ingredents);


        OrderManager.Orders.Remove(OrderToServe);
        return TheOrder;
    }


    float CompareFoodToOrder(List<string> SubmittedFood, List<string> Order)
    {
        //this functin compares the two lists and gives a float for the percentage of correctness
        float score=-1;
        if (SubmittedFood.Count == Order.Count)
        {

        }






        return score;
    }
}
