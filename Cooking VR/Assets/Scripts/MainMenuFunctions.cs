using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuFunctions : MonoBehaviour
{
    private bool page1 = true;

    [SerializeField]
    private int sceneNum = 1;

    [SerializeField]
    private int characterSize;

    [SerializeField]
    private Text linkText1;

    [SerializeField]
    private Text linkText2;

    [SerializeField]
    private GameObject creditSwitch1;

    [SerializeField]
    private GameObject creditSwitch2;

    // audio
    // this should stay between 0 - 100
    [SerializeField]
    private float masterVolume;

    public void GoToScene(int sceneNum)
    {
        // audio
        // stops all Wwise audio events except the music
        AkSoundEngine.PostEvent("Stop_All_Except", null);

        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

        SceneManager.LoadScene(sceneNum);
    }

    public void ResetPosition()
    {
        SettingsManager settingsManager = (SettingsManager)FindObjectOfType(typeof(SettingsManager));
        settingsManager.ResetPosition();
    }

    public void SettingSize(int _characterSize)
    {
        characterSize = _characterSize;
    }

    public void SaveSettings()
    {
        SettingsManager settingsManager = (SettingsManager)FindObjectOfType(typeof(SettingsManager));
        settingsManager.characterSize = characterSize;
        settingsManager.ApplySettings();
    }

    public void LondrinaLink()
    {
        Application.OpenURL("https://tipospereira.com/");
        linkText1.text = "Link Opened on Desktop.";
    }

    public void CreditPageSwitch()
    {
        page1 = !page1;
        creditSwitch1.SetActive(!page1);
        creditSwitch2.SetActive(page1);
    }

    public void SiteLink()
    {
        Application.OpenURL("http://hellyeahburgers.com/");
        linkText2.text = "Link Opened on Desktop.";
    }

    // audio
    void VolumeSliderStuff()
    {
        // sets Wwise's Master Volume to the value of "masterVolume" between 0 and 100.
        // 0 is the lowest, 100 is the max
        AkSoundEngine.SetRTPCValue("Volume_Master", masterVolume);

        // use this after the volume is changed to play a spatula impact sound globally (in the listener's head, not 3D space)
        AkSoundEngine.PostEvent("Impact_Spatula", null);
    }
}
