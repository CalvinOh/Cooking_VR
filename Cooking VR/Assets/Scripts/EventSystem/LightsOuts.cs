using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsOuts : RandomEvent
{


    [SerializeField]
    private List<GameObject> LightSources = new List<GameObject>();



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void TriggerNow()
    {
        foreach (GameObject a in LightSources)
        {
            a.SetActive(false);
        }
    }
}
