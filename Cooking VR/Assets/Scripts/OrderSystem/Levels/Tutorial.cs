using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : ILevels
{ 

    protected override void AddPredeterminedOrders()
    {

        
        List<OrderManager.Ingridents> TempBurger = new List<OrderManager.Ingridents>();
        List<OrderManager.Sides> TempSides = new List<OrderManager.Sides>();

        TempBurger.Add(OrderManager.Ingridents.TopBun);
        TempBurger.Add(OrderManager.Ingridents.Cheese);
        TempBurger.Add(OrderManager.Ingridents.MediumPatty);
        TempBurger.Add(OrderManager.Ingridents.BottomBun);

        base.AddOrder(0, TempBurger, TempSides);
    }
}
