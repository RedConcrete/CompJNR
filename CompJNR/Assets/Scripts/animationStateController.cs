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
        bool isJumping = animator.GetBool("isJumping");
        bool isRunning = animator.GetBool("isRunning");
        bool isWalking = animator.GetBool("isWalking");


        if (!isJumping && isSpacePressed)
        {
            animator.SetBool("isJumping", true);
        }
        if (isJumping && !isSpacePressed)
        {
            animator.SetBool("isJumping", false);
        }
        if (!isRunning && isShiftPressed)
        {
            animator.SetBool("isRunning", true);
        }
        if (isRunning && !isShiftPressed)
        {
            animator.SetBool("isRunning", false);
        }
        if (!isWalking && isForwaredPressed)
        {
            animator.SetBool("isWalking", true);
        }
        if (isWalking && !isForwaredPressed)
        {
            animator.SetBool("isWalking", false);
        }
        
    }
}
