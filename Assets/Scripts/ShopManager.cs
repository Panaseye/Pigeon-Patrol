using UnityEngine;

public class ShopManager : MonoBehaviour
{


    [SerializeField] int maxBalloons = 2;
    int currBallonsBought=0;
    void Start()
    {
        ResetBalloonsBought();
    }


    void Update()
    {
        
    }


    public void OnClickBuyItem(int itemindex)
    {
        Debug.Log(itemindex + " was bought " );
        if(itemindex==0) {BuyBalloons();}
        else if (itemindex==1){};
    }


    void BuyBalloons()
    {
        if(currBallonsBought >= maxBalloons){return;}
        currBallonsBought++;
        Debug.Log("Ballon bought " +  currBallonsBought);

    }


    //getter & setter for max balloons that can be bought
    public int GetMaxBalloons(){return maxBalloons;}
    public void SetMaxBalloons(int balloons){maxBalloons = balloons;}
    public void ResetBalloonsBought(){currBallonsBought=0;}
}
