using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SetNameScript : MonoBehaviour
{

    public TMP_InputField usernameInput;
    public TMP_Text buttonText;

    public void OnClickConnect(string loadingScene)
    {
        if (usernameInput.text.Length >= 1)
        {
            PhotonNetwork.NickName = usernameInput.text;
            buttonText.text = "Connecting...";
            PhotonNetwork.AutomaticallySyncScene = true;
            SceneManager.LoadScene(loadingScene);
        }
    }
}
