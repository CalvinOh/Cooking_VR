using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    private Scene currentScene;
    private float scale;
    private GameObject player;

    public int characterSize;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);

        currentScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        CheckSceneMatch();
    }

    private void CheckSceneMatch()
    {
        Scene sceneCheck = SceneManager.GetActiveScene();
        if (currentScene != sceneCheck)
        {
            Destroy(player.gameObject);
            ApplySettings();
        }
        currentScene = sceneCheck;
    }

    public void ResetPosition()
    {
        Transform resetPoint = GameObject.FindGameObjectWithTag("Reset").transform;
        GameObject.FindGameObjectWithTag("Player").transform.SetPositionAndRotation(resetPoint.position, resetPoint.rotation);
    }

    public void ApplySettings()
    {
        CheckSize();
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.localScale = new Vector3(scale, scale, scale);
    }

    private void CheckSize()
    {
        switch (characterSize)
        {
            case 0:
                scale = 1f;
                break;
            case 1:
                scale = 1.05f;
                break;
            case 2:
                scale = 1.1f;
                break;
            case 3:
                scale = 1.15f;
                break;
            case 4:
                scale = 1.2f;
                break;
        }
    }
}
