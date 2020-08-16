using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{

    [SerializeField]
    float RandomRollInterval;

    [SerializeField]
    [Tooltip("Shoould be between 0-1, 1 meaninig an event always triggers on random roll, 0 means never")]
    float RandomRollChance;

    [SerializeField]
    float EventCoolDown;

    public static bool EventsEnabled;

    public int EventTriggeredTimes;
    private List<RandomEvent> Events = new List<RandomEvent>();

    // Start is called before the first frame update
    void Start()
    {
        EventsEnabled = true;
        StartCoroutine(ConstantRandomRoll(RandomRollInterval));
        EventTriggeredTimes = 0;
        foreach (RandomEvent a in GetComponentsInChildren<RandomEvent>())
        {
            Events.Add(a);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator ConstantRandomRoll(float Delay)
    {
        yield return new WaitForSeconds(Delay);
        if (EventsEnabled)
        {
            if (RandomRoll()&&EventCoolDown>RandomRollInterval)
            {
                StartCoroutine(ConstantRandomRoll(EventCoolDown));
            }
            else
            {
                StartCoroutine(ConstantRandomRoll(RandomRollInterval));
            }
        }
        
    }

    private bool RandomRoll()
    {
        if (Random.Range(0f, 1f) < RandomRollChance)
        {
            TriggerRandomEvent();
            EventTriggeredTimes++;
            Debug.Log("EventRolled True");
            return true;
        }
        else
        {
            Debug.Log("EventRolled False");
            return false;
        }
    }

    private void TriggerRandomEvent()
    {
        int decider = Random.Range(0, Events.Count);
        if(!Events[decider].CurrentlyActive)
            Events[decider].TriggerNow();

    }

}
