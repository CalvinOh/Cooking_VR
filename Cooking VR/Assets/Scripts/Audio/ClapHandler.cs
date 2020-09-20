using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClapHandler : MonoBehaviour
{
    void ClapEvent()
    {
        AkSoundEngine.PostEvent("Gianna_Clap", gameObject);
    }
}
