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

    [SerializeField] Terrain terrain;
    [SerializeField] Terrain terrain2;
    [SerializeField] float speed;
    

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

        terrain.transform.Translate(Vector3.left * speed * Time.deltaTime);
        terrain2.transform.Translate(Vector3.left * speed * Time.deltaTime);
        if (terrain.transform.position.x <= -1500)
        {
            terrain.transform.Translate(1995, 0 , 0);
        }
        if (terrain2.transform.position.x <= -1500)
        {
            terrain2.transform.Translate(1995, 0 , 0);
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
