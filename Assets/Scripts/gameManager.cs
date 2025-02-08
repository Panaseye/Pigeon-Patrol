using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;
    private bool isPaused = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetKeyDown(KeyCode.P) && !isPaused)
        {
            PauseGame();

        }
        else if (Input.GetKeyDown(KeyCode.P) && isPaused)
        {
            ResumeGame();
        }
    }


    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
        isPaused = false;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(1);
    }
}
