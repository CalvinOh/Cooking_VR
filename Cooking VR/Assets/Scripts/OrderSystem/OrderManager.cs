using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{

    public class Order
    {
        //An order
        public float TimeIssued;
        public float TimeExpected;
        public List<Ingridents> Ingredents;
    }

    public class FinishedOrder
    {
        //Completed order that is saved
        public int TotalAmountOfIngredients;
        public float Score;
        public float TimeTaken;
        public string Notes;

    }

    public enum Ingridents
    {
        RawPatty,
        RarePatty,
        MediumPatty,
        WellDonePatty,
        BurntPatty,
        Ketchup,
        Tomato,
        Lettuce,
        Pickle,
        Mayo,
        Mustard,
        Cheese,
        TopBun,
        BottomBun

    }

    private List<FinishedOrder> finishedOrders = new List<FinishedOrder>();
    public static List<Order> Orders = new List<Order>();


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("value: " + (int)Ingridents.RarePatty);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
