using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoin : MonoBehaviour
{

    public GameObject coinPrefab;

    public int coinAmount;

    public Vector3 size;


    private void Start()
    {
        SpawnCoinOnRandomePos();
    }

    public void SpawnCoinOnRandomePos()
    {
        for (int i = 0; i <= coinAmount; i++)
        {
            Vector3 pos = transform.localPosition + new Vector3(Random.Range(-size.x / 2, size.x / 2), 0, Random.Range(-size.z / 2, size.z / 2));
            Instantiate(coinPrefab, pos, Quaternion.identity);
        }
        
    }


    void OnDrawGizmosSelected()
    {
         Gizmos.color = Color.green;
         Gizmos.DrawCube(transform.localPosition , size);
    }

}
