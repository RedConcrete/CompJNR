using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    CoinAmountManager coinAmountManager;
    

    public void collectCoin(GameObject coin, GameObject coinText)
    {
        CoinAmountManager.Instance()
            .setCoinText(coinText)
            .increaseCoin(1);
        Destroy(coin);
    }
    
    public void decreaseCoins(GameObject coinText)
    {
        CoinAmountManager.Instance()
            .decreaseCoin(calcLoseAmount(CoinAmountManager.Instance().getCoins()))
            .setCoinText(coinText);
    }


    public int calcLoseAmount(int curentCoinAmount)
    {
        int oneProzent;
        oneProzent = curentCoinAmount / 100;
        return oneProzent * 25;
    }
}
