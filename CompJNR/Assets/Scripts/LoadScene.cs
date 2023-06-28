using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class LoadScene : MonoBehaviour
{
    public void LoadSceneWithName(string scene)
    {

        if (PhotonNetwork.IsConnected){

            PhotonNetwork.Disconnect();
            SceneManager.LoadScene(scene);
        }
        else
        {
            SceneManager.LoadScene(scene);
        }
    }
}
