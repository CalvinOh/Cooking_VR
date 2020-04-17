﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterTriggers : MonoBehaviour
{
    private bool isRaw = false; //is the burger raw
    private bool isBurnt = false; //is the burger burnt
    private bool isEarly = false;
    private bool isLate = false;
    private int grade; //0 is horrible, 1 is bad, 2 is okay, and 3 is good.
    private bool isMessy = false; //is the countertop messy?

    //this is the buffer that will be randomized between 20-35 seconds for when idle VO will be spoken
    private float VOBuffer;

    //stores last time a line was spoken and also adds the VOBuffer to get the time the next line will be spoken.
    private float VOTimer;

    //this shall be the queue of the dialogue that is to be spoken.
    private List<string> VOQueue;

    System.Random rnd = new System.Random();

    private void FixedUpdate()
    {
        if(Time.time >= VOTimer)
        {
            //where idle lines will be called.
            AddBuffer();
        }
    }

    private void AddBuffer()
    {
        VOBuffer = rnd.Next(20, 35);
        VOTimer = Time.time + VOBuffer;
    }

    private void RecieveFinalBurgerParts(string burgerRecieved)
    {
        switch (burgerRecieved)
        {
            case "burntPatty":
                isBurnt = true;
                break;
            case "rawPatty":
                isRaw = true;
                break;
            case "isLate":
                isLate = true;
                break;
            case "isEarly":
                isEarly = true;
                break;
            case "0":
                grade = 0;
                break;
            case "1":
                grade = 1;
                break;
            case "2":
                grade = 2;
                break;
            case "3":
                grade = 3;
                break;
        }
    }

    private void BurgerComplete(bool isComplete)
    {
        //this is where the lines will be sequenced and said in the order we desire. if it's early/late then raw or burnt and finally the grade.
        AddBuffer();
    }

    private void OnEnable()
    {
        OrderCheck.giveToGianna += RecieveFinalBurgerParts;
        OrderCheck.orderComplete += BurgerComplete;
    }

    private void OnDisable()
    {
        OrderCheck.giveToGianna -= RecieveFinalBurgerParts;
        OrderCheck.orderComplete -= BurgerComplete;
    }
}
