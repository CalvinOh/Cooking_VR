﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    private float finalOrderScore;

    public static event Action<string> giveToGianna;

    public static event Action<bool> orderComplete;

    private List<string> burgerPartsForVO; //this checks the issues and the grade of the burger and holds onto it to hand off to CharacterTriggers



    // Start is called before the first frame update
    void Start()
    {
        burgerPartsForVO = new List<string>();
       // MockBurgerCheck();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(BurgerCheck.position,this.transform.forward*BurgerCheckRadius,Color.red);
        Debug.DrawRay(TicketCheck.position, this.transform.forward*TicketCheckRadius, Color.red);
    }

    OrderManager.FinishedOrder ArchiveOrder(List<OrderManager.Ingridents> SubmittedFood,OrderManager.Order OrderToServe)
    {
        burgerPartsForVO.Clear();
        //this function returns a completed order to be achived for the end of the game
        OrderManager.FinishedOrder TheOrder = new OrderManager.FinishedOrder();
        TheOrder.TotalAmountOfIngredients = OrderToServe.Ingredents.Count;
        TheOrder.OriginalOrder = OrderToServe;
        TheOrder.TimeTaken = Time.fixedTime - OrderToServe.TimeIssued;
        TheOrder.Score = CompareFoodToOrder(SubmittedFood, OrderToServe.Ingredents);
        if (TheOrder.TimeTaken > .65f * (OrderToServe.TimeExpected - OrderToServe.TimeIssued))
        {
            burgerPartsForVO.Add("fastOrder");
        }
        else if (TheOrder.TimeTaken > 1.5f * (OrderToServe.TimeExpected - OrderToServe.TimeIssued))
        {
            burgerPartsForVO.Add("slowOrder");
        }
        foreach(string sin in burgerPartsForVO)
        {
            giveToGianna.Invoke(sin);
        }

        //finalOrderScore = TheOrder.Score/TheOrder.
        //I need to get the maximum possible points and divide the final score by it in order to get a percentage.

        OrderGrade();

        orderComplete.Invoke(true);

        //score on the order turned in
        Debug.Log(TheOrder.Score);

        OrderManager.Orders.Remove(OrderToServe);
        burgerPartsForVO.Clear();
        return TheOrder;
    }

    private void OrderGrade()
    {
        //this will contain the if statements for assigning a status to the final percentage grade i.e. bad, okay, good, great
    }

    int CompareFoodToOrder(List<OrderManager.Ingridents> SubmittedFood, List<OrderManager.Ingridents> Order)
    {
        int score=(Order.Count-2)*100;
        int IncorrectPositions = 0;
        int LayersTakenOut = 0;
        float CurrentBurgerPercentage = 0;

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
        score -= 10 * IncorrectPositions;// deduct 10 points for each incorrect position but right ingrident
        score -= 50 * SubmittedFood.Count;// deduct 50 points for each extra ingrident not on the order
        score -= 100 * Order.Count;// deduct 100 points for every missing ingrident

        CurrentBurgerPercentage = score / ((Order.Count - 2) * 100);
        CommentOnBurger(CurrentBurgerPercentage);
        return score;
    }

    private void CommentOnBurger(float BurgerPercent)
    {
        if (BurgerPercent > 0.75f)
        {
            burgerPartsForVO.Add("2");
        }
        else if (BurgerPercent > 0.50f)
        {
            burgerPartsForVO.Add("1");
        }
        else if (BurgerPercent <= 0.50f)
        {
            burgerPartsForVO.Add("0");
        }

    }



    private int CheckMeat(OrderManager.Ingridents Meat, OrderManager.Ingridents Order)
    {
        if (Meat == Order)
            return 0;

        if (Meat == OrderManager.Ingridents.BurntPatty || Meat == OrderManager.Ingridents.RawPatty)
        {
            if(Meat == OrderManager.Ingridents.BurntPatty)
            {
                burgerPartsForVO.Add("burntPatty");
            }
            else
            {
                burgerPartsForVO.Add("rawPatty");
            }
            return 90;
        }
        if (Order == OrderManager.Ingridents.MediumPatty)
            return 20;

        if (Meat == OrderManager.Ingridents.MediumPatty)
            return 20;

        return 40;
    }

    public void SubmitFood()
    {
        ////audio
        //PlaySoundBell();

        Debug.Log("Bell rung");
        GameObject Burger = null;
        GameObject Ticket = null;


        Collider[] BurgerCheckColliderHitResults = Physics.OverlapSphere(BurgerCheck.position, BurgerCheckRadius);
        foreach (Collider BurgerC in BurgerCheckColliderHitResults)
        {
            if (BurgerC.CompareTag("Plate"))
            {
                Burger = BurgerC.gameObject;

            }
        }

        Collider[] TicketCheckColliderHitResults = Physics.OverlapSphere(TicketCheck.position, TicketCheckRadius);
        foreach (Collider TicketC in TicketCheckColliderHitResults)
        {
            if (TicketC.CompareTag("Ticket"))
            {
                Ticket = TicketC.gameObject;
            }
        }

        Debug.Log("Burger check: "+Burger.name);
        Debug.Log("Ticket check: "+Ticket.name);

        if (Burger != null && Ticket != null)
        {
            ManualStack BurgerS = Burger.GetComponent<ManualStack>();
            OrderTicket TicketS = Ticket.GetComponent<OrderTicket>();

            List<OrderManager.Ingridents> SubmittedBurger = StackableToListOfIngridents(BurgerS);
            OrderManager.finishedOrders.Add(ArchiveOrder(SubmittedBurger, TicketS.MyOrder));

            Destroy(Burger);
            Destroy(Ticket);
        }
    }

    private List<OrderManager.Ingridents> StackableToListOfIngridents(ManualStack Stack)
    {
        List<OrderManager.Ingridents> TheList = new List<OrderManager.Ingridents>();
        for (int i = 0; i < Stack.ChildrenGameObjects.Count;i++)
        {
            TheList.Add(Stack.ChildrenGameObjects[i].GetComponent<ManualStack>().ingredientName);
        }
        TheList.Remove(OrderManager.Ingridents.Plate);
        return TheList;
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(BurgerCheck.position, BurgerCheckRadius);
        Gizmos.DrawWireSphere(TicketCheck.position, TicketCheckRadius);
    }

    ////audio
    //private void PlaySoundBell()
    //{
    //    //add proper GameObject name once bell is implemented
    //    AkSoundEngine.PostEvent("Bell", GameObject.Find(""));
    //}
}
