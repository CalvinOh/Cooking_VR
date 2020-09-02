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

    [SerializeField]
    private UnityEngine.UI.Text SidesBody;

    private List<string> FunnyNotes = new List<string>();
    public OrderManager.Order MyOrder;

    // Start is called before the first frame update
    void Start()
    {
        AddFunnyNotes();
        
        Note.text = "Note: " + FunnyNotes[Random.Range(0, FunnyNotes.Count)];
    }

    public void UpdateTicket(OrderManager.Order OrderA)
    {
        
        MyOrder = OrderA;
        //Debug.Log(MyOrder.TimeIssued);
        //Debug.Log(MyOrder.TimeExpected);
        OrderNumber.text = "Order: " + MyOrder.OrderNum;
        int IntTime = 0;
        IntTime = (int)MyOrder.TimeIssued;
        TimeOrdered.text = "Time Ordered: "+ IntTime / 60+":"+IntTime%60;
        IntTime = (int)MyOrder.TimeExpected;
        TimeExpected.text = "Time Expected: " + IntTime / 60 + ":" + IntTime % 60;
        Body.text = "";
        SidesBody.text = "";
        foreach (OrderManager.Ingridents a in MyOrder.Ingredents)
        {
            string Temp = a.ToString();
            for (int i = 1; i < Temp.Length; i++)//skips first letter in string
            {
                if (char.IsUpper(Temp[i]))
                {
                    Temp = Temp.Insert(i," ");
                    i += 2;
                    
                }
            }
            Body.text += Temp + "\n";

        }

        foreach (OrderManager.Sides a in MyOrder.OrderSides)
        {
            string Temp = a.ToString();
            for (int i = 1; i < Temp.Length; i++)//skips first letter in string
            {
                if (char.IsUpper(Temp[i]))
                {
                    Temp = Temp.Insert(i, " ");
                    i += 2;

                }
            }
            SidesBody.text += Temp + "\n";
        }
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
        FunnyNotes.Add("Look at all those chickens");
        FunnyNotes.Add("Batteries not included");
        FunnyNotes.Add("Idiot sandwich not included");
        FunnyNotes.Add("I'm making fucking mac and cheese, and nobody can stop me!");
        FunnyNotes.Add("Sombadi toucha ma spaghit");
        FunnyNotes.Add(".................................................I love refrigerators");
        FunnyNotes.Add("I'm glad i don't have to hunt for my food, i don't even know where burgers live.");
        FunnyNotes.Add("Over 1 million burnt");
        FunnyNotes.Add("Don't drop the patty");
        FunnyNotes.Add("Rated 5/5 by bear grills");
        FunnyNotes.Add("Now FDA approved");
        FunnyNotes.Add("You're the chief here, not chief microwave");
        FunnyNotes.Add("I'll have the fire department on stand by");
    }
}
