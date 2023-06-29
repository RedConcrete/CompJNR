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
    bool mouseActive;

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
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        PhotonNetwork.Disconnect();
        UnityEditor.EditorApplication.isPlaying = false;
#else
            PhotonNetwork.Disconnect();
            Application.Quit();
#endif
    }

    void OnEscapeShowGameMenu()
    {
        gameMenu.SetActive(!gameMenu.activeSelf);
        //Cursor.visible = !mouseActive;
        //UnityEngine.Cursor.lockState = CursorLockMode.None;

        //mouseActive = !mouseActive;

    }

    public void ExitMaker()
    {
        SceneManager.UnloadSceneAsync(3);
        SceneManager.LoadScene(1);

    }

}
