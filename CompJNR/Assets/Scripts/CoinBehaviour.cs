using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;
using PlayFab.EconomyModels;

public class CoinBehaviour : MonoBehaviour
{
    CoinAmountManager coinAmountManager;

    public void collectCoin(GameObject coin, GameObject coinText)
    {
        CoinAmountManager.Instance()
            .setCoinText(coinText)
            .increaseCoin(1);
        PhotonNetwork.Destroy(coin);
    }

    public void increaseCoin(int amount, GameObject coinText)
    {
        CoinAmountManager.Instance()
            .setCoinText(coinText)
            .increaseCoin(amount);
    }


    public void decreaseCoin()
    {
        int currnetCoinAmount = CoinAmountManager.Instance()
            .getCoinAmount();
        if (currnetCoinAmount > 3)
        {
            CoinAmountManager.Instance()
            .decreaseCoin(getLoseCoinAmount(currnetCoinAmount));
        }
        else
        {
            CoinAmountManager.Instance()
            .decreaseCoin(1);
        }
    }

    private int getLoseCoinAmount(int currentCoinAmount)
    {
        return (int) (currentCoinAmount * 0.25f);
    }
}
