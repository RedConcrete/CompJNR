using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject firstButton;
    public GameObject mainCamera;
    public GameObject maker;
    public GameObject mario;

    private bool cameraActive;
    private bool buttonActive;
    private bool makerActive;
    private bool marioActive;
    private bool mouseActive;

    public void SwitchChar()
    {
        mainCamera.gameObject.SetActive(!cameraActive);
        firstButton.gameObject.SetActive(!buttonActive);
        maker.gameObject.SetActive(!makerActive);

        mario.gameObject.SetActive(marioActive);

        cameraActive = !cameraActive;
        buttonActive = !buttonActive;
        makerActive = !makerActive;
        marioActive = !marioActive;
        mouseActive = !mouseActive;
        //OnOffMouse();
    }

    private void OnOffMouse()
    {
        Cursor.visible = mouseActive;
        if (mouseActive)
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None;
        }
    }


}

