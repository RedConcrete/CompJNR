using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MakerManager : MonoBehaviour
{
    public GameObject[] objects;
    public List<GameObject> placedObjects;
    private GameObject pendingObject;
    private Vector3 pos;
    private RaycastHit hit;
    [SerializeField] private LayerMask layerMask;
    public float gridSize;
    public bool canPlace = true;
    private int yOffset = 0;

    private void Start()
    {
        placedObjects = new();
    }



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(pendingObject != null)
            {
                placedObjects.Remove(pendingObject);
                PhotonNetwork.Destroy(pendingObject);
                Destroy(pendingObject);
            }
            SelectObject(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (pendingObject != null)
            {
                placedObjects.Remove(pendingObject);
                PhotonNetwork.Destroy(pendingObject);
                Destroy(pendingObject);
            }
            SelectObject(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (pendingObject != null)
            {
                placedObjects.Remove(pendingObject);
                PhotonNetwork.Destroy(pendingObject);
                Destroy(pendingObject);
            }
            SelectObject(2);
        }



        if (pendingObject == null)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit))
            {
                var selection = hit.transform.gameObject;
                if (Input.GetMouseButtonDown(1) && selection.CompareTag("MakerObject"))
                {
                    placedObjects.Remove(selection);
                    PhotonNetwork.Destroy(selection);
                    if(selection.name.Equals("Box(Clone)"))
                        SelectObject(0);
                    if (selection.name.Equals("Box Framed(Clone)"))
                        SelectObject(1);
                    if (selection.name.Equals("Brick Hard(Clone)"))
                        SelectObject(2);
                }
            }
        }


        if(pendingObject != null)
        {
            pendingObject.transform.position = new Vector3(
                RoundToNearestGrid(pos.x),
                RoundToNearestGrid(pos.y + yOffset),
                RoundToNearestGrid(pos.z)
                );

            if (Input.GetMouseButtonDown(0) && canPlace)
            {
                PlaceObject();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                yOffset+= 2;
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                yOffset-= 2;
            }
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                placedObjects.Remove(pendingObject);
                PhotonNetwork.Destroy(pendingObject);
                Destroy(pendingObject);
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            GetPlacedMakerObject();
        }
    }

    public void PlaceObject()
    {
        placedObjects.Add(pendingObject);
        pendingObject = null;

    }

    private void GetPlacedMakerObject()
    {
        foreach (var item in placedObjects)
        {
            if(item.transform.position.x == RoundToNearestGrid(pos.x) && item.transform.position.y == RoundToNearestGrid(pos.y) && item.transform.position.z== RoundToNearestGrid(pos.z))
            {
                Debug.Log(item.transform.position);
            }
        }
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
