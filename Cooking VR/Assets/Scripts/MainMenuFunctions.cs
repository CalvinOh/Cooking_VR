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

    public void GoToScene(int sceneNum)
    {
        // audio
        AkSoundEngine.PostEvent("Stop_All_Except", gameObject);
        Debug.Log("Stopped all except");

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
}
