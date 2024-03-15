using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MenuHandler
{
    private FadeSystem fadeSystem;

    private void Start()
    {
        fadeSystem = FindObjectOfType<FadeSystem>();
        fadeSystem.ToggleFade(false);
    }

    public void StartGame()
    {
        fadeSystem.ToggleFade(true, () => SceneManager.LoadScene("Trauma1"));
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        if (Application.isPlaying)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
#else
        Application.Quit();
#endif
    }
}
