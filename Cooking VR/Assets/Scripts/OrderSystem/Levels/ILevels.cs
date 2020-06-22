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
    protected List<float> WaitTimes;

    protected int currentOrderNumber;

    void Start()
    {
        WaitTimes = new List<float>();
        PredeterminedOrders = new List<List<OrderManager.Ingridents>>();
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
            MyOrderSpawn.SpawnPredeterminedOrder(PredeterminedOrders[currentOrderNumber],LevelHeader+"-"+(currentOrderNumber+1));
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
}
