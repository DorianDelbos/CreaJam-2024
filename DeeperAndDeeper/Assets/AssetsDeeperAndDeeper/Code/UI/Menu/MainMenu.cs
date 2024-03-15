using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MenuHandler
{
    public void StartGame()
    {
        SceneManager.LoadScene("Trauma1");
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
