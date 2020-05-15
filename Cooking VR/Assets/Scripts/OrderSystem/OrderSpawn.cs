﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OrderSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject TicketGO;

    [SerializeField]
    private GameObject TicketParent;

    [SerializeField]
    private List<Transform> OrderLocations;

    [SerializeField]
    private GameObject ArchivedTicketGO;

    [SerializeField]
    private GameObject CompleteTicketGO;

    [SerializeField]
    private GameObject ArchivedTicketsPin;
    private int SpawnLocationNumber;
    [SerializeField]
    private float ArchivedTicketSpacing = 1;

    [SerializeField]
    private bool TutorialLevel;

    public static event Action<bool> OrderSpawnedEvent;

    void Start()
    {
        SpawnLocationNumber = 0;
    }


    void Update()
    {

    }
    
    public void SpawnPredeterminedOrder(List<OrderManager.Ingridents> NewOrder,string OrderNum)
    {
        OrderSpawnedEvent.Invoke(true);

        OrderManager.Order SpawnedOrder = new OrderManager.Order();
        SpawnedOrder.OrderNum = OrderNum;
        SpawnedOrder.Ingredents = NewOrder;

        SpawnedOrder.TimeIssued = Time.fixedTime;
        SpawnedOrder.TimeExpected = SpawnedOrder.TimeIssued + SpawnedOrder.Ingredents.Count * 20 + 10;

        GameObject Ticket = Instantiate(TicketGO, OrderLocations[SpawnLocationNumber % OrderLocations.Count]);

        OrderTicket TicketScript = Ticket.GetComponent<OrderTicket>();
        TicketScript.UpdateTicket(SpawnedOrder);
        Ticket.transform.parent = TicketParent.transform;
        Ticket.GetComponent<Rigidbody>().isKinematic = true;

        OrderManager.Orders.Add(SpawnedOrder);
        SpawnLocationNumber++;
    }
    
    public void SpawnRandomOrder(int Burgersize)
    {
        OrderSpawnedEvent.Invoke(true);

        OrderManager.Order SpawnedOrder = new OrderManager.Order();

        SpawnedOrder.Ingredents = RandomBurger(Burgersize);

        SpawnedOrder.TimeIssued = Time.fixedTime;
        SpawnedOrder.TimeExpected = SpawnedOrder.TimeIssued + SpawnedOrder.Ingredents.Count * 20 + 10;

        GameObject Ticket = Instantiate(TicketGO, OrderLocations[SpawnLocationNumber%OrderLocations.Count]);
        OrderTicket TicketScript = Ticket.GetComponent<OrderTicket>();
        TicketScript.UpdateTicket(SpawnedOrder);
        Ticket.transform.parent = TicketParent.transform;
        Ticket.GetComponent<Rigidbody>().isKinematic = true;

        OrderManager.Orders.Add(SpawnedOrder);
        SpawnLocationNumber++;

    }

    List<OrderManager.Ingridents> RandomBurger(int AmountOfLayer)
    {
        List<OrderManager.Ingridents> TempBurger = new List<OrderManager.Ingridents>();
        
        for (int i = 0; i < AmountOfLayer-1; i++)
        {
            float Decider = UnityEngine.Random.Range(0f, 100f);
            if (Decider < 5)
            {
                switch (UnityEngine.Random.Range(0, 4))
                {
                    case 0:
                        TempBurger.Add(OrderManager.Ingridents.RarePatty);
                        break;
                    case 1:
                    case 2:
                        TempBurger.Add(OrderManager.Ingridents.MediumPatty);
                        break;
                    case 3:
                        TempBurger.Add(OrderManager.Ingridents.WellDonePatty);
                        break;

                }
            }
            else if (5 <= Decider && Decider < 27)
            {
                TempBurger.Add(OrderManager.Ingridents.Tomato);
            }
            else if (27 <= Decider && Decider < 50)
            {
                TempBurger.Add(OrderManager.Ingridents.Pickle);
            }
            else if (50 <= Decider && Decider < 60)
            {
                TempBurger.Add(OrderManager.Ingridents.Ketchup);
            }
            else if (60 <= Decider && Decider < 70)
            {
                TempBurger.Add(OrderManager.Ingridents.Mayo);
            }
            else if (70 <= Decider && Decider < 80)
            {
                TempBurger.Add(OrderManager.Ingridents.Mustard);
            }
            else if (80 <= Decider && Decider <= 100)
            {
                TempBurger.Add(OrderManager.Ingridents.Cheese);
            }



        }
        switch (UnityEngine.Random.Range(0, 4))
        {
            case 0:
                TempBurger.Insert(AmountOfLayer / 2, OrderManager.Ingridents.RarePatty);
                break;
            case 1:
            case 2:
                TempBurger.Insert(AmountOfLayer / 2, OrderManager.Ingridents.MediumPatty);
                break;
            case 3:
                TempBurger.Insert(AmountOfLayer / 2, OrderManager.Ingridents.WellDonePatty);
                break;

        }
        TempBurger.Insert(0,OrderManager.Ingridents.TopBun);
        TempBurger.Add(OrderManager.Ingridents.BottomBun);
        return TempBurger;
    }

    public void SpawnArchivedOrder(OrderManager.FinishedOrder a)
    {
        if (!TutorialLevel)
        {
            GameObject ArchivedTicket = Instantiate(ArchivedTicketGO, ArchivedTicketsPin.transform.position, ArchivedTicketsPin.transform.rotation);
            ArchivedTicket.GetComponent<ArchivedTicket>().UpdateTicket(a);
            ArchivedTicket.transform.parent = ArchivedTicketsPin.transform;
            ArchivedTicket.transform.Translate(new Vector3(0, ArchivedTicketSpacing, 0), Space.Self);
        }
    }

    public void SpawnLevelCompleteOrder(int NSTL)
    {
        GameObject CompleteTicket = Instantiate(CompleteTicketGO, OrderLocations[SpawnLocationNumber % OrderLocations.Count]);
        CompleteTicket.GetComponent<LevelCompleteOrder>().SetUp(NSTL);
        CompleteTicket.transform.parent = TicketParent.transform;
    }
}
