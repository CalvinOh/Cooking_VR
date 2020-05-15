using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeSceneChanger : MonoBehaviour
{
    public static Animator animator;
    private static int SceneToLoad;

    public static void FadeToNextScene()
    {
        FadeToScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static void FadeToMainMenu()
    {
        // Return To Main Menu /IF/ the main menu's build index is 0!
        FadeToScene(0);
    }

    public static void FadeToScene(int SceneIndex)
    {
        SceneToLoad = SceneIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(SceneToLoad);
    }
}
