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

    private List<string> FunnyNotes;
    private OrderManager.Order MyOrder;

    // Start is called before the first frame update
    void Start()
    {
        OrderNumber.text = Random.Range(100000, 999999)+"";
        Note.text = "Note: " + FunnyNotes[Random.Range(0, FunnyNotes.Count)];
    }

    void UpdateTicket()
    {
        int IntTime = (int)MyOrder.TimeIssued;
        TimeOrdered.text = IntTime/60+":"+IntTime%60;
        IntTime = (int)MyOrder.TimeExpected;
        TimeExpected.text = IntTime / 60 + ":" + IntTime % 60;
    }



    void AddFunnyNotes()
    {
        FunnyNotes.Add("Don't tip the chief, they are on a diet of sarcasm and creative insults.");
        FunnyNotes.Add("Mitochondria is the power of the house");
        FunnyNotes.Add("No ice");
        FunnyNotes.Add("None pizza left beef");
        FunnyNotes.Add("Boneless Pizza");
        FunnyNotes.Add("Antidote not included");
        FunnyNotes.Add("Work fast you donkey!");
        FunnyNotes.Add("The cake is a lie");
        FunnyNotes.Add("Be a chief they said");
        FunnyNotes.Add("Try Terasphere too!");

    }
}
