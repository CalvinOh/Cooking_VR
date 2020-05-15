using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class CharacterTriggers : MonoBehaviour
{
    private bool isRaw = false; //is the burger raw
    private bool isBurnt = false; //is the burger burnt
    private bool isEarly = false;
    private bool isLate = false;
    private int grade; //0 is bad, 1 is okay, and 2 is good.
    private bool isMessy = false; //is the countertop messy?
    private bool isTutorialScene = false;

    //For when lines are spoken continuously, this adds time until the line is spoken so that lines do not overlap.
    private float continuousBuffer;

    //this is the buffer that will be randomized between 20-35 seconds for when idle VO will be spoken
    private float VOBuffer;

    //stores last time a line was spoken and also adds the VOBuffer to get the time the next line will be spoken.
    private float VOTimer;

    //this shall be the queue of the dialogue that is to be spoken.
    private List<int> VOQueue = new List<int>();
    private GiannaAnimator MyAnimator;

    public static event Action<string,float> VOTrigger;

    System.Random rnd = new System.Random();


    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
            isTutorialScene = true;
        if(!isTutorialScene)
            MyAnimator = GetComponentInChildren<GiannaAnimator>();


       // VOTrigger.Invoke("Play_vx_b_6", 3);
        //MyAnimator.PlayAngry(3);
    }
    private void FixedUpdate()
    {
        if(Time.time >= VOTimer && !isTutorialScene)
        {
            IdleDialogue();
            //where idle lines will be called.
            AddBuffer(0);
        }
    }

    private void IdleDialogue()
    {
        int rndLine = rnd.Next(0, 5);
        //adds a random voice line to be played at the end of the queue, if something else more important has come up to be mentioned than it shall be inserted at the beginning of VOQueue.
        VOQueue.Add(rndLine); //0 through 5 are lines that require no prerequisite to be spoken.
        switch (VOQueue[0])
        {
            //VOTrigger.Invokes that are commented out are lines that have yet to be implemented.
            case 0:
                //VOTrigger.Invoke("Play_vx_i_1", 0);
                break;
            case 1:
                //VOTrigger.Invoke("Play_vx_i_2", 0);
                break;
            case 2:
                //VOTrigger.Invoke("Play_vx_i_5", 0);
                break;
            case 3:
                //VOTrigger.Invoke("Play_vx_i_8", 0);
                break;
            case 4:
                //VOTrigger.Invoke("Play_vx_j_4", 0);
                break;
            case 5:
                //VOTrigger.Invoke("Play_vx_j_5", 0);
                break;
            case 6:
                // when a bad move is made
                VOTrigger.Invoke("Play_vx_b_6", 0);
                break;
            case 7:
                // when a bad move is made
                VOTrigger.Invoke("Play_vx_b_7", 0);
                MyAnimator.PlayDisappointed();
                break;
            case 8:
                // when a good move is made
                //VOTrigger.Invoke("Play_vx_h_1", 0);
                //MyAnimator.Face.Smile(4.5f, 70);
                break;
            case 9:
                // when a good move is made
                //VOTrigger.Invoke("Play_vx_h_2", 0);
                //MyAnimator.Face.Smile(4.5f, 70);
                break;
            case 10:
                // when the place is messy
                //VOTrigger.Invoke("Play_vx_i_3", 0);
                //MyAnimator.Face.Question(4.5f, 100);
                break;
            case 11:
                // when the player is idle
                //VOTrigger.Invoke("Play_vx_i_4", 0);
                //MyAnimator.PlaySassy();
                //MyAnimator.Face.RessetFace();
                //MyAnimator.Face.Neutral(4.5f,100);
                break;
            case 12:
                // when the top bun is stacked
                //VOTrigger.Invoke("Play_vx_i_7", 0);
                break;
            case 13:
                // when a new ticket spawns
                //VOTrigger.Invoke("Play_vx_i_9", 0);
                break;
        }
        VOQueue.RemoveAt(0);
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
        if (!isTutorialScene)
        {
            float rndLine = rnd.Next(0, 2);
            if (isEarly)
            {
                VOTrigger.Invoke("Play_vx_g_1", continuousBuffer);
                continuousBuffer += 5;
            }
            else if (isLate)
            {
                VOTrigger.Invoke("Play_vx_f_1", continuousBuffer);
                continuousBuffer += 5;
            }
            if (isRaw)
            {
                MyAnimator.PlayAngry(continuousBuffer);
                VOTrigger.Invoke("Play_vx_b_4", continuousBuffer);
                continuousBuffer += 5;
            }
            else if (isBurnt)
            {
                MyAnimator.PlayAngry(continuousBuffer);
                VOTrigger.Invoke("Play_vx_b_3", continuousBuffer);
                continuousBuffer += 5;
            }
            if (grade == 0)
            {
                switch (rndLine)
                {
                    case 0:
                        VOTrigger.Invoke("Play_vx_e_1", continuousBuffer);
                        MyAnimator.PlayDisappointed(continuousBuffer);
                        break;
                    case 1:
                        VOTrigger.Invoke("Play_vx_e_2", continuousBuffer);
                        MyAnimator.PlayAngry(continuousBuffer);
                        break;
                    case 2:
                        VOTrigger.Invoke("Play_vx_e_3", continuousBuffer);
                        MyAnimator.PlaySassy(continuousBuffer);
                        break;
                }
            }
            else if (grade == 1)
            {
                switch (rndLine)
                {
                    case 0:
                        VOTrigger.Invoke("Play_vx_c_1", continuousBuffer);
                        MyAnimator.PlaySassy(continuousBuffer);
                        break;
                    case 1:
                        VOTrigger.Invoke("Play_vx_c_2", continuousBuffer);
                        break;
                    case 2:
                        VOTrigger.Invoke("Play_vx_c_3", continuousBuffer);
                        break;
                }
            }
            else if (grade == 2)
            {
                switch (rndLine)
                {
                    case 0:
                        VOTrigger.Invoke("Play_vx_d_1", continuousBuffer);
                        MyAnimator.PlayImpressed(continuousBuffer);
                        break;
                    case 1:
                        VOTrigger.Invoke("Play_vx_d_2", continuousBuffer);
                        MyAnimator.PlayImpressed(continuousBuffer);
                        break;
                    case 2:
                        VOTrigger.Invoke("Play_vx_d_3", continuousBuffer);

                        break;
                }
            }



            //this is where the lines will be sequenced and said in the order we desire. if it's early/late then raw or burnt and finally the grade.
            AddBuffer(continuousBuffer);
            continuousBuffer = 0;
        }
    }

    private void OrderSpawned(bool obj)
    {
        VOQueue.Insert(0, 9);
    }


    private void OnEnable()
    {
        OrderCheck.giveToGianna += RecieveFinalBurgerParts;
        OrderCheck.orderComplete += BurgerComplete;
        OrderSpawn.OrderSpawnedEvent += OrderSpawned;
    }

    private void OnDisable()
    {
        OrderCheck.giveToGianna -= RecieveFinalBurgerParts;
        OrderCheck.orderComplete -= BurgerComplete;
        OrderSpawn.OrderSpawnedEvent -= OrderSpawned;
    }
}
