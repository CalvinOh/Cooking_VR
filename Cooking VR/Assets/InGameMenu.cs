using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public void BackToMainMenu()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

        // audio
        AkSoundEngine.PostEvent("Stop_All_Except", gameObject);
        Debug.Log("Stopped All Except");

        SceneManager.LoadScene(0);
    }
}