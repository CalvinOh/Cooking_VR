using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RandomEvent))]
public class DelayedEventTrigger : MonoBehaviour
{
    [SerializeField]
    private float Delay;

    private RandomEvent MyEvent;
    void Start()
    {
        StartCoroutine(Delayedtrigger(Delay));
        MyEvent = GetComponent<RandomEvent>();
    }

    private IEnumerator Delayedtrigger(float Delay)
    {
        yield return new WaitForSeconds(Delay);
        MyEvent.TriggerNow();
    }
}
