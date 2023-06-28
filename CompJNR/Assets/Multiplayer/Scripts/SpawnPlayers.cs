using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using TMPro;

public class SpawnPlayers : MonoBehaviour
{

    public bool inPraesentation;

    public GameObject playerPrefab;
    private GameObject spawnedPlayer;

    public GameObject makerCamSpawner;
    public GameObject makeCamGameObject;
    private GameObject spawnedSpawnedCamera;
    public GameObject MakerUi;
    public GameObject GameUi;

    private CharacterController characterController;

    public int countdownTime;
    public int makerTime;
    private GameObject countdownDisplayGameObject;
    private TMP_Text countdownDisplay;
    private GameObject timeLeftBackgroundGameObject;
    private GameObject timeLeftDisplayGameObject;
    private TMP_Text timeLeftDisplay;


    private void Start()
    {
        if (!inPraesentation)
        {
            spawnedPlayer = PhotonNetwork.Instantiate(playerPrefab.name, transform.position, Quaternion.identity);
            characterController = spawnedPlayer.gameObject.GetComponent<CharacterController>();
            spawnedPlayer.SetActive(false);
            GameUi.SetActive(false);
            MakerUi.SetActive(true);

            timeLeftBackgroundGameObject = GameObject.Find("MakerTimeBackground");
            timeLeftDisplayGameObject = GameObject.Find("MakerTime");
            timeLeftDisplay = timeLeftDisplayGameObject.GetComponent<TMP_Text>();
            timeLeftDisplayGameObject.SetActive(true);

            spawnedSpawnedCamera = Instantiate(makeCamGameObject, makerCamSpawner.gameObject.transform.position, Quaternion.identity);
            spawnedSpawnedCamera.SetActive(true);
            StartCoroutine(MakerTimeCountdownStart());
        }
    }
    IEnumerator MakerTimeCountdownStart()
    {

        while (makerTime > 0)
        {
            timeLeftDisplay.text = $"Time : {makerTime}";
            yield return new WaitForSeconds(1f);
            makerTime--;
        }

        timeLeftDisplayGameObject.SetActive(false);
        timeLeftBackgroundGameObject.SetActive(false);

        spawnedSpawnedCamera.SetActive(false);
        spawnedPlayer.SetActive(true);
        MakerUi.SetActive(false);

        GameUi.SetActive(true);
        countdownDisplayGameObject = GameObject.Find("CountdownTimer");
        countdownDisplay = countdownDisplayGameObject.GetComponent<TMP_Text>();

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
