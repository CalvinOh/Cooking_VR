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
        public List<string> Ingredents;
    }

    public class FinishedOrder
    {
        //Completed order that is saved
        public int TotalAmountOfIngredients;
        public float Score;
        public float TimeTaken;
        public string Notes;

    }

    private List<FinishedOrder> finishedOrders = new List<FinishedOrder>();
    public static List<Order> Orders = new List<Order>();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
