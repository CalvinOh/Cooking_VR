using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using System;

public class IdleCheck : MonoBehaviour
{

    private Hand[] hands;
    private float currentTime;

    [SerializeField]
    private float timeToIdle = 60;

    public static event Action<bool> Idle;

    // Start is called before the first frame update
    void Start()
    {
        hands = FindObjectsOfType<Hand>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentTime += Time.deltaTime;
        foreach (Hand i in hands)
            if (i.currentAttachedObject != null)
                currentTime = 0;
        if (currentTime >= timeToIdle)
            Idle.Invoke(true);

    }
}
