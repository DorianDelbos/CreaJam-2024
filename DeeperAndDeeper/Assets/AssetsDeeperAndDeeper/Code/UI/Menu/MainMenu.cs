using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MenuHandler
{
    private void Start()
    {
        FadeSystem.instance.ToggleFade(false);
    }

    public void StartGame()
    {
        FadeSystem.instance.ToggleFade(true, () => SceneManager.LoadScene("Trauma1"));
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
