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
    private List<List<string>> PredeterminedOrders;

    private int SpawnLocationNumber;

    void Start()
    {
        SpawnLocationNumber = 0;

        PredeterminedOrders = new List<List<string>>();
        AddPredeterminedOrders();

        //List<string> TestBurger = RandomBurger(6);
        //foreach (string a in TestBurger)
        //    Debug.Log(a);
        //SpawnRandomOrder(4);
        StartCoroutine(SpawnTicketCountDown(2,4));
        StartCoroutine(SpawnTicketCountDown(4, 4));
        StartCoroutine(SpawnTicketCountDown(6, 4));
        StartCoroutine(SpawnTicketCountDown(8, 4));
        StartCoroutine(SpawnTicketCountDown(10, 4));
    }
    private IEnumerator SpawnTicketCountDown(float CountDown, int BurgerSize)
    {
        yield return new WaitForSeconds(CountDown);
        SpawnRandomOrder(BurgerSize);
    }

    void Update()
    {

    }

    void SpawnPredeterminedOrder()
    {
        OrderManager.Order SpawnedOrder = new OrderManager.Order();

        SpawnedOrder.Ingredents = PredeterminedOrders[Random.Range(0, PredeterminedOrders.Count)];

        SpawnedOrder.TimeIssued = Time.fixedTime;
        SpawnedOrder.TimeExpected = SpawnedOrder.TimeIssued + SpawnedOrder.Ingredents.Count * 20 + 10;

        GameObject Ticket = Instantiate(TicketGO, OrderLocations[SpawnLocationNumber % OrderLocations.Count]);

        OrderTicket TicketScript = Ticket.GetComponent<OrderTicket>();
        TicketScript.UpdateTicket(SpawnedOrder);
        Ticket.transform.parent = TicketParent.transform;

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

        OrderTicket TicketScript = Ticket.GetComponent<OrderTicket>();
        TicketScript.UpdateTicket(SpawnedOrder);
        Ticket.transform.parent = TicketParent.transform;

        OrderManager.Orders.Add(SpawnedOrder);
        SpawnLocationNumber++;

    }

    void AddPredeterminedOrders()
    {
        //adds predesigned burger to the possible burger list
        //designed burger 1
        List<string> TempBurger = new List<string>();

        TempBurger.Add("Top Bun");
        TempBurger.Add("Cheese");
        TempBurger.Add("Patty");
        TempBurger.Add("Lettuce");
        TempBurger.Add("Bottom Bun");

        PredeterminedOrders.Add(TempBurger);

        //designed burger 2
        TempBurger = new List<string>();

        TempBurger.Add("Top Bun");
        TempBurger.Add("Cheese");
        TempBurger.Add("Pickle");
        TempBurger.Add("Patty");
        TempBurger.Add("Tomato");
        TempBurger.Add("Lettuce");
        TempBurger.Add("Bottom Bun");

        PredeterminedOrders.Add(TempBurger);

        //designed burger 3
        TempBurger = new List<string>();

        TempBurger.Add("Top Bun");
        TempBurger.Add("Lettuce");
        TempBurger.Add("Tomato");
        TempBurger.Add("Patty");
        TempBurger.Add("Cheese");
        TempBurger.Add("Patty");
        TempBurger.Add("Bottom Bun");

        PredeterminedOrders.Add(TempBurger);
    }

    List<string> RandomBurger(int AmountOfLayer)
    {
        List<string> TempBurger = new List<string>();
        
        for (int i = 0; i < AmountOfLayer-1; i++)
        {
            /*
            switch (Random.Range(0, 8))
            {
                case 0:
                    TempBurger.Add("Cheese");
                    break;
                case 1:
                    TempBurger.Add("Lettuce");
                    break;
                case 2:
                    TempBurger.Add("Tomato");
                    break;
                case 3:
                    TempBurger.Add("Pickle");
                    break;
                case 4:
                    TempBurger.Add("Mayo");
                    break;
                case 5:
                    TempBurger.Add("Mustard");
                    break;
                case 6:
                    TempBurger.Add("Ketchup");
                    break;
                case 7:
                    TempBurger.Add("Patty");
                    break;
                default:
                    TempBurger.Add("Lettuce");
                    break;
            }
            */
            float Decider = Random.Range(0f, 100f);
            if (Decider < 5)
            {
                TempBurger.Add("Ketchup");
            }
            else if (5 <= Decider && Decider < 20)
            {
                TempBurger.Add("Tomato");
            }
            else if (20 <= Decider && Decider < 35)
            {
                TempBurger.Add("Lettuce");
            }
            else if (35 <= Decider && Decider < 50)
            {
                TempBurger.Add("Pickle");
            }
            else if (50 <= Decider && Decider < 60)
            {
                TempBurger.Add("Ketchup");
            }
            else if (60 <= Decider && Decider < 70)
            {
                TempBurger.Add("Mayo");
            }
            else if (70 <= Decider && Decider < 80)
            {
                TempBurger.Add("Mustard");
            }
            else if (80 <= Decider && Decider <= 100)
            {
                TempBurger.Add("Cheese");
            }



        }
        TempBurger.Insert(AmountOfLayer/2,"Patty");
        TempBurger.Insert(0,"Top Bun");
        TempBurger.Add("Bottom Bun");
        return TempBurger;
    }
}
