using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickHardButton : MonoBehaviour
{
    public MakerManager makerManager;
    private void OnMouseUpAsButton()
    {
        if (this.gameObject.name == "Box")
        {
            makerManager.SelectObject(0);
            Debug.Log("Box has been clicked!!");
        }
        if (this.gameObject.name == "Box Framed")
        {
            makerManager.SelectObject(1);
            Debug.Log("Box Framed has been clicked!!");
        }
        if (this.gameObject.name == "Brick Hard")
        {
            makerManager.SelectObject(2);
            Debug.Log("Brick Hard has been clicked!!");
        }
    }
}
