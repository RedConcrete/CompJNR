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

    public GameObject makerCamSpawner;
    public GameObject makeCamGameObject;
    private GameObject spawnedSpawnedCamera;

    private CharacterController characterController;

    public int countdownTime;
    public int makerTime;
    private GameObject countdownDisplayGameObject;
    private TMP_Text countdownDisplay;

    private void Start()
    {
        spawnedPlayer = PhotonNetwork.Instantiate(playerPrefab.name, transform.position, Quaternion.identity);
        characterController = spawnedPlayer.gameObject.GetComponent<CharacterController>();
        spawnedPlayer.SetActive(false);

        countdownDisplayGameObject = GameObject.Find("CountdownTimer");
        countdownDisplay = countdownDisplayGameObject.GetComponent<TMP_Text>();
        countdownDisplayGameObject.SetActive(false);

        spawnedSpawnedCamera = Instantiate(makeCamGameObject, makerCamSpawner.gameObject.transform.position, Quaternion.identity);
        StartCoroutine(MakerTimeCountdownStart());
    }
    IEnumerator MakerTimeCountdownStart()
    {

        while (makerTime > 0)
        {
            //countdownDisplay.text = countdownTime.ToString();
            Debug.Log("Timeleft: " + makerTime);
            yield return new WaitForSeconds(1f);
            makerTime--;
        }

        spawnedSpawnedCamera.SetActive(false);
        spawnedPlayer.SetActive(true);
        countdownDisplayGameObject.SetActive(true);

        StartCoroutine(CountdownToStart());

        yield return new WaitForSeconds(1f);
    }

    IEnumerator CountdownToStart()
    {
        
        characterController.enabled = false;

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
