using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : ILevels
{

    protected override void AddPredeterminedOrders()
    {

        //burger 1
        List<OrderManager.Ingridents> TempBurger = new List<OrderManager.Ingridents>();

        TempBurger.Add(OrderManager.Ingridents.TopBun);
        TempBurger.Add(OrderManager.Ingridents.Cheese);
        TempBurger.Add(OrderManager.Ingridents.MediumPatty);
        TempBurger.Add(OrderManager.Ingridents.BottomBun);

        base.WaitTimes.Add(2);
        PredeterminedOrders.Add(TempBurger);

        //burger 2
        TempBurger = new List<OrderManager.Ingridents>();

        TempBurger.Add(OrderManager.Ingridents.TopBun);
        TempBurger.Add(OrderManager.Ingridents.Cheese);
        TempBurger.Add(OrderManager.Ingridents.MediumPatty);
        TempBurger.Add(OrderManager.Ingridents.BottomBun);

        base.WaitTimes.Add(2);
        PredeterminedOrders.Add(TempBurger);

        //burger 3
        TempBurger = new List<OrderManager.Ingridents>();

        TempBurger.Add(OrderManager.Ingridents.TopBun);
        TempBurger.Add(OrderManager.Ingridents.Cheese);
        TempBurger.Add(OrderManager.Ingridents.RarePatty);
        TempBurger.Add(OrderManager.Ingridents.BottomBun);

        base.WaitTimes.Add(5);
        PredeterminedOrders.Add(TempBurger);

        //burger 2
        TempBurger = new List<OrderManager.Ingridents>();

        TempBurger.Add(OrderManager.Ingridents.TopBun);
        TempBurger.Add(OrderManager.Ingridents.Cheese);
        TempBurger.Add(OrderManager.Ingridents.Pickle);
        TempBurger.Add(OrderManager.Ingridents.WellDonePatty);
        TempBurger.Add(OrderManager.Ingridents.BottomBun);

        base.WaitTimes.Add(5);
        PredeterminedOrders.Add(TempBurger);


    }
}
