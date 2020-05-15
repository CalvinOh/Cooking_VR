using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeSceneChanger : MonoBehaviour
{
    public Animator animator;
    private int SceneToLoad;

    private void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    public void FadeToNextScene()
    {
        
        FadeToScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void FadeToMainMenu()
    {
        // Return To Main Menu /IF/ the main menu's build index is 0!
        FadeToScene(0);
    }

    public void FadeToScene(int SceneIndex)
    {
        SceneToLoad = SceneIndex;
        
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadScene(SceneToLoad);
        
    }
}
