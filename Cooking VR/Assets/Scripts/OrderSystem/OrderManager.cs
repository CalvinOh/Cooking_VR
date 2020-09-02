using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OrderManager : MonoBehaviour
{
    public class Order
    {
        //An order
        public string OrderNum;
        public float TimeIssued;
        public float TimeExpected;
        public List<Ingridents> Ingredents;
        public List<Sides> OrderSides;
    }

    public class FinishedOrder
    {
        //Completed order that is saved
        public string OrderNum;
        public int TotalAmountOfIngredients;
        public float Score;
        public float TimeTaken;
        public int IncorrectPlacement;
        public int ExtraItems;
        public int MissingItems;
        public int SidesScore;
        public int ExtraSides;
        public int MissingSides;
        public string Notes;
        public Order OriginalOrder;

    }

    public enum Ingridents
    {
        RawPatty = 0,
        RarePatty = 1,
        MediumPatty = 2,
        WellDonePatty = 3,
        BurntPatty = 4,
        Ketchup = 5,
        Tomato = 6,
        Pickle = 8,
        Mayo = 9,
        Mustard = 10,
        Cheese = 11,
        TopBun = 12,
        BottomBun = 13,
        Plate = 14,
        OnionRing = 15,
        Lettuce = 16,
        OnionRaw = 17,
        Fries = 18

    }

    public enum Sides
    {
        Fries,
        OnionRings,
    }



    private OrderCheck OrderChecker = new OrderCheck();
    private OrderSpawn OrderSpawner = new OrderSpawn();
    private bool LevelCompleted;

    public static List<FinishedOrder> finishedOrders = new List<FinishedOrder>();
    public static List<Order> Orders = new List<Order>();
    public static bool LastOrderSpawned;
    public static int NextSceneToLoad;
    public static string saveLoc = AppDomain.CurrentDomain.DynamicDirectory + "HighScores.txt";

    // Start is called before the first frame update
    void Start()
    {
        finishedOrders.Clear();
        Orders.Clear();
        LastOrderSpawned = false;
        LevelCompleted = false;
        FindOrderCheck();
        FindOrderSpawner();
        OrderChecker.RecieveOrderSpawn(OrderSpawner);
    }

    // Update is called once per frame
    void Update()
    {
        if(LastOrderSpawned)
        LevelFinishCheck();
    }

    private void FindOrderCheck()
    {
        OrderChecker = FindObjectOfType<OrderCheck>();
        if (OrderChecker == null)
            Debug.Log("OrderManager can't find OrderCheck");
        else
            Debug.Log("OrderManager found OrderCheck at: "+OrderChecker.gameObject.name);

    }

    private void FindOrderSpawner()
    {
        OrderSpawner = FindObjectOfType<OrderSpawn>();
        if (OrderSpawner == null)
            Debug.Log("OrderManager can't find OrderSpawn");
        else
            Debug.Log("OrderManager found OrderSpawn at: " + OrderSpawner.gameObject.name);
    }

    private void LevelFinishCheck()
    {
        if (Orders.Count == 0&&!LevelCompleted)
        {
            SaveHighScore();

            OrderSpawner.SpawnLevelCompleteOrder(NextSceneToLoad);
            LevelCompleted = true;
        }
    }

    private void SaveHighScore()
    {
        string[] highScores;
        float[] highScoreNums;
        try
        {
            highScores = File.ReadAllLines(saveLoc);
            highScoreNums = new float[highScores.Length];
            for (int i = 0; i < highScores.Length; i++)
            {
                if (Convert.ToInt32(highScores[i]) != 0)
                    highScoreNums[i] = Convert.ToInt32(highScores[i]);
                else
                    highScoreNums[i] = 0;
            }
        }
        catch (Exception e)
        {
            highScores = new string[5];
            highScoreNums = new float[highScores.Length];
            for (int i = 0; i < highScoreNums.Length; i++)
                highScoreNums[i] = 0;
        }

        string levelName = SceneManager.GetActiveScene().name;
        int targetIndex = 0;

        switch (levelName)
        {
            case "Level 1":
                {
                    targetIndex = 0;
                    break;
                }
            case "Level 2":
                {
                    targetIndex = 1;
                    break;
                }
            case "Level 3":
                {
                    targetIndex = 2;
                    break;
                }
            case "Level 4":
                {
                    targetIndex = 3;
                    break;
                }
            case "Level 5":
                {
                    targetIndex = 4;
                    break;
                }
        }

        float newHighScore = 0;
        foreach (FinishedOrder f in finishedOrders)
        {
            newHighScore += f.Score;
        }
        
        if(newHighScore > highScoreNums[targetIndex])
        {
            highScoreNums[targetIndex] = newHighScore;
            highScores[targetIndex] = highScoreNums[targetIndex].ToString();

            File.WriteAllLines(saveLoc, highScores);
        }
    }
}
