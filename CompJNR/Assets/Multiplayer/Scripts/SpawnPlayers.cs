using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using TMPro;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;
    private GameObject spawnedPlayer;

    private CharacterController characterController;

    public int countdownTime;
    private GameObject countdownDisplayGameObject;
    private TMP_Text countdownDisplay;

    private void Start()
    {
        spawnedPlayer = PhotonNetwork.Instantiate(playerPrefab.name, transform.position, Quaternion.identity);
        characterController = spawnedPlayer.gameObject.GetComponent<CharacterController>();
        characterController.enabled = false;

        countdownDisplayGameObject = GameObject.Find("CountdownTimer");
        countdownDisplay = countdownDisplayGameObject.GetComponent<TMP_Text>();

        StartCoroutine(CountdownToStart());
    }


    IEnumerator CountdownToStart()
    {
        while (countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);
            countdownTime--;
        }
        countdownDisplay.text = "GO!!";

        characterController.enabled = true;

        yield return new WaitForSeconds(1f);
        countdownDisplay.gameObject.SetActive(false);
    }

}
