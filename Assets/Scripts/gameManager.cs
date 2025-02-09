using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class gameManager : MonoBehaviour
{
    public house house;
    public houseDestruction houseDestruction;
    [SerializeField] GameObject gameScreen;
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject shopScreen; 
    [SerializeField] GameObject gameOverScreen; 

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
    [SerializeField] TextMeshProUGUI finalScoreText;
    [SerializeField] TextMeshProUGUI finalKillingText;

    public AudioSource fall;
    private int score;
    private float timeElapsed;
    public bool gameIsOn;

    void Start()
    {
        gameIsOn = true;
        gameScreen.SetActive(true);
        pauseScreen.SetActive(false);
        shopScreen.SetActive(false);
        gameOverScreen.SetActive(false);
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

        if (gameIsOn)
        
        
        {
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
        SceneManager.LoadScene(0);
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
        gameIsOn = false;
        fall.Play();
        gameOverScreen.SetActive(true);
        gameScreen.SetActive(false);
        houseDestruction.DestroyHouse();
        finalScoreText.text = "Score: "+ score;
    }

    public void UpdateFeathers(int count)
    {
        //UI will be updated automatically
        feathers += count;     
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }

   

    public int GetFeathers(){return feathers;}

}
