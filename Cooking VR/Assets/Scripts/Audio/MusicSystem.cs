using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicSystem : MonoBehaviour
{
    private string sceneName;
    public bool musicIsPlaying;

    // Start is called before the first frame update
    void Start()
    {
        PlayMusic();
    }

    // Update is called once per frame
    void Update()
    {
        SetMusicIntensity();
    }

    private void PlayMusic()
    {
        AkSoundEngine.PostEvent("Play_Music", gameObject);
    }

    //determine intensity of music based on scene name
    private void SetMusicIntensity()
    {
        sceneName = SceneManager.GetActiveScene().name;

        switch (sceneName)
        {
            case "MainMenu":
                AkSoundEngine.SetSwitch("Intensity", "Level_1", gameObject);
                break;

            case "Tutorial":
                AkSoundEngine.SetSwitch("Intensity", "Level_1", gameObject);
                break;

            case "Level 1":
                AkSoundEngine.SetSwitch("Intensity", "Level_1", gameObject);
                break;

            case "Level 2":
                AkSoundEngine.SetSwitch("Intensity", "Level_1", gameObject);
                break;

            case "Level 3":
                AkSoundEngine.SetSwitch("Intensity", "Level_2", gameObject);
                break;

            case "Level 4":
                AkSoundEngine.SetSwitch("Intensity", "Level_2", gameObject);
                break;

            case "Level 5":
                AkSoundEngine.SetSwitch("Intensity", "Level_2", gameObject);
                break;

            case "Level 6":
                AkSoundEngine.SetSwitch("Intensity", "Level_3", gameObject);
                break;

            case "Level 7":
                AkSoundEngine.SetSwitch("Intensity", "Level_3", gameObject);
                break;

            default:
                AkSoundEngine.SetSwitch("Intensity", "Level_1", gameObject);
                break;
        }
    }
}
