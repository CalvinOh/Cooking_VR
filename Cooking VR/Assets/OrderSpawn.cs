using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject OrderGO;
    [SerializeField]
    private List<Transform> OrderLocations;

    [SerializeField]
    private List<List<string>> PredeterminedOrders;

    void Start()
    {
        AddPredeterminedOrders();
    }


    void Update()
    {

    }

    void Spawnorder()
    {
        OrderManager.Order SpawnedOrder = new OrderManager.Order();

        SpawnedOrder.Ingredents = PredeterminedOrders[Random.Range(0, PredeterminedOrders.Count)];
        SpawnedOrder.TimeIssued = Time.fixedTime;

        GameObject Order = Instantiate(OrderGO, OrderLocations[0]);

        OrderManager.Orders.Add(SpawnedOrder);
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
        TempBurger.Add("Top Bun");
        for (int i = 0; i < AmountOfLayer-1; i++)
        {
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
        }
        TempBurger.Add("Bottom Bun");
        return TempBurger;
    }
}
