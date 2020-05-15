using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    private Scene currentScene;
    private float scale;
    private GameObject player;
    Scene sceneCheck;

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
        sceneCheck = SceneManager.GetActiveScene();
        if (currentScene != sceneCheck)
        {
            ApplySettings();
            SettingsManager[] managers = FindObjectsOfType<SettingsManager>();
            foreach (SettingsManager sm in managers)
            {
                if (!sm.Equals(this))
                {
                    SceneManager.MoveGameObjectToScene(sm.gameObject, SceneManager.GetActiveScene());
                    Destroy(sm.gameObject);
                }

            }
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
                scale = 1.0f;
                break;
            case 1:
                scale = 1.1f;
                break;
            case 2:
                scale = 1.2f;
                break;
            case 3:
                scale = 1.3f;
                break;
            case 4:
                scale = 1.4f;
                break;
        }
    }
}
