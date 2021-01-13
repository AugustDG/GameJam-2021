using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        GameEvents.MainSceneLoading.Invoke(this, EventArgs.Empty);
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}