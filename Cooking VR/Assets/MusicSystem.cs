using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSystem : MonoBehaviour
{
    public AK.Wwise.Switch musicSwitch;

    // Start is called before the first frame update
    void Start()
    {
        PlayMusic();
    }

    // Update is called once per frame
    void Update()
    {
        //AkSoundEngine.SetSwitch();
    }

    private void PlayMusic()
    {
        AkSoundEngine.PostEvent("Play_Music", gameObject);
        Debug.Log("Playing Music");
    }
}
