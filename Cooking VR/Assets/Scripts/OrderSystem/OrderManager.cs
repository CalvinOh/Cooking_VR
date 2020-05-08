using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{

    public class Order
    {
        //An order
        public string OrderNum;
        public float TimeIssued;
        public float TimeExpected;
        public List<Ingridents> Ingredents;
    }

    public class FinishedOrder
    {
        //Completed order that is saved
        public string OrderNum;
        public int TotalAmountOfIngredients;
        public float Score;
        public float TimeTaken;
        public int IncorrectPlacement;
        public int ExtraItems;
        public int MissingItems;
        public string Notes;
        public Order OriginalOrder;

    }

    public enum Ingridents
    {
        RawPatty = 0,
        RarePatty = 1,
        MediumPatty = 2,
        WellDonePatty = 3,
        BurntPatty = 4,
        Ketchup = 5,
        Tomato = 6,
        Lettuce = 7,
        Pickle = 8,
        Mayo = 9,
        Mustard = 10,
        Cheese = 11,
        TopBun = 12,
        BottomBun = 13,
        Plate = 14,

    }

    private OrderCheck OrderChecker = new OrderCheck();
    private OrderSpawn OrderSpawner = new OrderSpawn();

    public static List<FinishedOrder> finishedOrders = new List<FinishedOrder>();
    public static List<Order> Orders = new List<Order>();


    // Start is called before the first frame update
    void Start()
    {
        FindOrderCheck();
        FindOrderSpawner();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FindOrderCheck()
    {
        OrderChecker = FindObjectOfType<OrderCheck>();
        if (OrderChecker == null)
            Debug.Log("OrderManager can't find OrderCheck");
        else
            Debug.Log("OrderManager found OrderCheck at: "+OrderChecker.gameObject.name);

    }

    private void FindOrderSpawner()
    {
        OrderSpawner = FindObjectOfType<OrderSpawn>();
        if (OrderSpawner == null)
            Debug.Log("OrderManager can't find OrderSpawn");
        else
            Debug.Log("OrderManager found OrderSpawn at: " + OrderSpawner.gameObject.name);
    }

    public void SpawnArchivedOrder()
    {

    }
}
