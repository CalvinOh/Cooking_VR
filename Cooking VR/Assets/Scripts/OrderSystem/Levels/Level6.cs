using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level6 : ILevels
{

    // Start is called before the first frame update
    protected override void AddPredeterminedOrders()
    {
        List<OrderManager.Ingridents> TempBurger = new List<OrderManager.Ingridents>();
        List<OrderManager.Sides> TempSides = new List<OrderManager.Sides>();

        //burger 1
        TempBurger = new List<OrderManager.Ingridents>();
        TempSides.Add(OrderManager.Sides.Fries);

        TempBurger.Add(OrderManager.Ingridents.TopBun);
        TempBurger.Add(OrderManager.Ingridents.MediumPatty);
        TempBurger.Add(OrderManager.Ingridents.BottomBun);

        base.AddOrder(2, TempBurger, TempSides);

        //burger 2
        TempBurger = new List<OrderManager.Ingridents>();

        TempBurger.Add(OrderManager.Ingridents.TopBun);
        TempBurger.Add(OrderManager.Ingridents.Cheese);
        TempBurger.Add(OrderManager.Ingridents.Pickle);
        TempBurger.Add(OrderManager.Ingridents.WellDonePatty);
        TempBurger.Add(OrderManager.Ingridents.Cheese);
        TempBurger.Add(OrderManager.Ingridents.BottomBun);

        base.AddOrder(10, TempBurger, TempSides);

        //burger 3-7 (using random)
        base.AddOrder(35, MyOrderSpawn.RandomBurger(3), MyOrderSpawn.RandomSides(0));
        base.AddOrder(20, MyOrderSpawn.RandomBurger(4), MyOrderSpawn.RandomSides(1));
        base.AddOrder(40, MyOrderSpawn.RandomBurger(4), MyOrderSpawn.RandomSides(0));
        base.AddOrder(20,MyOrderSpawn.RandomBurger(2),MyOrderSpawn.RandomSides(1));
        base.AddOrder(0, MyOrderSpawn.RandomBurger(2), MyOrderSpawn.RandomSides(1));

    }
}
