using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinAmountManager 
{
    private int coins;
    private TMP_Text coinText;
    private static CoinAmountManager coinAmountManager;

    private CoinAmountManager()
    {

    }

    public static CoinAmountManager Instance()
    {
        if (coinAmountManager == null)
        {
            coinAmountManager = new CoinAmountManager();
        }
        return coinAmountManager;
    }

    public CoinAmountManager setCoinText(GameObject _coinText)
    {
        coinText = _coinText.GetComponent<TMP_Text>();
        return coinAmountManager;
    }

   public CoinAmountManager increaseCoin(int amount){
        coins += amount;
        coinText.text = "Coins: " + coins.ToString();
        return coinAmountManager;
    }

   public CoinAmountManager decreaseCoin(int amount)
    {
        coins -= amount;
        coinText.text = "Coins: " + coins.ToString();
        return coinAmountManager;
    }

    public int getCoins()
    {
        return coins;
    }
}
