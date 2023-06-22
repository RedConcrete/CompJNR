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

}