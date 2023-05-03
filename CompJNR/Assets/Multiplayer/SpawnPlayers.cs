using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;

    private void Start()
    {
        PhotonNetwork.Instantiate(playerPrefab.name ,new Vector3(0, 136 ,0 ), Quaternion.identity);
    }

}
