using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderCheck : MonoBehaviour
{

    [SerializeField]
    private Transform BurgerCheck;
    [SerializeField]
    private Transform TicketCheck;

    [SerializeField]
    float BurgerCheckRadius = 0.3f;
    [SerializeField]
    float TicketCheckRadius = 0.1f;



    // Start is called before the first frame update
    void Start()
    {
        MockBurgerCheck();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(BurgerCheck.position,this.transform.forward*BurgerCheckRadius,Color.red);
        Debug.DrawRay(TicketCheck.position, this.transform.forward*TicketCheckRadius, Color.red);
    }

    OrderManager.FinishedOrder ArchiveOrder(List<OrderManager.Ingridents> SubmittedFood,OrderManager.Order OrderToServe)
    {
        //this function returns a completed order to be achived for the end of the game
        OrderManager.FinishedOrder TheOrder = new OrderManager.FinishedOrder();
        TheOrder.TotalAmountOfIngredients = OrderToServe.Ingredents.Count;
        TheOrder.TimeTaken = Time.fixedTime - OrderToServe.TimeIssued;
        TheOrder.Score = CompareFoodToOrder(SubmittedFood, OrderToServe.Ingredents);


        OrderManager.Orders.Remove(OrderToServe);
        return TheOrder;
    }


    int CompareFoodToOrder(List<OrderManager.Ingridents> SubmittedFood, List<OrderManager.Ingridents> Order)
    {
        int score=(Order.Count-2)*100;

        int IncorrectPositions = 0;
        int LayersTakenOut = 0;

        bool LayerFound = false;

        for (int i = 0; i < Order.Count; i++)
        {

            LayerFound = false;
            for (int j = 0; j < SubmittedFood.Count && !LayerFound; j++)
            {
                if (Order[i] == SubmittedFood[j])//found the ingrident
                {
                    if (i != j)
                        IncorrectPositions++;

                    SubmittedFood.RemoveAt(j);
                    Order.RemoveAt(i);
                    LayersTakenOut++;


                    LayerFound = true;
                    i--;
                }
                else if ((int)Order[i] <= 4 && (int)SubmittedFood[j] <= 4)//didn't find exact ingrident, goes for meat checking
                {
                    score -= CheckMeat(SubmittedFood[i], Order[j]);
                    if (i != j)
                        IncorrectPositions++;

                    SubmittedFood.RemoveAt(j);
                    Order.RemoveAt(i);
                    LayersTakenOut++;


                    LayerFound = true;
                    i--;
                }

            }
        }

        //Debug.Log("LTO: " + LayersTakenOut);
        //Debug.Log("IP: "+IncorrectPositions);
        //Debug.Log("SFC: " + SubmittedFood.Count);
        //Debug.Log("OC: " + Order.Count);
        score -= 10 * IncorrectPositions;// deduct 10 points for each incorrect position but right ingrident
        score -= 50 * SubmittedFood.Count;// deduct 50 points for each extra ingrident not on the order
        score -= 100 * Order.Count;// deduct 100 points for every missing ingrident







        return score;
    }

    private int CheckMeat(OrderManager.Ingridents Meat, OrderManager.Ingridents Order)
    {
        if (Meat == Order)
            return 0;

        if (Meat == OrderManager.Ingridents.BurntPatty || Meat == OrderManager.Ingridents.RawPatty)
            return 90;

        if (Order == OrderManager.Ingridents.MediumPatty)
            return 20;

        if (Meat == OrderManager.Ingridents.MediumPatty)
            return 20;

        return 40;
    }

    public void SubmitFood()
    {

    }

    private void MockBurgerCheck()
    {
        List<OrderManager.Ingridents> Order = new List<OrderManager.Ingridents>();
        List < OrderManager.Ingridents>SubmittedFood = new List<OrderManager.Ingridents>();


        Order.Add(OrderManager.Ingridents.TopBun);
        Order.Add(OrderManager.Ingridents.MediumPatty);
        Order.Add(OrderManager.Ingridents.Cheese);
        Order.Add(OrderManager.Ingridents.BottomBun);

        SubmittedFood.Add(OrderManager.Ingridents.TopBun);
        SubmittedFood.Add(OrderManager.Ingridents.Cheese);
        SubmittedFood.Add(OrderManager.Ingridents.Lettuce);
        SubmittedFood.Add(OrderManager.Ingridents.MediumPatty);
        SubmittedFood.Add(OrderManager.Ingridents.BottomBun);


        string OrderS = "";
        string SubmittedS="";

        foreach (OrderManager.Ingridents a in Order)
        {
            OrderS += (", " + (int)a);
        }

        foreach (OrderManager.Ingridents a in SubmittedFood)
        {
            SubmittedS += (", " + (int)a);
        }

        Debug.Log("Order: " + OrderS);
        Debug.Log("Submitted: "+SubmittedS);
        Debug.Log("Mock Burger Score: " + CompareFoodToOrder(SubmittedFood, Order));


    }

}
