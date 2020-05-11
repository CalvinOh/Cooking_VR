using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public void BackToMainMenu()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0));
    }
}
