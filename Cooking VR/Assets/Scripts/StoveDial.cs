﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace Valve.VR.InteractionSystem
{
    enum EDialState
    {
        EDS_Off,
        EDS_On,

        EDS_Default
    }
    public class StoveDial : MonoBehaviour
    {
        [SerializeField]
        float SetTime;

        public Interactable interactable;
        public float CountdownTime;
        EDialState DialState;
        EDialState PreviousState;

        // Start is called before the first frame update
        void Start()
        {
            DialState = EDialState.EDS_Off;
            CountdownTime = SetTime;

            //in case the interactable script does not get set
            if (interactable == null)
            {
                interactable = GetComponent<Interactable>();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (interactable.attachedToHand)
            {
                if (CountdownTime <= 0.0f)
                {
                    if (SteamVR_Actions._default.GrabPinch.GetStateDown(SteamVR_Input_Sources.Any))
                    {
                        Debug.Log("Dial State Change");
                        ChangeDialState();
                        ChangeRotation();
                        CountdownTime = SetTime;
                    }
                }
                else
                {
                    CountdownTime -= Time.deltaTime;
                }
            }
        }

        void ChangeRotation()
        {
            if(PreviousState != DialState)
            {
                if(DialState == EDialState.EDS_Off)
                {
                    this.gameObject.transform.Rotate(0, 0, 0);
                }
                else if(DialState == EDialState.EDS_On)
                {
                    this.gameObject.transform.Rotate(0, 0, 90);
                }
            }
        }
        void ChangeDialState()
        {
            PreviousState = DialState;
            switch(DialState)
            {
                case EDialState.EDS_Off:
                    DialState = EDialState.EDS_On;
                    break;
                case EDialState.EDS_On:
                    DialState = EDialState.EDS_Off;
                    break;
            }
        }
    }
}
