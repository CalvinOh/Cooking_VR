using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4 : ILevels
{

    // Start is called before the first frame update
    protected override void AddPredeterminedOrders()
    {

        //burger 1
        List<OrderManager.Ingridents> TempBurger = new List<OrderManager.Ingridents>();
        List<OrderManager.Sides> TempSides = new List<OrderManager.Sides>();

        TempBurger.Add(OrderManager.Ingridents.TopBun);
        TempBurger.Add(OrderManager.Ingridents.Cheese);
        TempBurger.Add(OrderManager.Ingridents.MediumPatty);
        TempBurger.Add(OrderManager.Ingridents.Cheese);
        TempBurger.Add(OrderManager.Ingridents.BottomBun);

        base.AddOrder(1, TempBurger, TempSides);

        //burger 2
        TempBurger = new List<OrderManager.Ingridents>();

        TempBurger.Add(OrderManager.Ingridents.TopBun);
        TempBurger.Add(OrderManager.Ingridents.Cheese);
        TempBurger.Add(OrderManager.Ingridents.Pickle);
        TempBurger.Add(OrderManager.Ingridents.MediumPatty);
        TempBurger.Add(OrderManager.Ingridents.BottomBun);

        TempSides.Add(OrderManager.Sides.Fries);

        base.AddOrder(15, TempBurger, TempSides);

        //burger 3
        TempBurger = new List<OrderManager.Ingridents>();
        TempSides = new List<OrderManager.Sides>();

        TempBurger.Add(OrderManager.Ingridents.TopBun);
        TempBurger.Add(OrderManager.Ingridents.Cheese);
        TempBurger.Add(OrderManager.Ingridents.Pickle);
        TempBurger.Add(OrderManager.Ingridents.WellDonePatty);
        TempBurger.Add(OrderManager.Ingridents.Cheese);
        TempBurger.Add(OrderManager.Ingridents.BottomBun);

        TempSides.Add(OrderManager.Sides.OnionRings);

        base.AddOrder(35, TempBurger, TempSides);


    }
}
