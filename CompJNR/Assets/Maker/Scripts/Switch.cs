using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject firstButton;
    public GameObject mainCamera;
    public GameObject maker;
    public GameObject mario;
    public GameObject gameMenu;

    private bool cameraActive;
    private bool buttonActive;
    private bool makerActive;
    private bool marioActive;

    public void SwitchChar()
    {
        marioActive = !marioActive;
        buttonActive = !buttonActive;
        cameraActive = !cameraActive;
        makerActive = !makerActive;

        mainCamera.gameObject.SetActive(!cameraActive);
        firstButton.gameObject.SetActive(!buttonActive);
        maker.gameObject.SetActive(!makerActive);
        gameMenu.gameObject.SetActive(false);
        
        mario.gameObject.SetActive(marioActive);

        //OnOffMouse();
    }

    private void OnOffMouse()
    {
        
        if (marioActive)
        {
            Cursor.visible = true;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            UnityEngine.Cursor.lockState = CursorLockMode.None;
        }
    }


}

