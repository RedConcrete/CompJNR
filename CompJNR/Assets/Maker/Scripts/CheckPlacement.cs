using UnityEngine;

public class CheckPlacement : MonoBehaviour
{
    MakerManager makerManager;

    void Start()
    {
        makerManager = GameObject.Find("MakerManager").GetComponent<MakerManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MakerObject"))
        {
            makerManager.canPlace = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("MakerObject"))
        {
            makerManager.canPlace = true;
        }
    }

}
