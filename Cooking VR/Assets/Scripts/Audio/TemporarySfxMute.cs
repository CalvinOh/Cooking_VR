using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporarySfxMute : MonoBehaviour
{
    // audio
    // this script sets volume of collide-able objects & the reverb bus to 0 for the first second after loading a level
    // this avoids playing all collision sounds at once when the scene loads and objects detect their first collision with the environment

    [SerializeField]
    private float secondsPassed;

    private bool didMute;

    void Start()
    {
        didMute = false;
        secondsPassed = 0f;
    }

    void Update()
    {
        secondsPassed += Time.deltaTime;

        if (secondsPassed < 1f && !didMute)
        {
            AkSoundEngine.SetRTPCValue("SFX_Volume", 0);
            didMute = true;
        }

        if (secondsPassed >= 1f && didMute)
        {
            AkSoundEngine.SetRTPCValue("SFX_Volume", 100);
        }
    }
}
