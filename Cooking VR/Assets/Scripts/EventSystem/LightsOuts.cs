using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsOuts : RandomEvent
{


    [SerializeField]
    private List<GameObject> LightSources = new List<GameObject>();

    [SerializeField]
    private Color LightsOutAmbientColor;
    private Color DefaultAmbientColor;



    private bool CurrentlyActive;
    // Start is called before the first frame update
    void Start()
    {
        DefaultAmbientColor = RenderSettings.ambientLight;
        CurrentlyActive = false;
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
        RenderSettings.ambientLight = LightsOutAmbientColor;
        CurrentlyActive = true;
    }

    public override void EndEvent()
    {
        foreach (GameObject a in LightSources)
        {
            a.SetActive(true);
        }
        RenderSettings.ambientLight = DefaultAmbientColor;
        CurrentlyActive = false;
    }
}
