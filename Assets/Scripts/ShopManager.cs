using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ShopManager : MonoBehaviour
{

    [Header("Balloons")]
    [SerializeField] int maxBalloons = 2;
    [SerializeField] int balloonCost= 40;
    int currBallonsBought=0;
    [SerializeField] TextMeshProUGUI balloonLeftText;
    [SerializeField] TextMeshProUGUI balloonCostText;

    [Header("House Upgrades")]
    int maxUpgradeLevel = 3;
    [SerializeField] List<int> upgradeCosts;

    int currUpgradeLevel;

    [SerializeField] TextMeshProUGUI upgradeLevelText;
    [SerializeField] TextMeshProUGUI upgradeCostText;

    [Header("House Repair")]
    [SerializeField] int houseRepairCost = 10;
    [SerializeField] int maxHouseRepairs=3;
    [SerializeField] int repairAmount = 10;
    int currHouseRepairs=0;
    

    [SerializeField] TextMeshProUGUI repairLeftText;
    [SerializeField] TextMeshProUGUI repairCostText;
    [SerializeField] TextMeshProUGUI repairAmountText;


    gameManager gameManagerScript;
    [SerializeField] house houseScript;


    void Awake() 
    {   
        gameManagerScript = GameObject.Find("GameManager").GetComponent<gameManager>();
        InitBalloon();   
        InitHouseRepair(); 
        InitHouseUpgrades();
    }
    void Start()
    {
        
    }


    void Update()
    {
        
    }


    public void OnClickBuyItem(int itemindex)
    {
        Debug.Log(itemindex + " was bought " );

        if(itemindex==0) {BuyBalloons();}
        else if (itemindex==1){
            BuyRepair();
        }
        else if (itemindex==2){
            BuyUpgrade();
        };
    }


    void BuyBalloons()
    {
        if(currBallonsBought >= maxBalloons || gameManagerScript.GetFeathers()<balloonCost){return;}
        
        if(houseScript!=null)
        {
            houseScript.AddBalloons();
        }       
        
        
        currBallonsBought++;
        
        balloonLeftText.text = "Balloons Left: " + (maxBalloons-currBallonsBought);
        if(gameManagerScript!=null)
        {
            gameManagerScript.UpdateFeathers(-balloonCost);
        }

        Debug.Log("Balloon bought " +  currBallonsBought);

    }
    

    void BuyRepair()
    {
       if(currHouseRepairs>=maxHouseRepairs || gameManagerScript.GetFeathers()<houseRepairCost){return;}
       
       if(houseScript!=null)
       {
            houseScript.RepairHouseDamage(repairAmount);
       }
       
       currHouseRepairs++; 

       repairLeftText.text = "Repairs Left: " + (maxHouseRepairs-currHouseRepairs);
       if(gameManagerScript!=null)
       {
            gameManagerScript.UpdateFeathers(-houseRepairCost);
       }


    }

    void BuyUpgrade()
    {
        //player will start at level 0, first upgrade will be level 1 and so on
        //upgradeCosts[currLevel] => cost for currLevel+1

        if(currUpgradeLevel>= maxUpgradeLevel || gameManagerScript.GetFeathers() < upgradeCosts[currUpgradeLevel]){return;}
        if(gameManagerScript!=null)
        {
            gameManagerScript.UpdateFeathers(-upgradeCosts[currUpgradeLevel]);
        }
        
        currUpgradeLevel++;

        upgradeLevelText.text = "Upgrade Level: "+currUpgradeLevel;
        if(currUpgradeLevel==maxUpgradeLevel)
        {
            upgradeCostText.text = "No Further Upgrades Available" ;
        }
        else{
            upgradeCostText.text = "Cost: "+ upgradeCosts[currUpgradeLevel];
        }
        


    }


    //getter & setter for max balloons that can be bought
    public int GetMaxBalloons(){return maxBalloons;}
    public void SetMaxBalloons(int balloons){maxBalloons = balloons;}
    public void ResetBalloonsBought(){currBallonsBought=0;}

    public void UpdateBalloonCost(int newCost)
    {
        balloonCost=newCost;
        balloonCostText.text = "Cost: " + balloonCost;
    }

    void InitBalloon()
    {
        ResetBalloonsBought();
        balloonLeftText.text = "Balloons Left: " + (maxBalloons-currBallonsBought);
        UpdateBalloonCost(balloonCost);
    }

    //getter & setter for house repairs
    public int GetMaxRepairs(){return maxHouseRepairs;}
    public void SetMaxRepairs(int repairs){maxHouseRepairs=repairs;}
    public void ResetRepairsBought(){currHouseRepairs=0;}

    public void UpdateRepairCost(int newCost)
    {
        houseRepairCost = newCost;
        repairCostText.text = "Cost: "+ houseRepairCost;
    }

    void InitHouseRepair()
    {
        ResetRepairsBought();
        UpdateRepairCost(houseRepairCost);
        repairLeftText.text = "Repairs Left: " + (maxHouseRepairs-currHouseRepairs);
        repairAmountText.text = "Repair Amount: " + repairAmount;

    }
    public void ResetUpgrades(){currUpgradeLevel=0;}

    void InitHouseUpgrades()
    {

        ResetUpgrades();
        maxUpgradeLevel = upgradeCosts.Count;
        upgradeCostText.text = "Cost: "+ upgradeCosts[currUpgradeLevel];
        upgradeLevelText.text = "Upgrade Level: "+currUpgradeLevel;
    }

}
