using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    [SerializeField]
    private Text text;
    private int hours, minutes, seconds;
    private float compensateTime;
    private string timeString;

    [SerializeField]
    private bool useRealTime;
    private bool addedMinute = false;

    // Start is called before the first frame update
    void Start()
    {
        if(this.text == null)
        {
            this.gameObject.GetComponent<Text>();
            timeString = text.text;
        }

        if(!useRealTime)
        {
            hours = 2;
            minutes = 30;
            seconds = 0;
            addedMinute = true;
            compensateTime = 0;
        }
        else
        {
            //DateTime now = DateTime.Now;
            seconds = DateTime.Now.Second;
            compensateTime = seconds;
            minutes = DateTime.Now.Minute;
            hours = DateTime.Now.Hour;

            if(hours > 12)
                hours -= 12;            
        }

        SetUITime();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTime();
    }

    private void UpdateTime()
    {
        //displayTime = timeLimit - Time.time + compensateTime;
        //if((Time.time + compensateTime) > 60)
        //    seconds = Mathf.FloorToInt((Time.time % 60) + compensateTime) - 60;
        //else
            seconds = (Mathf.FloorToInt((Time.time) + compensateTime)) % 60;

        if (seconds == 59)
            addedMinute = false;

        if (seconds == 0 && !addedMinute)// 59)
        {
            //minutes = Mathf.FloorToInt((displayTime / 60));
            //seconds = Mathf.FloorToInt(displayTime - (60 * minutes));
            //seconds -= 60;
            minutes += 1;
            addedMinute = true;
            if (minutes > 59)
            {
                minutes -= 60;
                hours += 1;
                if (hours > 12)
                    hours -= 12;

            }

            SetUITime();
        }
        

    }

    public void SetUITime()
    {
        if (hours > 9)
            timeString = Convert.ToString(hours);
        else
            timeString = $" {Convert.ToString(hours)}";

        timeString += ":";

        if (minutes < 10)
            timeString += $"0{Convert.ToString(minutes)}";
        else
            timeString += Convert.ToString(minutes);

        text.text = timeString;

    }
}
