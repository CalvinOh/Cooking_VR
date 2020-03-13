using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OrderSpawn))]

public class ILevels : MonoBehaviour
{

    protected OrderSpawn MyOrderSpawn;
    protected List<List<OrderManager.Ingridents>> PredeterminedOrders;
    protected List<float> WaitTimes;

    protected int currentOrderNumber;

    void Start()
    {
        WaitTimes = new List<float>();
        PredeterminedOrders = new List<List<OrderManager.Ingridents>>();
        MyOrderSpawn = GetComponent<OrderSpawn>();
        currentOrderNumber = 0;
        AddPredeterminedOrders();
        StartCoroutine(SpawnPTCountDown());
    }

    private IEnumerator SpawnPTCountDown()//spawn for predetermined ticket
    {
        while (currentOrderNumber < PredeterminedOrders.Count)
        {
            yield return new WaitForSeconds(WaitTimes[currentOrderNumber]);
            MyOrderSpawn.SpawnPredeterminedOrder(PredeterminedOrders[currentOrderNumber]);
            currentOrderNumber++;
        }
        Debug.Log("All orders for the level spawned.");
        

        
    }
    protected virtual void AddPredeterminedOrders()
    {

    }
}
