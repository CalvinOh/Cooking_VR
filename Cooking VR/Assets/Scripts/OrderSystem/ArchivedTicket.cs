using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchivedTicket : MonoBehaviour
{

    [SerializeField]
    private UnityEngine.UI.Text OrderNumber;

    [SerializeField]
    public UnityEngine.UI.Text TotalScore;

    [SerializeField]
    private UnityEngine.UI.Text ActualScore;

    [SerializeField]
    private UnityEngine.UI.Text TimeTaken;

    [SerializeField]
    private UnityEngine.UI.Text Body;
    [SerializeField]
    private UnityEngine.UI.Text Body2;

    [SerializeField]
    private UnityEngine.UI.Text ChiefsNote;

    public OrderManager.FinishedOrder MyOrder;


    public void UpdateTicket(OrderManager.FinishedOrder OrderA)
    {
        
        MyOrder = OrderA;
        //Debug.Log(MyOrder.TimeIssued);
        //Debug.Log(MyOrder.TimeExpected);
        OrderNumber.text = "Order: " + MyOrder.OrderNum;
        TotalScore.text = "Total Score: " + (MyOrder.TotalAmountOfIngredients - 2 * 100);
        ActualScore.text = "Actual Score: " + MyOrder.Score;
        TimeTaken.text = "Time Taken: " + MyOrder.TimeTaken;
        Body.text = "Misplaced: \n";
        Body.text += "Extra: \n ";
        Body.text += "Missing: \n ";

        Body2.text = MyOrder.IncorrectPlacement + "   -----   -" + MyOrder.IncorrectPlacement * 10;
        Body2.text += MyOrder.ExtraItems + "   -----   -" + MyOrder.ExtraItems * 50;
        Body2.text += MyOrder.MissingItems + "   -----   -" + MyOrder.MissingItems * 100;







    }


}
