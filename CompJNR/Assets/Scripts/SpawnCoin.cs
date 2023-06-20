using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon;
using Photon.Pun;
using System;

public class SpawnCoin : MonoBehaviour
{

    public GameObject coinPrefab;

    public int coinAmount;

    public Vector3 size;

    private TMP_Text coinText;


    private void Start()
    {
        SpawnCoinOnRandomePos();
    }

    public void SpawnCoinOnRandomePos()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            for (int i = 0; i <= coinAmount; i++)
            {
                Vector3 pos = transform.localPosition + new Vector3(0, 0, UnityEngine.Random.Range(-size.z / 2, size.z / 2));
                //Instantiate(coinPrefab, pos + new Vector3(0,0, i), Quaternion.identity);
                PhotonNetwork.Instantiate("Coin", pos + new Vector3(0, 0, i), Quaternion.identity);
            }
        }
        else
        {
            Console.WriteLine("no masterclient");
        }
    }


    void OnDrawGizmosSelected()
    {
         Gizmos.color = Color.green;
         Gizmos.DrawCube(transform.localPosition , size);
    }

}
