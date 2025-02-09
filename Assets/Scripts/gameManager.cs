using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class gameManager : MonoBehaviour
{
    public house house;
    public houseDestruction houseDestruction;
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject shopScreen; 

    [SerializeField] Button shopButton;
    private bool isPaused = false;

    [SerializeField] Terrain terrain;
    [SerializeField] Terrain terrain2;
    [SerializeField] float speed;
    public int feathers;
    [SerializeField] TextMeshProUGUI feathersText;
    [SerializeField] TextMeshProUGUI buoyancyText;
    [SerializeField] TextMeshProUGUI houseDamageText;
    [SerializeField] TextMeshProUGUI scoreText;
    private int score;
    private float timeElapsed;

    void Start()
    {
        pauseScreen.SetActive(false);
        shopScreen.SetActive(false);
        EnableShop();
        feathers =0;
        score = 0;
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
        terrain2.transform.Translate(Vector3.left * speed  * Time.deltaTime);
        if (terrain.transform.position.x <= -1500)
        {
            terrain.transform.Translate(1995, 0 , 0);
        }
        if (terrain2.transform.position.x <= -1500)
        {
            terrain2.transform.Translate(1995, 0 , 0);
        }

        timeElapsed += Time.deltaTime; 

    if (timeElapsed >= 1f) 
    {
        score += 1;
        timeElapsed = 0f; 
        Debug.Log("Score: " + score);
    }

        feathersText.text = "Feathers: " + feathers;
        buoyancyText.text = "Buoyancy: " + house.buoyancy;
        houseDamageText.text = "House Damage: " + house.houseDamage;
        scoreText.text = "Score: " + score;

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

    public void ResumeGameFromShop()
    {
        Time.timeScale = 1;
        shopScreen.SetActive(false);
    }


    public void BackToMenu()
    {
        SceneManager.LoadScene(1);
    }



    public void OnClickShopButton()
    {
        if(shopScreen.activeInHierarchy){Time.timeScale = 1;}
        else {Time.timeScale=0;}
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

    public void GameOver()
    {
        houseDestruction.DestroyHouse();
    }

    public void UpdateFeathers(int count)
    {
        //UI will be updated automatically
        feathers += count;     
    }

    public int GetFeathers(){return feathers;}

}
