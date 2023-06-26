using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickHardButton : MonoBehaviour
{
    public MakerManager makerManager;
    private void OnMouseUpAsButton()
    {
        makerManager.SelectObject(0);
        Debug.Log("Brick has been Clicked");

    }
}
