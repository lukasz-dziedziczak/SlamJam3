using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_StartScreen : MonoBehaviour
{
    [SerializeField] UI_Blackscreen Blackscreen;

    public void OnStartPress()
    {
        Blackscreen.StartFadeOut();
        Blackscreen.FadeOutComplete += LoadNextScene;
    }

    public void OnExitPress()
    {
        Application.Quit();
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }
}
