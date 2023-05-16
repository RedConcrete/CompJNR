using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftUpAndDown : MonoBehaviour
{
    [SerializeField]
    public float speed = 5f;
    [SerializeField]
    public float hight = 0.5f;
    [SerializeField]
    public bool isInverted;
    private float newY;
    Vector3 pos;
    private void Start()
    {
        pos = transform.position;
    }

    void Update()
    {
        MoveLift();
    }

    void MoveLift()
    {
        if (isInverted)
        {
            newY = Mathf.Sin(Time.time * speed) * hight + pos.y;
        }
        else
        {
            newY = Mathf.Cos(Time.time * speed) * hight + pos.y;
        }
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

}
