using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuse : MonoBehaviour
{
    [SerializeField]
    bool Broken;

    private List<GameObject> SlotsInRange;


    private void Start()
    {
        SlotsInRange = new List<GameObject>();
    }

    public void Break(bool broken)
    {
        Broken = broken;
    }






}
