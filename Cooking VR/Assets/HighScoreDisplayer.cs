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
    
    // Start is called before the first frame update
    void Start()
    {
        SetReadHighScores();
    }

    private void SetReadHighScores()
    {
        try
        {
            readNums = File.ReadAllLines(OrderManager.saveLoc);
        }
        catch(Exception e)
        {
            readNums = new string[0];
        }

        for (int i = 0; i < readNums.Length; i++)
        {
            levelTextBoxes[i].text = $"${readNums[i]}";
        }
    }
}
