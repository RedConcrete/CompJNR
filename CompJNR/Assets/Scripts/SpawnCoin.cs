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

        Quaternion parentRotation = transform.rotation;

        if (PhotonNetwork.IsMasterClient)
        {
            for (int i = 0; i <= coinAmount; i++)
            {
                if (parentRotation == Quaternion.identity)
                {
                    Vector3 pos = transform.localPosition + new Vector3(0, 0, i);
                    PhotonNetwork.Instantiate("Coin", pos + new Vector3(0, 0, i / 2), Quaternion.identity);
                }
                else
                {
                    Vector3 pos = transform.localPosition + new Vector3(i, 0, 0);
                    PhotonNetwork.Instantiate("Coin", pos + new Vector3(i / 2, 0, 0), Quaternion.identity);
                }
                
                
            }
            Debug.Log(parentRotation);
        }
        else
        {
            Console.WriteLine("no masterclient");
        }
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.zero, size);

    }

}
