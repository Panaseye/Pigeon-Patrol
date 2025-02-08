using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject shopScreen; 
    [SerializeField] Button shopButton;
    private bool isPaused = false;
    

    void Start()
    {
        pauseScreen.SetActive(false);
        shopScreen.SetActive(false);
        EnableShop();
    }

    
    void Update()
    {
         if (Input.GetKeyDown(KeyCode.P) && !isPaused)
        {
            PauseGame();
            DisableShop();

        }
        else if (Input.GetKeyDown(KeyCode.P) && isPaused)
        {
            ResumeGame();
            EnableShop();
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



    public void OnClickShopButton()
    {
        shopScreen.SetActive(!shopScreen.activeInHierarchy);
    }

    void EnableShop()
    {
        shopButton.interactable = true;
    }
    
    void DisableShop()
    {
        shopButton.interactable = false;
    }

}
