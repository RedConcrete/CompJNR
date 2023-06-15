using System.Collections;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class ExitGameOrRoom : MonoBehaviourPunCallbacks
{

    PlayerInput _playerInput;
    public GameObject gameMenu;
    public int sceneID;

    private void Awake()
    {

        _playerInput = new PlayerInput();
        _playerInput.CharacterControls.Enable();

        _playerInput.CharacterControls.Exit.performed += e => OnEscapeShowGameMenu();

    }

    private void Update()
    {

    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        if (SceneManager.GetActiveScene().buildIndex != sceneID) // check if scene is already loaded
        {
            PhotonNetwork.LoadLevel(sceneID);
        }
        else
        {
            Debug.LogError("Scene still activ: " + sceneID);
        }
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    void OnEscapeShowGameMenu()
    {
        gameMenu.SetActive(!gameMenu.activeSelf);
    }

    public void ExitMaker()
    {
        SceneManager.UnloadSceneAsync(3);
        SceneManager.LoadScene(1);

    }

}
