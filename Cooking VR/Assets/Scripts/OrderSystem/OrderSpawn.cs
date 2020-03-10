using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject TicketGO;
    [SerializeField]
    private GameObject TicketParent;

    [SerializeField]
    private List<Transform> OrderLocations;

    [SerializeField]
    private List<List<OrderManager.Ingridents>> PredeterminedOrders;

    private int SpawnLocationNumber;

    void Start()
    {
        SpawnLocationNumber = 0;

        PredeterminedOrders = new List<List<OrderManager.Ingridents>>();
        AddPredeterminedOrders();

       StartCoroutine( SpawnPreDeterminedTicketCountDown(5,0));

        //List<string> TestBurger = RandomBurger(6);
        //foreach (string a in TestBurger)
        //    Debug.Log(a);
        //SpawnRandomOrder(4);
        /*
        StartCoroutine(SpawnTicketCountDown(1,4));
        StartCoroutine(SpawnTicketCountDown(5, 4));
        StartCoroutine(SpawnTicketCountDown(10, 4));
        StartCoroutine(SpawnTicketCountDown(15, 4));
        StartCoroutine(SpawnTicketCountDown(20, 6));
        */

    }
    private IEnumerator SpawnTicketCountDown(float CountDown, int BurgerSize)
    {
        yield return new WaitForSeconds(CountDown);
        SpawnRandomOrder(BurgerSize);
        
    }


    private IEnumerator SpawnPreDeterminedTicketCountDown(float CountDown, int PreTicketNum)
    {
        yield return new WaitForSeconds(CountDown);
        SpawnPredeterminedOrder(PreTicketNum);

    }

    void Update()
    {

    }
    
    void SpawnPredeterminedOrder(int PreTicketNum)
    {
        OrderManager.Order SpawnedOrder = new OrderManager.Order();

        SpawnedOrder.Ingredents = PredeterminedOrders[PreTicketNum];

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
    
    void SpawnRandomOrder(int Burgersize)
    {
        OrderManager.Order SpawnedOrder = new OrderManager.Order();

        SpawnedOrder.Ingredents = RandomBurger(Burgersize);

        SpawnedOrder.TimeIssued = Time.fixedTime;
        SpawnedOrder.TimeExpected = SpawnedOrder.TimeIssued + SpawnedOrder.Ingredents.Count * 20 + 10;

        GameObject Ticket = Instantiate(TicketGO, OrderLocations[SpawnLocationNumber%OrderLocations.Count]);
        Ticket.name = "fuck you";
        OrderTicket TicketScript = Ticket.GetComponent<OrderTicket>();
        TicketScript.UpdateTicket(SpawnedOrder);
        Ticket.transform.parent = TicketParent.transform;
        Ticket.GetComponent<Rigidbody>().isKinematic = true;

        OrderManager.Orders.Add(SpawnedOrder);
        SpawnLocationNumber++;

    }

    void AddPredeterminedOrders()
    {
        //adds predesigned burger to the possible burger list
        //designed burger 1
        List<OrderManager.Ingridents> TempBurger = new List<OrderManager.Ingridents>();

        TempBurger.Add(OrderManager.Ingridents.TopBun);
        TempBurger.Add(OrderManager.Ingridents.Cheese);
        TempBurger.Add(OrderManager.Ingridents.MediumPatty);
        TempBurger.Add(OrderManager.Ingridents.BottomBun);

        PredeterminedOrders.Add(TempBurger);
    }

    List<OrderManager.Ingridents> RandomBurger(int AmountOfLayer)
    {
        List<OrderManager.Ingridents> TempBurger = new List<OrderManager.Ingridents>();
        
        for (int i = 0; i < AmountOfLayer-1; i++)
        {
            /*
            switch (Random.Range(0, 8))
            {
                case 0:
                    TempBurger.Add(OrderManager.Ingridents.Cheese");
                    break;
                case 1:
                    TempBurger.Add(OrderManager.Ingridents.Lettuce");
                    break;
                case 2:
                    TempBurger.Add(OrderManager.Ingridents.Tomato");
                    break;
                case 3:
                    TempBurger.Add(OrderManager.Ingridents.Pickle");
                    break;
                case 4:
                    TempBurger.Add(OrderManager.Ingridents.Mayo");
                    break;
                case 5:
                    TempBurger.Add(OrderManager.Ingridents.Mustard");
                    break;
                case 6:
                    TempBurger.Add(OrderManager.Ingridents.Ketchup");
                    break;
                case 7:
                    TempBurger.Add(OrderManager.Ingridents.Patty");
                    break;
                default:
                    TempBurger.Add(OrderManager.Ingridents.Lettuce");
                    break;
            }
            */
            float Decider = Random.Range(0f, 100f);
            if (Decider < 5)
            {
                switch (Random.Range(0, 4))
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
            else if (5 <= Decider && Decider < 20)
            {
                TempBurger.Add(OrderManager.Ingridents.Tomato);
            }
            else if (20 <= Decider && Decider < 35)
            {
                TempBurger.Add(OrderManager.Ingridents.Lettuce);
            }
            else if (35 <= Decider && Decider < 50)
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
        switch (Random.Range(0, 4))
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
}
