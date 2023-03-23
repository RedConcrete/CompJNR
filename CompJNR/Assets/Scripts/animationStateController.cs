using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        bool isForwaredPressed = Input.GetKey(KeyCode.W);
        bool isShiftPressed = Input.GetKey(KeyCode.LeftShift);
        bool isSpacePressed = Input.GetKey(KeyCode.Space);


        if (isSpacePressed)
        {
            animator.SetBool("isJumping", true);
        }
        if (!isSpacePressed)
        {
            animator.SetBool("isJumping", false);
        }
        if (isShiftPressed)
        {
            animator.SetBool("isRunning", true);
        }
        if (!isShiftPressed)
        {
            animator.SetBool("isRunning", false);
        }
        if (isForwaredPressed)
        {
            animator.SetBool("isWalking", true);
        }
        if (!isForwaredPressed)
        {
            animator.SetBool("isWalking", false);
        }
        
    }
}
