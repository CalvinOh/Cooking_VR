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

    [SerializeField]
    private int AmountOfFusesThatCanBreak;

    FuseBox Box;

    // Start is called before the first frame update
    void Start()
    {
        DefaultAmbientColor = RenderSettings.ambientLight;
        CurrentlyActive = false;
        Box = FindObjectOfType<FuseBox>();
        Box.MyEventScript = this;
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

        Box.TriggerEvent(Random.Range(0,AmountOfFusesThatCanBreak));

        // audio
        AkSoundEngine.PostEvent("Lightsout_Start", gameObject);
    }

    public override void EndEvent()
    {
        foreach (GameObject a in LightSources)
        {
            a.SetActive(true);
        }
        RenderSettings.ambientLight = DefaultAmbientColor;

        // audio
        if (CurrentlyActive)
        {
            AkSoundEngine.PostEvent("Lightsout_End", gameObject);
        }

        CurrentlyActive = false;
    }

    public void ManualTrigger()
    {
        foreach (GameObject a in LightSources)
        {
            a.SetActive(false);
        }
        RenderSettings.ambientLight = LightsOutAmbientColor;
        CurrentlyActive = true;

        // audio
        AkSoundEngine.PostEvent("Lightsout_Start", gameObject);
    }
}
