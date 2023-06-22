using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MakerManager : MonoBehaviour
{
    public GameObject[] objects;
    private GameObject pendingObject;
    private Vector3 pos;
    private RaycastHit hit;
    [SerializeField] private LayerMask layerMask;
    public float gridSize;
    public bool canPlace = true;
    

    void Update()
    {
        if(pendingObject != null)
        {
            pendingObject.transform.position = new Vector3(
                RoundToNearestGrid(pos.x),
                RoundToNearestGrid(pos.y),
                RoundToNearestGrid(pos.z)
                );

            if (Input.GetMouseButtonDown(0) && canPlace)
            {
                PlaceObject();
            }
        }
    }

    public void PlaceObject()
    {
        pendingObject = null;
    }


    private void FixedUpdate()
    {
       Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit,1000, layerMask))
        {
            pos = hit.point;
        }
    }

    public void SelectObject(int index)
    {
        if (!PhotonNetwork.IsConnected)
        {
            pendingObject = Instantiate(objects[index], pos, transform.rotation);
        }
        else
        {
            pendingObject = PhotonNetwork.Instantiate(objects[index].name, pos, transform.rotation);
        }
        
    }

    float RoundToNearestGrid(float pos)
    {
        float xDiff = pos % gridSize;
        pos -= xDiff;
        if(xDiff > (gridSize / 2))
        {
            pos += gridSize;
        }
        return pos;
    }
}
