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

    }
}
