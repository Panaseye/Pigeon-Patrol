using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public GameObject settings;
    public GameObject credits;
    public GameObject mainMenu;
    void Start()
    {
        mainMenu.SetActive(true);
        settings.SetActive(false);
        credits.SetActive(false);
    }

    
    public void StartGame()
    {
        SceneManager.LoadScene(0);

    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        // If running in the Unity editor, stop play mode
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        // If the game is built and running, quit the application
        Application.Quit();
        #endif
    }

    public void SettingsToggle()
    {
        if (mainMenu.gameObject.activeSelf)
        {
            mainMenu.SetActive(false);
            settings.SetActive(true);
        }else
        {
            mainMenu.SetActive(true);
            settings.SetActive(false);
        } 
        
    }

    public void CreditsToggle()
    {
        if (mainMenu.gameObject.activeSelf)
        {
            mainMenu.SetActive(false);
            credits.SetActive(true);
        }else
        {
            mainMenu.SetActive(true);
            credits.SetActive(false);
        } 
        
    }
}
