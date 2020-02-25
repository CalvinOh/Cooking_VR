using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuFunctions : MonoBehaviour
{
    [SerializeField]
    private int sceneNum = 1;

    [SerializeField]
    private int characterSize;

    public void GoToScene(int sceneNum)
    {
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
}
