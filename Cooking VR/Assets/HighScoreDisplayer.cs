using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreDisplayer : MonoBehaviour
{
    [SerializeField]
    Text[] levelTextBoxes;

    string[] readNums;
    bool set = false;
    
    private void Awake()
    {
        SetReadHighScores();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!set)
            SetReadHighScores();
    }


    private void OnEnable()
    {
        if(!set)
            SetReadHighScores();
    }

    private void SetReadHighScores()
    {
        try
        {
            readNums = File.ReadAllLines(OrderManager.saveLoc);
            set = true;
        }
        catch(Exception e)
        {
            readNums = new string[levelTextBoxes.Length];
            set = false;
        }

        for (int i = 0; i < readNums.Length; i++)
        {
            levelTextBoxes[i].text = $"${readNums[i]}";
        }
    }
}
