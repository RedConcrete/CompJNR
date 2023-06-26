using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountdownManager : MonoBehaviour
{
    public int countdownTime;
    private TMP_Text countdownDisplay;
    private GameObject player;
    private CharacterController characterController;

    private void Start()
    {
        player = GameObject.Find("MarioWithMovemnetAndCameraMultiplayer");
        countdownDisplay = GetComponent<TMP_Text>();
        characterController = player.GetComponent<CharacterController>();
        characterController.enabled = false;
        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    {
        while (countdownTime > 0) { 
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
