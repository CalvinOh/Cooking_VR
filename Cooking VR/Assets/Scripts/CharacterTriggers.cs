using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterTriggers : MonoBehaviour
{
    private bool isRaw = false; //is the burger raw
    private bool isBurnt = false; //is the burger burnt
    private bool isEarly = false;
    private bool isLate = false;
    private int grade; //0 is bad, 1 is okay, and 2 is good.
    private bool isMessy = false; //is the countertop messy?

    //For when lines are spoken continuously, this adds time until the line is spoken so that lines do not overlap.
    private float continuousBuffer;

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
            AddBuffer(0);
        }
    }

    private void AddBuffer(float extraTime)
    {
        VOBuffer = rnd.Next(20, 35);
        VOTimer = Time.time + VOBuffer + extraTime;
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
        }
    }

    private void BurgerComplete(bool isComplete)
    {
        float rndLine = rnd.Next(0, 2);
        if (isEarly)
        {
            Invoke("Play_vx_g_1", continuousBuffer);
            continuousBuffer += 5;
        }
        if (isLate)
        {
            Invoke("Play_vx_f_1", continuousBuffer);
            continuousBuffer += 5;
        }
        if (isRaw)
        {
            Invoke("Play_vx_b_4",continuousBuffer);
            continuousBuffer += 5;
        }
        if (isBurnt)
        {
            Invoke("Play_vx_b_3", continuousBuffer);
            continuousBuffer += 5;
        }
        if(grade == 0)
        {
            switch (rndLine)
            {
                case 0:
                    Invoke("Play_vx_e_1", continuousBuffer);
                    break;
                case 1:
                    Invoke("Play_vx_e_2", continuousBuffer);
                    break;
                case 2:
                    Invoke("Play_vx_e_3", continuousBuffer);
                    break;
            }
        }
        else if (grade == 1)
        {
            switch (rndLine)
            {
                case 0:
                    Invoke("Play_vx_c_1", continuousBuffer);
                    break;
                case 1:
                    Invoke("Play_vx_c_2", continuousBuffer);
                    break;
                case 2:
                    Invoke("Play_vx_c_3", continuousBuffer);
                    break;
            }
        }
        else if (grade == 2)
        {
            switch (rndLine)
            {
                case 0:
                    Invoke("Play_vx_d_1", continuousBuffer);
                    break;
                case 1:
                    Invoke("Play_vx_d_2", continuousBuffer);
                    break;
                case 2:
                    Invoke("Play_vx_d_3", continuousBuffer);
                    break;
            }
        }



        //this is where the lines will be sequenced and said in the order we desire. if it's early/late then raw or burnt and finally the grade.
        AddBuffer(continuousBuffer);
        continuousBuffer = 0;
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
