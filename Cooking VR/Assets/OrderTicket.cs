using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderTicket : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Text OrderNumber;

    [SerializeField]
    public UnityEngine.UI.Text TimeOrdered;

    [SerializeField]
    private UnityEngine.UI.Text TimeExpected;

    [SerializeField]
    private UnityEngine.UI.Text Body;

    [SerializeField]
    private UnityEngine.UI.Text Note;

    private List<string> FunnyNotes = new List<string>();
    private OrderManager.Order MyOrder;

    // Start is called before the first frame update
    void Start()
    {
        AddFunnyNotes();
        OrderNumber.text = "Order# "+Random.Range(100000, 999999);
        Note.text = "Note: " + FunnyNotes[Random.Range(0, FunnyNotes.Count)];
    }

    public void UpdateTicket(OrderManager.Order OrderA)
    {
        MyOrder = OrderA;
        Debug.Log(MyOrder.TimeIssued);
        Debug.Log(MyOrder.TimeExpected);
        int IntTime = 0;
        IntTime = (int)MyOrder.TimeIssued;
        TimeOrdered.text = "Time Ordered: "+ IntTime / 60+":"+IntTime%60;
        IntTime = (int)MyOrder.TimeExpected;
        TimeExpected.text = "Time Expected: " + IntTime / 60 + ":" + IntTime % 60;
        Body.text = "";
        foreach (string a in MyOrder.Ingredents)
            Body.text += a + "\n";



    }



    void AddFunnyNotes()
    {
        FunnyNotes.Add("Don't tip the chief, they are on a diet of sarcasm and creative insults.");
        FunnyNotes.Add("Mitochondria is the power of the house");
        FunnyNotes.Add("No ice");
        FunnyNotes.Add("None pizza left beef");
        FunnyNotes.Add("Boneless Pizza");
        FunnyNotes.Add("Antidote not included");
        FunnyNotes.Add("Work faster you donkey!");
        FunnyNotes.Add("The cake is a lie");
        FunnyNotes.Add("Be a chief they said");
        FunnyNotes.Add("Try Terasphere too!");
        FunnyNotes.Add("Professionally burnt food");


    }
}
