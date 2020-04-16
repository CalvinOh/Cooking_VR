using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTriggers : MonoBehaviour
{
    private bool isRaw; //is the burger raw
    private bool isBurnt; //is the burger burnt
    private bool isEarly;
    private bool isLate;
    private float gradeNumber; //the numeric grade assigned by the order check.
    private int grade; //0 is horrible, 1 is bad, 2 is okay, and 3 is good.
    private bool isMessy; //is the countertop messy?

    private void recieveSin(string sinRecieved)
    {

    }

    private void OnEnable()
    {
        OrderCheck.giveToGianna += recieveSin;
    }

    private void OnDisable()
    {
        OrderCheck.giveToGianna -= recieveSin;
    }
}
