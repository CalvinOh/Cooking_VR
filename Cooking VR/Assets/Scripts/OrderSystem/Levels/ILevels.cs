using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OrderSpawn))]

public class ILevels : MonoBehaviour
{
    [SerializeField]
    private string LevelHeader;

    [SerializeField]
    private int NextSceneToLoad;

    protected OrderSpawn MyOrderSpawn;
    protected List<List<OrderManager.Ingridents>> PredeterminedOrders;
    protected List<List<OrderManager.Sides>> SidesToGoWithOrders;
    protected List<float> WaitTimes;

    protected int currentOrderNumber;

    void Start()
    {
        WaitTimes = new List<float>();
        PredeterminedOrders = new List<List<OrderManager.Ingridents>>();
        SidesToGoWithOrders = new List<List<OrderManager.Sides>>();
        MyOrderSpawn = GetComponent<OrderSpawn>();
        currentOrderNumber = 0;//displayed order number starts from one
        AddPredeterminedOrders();
        StartCoroutine(SpawnPTCountDown());
    }

    private IEnumerator SpawnPTCountDown()//spawn for predetermined ticket
    {
        while (currentOrderNumber < PredeterminedOrders.Count)
        {
            yield return new WaitForSeconds(WaitTimes[currentOrderNumber]);
            MyOrderSpawn.SpawnPredeterminedOrder(PredeterminedOrders[currentOrderNumber],LevelHeader+"-"+(currentOrderNumber+1),SidesToGoWithOrders[currentOrderNumber]);
            Debug.Log("Spawning Order Number: " + currentOrderNumber);
            currentOrderNumber++;
        }
        OrderManager.LastOrderSpawned = true;
        OrderManager.NextSceneToLoad = NextSceneToLoad;
        Debug.Log("All orders for the level spawned.");
        

        
    }
    protected virtual void AddPredeterminedOrders()
    {

    }

    protected virtual void AddOrder(float WaitTime, List<OrderManager.Ingridents> Burger, List<OrderManager.Sides> Side)
    {
        WaitTimes.Add(WaitTime);
        PredeterminedOrders.Add(Burger);
        SidesToGoWithOrders.Add(Side);
    }
}
